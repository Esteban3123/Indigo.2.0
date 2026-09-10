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

    Public Function SaveQuotation(Quotation As Quotation, idSequense As Long, audit As AuditMessage) As ActionResult(Of Quotation) Implements IBillingServiceQuotation.SaveQuotation
        Using service As IQuotationAdminService = Container.Current.Resolve(Of IQuotationAdminService)()
            Return service.SaveQuotation(Quotation, audit, idSequense)
        End Using
    End Function

    Public Function GetQuotation(code As String) As ActionResult(Of Quotation) Implements IBillingServiceQuotation.GetQuotation
        Using service As IQuotationAdminService = Container.Current.Resolve(Of IQuotationAdminService)()
            Return service.GetQuotation(code)
        End Using
    End Function

    Public Function GetQuotationById(id As Integer) As ActionResult(Of Quotation) Implements IBillingServiceQuotation.GetQuotationById
        Using service As IQuotationAdminService = Container.Current.Resolve(Of IQuotationAdminService)()
            Return service.GetQuotationById(id)
        End Using
    End Function

End Class
