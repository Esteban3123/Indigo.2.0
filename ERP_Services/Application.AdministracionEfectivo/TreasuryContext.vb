'***********************************************************************
' Assembly         : Application.Accounting.Tests
' Author           : LFP
' Created          : 2017-07-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

#End Region

Imports Application.Treasury
Imports DistributedServices.Treasury
Imports DistributedServices.Accounting
Imports DistributedServices.Payments
Imports DistribuitedServices.Portfolio
Imports Microsoft.Practices.Unity
Imports Application.Accounting
Imports Application.Payments
Imports Application.Portfolio

Public Class TreasuryContext
    Inherits TestingContext

#Region "Fields"
    Public _containerAccounting As IUnityContainer
    Public _containerPayment As IUnityContainer
    Public _containerPortfolio As IUnityContainer

    Public _treasuryNoteAdminService As ITreasuryNoteAdminService
    Public _treasuryServiceCard As ICardAdminService
    Public _cashReceiptsAdminService As ICashReceiptsAdminService
    Public _cashReceiptConceptAdminService As ICashReceiptConceptAdminService
    Public _PUCAdminService As IPUCAdminService
    Public _accountPayableAdminService As IAccountPayableAdminService
    Public _accountReceivableAdminService As IAccountReceivableAdminService
    Public _CrossingAccountAdminService As ICrossingAccountAdminService
    Public _voucherTransactionAdminService As IVoucherTransactionAdminService
    Public _expenseConceptAdminService As IExpenseConceptAdminService
    Public _moneyAdvanceAdminService As IMoneyAdvanceAdminService

    Public _treasurySequenseAdminService As ITreasurySequenseAdminService



#End Region

#Region "Builder"
    Public Sub New()
        MyBase.New()

        _container = DistributedServices.Treasury.Container.Current
        _containerAccounting = DistributedServices.Accounting.Container.Current
        _containerPayment = DistributedServices.Payments.Container.Current
        _containerPortfolio = DistribuitedServices.Portfolio.Container.Current


        _treasuryNoteAdminService = _container.Resolve(Of ITreasuryNoteAdminService)
        _treasuryServiceCard = _container.Resolve(Of ICardAdminService)
        _cashReceiptsAdminService = _container.Resolve(Of ICashReceiptsAdminService)
        _cashReceiptConceptAdminService = _container.Resolve(Of ICashReceiptConceptAdminService)
        _voucherTransactionAdminService = _container.Resolve(Of IVoucherTransactionAdminService)
        _expenseConceptAdminService = _container.Resolve(Of IExpenseConceptAdminService)
        _moneyAdvanceAdminService = _containerPayment.Resolve(Of IMoneyAdvanceAdminService)

        _PUCAdminService = _containerAccounting.Resolve(Of IPUCAdminService)
        _accountPayableAdminService = _containerPayment.Resolve(Of IAccountPayableAdminService)
        _accountReceivableAdminService = _containerPortfolio.Resolve(Of IAccountReceivableAdminService)
        _CrossingAccountAdminService = _container.Resolve(Of ICrossingAccountAdminService)

        _treasurySequenseAdminService = _container.Resolve(Of ITreasurySequenseAdminService)


    End Sub
#End Region

End Class
