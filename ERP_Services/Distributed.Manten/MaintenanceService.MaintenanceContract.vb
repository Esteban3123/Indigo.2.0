Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.Maintenance

Partial Public Class MaintanceService

    ''' <summary>
    ''' Obtiene todos los contratos
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllMaintenanceContract(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceContract) Implements IMaintenanceContractService.ListAllMaintenanceContract
        Using service As IMaintenanceContractAdminService = Container.Current.Resolve(Of IMaintenanceContractAdminService)()
            Return service.ListAllMaintenanceContract()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene contrato por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetMaintenanceContractByCode(Code As String) As Domain.Entities.MaintenanceContract Implements IMaintenanceContractService.GetMaintenanceContractByCode
        Using service As IMaintenanceContractAdminService = Container.Current.Resolve(Of IMaintenanceContractAdminService)()
            Return service.GetMaintenanceContractByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un activo fijo por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IMaintenanceContractService.GetPhysicalAssetById
        Using service As IMaintenanceContractAdminService = Container.Current.Resolve(Of IMaintenanceContractAdminService)()
            Return service.GetPhysicalAssetById(Id)
        End Using
    End Function


    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="MaintenanceContract"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMaintenanceContract(MaintenanceContract As Domain.Entities.MaintenanceContract, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceContract) Implements IMaintenanceContractService.SaveMaintenanceContract
        Using service As IMaintenanceContractAdminService = Container.Current.Resolve(Of IMaintenanceContractAdminService)()
            Return service.SaveMaintenanceContract(MaintenanceContract, audit, idSequense)
        End Using
    End Function


End Class
