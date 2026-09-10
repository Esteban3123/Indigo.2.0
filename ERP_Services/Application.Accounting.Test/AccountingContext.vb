'***********************************************************************
' Assembly         : Application.Accounting.Tests
' Author           : LFP
' Created          : 2017-07-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Payments
Imports Application.Portfolio
Imports DistributedServices.Accounting
Imports Microsoft.Practices.Unity
#End Region

Public Class AccountingContext
    Inherits TestingContext

#Region "Fields"
    Private _containerPayments As IUnityContainer
    Private _containerPortafolio As IUnityContainer
    Public _accountingDocumentAdminService As IAccountingDocumentAdminService
    Public _accountPayableAdminService As IAccountPayableAdminService
    Public _notesDebitCreditAdminService As INotesDebitCreditAdminService
    Public _paymentsSequenseAdminService As IPaymentsSequenseAdminService
    Public _portfolioSequenseAdminService As IPortfolioSequenseAdminService
    Public _portfolioNoteAdminService As IPortfolioNoteAdminService


#End Region

#Region "Builder"
    Public Sub New()
        MyBase.New()
        _containerPayments = DistributedServices.Payments.Container.Current
        _containerPortafolio = DistribuitedServices.Portfolio.Container.Current
        _container = Container.Current

        _accountingDocumentAdminService = _container.Resolve(Of IAccountingDocumentAdminService)
        _accountPayableAdminService = _containerPayments.Resolve(Of IAccountPayableAdminService)
        _notesDebitCreditAdminService = _containerPayments.Resolve(Of INotesDebitCreditAdminService)
        _paymentsSequenseAdminService = _containerPayments.Resolve(Of IPaymentsSequenseAdminService)
        _portfolioNoteAdminService = _containerPortafolio.Resolve(Of IPortfolioNoteAdminService)
        _portfolioSequenseAdminService = _containerPortafolio.Resolve(Of IPortfolioSequenseAdminService)

    End Sub
#End Region

End Class
