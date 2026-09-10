Imports System.Runtime.Serialization

<DataContract(IsReference:=True), Serializable()>
Partial Public Class FolioDataDetail

    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property Status As Byte
    <DataMember()>
    Public Property FolioName As String

End Class
