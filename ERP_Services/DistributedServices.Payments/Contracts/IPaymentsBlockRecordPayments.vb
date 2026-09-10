'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsBlockRecordPayments

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordPaymentsByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordPayments

    ''' <summary>
    ''' Gets the block record payments by idform and consecutive.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="Consecutive">Consecutive.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordPaymentsByIdformAndConsecutive(ByVal IdForm As String, ByVal Consecutive As String) As BlockRecordPayments

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordPayments(ByVal blockRecordPayments As BlockRecordPayments) As ActionResult(Of BlockRecordPayments)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordPayments(ByVal blockRecordPayments As BlockRecordPayments) As ActionResult

End Interface
