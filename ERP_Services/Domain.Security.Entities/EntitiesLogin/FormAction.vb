Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class FormAction

    Sub New()
    End Sub

    <DataMember()>
    Property Id As Integer

    <DataMember()>
    Property IdForm As Integer

    <DataMember()>
    Property IdAction As Integer

    <DataMember()>
    Property Action As Action

    <DataMember()>
    Property Crud As ECrud


End Class