'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class TransfersRepository
    Inherits GenericRepository(Of PaymentTransfer)
    Implements ITransfersRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function SP_ImportBillsToPaymentTransfer(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToPaymentTransfer_Result) Implements ITransfersRepository.SP_ImportBillsToPaymentTransfer
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportBillsToPaymentTransfer(XmlObject, XmlParameters).ToList
    End Function

    Public Function ListPaymentTransferMassiveConfirm(listDocuments As List(Of String)) As List(Of PaymentTransfer) Implements ITransfersRepository.ListPaymentTransferMassiveConfirm
        Dim res = (From cr In _context.PaymentTransfer.AsNoTracking().Include("PaymentTransferDetail").AsNoTracking() Where listDocuments.Contains(cr.Code) Select cr).ToList()
        For Each item In res
            For Each itemDeatil In item.PaymentTransferDetail
                Dim madet = (From mad In _context.MainAccounts.AsNoTracking Where mad.Id = itemDeatil.MainAccountId Select mad).FirstOrDefault
                itemDeatil.NumberNameMainAccount = madet.Number + " - " + madet.Name

                Dim bill = (From b In _context.AccountPayable.AsNoTracking.Include("Supplier").AsNoTracking() Where b.Id = itemDeatil.AccountPayableId Select b).FirstOrDefault
                itemDeatil.NumberBill = bill.BillNumber
                itemDeatil.BalanceBill = bill.Balance
                itemDeatil.SupplierDescription = bill.Supplier.Code + " - " + bill.Supplier.Name
                itemDeatil.ThirdPartyId = bill.IdThirdParty
                itemDeatil.SupplierId = bill.IdSupplier

                Dim share = (From s In _context.AccountPayableShares.AsNoTracking Where s.Id = itemDeatil.AccountPayableShareId Select s).FirstOrDefault
                itemDeatil.Share = share.Share
                itemDeatil.BalanceShare = share.Balance
            Next
        Next
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransfer(code As String, Optional tracking As Boolean = True) As PaymentTransfer Implements ITransfersRepository.GetPaymentsTransfer
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As PaymentTransfer In Me._context.PaymentTransfer.Include("PaymentTransferDetail").Include("PaymentTransferOtherConcept").Include("AdvancePayments").Include("AdvancePayments.Currency")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim supplier = (From s In _context.Supplier.AsNoTracking Where s.Id = res.SupplierId Select s).FirstOrDefault
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name

            If res.AdvancePaymentId <> Nothing AndAlso res.AdvancePaymentId <> 0 Then
                Dim advance = (From a In _context.AdvancePayments.AsNoTracking Where a.Id = res.AdvancePaymentId Select a).FirstOrDefault
                res.CodeAdvancePayments = advance.Code
                res.BalanceAdvancePayments = advance.Balance
            End If

            Dim ma = (From acc In _context.MainAccounts.AsNoTracking Where acc.Id = res.MainAccountId Select acc).FirstOrDefault
            res.NumberNameAccount = ma.Number + " - " + ma.Name

            If res.CostCenterId IsNot Nothing Then
                Dim cc = (From cos In _context.CostCenter.AsNoTracking Where cos.Id = res.CostCenterId Select cos).FirstOrDefault
                res.CodeNameCostCenter = cc.Code + " - " + cc.Name
            End If

            For Each itemDeatil As PaymentTransferDetail In res.PaymentTransferDetail

                Dim madet = (From mad In _context.MainAccounts.AsNoTracking Where mad.Id = itemDeatil.MainAccountId Select mad).FirstOrDefault
                itemDeatil.NumberNameMainAccount = madet.Number + " - " + madet.Name

                Dim bill = (From b In _context.AccountPayable.AsNoTracking() _
                                .Include("Currency") _
                                .Include("Supplier")
                            Where b.Id = itemDeatil.AccountPayableId Select b).FirstOrDefault

                itemDeatil.NumberBill = bill.BillNumber
                itemDeatil.BalanceBill = bill.Balance
                itemDeatil.SupplierDescription = bill.Supplier.Code + " - " + bill.Supplier.Name
                itemDeatil.ThirdPartyId = bill.IdThirdParty
                itemDeatil.SupplierId = bill.IdSupplier
                itemDeatil.CurrencyAbbreviationInvoice = bill?.Currency?.Abbreviation

                Dim share = (From s In _context.AccountPayableShares.AsNoTracking Where s.Id = itemDeatil.AccountPayableShareId Select s).FirstOrDefault
                itemDeatil.Share = share.Share
                itemDeatil.BalanceShare = share.Balance

            Next

            For Each itemOtherConcept In res.PaymentTransferOtherConcept
                itemOtherConcept.CodeNameConceptNote = (From e In _context.AccountPayableConceptNotes.AsNoTracking() Where e.Id = itemOtherConcept.AccountPayableConceptNoteId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                itemOtherConcept.NumberNameMainAccount = (From a In _context.MainAccounts Where a.Id = itemOtherConcept.MainAccountId Select String.Concat(a.Number, " - ", a.Name)).FirstOrDefault()
            Next

            res.OriginalValue = (From d As PaymentTransfer In Me._context.PaymentTransfer.AsNoTracking().Include("PaymentTransferDetail").AsNoTracking
                                 Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
            Return res
        Else
            Return New PaymentTransfer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransferById(id As Integer, Optional tracking As Boolean = True) As PaymentTransfer Implements ITransfersRepository.GetPaymentsTransferById
        If tracking Then
            Dim res = (From d As PaymentTransfer In Me._context.PaymentTransfer.AsNoTracking.Include("PaymentTransferDetail").AsNoTracking Where d.Id = id Select d).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                res(0).OriginalValue = (From d As PaymentTransfer In Me._context.PaymentTransfer.AsNoTracking().Include("PaymentTransferDetail").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New PaymentTransfer()
            End If
        Else
            Return (From d As PaymentTransfer In Me._context.PaymentTransfer.AsNoTracking Where d.Id = id Select d).FirstOrDefault()
        End If
    End Function

    Public Function GetPaymentTransferByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String) Implements ITransfersRepository.GetPaymentTransferByAccountPayableListId
        Dim Uno As Byte = 1
        Return (From r In _context.PaymentTransferDetail.AsNoTracking()
                Join pt In _context.PaymentTransfer On r.PaymentTransferId Equals pt.Id
                Join ap In _context.AccountPayable.AsNoTracking() On r.AccountPayableId Equals ap.Id
                Where listAccountPayableId.Contains(r.AccountPayableId) AndAlso pt.Status = Uno
                Select String.Concat(ap.BillNumber, ";", pt.Code)).ToList()
    End Function

    Function SP_ConfirmPaymentTransfer(paymentTransferId As Integer, codeUser As String) As SP_ConfirmPaymentTransfer_Result Implements ITransfersRepository.SP_ConfirmPaymentTransfer
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmPaymentTransfer(paymentTransferId, codeUser).FirstOrDefault()
    End Function

End Class
