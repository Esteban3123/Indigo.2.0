Imports System.Runtime.Serialization

Partial Public Class AccountManagementParameters

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre del tipo de ingreso
    ''' </summary>
    <DataMember()>
    Public Property EntryTypeName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la clase de cama
    ''' </summary>
    <DataMember()>
    Public Property BedClassName As String

    ''' <summary>
    ''' Relación con UsersAssignemt
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property usersAssignment As List(Of UsersAssignment)

    ''' <summary>
    ''' Relación con UserNovelties
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property userNovelties As List(Of UserNovelties)

#End Region


End Class
