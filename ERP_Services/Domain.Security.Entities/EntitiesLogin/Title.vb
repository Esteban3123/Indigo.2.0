Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract()>
Public Class Title

    <DataMember()>
    Property IdTitle As Integer

    <DataMember()>
    Property TitleName As String

    <DataMember()>
    Property State As Byte

    <DataMember()>
    Property TitleOrder As Integer

    <DataMember()>
    Property Crud As ECrud

End Class
