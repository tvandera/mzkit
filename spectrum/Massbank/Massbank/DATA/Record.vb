﻿Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text.Xml.Linq

Namespace DATA

    ''' <summary>
    ''' The massbank data records
    ''' </summary>
    Public Class Record : Implements INamedValue

        Public Property ACCESSION As String Implements INamedValue.Key
        Public Property RECORD_TITLE As String
        Public Property [DATE] As String
        Public Property AUTHORS As String
        Public Property LICENSE As String
        Public Property COPYRIGHT As String
        Public Property PUBLICATION As String
        Public Property COMMENT As String()
        Public Property CH As CH
        Public Property AC As AC
        Public Property MS As MS
        Public Property PK As PK
        Public Property SP As SP

        Public Overrides Function ToString() As String
            Return RECORD_TITLE
        End Function

    End Class

    Public Class SP

        Public Property SCIENTIFIC_NAME As String
        Public Property NAME As String
        Public Property LINEAGE As String
        Public Property LINK As String
        Public Property SAMPLE As String

    End Class

    Public Class CH

        Public Property NAME As String()
        Public Property COMPOUND_CLASS As String
        Public Property FORMULA As String
        Public Property EXACT_MASS As String
        Public Property SMILES As String

        ''' <summary>
        ''' ``InChI``
        ''' </summary>
        ''' <returns></returns>
        Public Property IUPAC As String
        Public Property LINK As String()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class

    Public Class AC

        Public Property INSTRUMENT As String
        Public Property INSTRUMENT_TYPE As String
        Public Property MASS_SPECTROMETRY As String()
        Public Property CHROMATOGRAPHY As String()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class

    Public Class MS

        Public Property FOCUSED_ION As String()
        Public Property DATA_PROCESSING As String()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class

    Public Class PK

        Public Property SPLASH As String
        Public Property ANNOTATION As Entity()
        Public Property NUM_PEAK As String
        Public Property PEAK As PeakData()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class

    Public Structure PeakData

        ''' <summary>
        ''' 碎片的质核比数据
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute, Column("m/z")> Public Property mz As Double
        <XmlAttribute, Column("int.")> Public Property int As Double

        ''' <summary>
        ''' 相对信号强度
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute, Column("rel.int.")>
        Public Property relint As Double

    End Structure
End Namespace