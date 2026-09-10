'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base

Imports Domain.Entities

Public Class AccountReceivableShareRepository
    Inherits GenericRepository(Of AccountReceivableShare)
    Implements IAccountReceivableShareRepository




#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    Public Function GetAccountReceivableShareById(idAccountReceivableShare As Integer) As AccountReceivableShare Implements IAccountReceivableShareRepository.GetAccountReceivableShareById
        Dim res = (From ars In _context.AccountReceivableShare.Include("AccountReceivable") Where ars.Id = idAccountReceivableShare Select ars).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From ars In _context.AccountReceivableShare.AsNoTracking Where ars.Id = idAccountReceivableShare Select ars).FirstOrDefault()
            res.InvoiceNumber = res.AccountReceivable.InvoiceNumber
            Return res
        Else
            Return New AccountReceivableShare
        End If
    End Function

    Public Function GetAccountReceivableShareByInvoiceNumber(invoiceNumber As String, idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer, Optional balance As Decimal = 0, Optional status As Integer = 2) As List(Of AccountReceivableShare) Implements IAccountReceivableShareRepository.GetAccountReceivableShareByInvoiceNumber
        If idCostCenter = 0 Then
            '/************************************
            ' And ar.MainAccountId = idMainAccount
            Dim accountReceivable = (From ar In _context.AccountReceivable Where ar.InvoiceNumber = invoiceNumber And ar.ThirdPartyId = idThirdParty And ar.Status = status Select ar).FirstOrDefault()

            If accountReceivable IsNot Nothing Then
                Dim accountReceivableShare = (From ars In _context.AccountReceivableShare Where ars.AccountReceivableId = accountReceivable.Id And ars.Balance > balance Select ars).ToList()

                Return (From ars In accountReceivableShare
                              Select New AccountReceivableShare With {.Id = ars.Id, .AccountReceivableId = ars.AccountReceivableId, .Number = ars.Number, .ExpiredDate = ars.ExpiredDate,
                                                          .Value = ars.Value, .Balance = ars.Balance, .DebitValue = ars.DebitValue, .CreditValue = ars.CreditValue,
                                                          .TransferValue = ars.TransferValue, .PaymentValue = ars.PaymentValue, .InterestPaymentDate = ars.InterestPaymentDate,
                                                          .InterestValue = ars.InterestValue, .SurchargesValue = ars.SurchargesValue, .CapitalRepaymentAgreement = ars.CapitalRepaymentAgreement,
                                                          .FinancialInterest = ars.FinancialInterest, .RepaymentAgreementInterest = ars.RepaymentAgreementInterest,
                                                          .InvoiceNumber = accountReceivable.InvoiceNumber, .InvoiceBalance = accountReceivable.Balance}).ToList()
            Else
                Return New List(Of AccountReceivableShare)
            End If
        Else
            '/***********************************************
            'And ar.MainAccountId = idMainAccount
            'And ar.CostCenterId = idCostCenter
            Dim accountReceivable = (From ar In _context.AccountReceivable Where ar.InvoiceNumber = invoiceNumber And ar.ThirdPartyId = idThirdParty And ar.Status = status Select ar).FirstOrDefault()

            If accountReceivable IsNot Nothing Then
                Dim accountReceivableShare = (From ars In _context.AccountReceivableShare Where ars.AccountReceivableId = accountReceivable.Id And ars.Balance > balance Select ars).ToList()

                Return (From ars In accountReceivableShare
                              Select New AccountReceivableShare With {.Id = ars.Id, .AccountReceivableId = ars.AccountReceivableId, .Number = ars.Number, .ExpiredDate = ars.ExpiredDate,
                                                          .Value = ars.Value, .Balance = ars.Balance, .DebitValue = ars.DebitValue, .CreditValue = ars.CreditValue,
                                                          .TransferValue = ars.TransferValue, .PaymentValue = ars.PaymentValue, .InterestPaymentDate = ars.InterestPaymentDate,
                                                          .InterestValue = ars.InterestValue, .SurchargesValue = ars.SurchargesValue, .CapitalRepaymentAgreement = ars.CapitalRepaymentAgreement,
                                                          .FinancialInterest = ars.FinancialInterest, .RepaymentAgreementInterest = ars.RepaymentAgreementInterest,
                                                          .InvoiceNumber = accountReceivable.InvoiceNumber, .InvoiceBalance = accountReceivable.Balance}).ToList()
            Else
                Return New List(Of AccountReceivableShare)
            End If

        End If
    End Function
    ''' <summary>
    ''' Obtiene la primer  cuota de una factura
    ''' </summary>
    ''' <param name="_AccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableSharebyAccountId(ByVal _AccountReceivableId As Integer) As AccountReceivableShare Implements IAccountReceivableShareRepository.GetAccountReceivableSharebyAccountId
        Dim accountReceivable = (From ar In _context.AccountReceivableShare Where ar.AccountReceivableId = _AccountReceivableId And ar.Number = 1 Select ar).FirstOrDefault()
        If accountReceivable IsNot Nothing Then
            Return accountReceivable
        Else
            Return New AccountReceivableShare
        End If
    End Function

End Class
