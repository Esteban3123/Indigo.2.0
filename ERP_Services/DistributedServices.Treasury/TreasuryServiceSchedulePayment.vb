'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 21-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceSchedulePayment

    ''' <summary>
    ''' Elimina una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Public Function DeleteSchedulePayment(schedulePayment As SchedulePayment, audit As AuditMessage) As ActionResult Implements ITreasuryServiceSchedulePayment.DeleteSchedulePayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.DeleteSchedulePayment(schedulePayment, audit)
        End Using
        'Return Me._schedulePaymentAdminService.DeleteSchedulePayment(schedulePayment, audit)
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetSchedulePayment(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of SchedulePayment) Implements ITreasuryServiceSchedulePayment.GetSchedulePayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.GetSchedulePayment(code, tracking, audit)
        End Using
        'Return Me._schedulePaymentAdminService.GetSchedulePayment(code, tracking, audit)
    End Function

    Public Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As ActionResult(Of SchedulePayment) Implements ITreasuryServiceSchedulePayment.GetSchedulePaymentByVoucherTransaction
        Using Service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)
            Return Service.GetSchedulePaymentByVoucherTransaction(VoucherTransaction)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentById(id As Integer) As SchedulePayment Implements ITreasuryServiceSchedulePayment.GetSchedulePaymentById
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.GetSchedulePaymentById(id)
        End Using
        'Return Me._schedulePaymentAdminService.GetSchedulePaymentById(id)
    End Function

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSPSchedulePayment(supplierTypeListId As String) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ITreasuryServiceSchedulePayment.GetSPSchedulePayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.GetSPSchedulePayment(supplierTypeListId)
        End Using
        'Return Me._schedulePaymentAdminService.GetSPSchedulePayment(supplierTypeListId)
    End Function

    ''' <summary>
    ''' Guarda una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Public Function SaveSchedulePayment(schedulePayment As SchedulePayment, ByVal withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of SchedulePayment) Implements ITreasuryServiceSchedulePayment.SaveSchedulePayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.SaveSchedulePayment(schedulePayment, audit, withConfirm, idSequence)
        End Using
        'Return Me._schedulePaymentAdminService.SaveSchedulePayment(schedulePayment, audit, withConfirm, idSequence)
    End Function

    ''' <summary>
    ''' Confirma una programación de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentId"></param>
    ''' <returns></returns>
    Public Function ConfirmSchedulePayment(SchedulePaymentId As Integer, audit As AuditMessage) As ActionResult(Of String) Implements ITreasuryServiceSchedulePayment.ConfirmSchedulePayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.ConfirmSchedulePayment(SchedulePaymentId, audit)
        End Using
        'Return Me._schedulePaymentAdminService.ConfirmSchedulePayment(SchedulePaymentId, audit)
    End Function

    ''' <summary>
    ''' Obtiene el listado de programacion de pagos con pagos hechos
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentDatasourceWithPayment(code As String, audit As AuditMessage, Optional FlagDispersionFunds As Boolean = False) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ITreasuryServiceSchedulePayment.GetSchedulePaymentDatasourceWithPayment
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.GetSchedulePaymentDatasourceWithPayment(code, audit, FlagDispersionFunds)
        End Using
        'Return Me._schedulePaymentAdminService.GetSchedulePaymentDatasourceWithPayment(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene la programación de pagos por tipo de proveedor o todos los hijos (no padres) de éste
    ''' </summary>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    Public Function GetSPSchedulePaymentBySupplierTypeId(supplierTypeId As Integer, code As String) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ITreasuryServiceSchedulePayment.GetSPSchedulePaymentBySupplierTypeId
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.GetSPSchedulePaymentBySupplierTypeId(supplierTypeId, code)
        End Using
        'Return Me._schedulePaymentAdminService.GetSPSchedulePaymentBySupplierTypeId(supplierTypeId, code)
    End Function

    Public Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As ActionResult Implements ITreasuryServiceSchedulePayment.IsAccountPayableShareInSchedulePaymentDetailActive
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId)
        End Using
        'Return _schedulePaymentAdminService.IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId)
    End Function

    ''' <summary>
    ''' funcion que calcula el descuento de la cxp por la fecha de pago
    ''' </summary>
    ''' <param name="ListSchedulePayment"></param>
    ''' <param name="PaymentDate"></param>
    ''' <returns></returns>
    Public Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As ActionMessageResult(Of List(Of SP_SchedulePayment_Result)) Implements ITreasuryServiceSchedulePayment.CalculateDiscountByPaymentDate
        Using service As ISchedulePaymentAdminService = Container.Current.Resolve(Of ISchedulePaymentAdminService)()
            Return service.CalculateDiscountByPaymentDate(ListSchedulePayment, PaymentDate)
        End Using
    End Function
End Class