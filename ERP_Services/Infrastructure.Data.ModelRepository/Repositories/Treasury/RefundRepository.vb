'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class RefundRepository
    Inherits GenericRepository(Of Refunds)
    Implements IRefundRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetRefund(code As String) As Refunds Implements IRefundRepository.GetRefund
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As Refunds In Me._context.Refunds Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As Refunds In Me._context.Refunds.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Dim IdRefund As Integer = res(0).Id
            Dim IdCash As Integer = res(0).IdCashRegister
            Dim cashRegister = (From cr In _context.CashRegisters.AsNoTracking().Include("Currency").AsNoTracking() Where cr.Id = IdCash Select cr).FirstOrDefault()
            res(0).FullNameCashRegister = String.Format("{0} - {1}", cashRegister.Code, cashRegister.Name)
            res(0).CurrencyAbbreviation = If(cashRegister?.Currency Is Nothing, (From d In _context.CompanySettings.AsNoTracking() _
                                                                                                  .Include("Currency").AsNoTracking() Select d.Currency.Abbreviation).FirstOrDefault _
                                                                                                  , cashRegister?.Currency?.Abbreviation)
            Dim _listVoucher As List(Of VoucherTransaction) = (From vt As VoucherTransaction In _context.VoucherTransaction Where vt.IdRefund = IdRefund).ToList()
            For Each vt As VoucherTransaction In _listVoucher
                vt.FullNameThird = (From T As ThirdParty In _context.ThirdParty Where T.Id = vt.IdThirdParty Select String.Concat(T.Nit, " - ", T.Name)).FirstOrDefault()
                vt.StatusName = ResourceManager.GetString("StatusName" + vt.Status.ToString, "Treasury")
                res(0).VoucherTransaction.Add(vt)
            Next

            Return res(0)
        Else
            Return New Refunds()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetRefundById(id As Integer) As Refunds Implements IRefundRepository.GetRefundById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As Refunds In Me._context.Refunds Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As Refunds In Me._context.Refunds.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New Refunds()
        End If
    End Function

    ''' <summary>
    ''' Valida que el reembolso este en estado anulado
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateRefundById(id As Integer) As Refunds Implements IRefundRepository.ValidateRefundById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As Refunds In Me._context.Refunds Where d.Id = id AndAlso d.Status <> 3 Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable que no hayan sido reembolsados
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdMainAccount</exception>
    Public Function ListRefundByAccount(IdMainAccount As Integer) As List(Of Refunds) Implements IRefundRepository.ListRefundByAccount
        If IdMainAccount = 0 Then
            Throw New ArgumentNullException("IdMainAccount")
        End If
        Dim refunded As Boolean = False
        Dim res = (From r As Refunds In Me._context.Refunds
                   Join cr As CashRegisters In _context.CashRegisters On r.IdCashRegister Equals cr.Id
                   Where cr.IdMainAccount = IdMainAccount And r.Refunded = refunded Select r).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each r As Refunds In res
                r.OriginalValue = (From rf As Refunds In Me._context.Refunds Where rf.Id = r.Id Select rf).FirstOrDefault()
                r.FullNameCashRegister = (From c As CashRegisters In _context.CashRegisters Where c.Id = r.IdCashRegister Select c.Name).FirstOrDefault()
            Next
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    Public Function ListRefundByCashRegisterId(CashRegisterId As Integer, Optional getRefundWithRefunded As Boolean = False) As List(Of Refunds) Implements IRefundRepository.ListRefundByCashRegisterId
        If CashRegisterId = 0 Then
            Throw New ArgumentNullException("CashRegisterId")
        End If
        Dim refunded As Boolean = False
        If getRefundWithRefunded Then
            refunded = True
        End If
        Dim Confirm As Integer = 2
        Dim res = (From r As Refunds In Me._context.Refunds
                   Where r.IdCashRegister = CashRegisterId And r.Refunded = refunded And r.Status = Confirm Select r).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each r As Refunds In res
                Dim CashRegister = (From c As CashRegisters In _context.CashRegisters.Include("Currency") Where c.Id = r.IdCashRegister Select c).FirstOrDefault()
                r.FullNameCashRegister = CashRegister?.Name
                r.OriginalValue = (From rf In _context.Refunds Where rf.Id = r.Id Select rf).FirstOrDefault()
                'Se consulta la abreviacion de la moneda, si la caja no tiene una moneda se postula la moneda oficial
                r.CurrencyAbbreviation = If(CashRegister?.Currency Is Nothing, (From d In _context.CompanySettings.AsNoTracking() _
                                                                                                  .Include("Currency").AsNoTracking() Select d.Currency.Abbreviation).FirstOrDefault _
                                                                                                  , CashRegister?.Currency?.Abbreviation)
            Next
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    ''' <param name="voucherTransactionId"></param>
    ''' <returns></returns>
    Public Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail Implements IRefundRepository.GetRefundDetailByVoucherTransactionId
        Dim query = (From r In _context.RefundDetail.Include("Refunds") Where r.VoucherTransactionId = voucherTransactionId Select r).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New RefundDetail()
        End If
    End Function

    Public Function ListRefundMassiveConfirm(listDocuments As List(Of String)) As List(Of Refunds) Implements IRefundRepository.ListRefundMassiveConfirm
        Return (From tn In _context.Refunds Where listDocuments.Contains(tn.Code) Select tn).ToList()
    End Function

    ''' <summary>
    ''' Valida si hay reembolso con la misma caja sin confirmar
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateRefundRegister(CashRegisterId As Integer) As Boolean Implements IRefundRepository.ValidateRefundRegister
        If CashRegisterId = 0 Then
            Throw New ArgumentNullException("CashRegisterId")
        End If
        Dim query = (From r In _context.Refunds.AsNoTracking Where r.IdCashRegister = CashRegisterId AndAlso r.Status = 1 Select r).Count
        If query > 0 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    '''  Valida si hay reembolso con la misma caja confirmado y con refunded en false
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateRefundConfirmed(CashRegisterId As Integer) As Boolean Implements IRefundRepository.ValidateRefundConfirmed
        If CashRegisterId = 0 Then
            Throw New ArgumentNullException("CashRegisterId")
        End If
        Dim query = (From r In _context.Refunds.AsNoTracking Where r.IdCashRegister = CashRegisterId AndAlso r.Status = 2 AndAlso r.Refunded = False Select r).Count
        If query > 0 Then
            Return True
        End If
        Return False
    End Function

End Class
