Imports System.Runtime.Serialization

Public Class DrugInteraction

    ''' <summary>
    ''' Propiedad para establecer si la interacción hereda de un padre
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property isInherited As Boolean

    ''' <summary>
    ''' Propiedad para establecer si la interacción hereda de un padre
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property isInheritedName As String

    <DataMember()>
    Public Property DCICode As String
    <DataMember()>
    Public Property DCIName As String
    <DataMember>
    Public Property DCIParentCodeName As String
    <DataMember>
    Public Property ParentDCIOriginId As Integer
    <DataMember>
    Public Property ATCCode As String

    <DataMember>
    Public Property ATCName As String

End Class
