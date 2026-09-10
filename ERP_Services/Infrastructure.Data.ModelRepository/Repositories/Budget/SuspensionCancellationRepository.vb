'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class SuspensionCancellationRepository
    Inherits GenericRepository(Of SuspensionCancellation)
    Implements ISuspensionCancellationRepository

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
    ''' Obtiene un levantamiento de suspencion presupuestal por codigo 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionCancellationByCode(code As String) As SuspensionCancellation Implements ISuspensionCancellationRepository.GetSuspensionCancellationByCode
        Dim result = (From e In _context.SuspensionCancellation.Include("SuspensionCancellationDetail")
                      Where e.Code = code Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim suspension = (From s In _context.Suspension.AsNoTracking() Where s.Id = result.SuspensionId Select s).FirstOrDefault()
            result.CodeSuspension = suspension.Code

            If result.SuspensionCancellationDetail IsNot Nothing AndAlso result.SuspensionCancellationDetail.Count > 0 Then
                For Each item In result.SuspensionCancellationDetail
                    Dim suspensionDetail = (From sd In _context.SuspensionDetail.AsNoTracking() Where sd.Id = item.SuspensionDetailId Select sd).FirstOrDefault
                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = suspensionDetail.BudgetId Select b).FirstOrDefault

                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceAffects = suspensionDetail.Balance
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
            result.OriginalValue = (From e In _context.SuspensionCancellation.AsNoTracking() Where e.Code = code Select e).FirstOrDefault()
            Return result
        Else
            Return New SuspensionCancellation
        End If
    End Function

    ''' <summary>
    ''' Obtiene un levantamiento de suspencion presupuestal por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionCancellationById(id As Integer) As SuspensionCancellation Implements ISuspensionCancellationRepository.GetSuspensionCancellationById
        Dim resul = (From rm In _context.SuspensionCancellation.AsNoTracking.Include("SuspensionCancellationDetail").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If resul IsNot Nothing Then
            Return resul
        Else
            Return New SuspensionCancellation
        End If
    End Function

#End Region

End Class
