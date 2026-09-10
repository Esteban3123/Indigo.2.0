'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class VoucherTransactionDetailRepository
    Inherits GenericRepository(Of VoucherTransactionDetails)
    Implements IVoucherTransactionDetailRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lists the voucher transaction detail by identifier voucher transaction.
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction As Integer) As List(Of VoucherTransactionDetails) Implements IVoucherTransactionDetailRepository.ListVoucherTransactionDetailByIdVoucherTransaction
        Dim query = (From vd As VoucherTransactionDetails In _context.VoucherTransactionDetails Where vd.IdVoucherTransaction = IdVoucherTransaction Select vd).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the voucher transaction detail by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionDetailById(Id As Integer) As VoucherTransactionDetails Implements IVoucherTransactionDetailRepository.GetVoucherTransactionDetailById
        Dim query = (From vd As VoucherTransactionDetails In _context.VoucherTransactionDetails Where vd.Id = Id Select vd).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New VoucherTransactionDetails()
        End If
    End Function

    ''' <summary>
    ''' Gets all the voucher transaction details associated with a supplier bank account id
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionDetailsBySupplierBankAccountId(supplierBankAccountId As Integer) As List(Of VoucherTransactionDetails) Implements IVoucherTransactionDetailRepository.GetVoucherTransactionDetailsBySupplierBankAccountId
        Dim query = (From vtd As VoucherTransactionDetails In _context.VoucherTransactionDetails Where vtd.SupplierBankAccountId = supplierBankAccountId Select vtd).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return New List(Of VoucherTransactionDetails)
        End If
    End Function

End Class