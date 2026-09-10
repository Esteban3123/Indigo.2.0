Imports System.Runtime.Serialization

<DataContract()>
Public Class ExcelRow

    <DataMember()>
    Public Property Cells As List(Of Object) = New List(Of Object)

End Class
