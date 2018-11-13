﻿#Region "Microsoft.VisualBasic::675c02c09c59b7dd8373419780eb2955, ms2_math-core\Chromatogram\AccumulateROI.vb"

    ' Author:
    ' 
    '       xieguigang (gg.xie@bionovogene.com, BioNovoGene Co., LTD.)
    ' 
    ' Copyright (c) 2018 gg.xie@bionovogene.com, BioNovoGene Co., LTD.
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:

    '     Module AccumulateROI
    ' 
    '         Function: PopulateROI
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Math.Scripting
Imports SMRUCC.MassSpectrum.Math.Chromatogram

Namespace Chromatogram

    ''' <summary>
    ''' 根据累加线来查找色谱峰的ROI
    ''' </summary>
    Public Module AccumulateROI

        ' 算法原理，每当出现一个峰的时候，累加线就会明显升高一个高度
        ' 当升高的时候，曲线的斜率大于零
        ' 当处于基线水平的时候，曲线的斜率接近于零
        ' 则可以利用这个特性将色谱峰给识别出来
        ' 这个方法仅局限于色谱峰都是各自相互独立的情况之下

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="angleThreshold#">区分色谱峰的累加线切线角度的阈值，单位为度</param>
        ''' <returns></returns>
        ''' 
        <Extension>
        Public Iterator Function PopulateROI(chromatogram As IVector(Of ChromatogramTick),
                                             Optional angleThreshold# = 5,
                                             Optional baselineQuantile# = 0.65) As IEnumerable(Of ROI)
            ' 先计算出基线和累加线
            Dim baseline# = chromatogram.Baseline(baselineQuantile)
            Dim time As Vector = chromatogram!time
            Dim intensity As Vector = chromatogram!intensity
            ' Dim maxInto# = intensity.Max - baseline
            Dim accumulate#
            Dim sumALL# = (chromatogram!intensity - baseline) _
                .Where(Function(x) x > 0) _
                .Sum
            ' 所有的基线下面的噪声加起来的积分面积总和
            Dim sumAllNoise# = baseline * chromatogram.Length
            Dim ay = Function(into As Double) As Double
                         into -= baseline
                         accumulate += If(into < 0, 0, into)
                         Return (accumulate / sumALL) * 100 ' * maxInto
                     End Function
            Dim accumulateLine = chromatogram _
                .Select(Function(tick)
                            Return New PointF(tick.Time, ay(tick.Intensity))
                        End Function) _
                .ToArray

            ' 使用滑窗计算出切线的斜率
            Dim windows = accumulateLine _
                .SlideWindows(winSize:=2) _
                .ToArray
            Dim peaks = windows _
                .Split(Function(tangent)
                           Return (tangent.First, tangent.Last).Angle <= angleThreshold
                       End Function) _
                .Where(Function(p) p.Length > 1)

            For Each window As SlideWindow(Of PointF)() In peaks
                Dim rtmin# = Fix(window.First()(0).X)
                Dim rtmax# = window.Last()(-1).X + 1
                Dim peak = chromatogram((time >= rtmin) & (time <= rtmax))
                ' 因为Y是累加曲线的值，所以可以近似的看作为峰面积积分值
                ' 在这里将区间的上限的积分值减去区间的下限的积分值即可得到当前的这个区间的积分值（近似于定积分）
                Dim integration = window.Last.Last.Y - window.First.First.Y
                Dim max#
                Dim rt#

                If peak.Length = 1 Then
                    With peak.First
                        max = .Intensity
                        rt = .Time
                    End With
                Else
                    With peak!intensity
                        max = .Max
                        rt = peak(index:=Which.Max(.ByRef)).Time
                    End With
                End If

                Yield New ROI With {
                    .Ticks = peak.ToArray,
                    .MaxInto = max,
                    .Baseline = baseline,
                    .Time = {rtmin, rtmax},
                    .Integration = integration,
                    .rt = rt,
                    .Noise = (peak.Length * baseline) / sumAllNoise
                }
            Next
        End Function
    End Module
End Namespace
