'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    Public Function ImportBillsToPaymentTransfer(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of PaymentTransferDetail)) Implements IPaymentsTransfers.ImportBillsToPaymentTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.ImportBillsToPaymentTransfer(data, parameters)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el traslado
    ''' </summary>
    ''' <param name="trasnfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentsTransfer(trasnfer As Domain.Entities.PaymentTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsTransfers.DeletePaymentsTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.DeletePaymentsTransfer(trasnfer, audit)
        End Using
        'Return Me._transferAdminService.DeletePaymentsTransfer(trasnfer, audit)
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransfer(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer) Implements IPaymentsTransfers.GetPaymentsTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.GetPaymentsTransfer(code, audit)
        End Using
        'Return Me._transferAdminService.GetPaymentsTransfer(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransferById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer) Implements IPaymentsTransfers.GetPaymentsTransferById
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.GetPaymentsTransferById(id, audit)
        End Using
        'Return Me._transferAdminService.GetPaymentsTransferById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado
    ''' </summary>
    ''' <param name="trasnfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentsTransfer(trasnfer As Domain.Entities.PaymentTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer) Implements IPaymentsTransfers.SavePaymentsTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.SavePaymentsTransfer(trasnfer, audit, idSequense)
        End Using
        'Return Me._transferAdminService.SavePaymentsTransfer(trasnfer, audit, idSequense)
    End Function

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <param name="paymentTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmTransfer(paymentTransfer As Domain.Entities.PaymentTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer) Implements IPaymentsTransfers.ConfirmTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.ConfirmTransfer(paymentTransfer, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y Confirma el traslado
    ''' </summary>
    ''' <param name="paymentTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmTransfer(paymentTransfer As Domain.Entities.PaymentTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer) Implements IPaymentsTransfers.SaveAndConfirmTransfer
        Using service As ITransfersAdminService = Container.Current.Resolve(Of ITransfersAdminService)()
            Return service.SaveAndConfirmTransfer(paymentTransfer, audit, idSequense)
        End Using
    End Function

End Class
