#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IMaintenanceContractService

    ''' <summary>
    ''' Obtiene todos los contratos
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllMaintenanceContract(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceContract)

    ''' <summary>
    ''' Obtiene contrato por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMaintenanceContractByCode(Code As String) As MaintenanceContract

    ''' <summary>
    ''' Obtiene un activo fijo por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset


    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="MaintenanceContract"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMaintenanceContract(MaintenanceContract As Domain.Entities.MaintenanceContract, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceContract)




End Interface
