Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<KnownType(GetType(Title))> _
<KnownType(GetType(Action))> _
<DataContract()>
Public Class VieDBForm

    Public Sub New()
        Me.ListAction = New List(Of Action)
        Me.ListFormAction = New List(Of FormAction)
    End Sub

    <DataMember()>
    Public Property IdForm As Integer
    <DataMember()>
    Public Property FormName As String
    <DataMember()>
    Public Property FormOrder As Integer
    <DataMember()>
    Public Property PrintEvents As String
    <DataMember()>
    Public Property HasSequence As Byte
    <DataMember()>
    Public Property IsNativeForm As Byte
    <DataMember()>
    Public Property HasForm As Byte
    <DataMember()>
    Public Property ClassName As String
    <DataMember()>
    Public Property AssemblyName As String
    <DataMember()>
    Public Property HandlesMassiveConfirm As Byte
    <DataMember()>
    Public Property SequenceModule As String
    <DataMember()>
    Public Property State As Byte
    <DataMember()>
    Public Property Title As Title
    <DataMember()>
    Public Property ListAction As List(Of Action)
    <DataMember()>
    Public Property ListFormAction As List(Of FormAction)
    <DataMember()>
    Public Property Crud As ECrud
End Class
