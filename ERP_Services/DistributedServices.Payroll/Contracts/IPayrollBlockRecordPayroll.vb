'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPayrollBlockRecordPayroll

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordPayrollByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, session As SessionValues) As BlockRecordPayroll

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordPayroll(ByVal blockRecordPayroll As BlockRecordPayroll, session As SessionValues) As ActionResult(Of BlockRecordPayroll)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordPayroll(ByVal blockRecordPayroll As BlockRecordPayroll, session As SessionValues) As ActionResult

End Interface
