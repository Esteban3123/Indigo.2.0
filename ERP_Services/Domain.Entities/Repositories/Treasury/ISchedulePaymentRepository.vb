'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 20-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ISchedulePaymentRepository
    Inherits IRepository(Of SchedulePayment)

    Function GenerateSchedulePaymentSP(xml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_SaveSchedulePayment_Result)

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetSchedulePayment(ByVal code As String, tracking As Boolean) As SchedulePayment

    ''' <summary>
    ''' Obtiene una programacion de pagos por egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction"></param>
    ''' <returns></returns>
    Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As SchedulePayment

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetSchedulePaymentById(id As Integer) As SchedulePayment

    ''' <summary>
    ''' Lista los detalles de la programacion de pagos por id de la programacion de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentId">The schedule payment identifier.</param>
    ''' <returns></returns>
    Function ListSchedulePaymentDetailBySchedulePaymentId(ByVal SchedulePaymentId As Integer, Optional ByVal tracking As Boolean = False) As List(Of SchedulePaymentDetail)

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Function GetSPSchedulePayment(supplierTypeListId As String, Optional paymentD As String = Nothing) As List(Of SP_SchedulePayment_Result)

    ''' <summary>
    ''' Gets the SP_ schedule payment_ result by account payable identifier account payable share identifier.
    ''' </summary>
    ''' <returns></returns>
    Function GetSp_SchedulePayment_ResultByAccountPayableIdAccountPayableShareId(ByVal AccountPayableId As Integer, ByVal AccountPayableShareId As Integer) As SP_SchedulePayment_Result

End Interface