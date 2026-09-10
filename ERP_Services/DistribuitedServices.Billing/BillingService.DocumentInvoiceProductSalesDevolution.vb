'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Billing

Partial Class BillingService

    Public Function SaveDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution, idSequense As Long, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IBillingServiceDocumentInvoiceProductSalesDevolution.SaveDocumentInvoiceProductSalesDevolution
        Using service As IDocumentInvoiceProductSalesDevolutionAdminService = Container.Current.Resolve(Of IDocumentInvoiceProductSalesDevolutionAdminService)()
            Return service.SaveDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution, audit, idSequense)
        End Using
    End Function

    Public Function GetDocumentInvoiceProductSalesDevolution(code As String, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IBillingServiceDocumentInvoiceProductSalesDevolution.GetDocumentInvoiceProductSalesDevolution
        Using service As IDocumentInvoiceProductSalesDevolutionAdminService = Container.Current.Resolve(Of IDocumentInvoiceProductSalesDevolutionAdminService)()
            Return service.GetDocumentInvoiceProductSalesDevolution(code, audit)
        End Using
    End Function

    Public Function GetDocumentInvoiceProductSalesDevolutionById(id As Integer, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IBillingServiceDocumentInvoiceProductSalesDevolution.GetDocumentInvoiceProductSalesDevolutionById
        Using service As IDocumentInvoiceProductSalesDevolutionAdminService = Container.Current.Resolve(Of IDocumentInvoiceProductSalesDevolutionAdminService)()
            Return service.GetDocumentInvoiceProductSalesDevolutionById(id)
        End Using
    End Function

End Class
