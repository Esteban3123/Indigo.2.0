Imports System.Runtime.Serialization

<DataContract()>
Public Class ExcelDataColumn
    <DataMember()>
    Property Name As String

    <DataMember()>
    Property Comment As String

    <DataMember()>
    Property Type As ExcelColumnFormat = ExcelColumnFormat.IsDefault

End Class