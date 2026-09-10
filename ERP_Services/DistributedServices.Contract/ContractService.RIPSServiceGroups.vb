'************************************************************
' Assembly         : Application.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities
#End Region

Partial Class ContractService
    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    Public Function ListAllRIPSServiceGroups() As List(Of RIPSServiceGroups) Implements IContractServiceRIPSServiceGroups.ListAllRIPSServiceGroups
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.ListAllRIPSServiceGroups()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Public Function GetRIPSServiceGroupById(id As Integer) As ActionResult(Of RIPSServiceGroups) Implements IContractServiceRIPSServiceGroups.GetRIPSServiceGroupById
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.GetRIPSServiceGroupById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Public Function GetRIPSServiceGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups) Implements IContractServiceRIPSServiceGroups.GetRIPSServiceGroupByCode
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.GetRIPSServiceGroupByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un grupo de servicios RIPS.
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Function SaveRIPSServiceGroup(ServiceGroup As RIPSServiceGroups, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServiceGroups) Implements IContractServiceRIPSServiceGroups.SaveRIPSServiceGroup
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.SaveRIPSServiceGroup(ServiceGroup, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado del grupo de servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Function ChangeStateRIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups) Implements IContractServiceRIPSServiceGroups.ChangeStateRIPSServiceGroup
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.ChangeStateRIPSServiceGroup(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el registro de un grupo de servicios RIPS
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteRIPSServiceGroup(ServiceGroup As RIPSServiceGroups, audit As AuditMessage) As ActionResult Implements IContractServiceRIPSServiceGroups.DeleteRIPSServiceGroup
        Using service As IRIPSServiceGroupsAdminService = Container.Current.Resolve(Of IRIPSServiceGroupsAdminService)()
            Return service.DeleteRIPSServiceGroup(ServiceGroup, audit)
        End Using
    End Function
End Class
