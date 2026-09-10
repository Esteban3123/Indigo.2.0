Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Payments
Imports Microsoft.Practices.Unity

Partial Class PaymentsService

    Public Function ConfirmPaymentDocument(processId As Integer, code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, Integer)) Implements IPaymentsServicePaymentsMassiveConfirm.ConfirmPaymentDocument
        Using service As IPaymentsMassiveConfirmAdminService = Container.Current.Resolve(Of IPaymentsMassiveConfirmAdminService)()
            Return service.ConfirmPaymentDocument(processId, code, audit)
        End Using
    End Function

    Public Function ConfirmPaymentsDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPaymentsServicePaymentsMassiveConfirm.ConfirmPaymentsDocuments
        Using service As IPaymentsMassiveConfirmAdminService = Container.Current.Resolve(Of IPaymentsMassiveConfirmAdminService)()
            Return service.ConfirmPaymentsDocuments(processId, listDocuments, audit)
        End Using
    End Function

End Class
