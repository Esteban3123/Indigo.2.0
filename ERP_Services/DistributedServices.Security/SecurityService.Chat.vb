'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.IOC
#End Region

Partial Public Class SecurityService

    ''' <summary>
    ''' Elimina un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteGroupUser(groupUser As GroupUser, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements ISecurityService.DeleteGroupUser
        Using obj As IGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGroupUserAdminService)()
            Return obj.DeleteGroupUser(groupUser, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un usuario relacionado a un grupo
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteUsersGroupUser(userGroupUser As UsersGroupUser, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements ISecurityService.DeleteUsersGroupUser
        Using obj As IUsersGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of UsersGroupUserAdminService)()
            Return obj.DeleteUsersGroupUser(userGroupUser, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    Public Function GetGroupById(id As Integer) As GroupUser Implements ISecurityService.GetGroupById
        Using obj As IGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGroupUserAdminService)()
            Return obj.GetGroupById(id)
        End Using
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    Public Function ListByGroupId(id As Integer) As List(Of UsersGroupUser) Implements ISecurityService.ListByGroupId
        Using obj As IUsersGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of UsersGroupUserAdminService)()
            Return obj.ListByGroupId(id)
        End Using
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUsercode(userCode As String) As List(Of GroupUser) Implements ISecurityService.ListByUsercode
        Using obj As IGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGroupUserAdminService)()
            Return obj.ListByUsercode(userCode)
        End Using
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUserId(userId As Integer) As List(Of GroupUser) Implements ISecurityService.ListByUserId
        Using obj As IGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGroupUserAdminService)()
            Return obj.ListByUserId(userId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a guardar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveGroupUser(groupUser As GroupUser, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of GroupUser) Implements ISecurityService.SaveGroupUser
        Using obj As IGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGroupUserAdminService)()
            Return obj.SaveGroupUser(groupUser, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un usuario relacionado a un grupo en el chat
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveUsersGroupUser(userGroupUser As UsersGroupUser, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of UsersGroupUser) Implements ISecurityService.SaveUsersGroupUser
        Using obj As IUsersGroupUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of UsersGroupUserAdminService)()
            Return obj.SaveUsersGroupUser(userGroupUser, audit)
        End Using
    End Function

End Class
