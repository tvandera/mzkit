﻿Imports System.Runtime.CompilerServices
Imports System.Text
Imports MetabolomeXchange
Imports MetabolomeXchange.Json
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Parser
Imports Microsoft.VisualBasic.Serialization.JSON

Module Program

    Sub New()
        'Dim data As New DataSet With {
        '    .accession = "123",
        '    .date = Now.ToString,
        '    .description = {"AAAA"},
        '    .publications = {New publication With {.pubmed = "444444", .doi = "doi_string", .title = "ffffffffffff"}},
        '    .title = "AAAAAAsd",
        '    .submitter = {"QQQQQ", "DDDDDD"},
        '    .timestamp = 1234567,
        '    .url = "ASDFFFFFFF",
        '    .meta = New meta With {
        '        .analysis = "AAAAA",
        '        .metabolites = {"X", "FGFFFF"},
        '        .organism = {"PPPP", "OOOO"},
        '        .organism_parts = {"DDCCCC", "KKKKKK"},
        '        .platform = "XXXXXXXXXXXXXXXXXXX"
        '    }
        '}

        'Call data.GetJson.SaveTo("./test.json")
    End Sub

    Public Function Main() As Integer
        Return GetType(Program).RunCLI(App.CommandLine)
    End Function

    <ExportAPI("/dataset.json")>
    <Usage("/dataset.json [/provider <shortname, default=""mtbls""> /out <save.json>]")>
    Public Function GetJson(args As CommandLine) As Integer
        Dim provider$ = (args <= "/provider") Or "mtbls".AsDefault
        Dim out$ = (args <= "/out") Or $"{App.HOME}/metabolomexchange_dataset.json".AsDefault

        Return MetabolomeXchange _
            .GetAllDataSetJson(provider) _
            .SaveTo(out, Encoding.UTF8) _
            .CLICode
    End Function

    <ExportAPI("/dump.table")>
    <Usage("/dump.table /in <json.txt> [/out <out.csv>]")>
    Public Function DumpTable(args As CommandLine) As Integer
        Dim in$ = args <= "/in"
        Dim out$ = (args <= "/out") Or ([in].TrimSuffix & ".csv").AsDefault

        Dim model = [in].ReadAllText.ParseJsonStr
        Return model _
            .ToTable() _
            .ToArray _
            .SaveTo(out, encoding:=Encoding.UTF8) _
            .CLICode

        Dim json = [in].ReadAllText.LoadObject(Of Dictionary(Of String, DataSet))
        Return json _
            .Values _
            .ToTable _
            .SaveTo(out, encoding:=Encoding.UTF8) _
            .CLICode
    End Function

    <Extension>
    Public Iterator Function ToTable(json As JsonElement) As IEnumerable(Of DataTable)
        Dim datasets As JsonObject = DirectCast(json, JsonObject)!datasets

        For Each node As JsonObject In datasets.Values
            Dim meta As JsonObject = node!meta

            Yield New DataTable With {
                .analysis = DirectCast(node!analysis, JsonValue).GetStripString
            }
        Next
    End Function
End Module
