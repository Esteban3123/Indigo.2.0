Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract(IsReference:=True)>
Public Class ProductCatalog

    Sub New()
        Me.ListModules = New List(Of Modules)
    End Sub

    <DataMember()>
    Property IdProduct As Integer
    <DataMember()>
    Property PlatformName As String
    <DataMember()>
    Property SuiteName As String
    <DataMember()>
    Property ProductName As String
    <DataMember()>
    Property State As Byte
    <DataMember()>
    Property Visible As Byte
    <DataMember()>
    Property ListModules As List(Of Modules)
    <DataMember()>
    Property Crud As ECrud

End Class
