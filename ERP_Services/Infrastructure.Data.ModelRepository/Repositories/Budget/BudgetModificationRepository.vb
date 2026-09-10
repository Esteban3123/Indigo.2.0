'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class BudgetModificationRepository
    Inherits GenericRepository(Of BudgetModification)
    Implements IBudgetModificationRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer) As BudgetModification Implements IBudgetModificationRepository.GetBudgetModification
        Dim result = (From e In _context.BudgetModification.Include("BudgetModificationDetail")
                      Where e.Code = code And e.DocumentSource = type And e.BudgetaryValidityId = budgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            If result.BudgetModificationDetail IsNot Nothing AndAlso result.BudgetModificationDetail.Count > 0 Then
                For Each item In result.BudgetModificationDetail
                    Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.Id = item.BudgetId Select bg).FirstOrDefault()
                    item.BalanceBudget = budget.Balance
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeCategory = category.Code
                    item.NameCategory = category.Name
                    item.CategoryId = category.Id

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                Next

            End If
            result.OriginalValue = (From bm In _context.BudgetModification.AsNoTracking() Where bm.Code = code And bm.DocumentSource = type Select bm).FirstOrDefault
            Return result
        Else
            Return New BudgetModification
        End If
    End Function

    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModificationById(id As Integer) As BudgetModification Implements IBudgetModificationRepository.GetBudgetModificationById
        Dim result = (From bm In _context.BudgetModification.Include("BudgetModificationDetail").Include("BudgetModificationDetail.Budget") Where bm.Id = id Select bm).FirstOrDefault
        If result IsNot Nothing Then
            If result.BudgetModificationDetail IsNot Nothing AndAlso result.BudgetModificationDetail.Count > 0 Then
                For Each item In result.BudgetModificationDetail
                    Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.Id = item.BudgetId Select bg).FirstOrDefault()
                    item.BalanceBudget = budget.Balance
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeCategory = category.Code
                    item.NameCategory = category.Name
                    item.CategoryId = category.Id

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                Next
            End If
            result.OriginalValue = (From bm In _context.BudgetModification.AsNoTracking Where bm.Id = id Select bm).FirstOrDefault
            Return result
        Else
            Return New BudgetModification
        End If
    End Function

    Public Function SP_SaveBudgetModification(BudgetModificationXml As String, BudgetModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveBudgetModification_Result Implements IBudgetModificationRepository.SP_SaveBudgetModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveBudgetModification(BudgetModificationXml, BudgetModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region


End Class
