'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base


<ServiceContract()> _
Public Interface IBlockRecordMaintenanceService

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordMaintenanceByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, ByVal session As SessionValues) As BlockRecordMaintenance

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordMaintenance(ByVal blockRecord As BlockRecordMaintenance, ByVal session As SessionValues) As ActionResult(Of BlockRecordMaintenance)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordMaintenance(ByVal blockRecord As BlockRecordMaintenance, ByVal session As SessionValues) As ActionResult

End Interface
