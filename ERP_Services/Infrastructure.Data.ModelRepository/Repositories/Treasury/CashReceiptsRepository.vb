'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core.Objects
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class CashReceiptsRepository
    Inherits GenericRepository(Of CashReceipts)
    Implements ICashReceiptsRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


#Region "Demo"
    Public Function GetFirstAccount() As EntityBankAccounts Implements ICashReceiptsRepository.GetFirstAccount
        Return (From e In _context.EntityBankAccounts Select e).Take(1).FirstOrDefault()
    End Function

    Public Function GetThird(id As Integer) As ThirdParty Implements ICashReceiptsRepository.GetThird
        Return (From t In _context.ThirdParty Where t.Id = id Select t).FirstOrDefault()
    End Function

    Public Function GetAccountReceivableByInvoiceNumberCashReceipts(invoiceNumber As String) As AccountReceivable Implements ICashReceiptsRepository.GetAccountReceivableByInvoiceNumberCashReceipts
        Return (From a In _context.AccountReceivable.Include("AccountReceivableAccounting") Where a.InvoiceNumber = invoiceNumber Select a).FirstOrDefault()
    End Function

    Public Function GetConcept(mainAccountId As Integer) As CashReceiptConcepts Implements ICashReceiptsRepository.GetConcept
        Return (From a In _context.CashReceiptConcepts Where a.IdMainAccount = mainAccountId And a.Affectation = 2 Select a).FirstOrDefault()
    End Function
#End Region

#Region "Methods"

    

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCashReceiptsMassiveConfirm(listDocuments As List(Of String)) As List(Of CashReceipts) Implements ICashReceiptsRepository.ListCashReceiptsMassiveConfirm
        Return (From cr In _context.CashReceipts.Include("PortfolioAdvance").Include("CashReceiptDetails").Include("PaymentMethods").Include("CashReceiptDetails.CashReceiptAccountReceivable").Include("CashReceiptDetails.CashReceiptAdvancePayment").Include("CashReceiptDetails.CashReceiptDetailAccountPayable") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

    Public Function ValidateCostcenterPaymentMethod(PaymentMethods As PaymentMethods, operatingUnitId As Integer) As Domain.Base.Entities.ActionResult Implements ICashReceiptsRepository.ValidateCostcenterPaymentMethod
        Dim card As Cards = (From c In _context.Cards.AsNoTracking().Include("CardCostCenter").AsNoTracking() Where c.Id = PaymentMethods.IdCard Select c).FirstOrDefault()
        Dim handlesCostcenterCommision As Boolean = False
        Dim handlesCostcenterRFT As Boolean = False
        Dim handlesCostcenterICA As Boolean = False
        If PaymentMethods.CommissionValue <> 0 Then
            handlesCostcenterCommision = (From c In _context.CashReceiptConcepts.AsNoTracking()
                                                             Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id
                                                             Where c.Id = card.IdCashReceiptConceptCommision Select m.HandlesCostCenter).FirstOrDefault()
        End If
        If PaymentMethods.RTFValue <> 0 Then
            handlesCostcenterRFT = (From c In _context.CashReceiptConcepts.AsNoTracking()
                                                             Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id
                                                             Where c.Id = card.IdCashReceiptConceptRTF Select m.HandlesCostCenter).FirstOrDefault()
        End If
        If PaymentMethods.CommissionValue <> 0 Then
            handlesCostcenterICA = (From c In _context.CashReceiptConcepts.AsNoTracking()
                                                             Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id
                                                             Where c.Id = card.IdCashReceiptConceptICA Select m.HandlesCostCenter).FirstOrDefault()
        End If
        If handlesCostcenterCommision OrElse handlesCostcenterRFT OrElse handlesCostcenterICA Then
            If card.GetCostCenter = 2 AndAlso (card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = operatingUnitId)) Then
                Return New Domain.Base.Entities.ActionResult With {.StateResult = False, .Message = String.Format("no se encontró centro de costo para la unidad operativa seleccionada. La tarjeta {0} está parametrizada con centro de costo por unidad operativa", String.Concat(card.Code, " - ", card.Name))}
            End If
        End If
        Return New Domain.Base.Entities.ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' metodo para obtener un recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetCashReceiptsByCode(code As String) As CashReceipts Implements ICashReceiptsRepository.GetCashReceiptsByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From cr In _context.CashReceipts.Include("PortfolioAdvance") Where cr.Code = code.Trim Select cr).FirstOrDefault()


        If res IsNot Nothing Then
            Dim third = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.IdThirdParty Select tp).FirstOrDefault()
            res.NitNameThirdParty = third.Nit + " - " + third.Name
            Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IdMainAccount Select ma).FirstOrDefault()
            res.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
            If res.IdCostCenter IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.IdCostCenter).FirstOrDefault
                res.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            If res.CurrencyId.HasValue Then
                Dim currency = (From c In _context.Currency.AsNoTracking() Where c.Id = res.CurrencyId Select c).FirstOrDefault()
                res.currencyAbbreviation = currency.Abbreviation
            End If

            If res.IdCashRegister IsNot Nothing Then
                Dim cashRegister = (From cr In _context.CashRegisters.Include("Currency").AsNoTracking() Where cr.Id = res.IdCashRegister).FirstOrDefault
                res.CodeNameCashRegister = cashRegister.Code + " - " + cashRegister.Name
                If Not res.CurrencyId.HasValue Then
                    res.CurrencyId = cashRegister.CurrencyId
                    res.currencyAbbreviation = cashRegister.Currency?.Abbreviation
                End If
            End If

            If res.IdBankAccount IsNot Nothing Then
                Dim bankAccount = (From ba In _context.EntityBankAccounts.Include("Currency").AsNoTracking() Where ba.Id = res.IdBankAccount Select ba).FirstOrDefault()
                Dim bank = (From b In _context.Bank.AsNoTracking() Where bankAccount.IdBank = b.Id Select b).FirstOrDefault()
                Dim type As String
                If bankAccount.Type = 1 Then
                    type = "Ahorro "
                Else
                    type = "Corriente "
                End If
                res.CodeNameBankAccount = bankAccount.Code + " - " + bank.Name + " CTA. " + type + bankAccount.Number
                If Not res.CurrencyId.HasValue Then
                    res.CurrencyId = bankAccount.CurrencyId
                    res.currencyAbbreviation = bankAccount.Currency?.Abbreviation
                End If
            End If
            res.OriginalValue = (From cr In _context.CashReceipts.AsNoTracking().Include("PortfolioAdvance").AsNoTracking().Include("CashReceiptDetails").Include("CashReceiptDetails.Currency").AsNoTracking() Where cr.Code = code.Trim Select cr).FirstOrDefault
            Return res
        Else
            Return New CashReceipts
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un recibo de caja por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptsById(id As Integer) As CashReceipts Implements ICashReceiptsRepository.GetCashReceiptsById
        Dim res = (From cr In _context.CashReceipts.AsNoTracking Where cr.Id = id Select cr).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New CashReceipts
        End If
    End Function



    ''' <summary>
    ''' metodo para traer los detalles del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipts"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptsDetailByIdCashReceipt(idCashReceipts As Integer) As List(Of CashReceiptDetails) Implements ICashReceiptsRepository.GetCashReceiptsDetailByIdCashReceipt
        Dim res = (From crd In _context.CashReceiptDetails.Include("CashReceiptAccountReceivable").Include("CashReceiptAdvancePayment").Include("CashReceiptDetailAccountPayable").Include("Currency") Where crd.IdCashReceipt = idCashReceipts Select crd).ToList()
        Dim _cashFlowConcept As CashFlowConcept = Nothing
        For Each item In res
            _cashFlowConcept = (From cfc In _context.CashFlowConcept.AsNoTracking Where cfc.Id = item.IdCashFlowConcept).FirstOrDefault
            If _cashFlowConcept IsNot Nothing Then
                item.CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
            End If
            Dim third = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = item.IdThirdParty Select tp).FirstOrDefault
            item.CodeNameThirdParty = third.Nit + " - " + third.Name
            Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.IdMainAccount Select ma).FirstOrDefault()
            item.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
            Dim concept = (From crs In _context.CashReceiptConcepts.AsNoTracking() Where crs.Id = item.IdCashReceiptConcept Select crs).FirstOrDefault()
            item.CodeNameCashReceiptConcept = concept.Code + " - " + concept.Name
            If item.IdCostCenter IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.IdCostCenter Select cc).FirstOrDefault()
                item.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            If item.IdRetentionConcept IsNot Nothing Then
                Dim retention = (From rc In _context.RetentionConcepts.AsNoTracking() Where rc.Id = item.IdRetentionConcept Select rc).FirstOrDefault
                item.CodeNameRetentionConcept = retention.Code + " - " + retention.Name
            End If
        Next
        Return res
    End Function

    Public Function GetCashReceiptsDetailByIdCashReceiptIncludes(idCashReceipts As Integer) As List(Of CashReceiptDetails) Implements ICashReceiptsRepository.GetCashReceiptsDetailByIdCashReceiptIncludes
        Dim res = (From crd In _context.CashReceiptDetails.Include("CashReceiptAccountReceivable").Include("CashReceiptAccountReceivable.CashReceiptAccountReceivableShare").Include("CashReceiptAdvancePayment").Include("CashReceiptDetailAccountPayable") Where crd.IdCashReceipt = idCashReceipts Select crd).ToList()
        For Each item In res
            Dim third = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = item.IdThirdParty Select tp).FirstOrDefault
            item.CodeNameThirdParty = third.Nit + " - " + third.Name
            Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.IdMainAccount Select ma).FirstOrDefault()
            item.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
            Dim concept = (From crs In _context.CashReceiptConcepts.AsNoTracking() Where crs.Id = item.IdCashReceiptConcept Select crs).FirstOrDefault()
            item.CodeNameCashReceiptConcept = concept.Code + " - " + concept.Name
            If item.IdCostCenter IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.IdCostCenter Select cc).FirstOrDefault()
                item.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            If item.IdRetentionConcept IsNot Nothing Then
                Dim retention = (From rc In _context.RetentionConcepts.AsNoTracking() Where rc.Id = item.IdRetentionConcept Select rc).FirstOrDefault
                item.CodeNameRetentionConcept = retention.Code + " - " + retention.Name
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' metodo para traer los metodos de pago del  recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    Public Function GetPaymentMethodsByIdCashReceipt(idCashReceipt As Integer) As List(Of PaymentMethods) Implements ICashReceiptsRepository.GetPaymentMethodsByIdCashReceipt
        Dim res = (From pm In _context.PaymentMethods.Include("Currency") Where pm.IdCashReceipt = idCashReceipt Select pm).ToList()
        For Each item In res
            If item.IdBank IsNot Nothing Then
                Dim bank = (From b In _context.Bank.AsNoTracking() Where b.Id = item.IdBank Select b).FirstOrDefault
                item.CodeNameBank = bank.Code + " - " + bank.Name
            End If
            If item.IdEntityBankAccount IsNot Nothing Then
                Dim bankAccount = (From ba In _context.EntityBankAccounts.AsNoTracking() Where ba.Id = item.IdEntityBankAccount Select ba).FirstOrDefault()
                Dim bank = (From b In _context.Bank.AsNoTracking() Where bankAccount.IdBank = b.Id Select b).FirstOrDefault()
                Dim type As String
                If bankAccount.Type = 1 Then
                    type = "Ahorro "
                Else
                    type = "Corriente "
                End If
                item.CodeNameBankAccount = bankAccount.Code + " - " + bank.Name + " CTA. " + type + bankAccount.Number
            End If
            If item.IdCard IsNot Nothing Then
                Dim card = (From c In _context.Cards.AsNoTracking Where c.Id = item.IdCard Select c).FirstOrDefault
                item.CodeNameCard = card.Code + " - " + card.Name
            End If
            If item.IdCostCenter IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = item.IdCostCenter Select cc).FirstOrDefault
                item.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            If item.CurrencyId > 0 Then
                item.CurrencyName = item.Currency.Abbreviation
            End If
        Next
        Return res
    End Function
#End Region


    Public Function GenerateCashReceiptSP(cashReceiptXml As String, UserCode As String, companyType As Integer) As ObjectResult(Of SP_SaveCashReceipts_Result) Implements ICashReceiptsRepository.GenerateCashReceiptSP
        Return _context.SP_SaveCashReceipts(cashReceiptXml, UserCode, companyType)
    End Function

    Public Function ReverseCashReceiptSP(CashReceiptId As Integer, UserCode As String, TreasuryNoteCode As String) As ObjectResult(Of SP_ReverseCashReceipt_Result) Implements ICashReceiptsRepository.ReverseCashReceiptSP
        Return _context.SP_ReverseCashReceipt(CashReceiptId, UserCode, TreasuryNoteCode)
    End Function

    ''' <summary>
    ''' ejecucion del sp que realiza la devolucion de un recibo de caja
    ''' </summary>
    ''' <param name="DevolutionCashReceipt"></param>
    ''' <returns></returns>
    Public Function SP_DevolutionCashReceipt(DevolutionCashReceipt As String) As ObjectResult(Of SP_DevolutionCashReceipt_Result) Implements ICashReceiptsRepository.SP_DevolutionCashReceipt
        Return _context.SP_DevolutionCashReceipt(DevolutionCashReceipt)
    End Function

    Public Function GetCashReceiptsByIdAccountReceivable(accountReceivableId As Integer) As CashReceipts Implements ICashReceiptsRepository.GetCashReceiptsByIdAccountReceivable
        'Return (From cr In _context.CashReceipts.Include("PortfolioAdvance").Include("CashReceiptDetails").Include("PaymentMethods").Include("CashReceiptDetails.CashReceiptAccountReceivable").Include("CashReceiptDetails.CashReceiptAdvancePayment").Include("CashReceiptDetails.CashReceiptDetailAccountPayable") Where listDocuments.Contains(cr.Code) Select cr).ToList()
        Return (From cr In _context.CashReceipts.Include("CashReceiptDetails")
                Join crd In _context.CashReceiptDetails On crd.IdCashReceipt Equals cr.Id
                Join cra In _context.CashReceiptAccountReceivable On cra.CashReceiptDetailId Equals crd.Id
                Where cra.AccountReceivableId = accountReceivableId
                Select cr).FirstOrDefault()
    End Function
End Class
