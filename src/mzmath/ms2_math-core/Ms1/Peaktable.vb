﻿#Region "Microsoft.VisualBasic::3d23265170e4819ac6dcde4997e6c4aa, src\mzmath\ms2_math-core\Ms1\Peaktable.vb"

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

    ' Class Peaktable
    ' 
    '     Properties: energy, index, intb, into, ionization
    '                 maxo, mz, mzmax, mzmin, name
    '                 rt, rtmax, rtmin, rtMinute, sample
    '                 scan, sn
    ' 
    '     Function: ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Data.Linq.Mapping
Imports BioNovoGene.Analytical.MassSpectrometry.Math.Chromatogram

''' <summary>
''' 一级信息表
''' </summary>
Public Class Peaktable
    Implements IMs1
    Implements IRetentionTime
    Implements IROI

    ''' <summary>
    ''' 可以是差异代谢物的编号
    ''' </summary>
    ''' <returns></returns>
    Public Property name As String
    Public Property mz As Double Implements IMs1.mz
    Public Property mzmin As Double
    Public Property mzmax As Double
    Public Property rt As Double Implements IMs1.rt

    ''' <summary>
    ''' 以分钟为单位的保留时间
    ''' </summary>
    ''' <returns></returns>
    <Column(Name:="rt(minute)")>
    Public ReadOnly Property rtMinute As Double
        Get
            Return rt / 60
        End Get
    End Property

    Public Property rtmin As Double Implements IROI.rtmin
    Public Property rtmax As Double Implements IROI.rtmax
    Public Property into As Double
    Public Property intb As Double
    Public Property maxo As Double
    Public Property sn As Double
    Public Property sample As String
    Public Property index As Double
    ''' <summary>
    ''' The scan number
    ''' </summary>
    ''' <returns></returns>
    Public Property scan As Integer
    ''' <summary>
    ''' CID/HCD
    ''' </summary>
    ''' <returns></returns>
    Public Property ionization As String
    Public Property energy As String

    Public Overrides Function ToString() As String
        Return $"{mz}@{rt}#{scan}-{ionization}-{energy}"
    End Function
End Class
