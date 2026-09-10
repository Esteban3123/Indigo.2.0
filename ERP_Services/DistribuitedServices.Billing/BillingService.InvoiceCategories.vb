#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BillingService
    Implements IBillingServiceInvoiceCategories

    Public Function DeleteInvoiceCategory(invoiceCategory As Domain.Entities.InvoiceCategories, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBillingServiceInvoiceCategories.DeleteInvoiceCategory
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.DeleteInvoiceCategory(invoiceCategory, audit)
        End Using
        'Return _invoiceCategoriesAdminService.DeleteInvoiceCategory(invoiceCategory, audit)
    End Function

    Public Function GetInvoiceCategory(code As String, audit As AuditMessage) As Domain.Entities.InvoiceCategories Implements IBillingServiceInvoiceCategories.GetInvoiceCategory
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.GetInvoiceCategory(code, audit)
        End Using
        'Return _invoiceCategoriesAdminService.GetInvoiceCategory(code, audit)
    End Function

    Public Function GetInvoiceCategoryById(id As Integer) As Domain.Entities.InvoiceCategories Implements IBillingServiceInvoiceCategories.GetInvoiceCategoryById
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.GetInvoiceCategoryById(id)
        End Using
        'Return _invoiceCategoriesAdminService.GetInvoiceCategoryById(id)
    End Function

    Public Function SaveInvoiceCategory(invoiceCategory As Domain.Entities.InvoiceCategories, audit As AuditMessage, Optional idSequence As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories) Implements IBillingServiceInvoiceCategories.SaveInvoiceCategory
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.SaveInvoiceCategory(invoiceCategory, audit, idSequence)
        End Using
        'Return _invoiceCategoriesAdminService.SaveInvoiceCategory(invoiceCategory, audit, idSequence)
    End Function

    Public Function UpdateStateInvoiceCategory(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories) Implements IBillingServiceInvoiceCategories.UpdateStateInvoiceCategory
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.UpdateStateInvoiceCategory(code, state, audit)
        End Using
        'Return _invoiceCategoriesAdminService.UpdateStateInvoiceCategory(code, state, audit)
    End Function

    Public Function CopyAndPasteCategories(data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) Implements IBillingServiceInvoiceCategories.CopyAndPasteCategories
        Using service As IInvoiceCategoriesAdminService = Container.Current.Resolve(Of IInvoiceCategoriesAdminService)()
            Return service.CopyAndPasteCategories(data)
        End Using
        'Return _invoiceCategoriesAdminService.CopyAndPasteCategories(data)
    End Function

End Class
