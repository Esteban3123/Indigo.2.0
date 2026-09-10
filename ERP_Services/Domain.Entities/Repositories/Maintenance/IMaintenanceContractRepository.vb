
Imports Domain.Base
Imports Domain.Entities

Public Interface IMaintenanceContractRepository
    Inherits IRepository(Of MaintenanceContract)

    ''' <summary>
    ''' Obtiene todos los contratos
    ''' </summary>
    ''' <returns></returns>
    Function ListAllMaintenanceContract() As List(Of MaintenanceContract)

    ''' <summary>
    ''' Obtiene un MaintenanceContract por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMaintenanceContractByCode(code As String) As MaintenanceContract


    ''' <summary>
    ''' Obtiene  las placas por Id de la tabla FixedAssetPhysicalAsset
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset





End Interface
