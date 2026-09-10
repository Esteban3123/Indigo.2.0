Imports System.Runtime.Serialization

<DataContract()>
Public Class Action
    <DataMember()>
    Property IdAction As Integer
    <DataMember()>
    Property ActionName As String
    <DataMember()>
    Property State As Boolean
End Class
