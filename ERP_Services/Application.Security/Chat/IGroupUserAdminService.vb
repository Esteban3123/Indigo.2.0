'***********************************************************************
' Assembly         : Application.Security
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Interface que define los metodos del servicio del chat
''' </summary>
Public Interface IGroupUserAdminService
    Inherits IDisposable

#Region "GroupUser"

    ''' <summary>
    ''' Guarda un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a guardar</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveGroupUser(ByVal groupUser As GroupUser, ByVal audit As AuditMessage) As ActionResult(Of GroupUser)

    ''' <summary>
    ''' Elimina un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Function DeleteGroupUser(ByVal groupUser As GroupUser, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    Function GetGroupById(id As Integer) As GroupUser

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Function ListByUsercode(userCode As String) As List(Of GroupUser)

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Function ListByUserId(userId As Integer) As List(Of GroupUser)

#End Region

End Interface
