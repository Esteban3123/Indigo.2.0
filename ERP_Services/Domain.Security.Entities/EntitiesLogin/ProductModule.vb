Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract(IsReference:=True)>
Public Class ProductModule

    Sub New()
    End Sub

    <DataMember()>
    Property Id As Integer

    <DataMember()>
    Property IdModule As Integer

    <DataMember()>
    Property IdProduct As Integer

    <DataMember()>
    Property Crud As ECrud

End Class
