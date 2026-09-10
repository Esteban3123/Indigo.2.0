#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class InvoiceEntityCapitatedDistributionDetailRepository
    Inherits GenericRepository(Of InvoiceEntityCapitatedDistributionDetail)
    Implements IInvoiceEntityCapitatedDistributionDetailRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Function SP_GetInvoiceEntityCapitatedDistributionDetails(invoiceEntityCapitatedId As Integer, invoiceEntityCapitatedDistributionId As Integer) As List(Of SP_GetInvoiceEntityCapitatedDistributionDetails_Result) Implements IInvoiceEntityCapitatedDistributionDetailRepository.SP_GetInvoiceEntityCapitatedDistributionDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetInvoiceEntityCapitatedDistributionDetails(invoiceEntityCapitatedId, invoiceEntityCapitatedDistributionId).ToList()
    End Function

End Class