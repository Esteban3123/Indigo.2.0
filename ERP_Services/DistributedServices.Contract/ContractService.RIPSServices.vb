'************************************************************
' Assembly         : Application.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

Partial Class ContractService
    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    Public Function ListAllRIPSService() As List(Of RIPSServices) Implements IContractServiceRIPSServices.ListAllRIPSServices
        Using service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return service.ListAllRIPSServices
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Public Function GetRIPSServiceById(id As Integer) As ActionResult(Of RIPSServices) Implements IContractServiceRIPSServices.GetRIPSServiceById
        Using service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return service.GetRIPSServiceById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Public Function GetRIPSServiceByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServices) Implements IContractServiceRIPSServices.GetRIPSServiceByCode
        Using service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return service.GetRIPSServiceByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un grupo de servicios RIPS.
    ''' </summary>
    ''' <param name="Service">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Function SaveRIPSService(Service As RIPSServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServices) Implements IContractServiceRIPSServices.SaveRIPSService
        Using _service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return _service.SaveRIPSService(Service, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el registro de un grupo de servicios RIPS
    ''' </summary>
    ''' <param name="Service">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteRIPSService(Service As RIPSServices, audit As AuditMessage) As ActionResult Implements IContractServiceRIPSServices.DeleteRIPSService
        Using _service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return _service.DeleteRIPSService(Service, audit)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado del servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del servicio RIPS</param>
    ''' <param name="state">Estado del servicio (Boolean)</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Servicio RIPS modificado</returns>
    Public Function ChangeStateRIPSService(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServices) Implements IContractServiceRIPSServices.ChangeStateRIPSService
        Using service As IRIPSServicesAdminService = Container.Current.Resolve(Of IRIPSServicesAdminService)()
            Return service.ChangeStateRIPSService(code, state, audit)
        End Using
    End Function
End Class
