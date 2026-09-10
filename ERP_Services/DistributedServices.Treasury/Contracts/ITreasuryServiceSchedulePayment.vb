'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceSchedulePayment

    ''' <summary>
    ''' Guarda una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSchedulePayment(schedulePayment As SchedulePayment, ByVal withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of SchedulePayment)

    ''' <summary>
    ''' Elimina una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSchedulePayment(schedulePayment As SchedulePayment, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSchedulePayment(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of SchedulePayment)

    ''' <summary>
    ''' Obtiene una dispersión por egreso
    ''' </summary>
    ''' <param name="VoucherTransaction"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As ActionResult(Of SchedulePayment)
    ''' <summary>
    ''' Obtiene el listado de programacion de pagos con pagos hechos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSchedulePaymentDatasourceWithPayment(code As String, audit As AuditMessage, Optional FlagDispersionFunds As Boolean = False) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSchedulePaymentById(id As Integer) As SchedulePayment

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSPSchedulePayment(supplierTypeListId As String) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Obtiene la programación de pagos por tipo de proveedor o todos los hijos (no padres) de éste
    ''' </summary>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSPSchedulePaymentBySupplierTypeId(supplierTypeId As Integer, code As String) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Confirma una programación de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmSchedulePayment(SchedulePaymentId As Integer, audit As AuditMessage) As ActionResult(Of String)

    <OperationContract()>
    Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As ActionResult

    <OperationContract()>
    Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As ActionMessageResult(Of List(Of SP_SchedulePayment_Result))

End Interface
