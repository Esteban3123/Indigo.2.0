'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 18-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class SuspensionRepository
    Inherits GenericRepository(Of Suspension)
    Implements ISuspensionRepository

#Region "Properties"

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una supension de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionByCode(code As String) As Suspension Implements ISuspensionRepository.GetSuspensionByCode
        Dim result = (From e In _context.Suspension.Include("SuspensionDetail")
                      Where e.Code = code Select e).FirstOrDefault
        If result IsNot Nothing Then
            If result.SuspensionDetail IsNot Nothing AndAlso result.SuspensionDetail.Count > 0 Then
                For Each item In result.SuspensionDetail
                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = item.BudgetId Select b).FirstOrDefault
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceBudget = budget.Balance
                    item.CategoryId = category.Id

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RevenueTypeId = revueneType.Id

                Next
            End If
            result.OriginalValue = (From e In _context.Suspension.AsNoTracking() Where e.Code = code Select e).FirstOrDefault()
            Return result
        Else
            Return New Suspension
        End If
    End Function

    ''' <summary>
    ''' obtiene una supension de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionById(id As Integer) As Suspension Implements ISuspensionRepository.GetSuspensionById
        Dim resul = (From rm In _context.Suspension.AsNoTracking.Include("SuspensionDetail").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If resul IsNot Nothing Then
            Return resul
        Else
            Return New Suspension
        End If
    End Function

#End Region

End Class
