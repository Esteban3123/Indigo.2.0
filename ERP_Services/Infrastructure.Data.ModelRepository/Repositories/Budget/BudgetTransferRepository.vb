'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetTransferRepository
    Inherits GenericRepository(Of BudgetTransfer)
    Implements IBudgetTransferRepository
    'Coexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <param name="yearValidity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer) As BudgetTransfer Implements IBudgetTransferRepository.GetBudgetTransfer
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As BudgetTransfer In Me._context.BudgetTransfer.Include("BudgetTransferDetail") Where d.Code.Equals(Code.Trim()) And d.DocumentSource = ItemType And d.DocumentDate.Year = yearValidity Select d).FirstOrDefault
        If res IsNot Nothing Then
            If res.BudgetTransferDetail IsNot Nothing AndAlso res.BudgetTransferDetail.Count > 0 Then
                res.BudgetTransferDetail.ToList.ForEach(Sub(item)
                                                            Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.Id = item.BudgetId Select bg).FirstOrDefault()
                                                            'Budget
                                                            item.CategoryId = budget.CategoryId
                                                            item.Balance = budget.Balance
                                                            'Category
                                                            Dim category = (From c In _context.Category.AsNoTracking Where c.Id = item.CategoryId Select c).FirstOrDefault
                                                            item.CodeCategory = category.Code
                                                            item.NameCategory = category.Name
                                                            'FinancialSource
                                                            Dim financialSource = (From f In _context.FinancialSource.AsNoTracking Where f.Id = category.FinancialSourceId Select f).FirstOrDefault
                                                            item.FinancialSourceId = financialSource.Id
                                                            item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                                                            'RevenueType
                                                            Dim revenueType = (From r In _context.RevenueType.AsNoTracking Where r.Id = item.RevenueTypeId Select r).FirstOrDefault
                                                            item.CodeNameRevenueType = revenueType.Code + " - " + revenueType.Name
                                                        End Sub)
            End If
            res.OriginalValue = (From d As BudgetTransfer In Me._context.BudgetTransfer.AsNoTracking() Where d.Code.Equals(Code.Trim()) And d.DocumentSource = ItemType And d.DocumentDate.Year = yearValidity Select d).SingleOrDefault
            Return res
        Else
            Return New BudgetTransfer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransferById(Id As Integer) As BudgetTransfer Implements IBudgetTransferRepository.GetBudgetTransferById
        Dim res = (From d In Me._context.BudgetTransfer.Include("BudgetTransferDetail").Include("BudgetTransferDetail.Budget") Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            If res.BudgetTransferDetail IsNot Nothing AndAlso res.BudgetTransferDetail.Count > 0 Then
                res.BudgetTransferDetail.ToList.ForEach(Sub(item)
                                                            Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.Id = item.BudgetId Select bg).FirstOrDefault()
                                                            'Budget
                                                            item.CategoryId = budget.CategoryId
                                                            item.Balance = budget.Balance
                                                            'Category
                                                            Dim category = (From c In _context.Category.AsNoTracking Where c.Id = item.CategoryId Select c).FirstOrDefault
                                                            item.CodeCategory = category.Code
                                                            item.NameCategory = category.Name
                                                            'FinancialSource
                                                            Dim financialSource = (From f In _context.FinancialSource.AsNoTracking Where f.Id = category.FinancialSourceId Select f).FirstOrDefault
                                                            item.FinancialSourceId = financialSource.Id
                                                            item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                                                            'RevenueType
                                                            Dim revenueType = (From r In _context.RevenueType.AsNoTracking Where r.Id = item.RevenueTypeId Select r).FirstOrDefault
                                                            item.CodeNameRevenueType = revenueType.Code + " - " + revenueType.Name
                                                        End Sub)
            End If
            res.OriginalValue = (From d As BudgetTransfer In Me._context.BudgetTransfer.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault
            Return res
        Else
            Return New BudgetTransfer
        End If
    End Function

#End Region

End Class
