Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MaintanceService

    ''' <summary>
    ''' Función que obtiene una falla por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceFailureRequestByCode(Code As String) As Domain.Entities.MaintenanceFailureRequest Implements IMaintenanceFailureRequestService.GetMaintenanceFailureRequestByCode
        Using service As IMaintenanceFailureRequestAdminService = Container.Current.Resolve(Of IMaintenanceFailureRequestAdminService)()
            Return service.GetMaintenanceFailureRequestByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las falla de los Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaintenanceFailureRequest(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceFailureRequest) Implements IMaintenanceFailureRequestService.ListAllMaintenanceFailureRequest
        Using service As IMaintenanceFailureRequestAdminService = Container.Current.Resolve(Of IMaintenanceFailureRequestAdminService)()
            Return service.ListAllMaintenanceFailureRequest()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar una falla
    ''' </summary>
    ''' <param name="MaintenanceFailure">Objeto MaintenanceFailureRequest</param>    
    ''' <remarks></remarks>
    Public Function SaveMaintenanceFailureRequest(MaintenanceFailure As Domain.Entities.MaintenanceFailureRequest, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestService.SaveMaintenanceFailureRequest
        Using service As IMaintenanceFailureRequestAdminService = Container.Current.Resolve(Of IMaintenanceFailureRequestAdminService)()
            Dim result = service.SaveMaintenanceFailureRequest(MaintenanceFailure, audit, idSequense)
            If result.StateResult Then
                service.SendMaintenanceFailureRequestNotification(New List(Of MaintenanceFailureRequest) From {result.ObjectEmbbeded})
            End If
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmMaintenanceFailureRequest(MaintenanceFailureRequest As Domain.Entities.MaintenanceFailureRequest, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestService.ConfirmMaintenanceFailureRequest
        Using service As IMaintenanceFailureRequestAdminService = Container.Current.Resolve(Of IMaintenanceFailureRequestAdminService)()
            Dim result = service.ConfirmMaintenanceFailureRequest(MaintenanceFailureRequest, audit, idSequense)
            If result.StateResult Then
                service.SendMaintenanceFailureRequestNotification(New List(Of MaintenanceFailureRequest) From {result.ObjectEmbbeded})
            End If
            Return result
        End Using
    End Function

End Class
