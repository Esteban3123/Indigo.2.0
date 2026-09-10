'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Anthony Ocampo
' Created          : 2026-05-06
' Description      : Implementación repositorio InitialBalanceInvoice.
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class InitialBalanceInvoiceRepository
    Inherits GenericRepository(Of InitialBalanceInvoice)
    Implements IInitialBalanceInvoiceRepository

#Region "Fields"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    Public Function GetByAccountReceivableId(accountReceivableId As Integer) As InitialBalanceInvoice Implements IInitialBalanceInvoiceRepository.GetByAccountReceivableId
        Return (From ibi In _context.InitialBalanceInvoice.AsNoTracking()
                Where ibi.AccountReceivableId = accountReceivableId
                Select ibi).FirstOrDefault()
    End Function

    Public Function GetByInvoiceNumber(invoiceNumber As String) As InitialBalanceInvoice Implements IInitialBalanceInvoiceRepository.GetByInvoiceNumber
        Return (From ibi In _context.InitialBalanceInvoice
                Where ibi.InvoiceNumber = invoiceNumber
                Select ibi).FirstOrDefault()
    End Function

    Public Function GetByInvoiceId(invoiceId As Integer) As InitialBalanceInvoice Implements IInitialBalanceInvoiceRepository.GetByInvoiceId
        If invoiceId <= 0 Then Return Nothing
        Return (From ibi In _context.InitialBalanceInvoice.AsNoTracking()
                Where ibi.InvoiceId = invoiceId
                Select ibi).FirstOrDefault()
    End Function

    Public Sub DeleteDetailsByInitialBalanceInvoiceId(initialBalanceInvoiceId As Integer) Implements IInitialBalanceInvoiceRepository.DeleteDetailsByInitialBalanceInvoiceId
        Dim toDelete = (From d In _context.InitialBalanceInvoiceDetail
                        Where d.InitialBalanceInvoiceId = initialBalanceInvoiceId
                        Select d).ToList()
        If toDelete.Count = 0 Then Return
        Dim dbSet As System.Data.Entity.DbSet(Of InitialBalanceInvoiceDetail) = _context.GetObjectSet(Of InitialBalanceInvoiceDetail)()
        For Each d In toDelete
            dbSet.Remove(d)
        Next
        UnitWork.Commit()
    End Sub

    Public Sub SaveDetail(detail As InitialBalanceInvoiceDetail) Implements IInitialBalanceInvoiceRepository.SaveDetail
        _context.SaveChangesEntity(detail)
    End Sub

End Class
