﻿#Region "Microsoft.VisualBasic::747288ded454efb98ca7da843d1812ae, src\mzmath\ms2_math-core\Spectra\LibraryMatrixExtensions.vb"

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

'     Module LibraryMatrixExtensions
' 
'         Function: AsMatrix, Max, Shrink
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports BioNovoGene.Analytical.MassSpectrometry.Math.Chromatogram
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math

Namespace Spectra

    ''' <summary>
    ''' Library matrix math
    ''' </summary>
    ''' 
    <HideModuleName>
    Public Module LibraryMatrixExtensions

        ''' <summary>
        ''' MAX(<see cref="ms2.quantity"/>)
        ''' </summary>
        ''' <param name="matrix"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function Max(matrix As LibraryMatrix) As Double
            Return matrix.ms2.Max(Function(r) r.quantity)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function AsMatrix(lib_ms2 As IEnumerable(Of Library)) As LibraryMatrix
            Return lib_ms2 _
                .Select(Function(l)
                            Return New ms2 With {
                                .mz = l.ProductMz,
                                .quantity = l.LibraryIntensity,
                                .intensity = l.LibraryIntensity
                            }
                        End Function) _
                .ToArray
        End Function

        '''' <summary>
        '''' 将符合误差范围的二级碎片合并在一起
        '''' </summary>
        '''' <param name="matrix"></param>
        '''' <returns></returns>
        '<MethodImpl(MethodImplOptions.AggressiveInlining)>
        '<Extension>
        'Public Function Shrink(matrix As LibraryMatrix, tolerance As Tolerance) As LibraryMatrix
        '    Dim ms2Peaks As ms2() = matrix _
        '        .GroupBy(Function(ms2) ms2.mz, AddressOf tolerance.Assert) _
        '        .Select(Function(g)
        '                    ' 合并在一起的二级碎片的相应强度取最高的为结果
        '                    Dim fragments As ms2() = g.ToArray
        '                    Dim maxi As Integer = Which.Max(fragments.Select(Function(m) m.intensity))
        '                    Dim max As ms2 = fragments(maxi)

        '                    Return max
        '                End Function) _
        '        .ToArray

        '    Return New LibraryMatrix With {
        '        .ms2 = ms2Peaks,
        '        .name = matrix.name,
        '        .centroid = matrix.centroid
        '    }
        'End Function

        ''' <summary>
        ''' Convert profile matrix to centroid matrix
        ''' </summary>
        ''' <param name="[lib]"></param>
        ''' <returns></returns>
        ''' 
        <Extension>
        Public Function CentroidMode([lib] As LibraryMatrix, Optional intoCutoff# = 0.05) As LibraryMatrix
            [lib].ms2 = [lib].ms2.Centroid(intoCutoff).ToArray
            [lib].centroid = True

            Return [lib]
        End Function

        ''' <summary>
        ''' Convert profile matrix to centroid matrix
        ''' </summary>
        ''' <param name="peaks"></param>
        ''' <param name="intoCutoff#"></param>
        ''' <returns></returns>
        <Extension>
        Public Iterator Function Centroid(peaks As ms2(), Optional intoCutoff# = 0.05) As IEnumerable(Of ms2)
            Dim maxInto = peaks.Select(Function(p) p.intensity).Max

            ' removes low intensity fragment peaks
            ' for save calculation time
            peaks = peaks _
                .Where(Function(p) p.intensity / maxInto >= intoCutoff) _
                .ToArray

            ' and then we could run peak detections for 
            ' Convert ms peaks DATA to centroid mode
            For Each peak As ROI In peaks _
                .Select(Function(p)
                            Return New ChromatogramTick(p.mz, p.intensity)
                        End Function) _
                .OrderBy(Function(m) m.Time) _
                .Shadows _
                .PopulateROI(baselineQuantile:=0)

                Yield New ms2 With {
                    .mz = peak.rt,
                    .intensity = peak.maxInto,
                    .quantity = .intensity,
                    .Annotation = peak.ToString
                }
            Next
        End Function
    End Module
End Namespace
