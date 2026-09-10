#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IMaintenanceContractAdminService
    Inherits IDisposable
    ''' <summary>
    ''' lista todos los contratos
    ''' </summary>
    ''' <returns></returns>
    Function ListAllMaintenanceContract() As List(Of MaintenanceContract)

    ''' <summary>
    ''' Obtiene un contrato por Codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Function GetMaintenanceContractByCode(Code As String) As MaintenanceContract

    ''' <summary>
    ''' Obtiene un activo fijo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset


    ''' <summary>
    '''Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="MaintenanceContract">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveMaintenanceContract(MaintenanceContract As MaintenanceContract, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceContract)




End Interface
