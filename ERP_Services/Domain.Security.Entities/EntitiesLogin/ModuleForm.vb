Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract(IsReference:=True)>
Public Class ModuleForm

    Sub New()
    End Sub

    <DataMember()>
    Property Id As Integer

    <DataMember()>
    Property IdModule As Integer

    <DataMember()>
    Property IdTitle As Integer

    <DataMember()>
    Property IdForm As Integer

    <DataMember()>
    Property FormOrder As Integer

    <DataMember()>
    Property Form As VieDBForm

    <DataMember()>
    Property Crud As ECrud

End Class
