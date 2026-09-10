Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IBatchSerialSettingService

    ''' <summary>
    ''' Guarda registro
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBatchSerialSetting(BatchSerialSetting As BatchSerialSetting, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el registro de configuracion de lotes
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBatchSerialSettings() As ActionResult(Of BatchSerialSetting)
End Interface
