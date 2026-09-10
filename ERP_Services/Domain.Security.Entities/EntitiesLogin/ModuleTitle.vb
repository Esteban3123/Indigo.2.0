Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract(IsReference:=True)>
Public Class ModuleTitle

    Sub New()
    End Sub

    <DataMember()>
    Property Id As Integer

    <DataMember()>
    Property IdModule As Integer

    <DataMember()>
    Property IdTitle As Integer

    <DataMember()>
    Property Order As Integer

    <DataMember()>
    Property Title As Title

    <DataMember()>
    Property Crud As ECrud

End Class
