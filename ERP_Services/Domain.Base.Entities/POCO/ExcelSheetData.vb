Imports System.Runtime.Serialization

<DataContract()>
Public Class ExcelSheetData

    <DataMember()>
    Property Name As String

    <DataMember()>
    Property Columns As List(Of ExcelDataColumn)

    <DataMember()>
    Property Rows As List(Of ExcelRow)

End Class
