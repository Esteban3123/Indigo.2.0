'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure

Public Class SchedulePaymentRepository
    Inherits GenericRepository(Of SchedulePayment)
    Implements ISchedulePaymentRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetSchedulePayment(code As String, tracking As Boolean) As SchedulePayment Implements ISchedulePaymentRepository.GetSchedulePayment
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query = Nothing
        If tracking Then
            query = (From sp In _context.SchedulePayment.Include("SchedulePaymentDetail.AccountPayable").Include("SchedulePaymentDetail.SchedulePaymentDetailBudget").Include("SchedulePaymentBankAccount") Where sp.Code.Equals(code) Select sp).FirstOrDefault() '.AsNoTracking() Where sp.Code.Equals(code) Select sp).FirstOrDefault()
        Else
            query = (From sp In _context.SchedulePayment.Include("SchedulePaymentDetail.AccountPayable").AsNoTracking().Include("SchedulePaymentDetail.SchedulePaymentDetailBudget").AsNoTracking().Include("SchedulePaymentBankAccount").AsNoTracking() Where sp.Code.Equals(code) Select sp).FirstOrDefault() '.AsNoTracking() Where sp.Code.Equals(code) Select sp).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From sp In _context.SchedulePayment.AsNoTracking() Where sp.Code.Equals(code) Select sp).FirstOrDefault()
            Return query
        Else
            Return New SchedulePayment
        End If
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por egreso
    ''' </summary>
    ''' <param name="VoucherTransaction"></param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As SchedulePayment Implements ISchedulePaymentRepository.GetSchedulePaymentByVoucherTransaction
        Dim IdVoucherTransaction = VoucherTransaction.Id
        Dim schedulePaymentId = VoucherTransaction.SchedulePaymentId
        Dim result = Me._context.SchedulePayment.Include("SchedulePaymentBankAccount").Where(Function(w) w.Id = schedulePaymentId).FirstOrDefault()
        Dim details = Me._context.SchedulePaymentDetail.Include("AccountPayable").Include("SchedulePaymentDetailBudget") _
                                                        .Where(Function(w) w.VoucherTransactionId = IdVoucherTransaction)

        details.ToList().ForEach(Sub(i)
                                     result.SchedulePaymentDetail.Add(i)
                                 End Sub)
        Return result
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetSchedulePaymentById(id As Integer) As SchedulePayment Implements ISchedulePaymentRepository.GetSchedulePaymentById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From sp In _context.SchedulePayment Where sp.Id = id Select sp).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From sp In _context.SchedulePayment.AsNoTracking() Where sp.Id = id Select sp).FirstOrDefault()
            Return query
        Else
            Return New SchedulePayment
        End If
    End Function

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSPSchedulePayment(supplierTypeListId As String, Optional paymentD As String = Nothing) As List(Of SP_SchedulePayment_Result) Implements ISchedulePaymentRepository.GetSPSchedulePayment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SchedulePayment(supplierTypeListId, paymentD).ToList()
    End Function

    ''' <summary>
    ''' Lista los detalles de la programacion de pagos por id de la programacion de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentId">The schedule payment identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">SchedulePaymentId</exception>
    Public Function ListSchedulePaymentDetailBySchedulePaymentId(SchedulePaymentId As Integer, Optional ByVal tracking As Boolean = False) As List(Of SchedulePaymentDetail) Implements ISchedulePaymentRepository.ListSchedulePaymentDetailBySchedulePaymentId
        If SchedulePaymentId = 0 Then
            Throw New ArgumentNullException("SchedulePaymentId")
        End If
        If tracking Then
            Return (From spd In _context.SchedulePaymentDetail.Include("ThirdParty") Where spd.SchedulePaymentId = SchedulePaymentId Select spd).ToList()
        Else
            Return (From spd In _context.SchedulePaymentDetail.AsNoTracking().Include("ThirdParty").AsNoTracking() Where spd.SchedulePaymentId = SchedulePaymentId Select spd).ToList()
        End If
    End Function

    Public Function GetSp_SchedulePayment_ResultByAccountPayableIdAccountPayableShareId(AccountPayableId As Integer, AccountPayableShareId As Integer) As SP_SchedulePayment_Result Implements ISchedulePaymentRepository.GetSp_SchedulePayment_ResultByAccountPayableIdAccountPayableShareId
        If AccountPayableId = 0 Then
            Throw New ArgumentNullException("AccountPayableId")
        End If
        If AccountPayableShareId = 0 Then
            Throw New ArgumentNullException("AccountPayableShareId")
        End If

        Dim query = (From s In _context.Supplier
                     Join ap In _context.AccountPayable On ap.IdSupplier Equals s.Id
                     Join aps In _context.AccountPayableShares On aps.IdAccountPayable Equals ap.Id
                     Join tp In _context.ThirdParty On tp.Id Equals s.IdThirdParty
                     Join spd In _context.SuppliersDistributionLines On ap.IdSuppliersDistributionLines Equals spd.Id
                     Join dl In _context.DistributionLines On spd.IdDistributionLine Equals dl.Id
                     Join ec In _context.ExpenseConcepts On ec.Id Equals dl.ExpensesConceptId
                     Join c In _context.Currency On c.Id Equals ap.CurrencyId
                     Where ap.Status = 2 And ap.Id = AccountPayableId And aps.Id = AccountPayableShareId
                     Select New With {.SupplierId = s.Id, .ThirdId = s.IdThirdParty, .SupplierName = s.Name, .SupplierCode = s.Code, .SupplierNit = tp.Nit,
                                                                .DescriptionLine = dl.Description, .DistributionLineId = dl.Id, .ExpenseConceptIdDistributionLine = dl.ExpensesConceptId,
                                                                .MainAccountIdDistributionLine = dl.IdMainAccount, .NatureExpenseConcept = ec.Nature, .Invoice = ap.BillNumber, .ExpirationDate = ap.ExpirationDate,
                                                                .InvoiceBalance = ap.Balance, .Share = aps.Share, .ShareExpirationDate = aps.DateExpires, .BalanceShare = aps.Balance, .AccountPayableId = ap.Id,
                                                                .AccountPayableShareId = aps.Id, .CXPValue = ap.Value, .CurrencyId = ap.CurrencyId, .Abrreviation = c.Abbreviation, .AccountPayableCode = ap.Code}).FirstOrDefault()

        If query IsNot Nothing Then
            Dim days As Integer = (Date.Now.Day - query.ShareExpirationDate.Day) + 1
            Dim age As String = (From ap In _context.AccountPayable
                                 Join sp In _context.SettingPayments On sp.IdOperatingUnit Equals ap.IdOperatingUnit
                                 Join agp In _context.AgesPayments On agp.SettingPaymentId Equals sp.Id
                                 Where agp.InitialRange <= days AndAlso agp.EndRange >= days AndAlso ap.Id = query.AccountPayableId
                                 Select agp.Name).FirstOrDefault()

            Dim _sp_SchedulePayment As New SP_SchedulePayment_Result()
            With _sp_SchedulePayment
                .SupplierId = query.SupplierId
                .ThirdId = query.ThirdId
                .SupplierName = query.SupplierName
                .SupplierCode = query.SupplierCode
                .SupplierNit = query.SupplierNit
                .DescriptionLine = query.DescriptionLine
                .DistributionLineId = query.DistributionLineId
                .ExpenseConceptIdDistributionLine = query.ExpenseConceptIdDistributionLine
                .MainAccountIdDistributionLine = query.MainAccountIdDistributionLine
                .NatureExpenseConcept = query.NatureExpenseConcept
                .Invoice = query.Invoice
                .ExpirationDate = query.ExpirationDate
                .InvoiceBalance = query.InvoiceBalance
                .Share = query.Share
                .ShareExpirationDate = query.ShareExpirationDate
                .BalanceShare = query.BalanceShare
                .AccountPayableId = query.AccountPayableId
                .AccountPayableShareId = query.AccountPayableShareId
                .AgePayment = age
                .CXPValue = query.CXPValue
                .CurrencyId = query.CurrencyId
                .CurrencyAbbreviation = query.Abrreviation
                .AccountPayableCode = query.AccountPayableCode
            End With
            Return _sp_SchedulePayment
        Else
            Return Nothing
        End If

    End Function


    Public Function GenerateSchedulePaymentSP(xml As String, UserCode As String) As ObjectResult(Of SP_SaveSchedulePayment_Result) Implements ISchedulePaymentRepository.GenerateSchedulePaymentSP
        Return _context.SP_SaveSchedulePayment(xml, UserCode)
    End Function
End Class