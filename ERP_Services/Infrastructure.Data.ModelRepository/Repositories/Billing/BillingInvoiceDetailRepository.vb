'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class BillingInvoiceDetailRepository
    Inherits GenericRepository(Of InvoiceDetail)
    Implements IBillingInvoiceDetailRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function SaveListInvoiceDetail(detailList As List(Of InvoiceDetail)) As Boolean Implements IBillingInvoiceDetailRepository.SaveListInvoiceDetail
        Try
            _context.InvoiceDetail.AddRange(detailList)
            _context.Commit()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class