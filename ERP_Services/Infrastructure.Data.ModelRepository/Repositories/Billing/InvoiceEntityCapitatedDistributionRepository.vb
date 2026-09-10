#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class InvoiceEntityCapitatedDistributionRepository
    Inherits GenericRepository(Of InvoiceEntityCapitatedDistribution)
    Implements IInvoiceEntityCapitatedDistributionRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetInvoiceEntityCapitatedDistributionById(Id As Integer) As InvoiceEntityCapitatedDistribution Implements IInvoiceEntityCapitatedDistributionRepository.GetInvoiceEntityCapitatedDistributionById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If

        Dim entity = (From i In _context.InvoiceEntityCapitatedDistribution Where i.Id = Id Select i).FirstOrDefault()
        If entity IsNot Nothing Then
            Return entity
        Else
            Return New InvoiceEntityCapitatedDistribution()
        End If
    End Function

    Public Function GetInvoiceEntityCapitatedDistributionByCode(code As String) As InvoiceEntityCapitatedDistribution Implements IInvoiceEntityCapitatedDistributionRepository.GetInvoiceEntityCapitatedDistributionByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If

        Dim entity = (From i In _context.InvoiceEntityCapitatedDistribution.AsNoTracking() Where i.Code.Equals(code) Select i).FirstOrDefault()
        If entity IsNot Nothing Then
            Dim invoiceEntityCapitated As InvoiceEntityCapitated = _context.InvoiceEntityCapitated.AsNoTracking().FirstOrDefault(Function(c) c.Id = entity.InvoiceEntityCapitatedId)
            Dim careGroup As CareGroup = _context.CareGroup.AsNoTracking().FirstOrDefault(Function(c) c.Id = invoiceEntityCapitated.CareGroupId)

            entity.InvoiceEntityCapitatedCode = invoiceEntityCapitated.Code
            entity.CareGroupId = careGroup.Id
            entity.CareGroupName = String.Concat(careGroup.Code, " - ", careGroup.Name)
            entity.InitialDate = invoiceEntityCapitated.InitialDate
            entity.EndDate = invoiceEntityCapitated.EndDate

            Return entity
        Else
            Return New InvoiceEntityCapitatedDistribution()
        End If
    End Function

    Public Function SP_SaveInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionXml As String, InvoiceEntityCapitatedDistributionDetailXml As String, codeUser As String) As List(Of SP_SaveInvoiceEntityCapitatedDistribution_Result) Implements IInvoiceEntityCapitatedDistributionRepository.SP_SaveInvoiceEntityCapitatedDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionXml, InvoiceEntityCapitatedDistributionDetailXml, codeUser).ToList()
    End Function

    Public Function SP_ConfirmInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId As Integer, codeUser As String) As List(Of SP_ConfirmInvoiceEntityCapitatedDistribution_Result) Implements IInvoiceEntityCapitatedDistributionRepository.SP_ConfirmInvoiceEntityCapitatedDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId, codeUser).ToList()
    End Function

    Public Function SP_ReverseInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId As Integer, codeUser As String) As List(Of SP_ReverseInvoiceEntityCapitatedDistribution_Result) Implements IInvoiceEntityCapitatedDistributionRepository.SP_ReverseInvoiceEntityCapitatedDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId, codeUser).ToList()
    End Function

End Class