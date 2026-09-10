'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-10-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class InvoicePortfolioAdvanceRepository
    Inherits GenericRepository(Of InvoicePortfolioAdvance)
    Implements IInvoicePortfolioAdvanceRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Sub AddRageEntity(listInvoicePortfolioAdvance As List(Of InvoicePortfolioAdvance)) Implements IInvoicePortfolioAdvanceRepository.AddRageEntity
        _context.InvoicePortfolioAdvance.AddRange(listInvoicePortfolioAdvance)
    End Sub

End Class