'***********************************************************************
' Assembly         : Application.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Generic
Imports System.Data
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lists the groups all.
    ''' </summary>
    ''' <returns></returns>
    Function ListGroupsAll() As List(Of GroupAll)

    ''' <summary>
    ''' lista todos los grupos
    ''' </summary>
    ''' <returns></returns>
    Function ListGroups() As IEnumerable(Of Group)

    ''' <summary>
    ''' Elimina un Grupo
    ''' </summary>
    ''' <param name="group">el Grupo</param>
    ''' <param name="session">sesion</param>
    ''' <returns></returns>
    Function DeleteGroup(ByVal group As Group, ByVal session As SessionValues) As Boolean

    ''' <summary>
    ''' graba un grupo
    ''' </summary>
    ''' <param name="group">el grupo</param>
    ''' <returns></returns>
    Function SaveGroup(ByVal group As Group, dtDetails As DataTable, eliminados As List(Of Integer), ByVal session As SessionValues) As Boolean


    ''' <summary>
    ''' consulta un grupo especifico
    ''' </summary>
    ''' <param name="codeGroup">el codigo del grupo</param>
    ''' <returns></returns>
    Function GetGroup(ByVal codeGroup As String) As Group

End Interface
