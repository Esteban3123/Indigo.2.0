'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISchedulePaymentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SaveSchedulePayment(ByVal schedulePayment As SchedulePayment, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of SchedulePayment)

    ''' <summary>
    ''' Elimina una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSchedulePayment(ByVal schedulePayment As SchedulePayment, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetSchedulePayment(ByVal code As String, tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SchedulePayment)

    ''' <summary>
    ''' Obtiene una dispersión por egreso
    ''' </summary>
    ''' <param name="VoucherTransaction"></param>
    ''' <returns></returns>
    Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As ActionResult(Of SchedulePayment)

    ''' <summary>
    ''' Obtiene el listado de programacion de pagos con pagos hechos
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetSchedulePaymentDatasourceWithPayment(ByVal code As String, ByVal audit As AuditMessage, ByVal Optional FlagDispersionFunds As Boolean = False) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetSchedulePaymentById(id As Integer) As SchedulePayment

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Function GetSPSchedulePayment(supplierTypeListId As String, Optional paymentD As String = Nothing) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Obtiene la programación de pagos por tipo de proveedor o todos los hijos (no padres) de éste
    ''' </summary>
    ''' <param name="code">Si este valor es distinto de vacío, es porque se esta editando por lo cual tendria que buscar cual detalle se ha pagado</param>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    Function GetSPSchedulePaymentBySupplierTypeId(supplierTypeId As Integer, code As String) As ActionResult(Of List(Of SP_SchedulePayment_Result))

    ''' <summary>
    ''' Confirma una programación de pagos
    ''' </summary>
    ''' <returns></returns>
    Function ConfirmSchedulePayment(ByVal SchedulePaymentId As Integer, ByVal audit As AuditMessage) As ActionResult(Of String)

    Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As ActionResult

    ''' <summary>
    ''' funcion para calcular el descuento de una cuenta por pagar dependiendo de la fecha de pago
    ''' </summary>
    ''' <param name="ListSchedulePayment"></param>
    ''' <param name="PaymentDate"></param>
    ''' <returns></returns>
    Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As ActionMessageResult(Of List(Of SP_SchedulePayment_Result))

End Interface
