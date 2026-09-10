'***********************************************************************
' Assembly         : DistributedServices.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-12-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity
Imports Application.AccountManagement

#End Region

Partial Class AccountManagementService
    Implements IAccountManagementServiceManagementAreas

    ''' <summary>
    ''' Lista todas las gestiones de cuenta por el código de usuario
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListManagementAreasByUserCode(userCode As String) As List(Of ManagementAreas) Implements IAccountManagementServiceManagementAreas.ListManagementAreasByUserCode
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.ListManagementAreasByUserCode(userCode)
        End Using
    End Function

    ''' <summary>
    ''' elimina la gestion de cuenta
    ''' </summary>
    ''' <param name="ManagementAreas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage) As ActionResult Implements IAccountManagementServiceManagementAreas.DeleteManagementAreas
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.DeleteManagementAreas(ManagementAreas, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza una gestion de cuenta
    ''' </summary>
    ''' <param name="ManagementAreas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveManagementAreas(ManagementAreas As ManagementAreas, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IAccountManagementServiceManagementAreas.SaveManagementAreas
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.SaveManagementAreas(ManagementAreas, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una gestion de cuenta por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManagementAreasById(id As Integer, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IAccountManagementServiceManagementAreas.GetManagementAreasById
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.GetManagementAreasById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una gestion de cuenta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManagementAreasByCode(code As String, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IAccountManagementServiceManagementAreas.GetManagementAreasByCode
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.GetManagementAreasByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateManagementAreas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IAccountManagementServiceManagementAreas.ChangeStateManagementAreas
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.ChangeStateManagementAreas(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todas las areas de gestión activas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllManagementAreas() As ActionResult(Of List(Of ManagementAreas)) Implements IAccountManagementServiceManagementAreas.GetAllManagementAreas
        Using service As IManagementAreasAdminService = Container.Current.Resolve(Of IManagementAreasAdminService)()
            Return service.GetAllManagementAreas()
        End Using
    End Function

End Class
