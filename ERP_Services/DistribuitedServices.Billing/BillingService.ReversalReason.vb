Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Billing
Imports Microsoft.Practices.Unity

Partial Public Class BillingService

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Public Function DeleteReversalReason(reversalReason As Domain.Entities.BillingReversalReason, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBillingServiceReversalReason.DeleteReversalReason
        Using service As IReversalReasonAdminService = Container.Current.Resolve(Of IReversalReasonAdminService)()
            Return service.DeleteReversalReason(reversalReason, audit)
        End Using
        'Return _reversalReasonAdminService.DeleteReversalReason(reversalReason, audit)
    End Function

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetReversalReason(code As String, tracking As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason) Implements IBillingServiceReversalReason.GetReversalReason
        Using service As IReversalReasonAdminService = Container.Current.Resolve(Of IReversalReasonAdminService)()
            Return service.GetReversalReason(code, tracking, audit)
        End Using
        'Return _reversalReasonAdminService.GetReversalReason(code, tracking, audit)
    End Function

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetReversalReasonById(id As Integer, tracking As Boolean) As Domain.Entities.BillingReversalReason Implements IBillingServiceReversalReason.GetReversalReasonById
        Using service As IReversalReasonAdminService = Container.Current.Resolve(Of IReversalReasonAdminService)()
            Return service.GetReversalReasonById(id, tracking)
        End Using
        'Return _reversalReasonAdminService.GetReversalReasonById(id, tracking)
    End Function

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Public Function SaveReversalReason(reversalReason As Domain.Entities.BillingReversalReason, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason) Implements IBillingServiceReversalReason.SaveReversalReason
        Using service As IReversalReasonAdminService = Container.Current.Resolve(Of IReversalReasonAdminService)()
            Return service.SaveReversalReason(reversalReason, audit, idSequence)
        End Using
        'Return _reversalReasonAdminService.SaveReversalReason(reversalReason, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state reversal reason.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function UpdateStateReversalReason(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason) Implements IBillingServiceReversalReason.UpdateStateReversalReason
        Using service As IReversalReasonAdminService = Container.Current.Resolve(Of IReversalReasonAdminService)()
            Return service.UpdateStateReversalReason(code, state, audit)
        End Using
        'Return _reversalReasonAdminService.UpdateStateReversalReason(code, state, audit)
    End Function

End Class
