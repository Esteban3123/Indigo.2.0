'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class RecognitionModificationRepository
    Inherits GenericRepository(Of RecognitionModification)
    Implements IRecognitionModificationRepository

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
    ''' obtiene un Modificación reconocimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer) As RecognitionModification Implements IRecognitionModificationRepository.GetRecognitionModificationByCode
        Dim result = (From e In _context.RecognitionModification.Include("RecognitionModificationDetail").Include("RecognitionModificationDetail.RecognitionDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = budgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim recognition = (From rc In _context.Recognition Where rc.Id = result.RecognitionId Select rc).FirstOrDefault()
            result.CodeRecognition = recognition.Code

            If result.RecognitionModificationDetail IsNot Nothing AndAlso result.RecognitionModificationDetail.Count > 0 Then
                For Each item In result.RecognitionModificationDetail
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = item.RecognitionDetail.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.CategoryId = item.RecognitionDetail.CategoryId And bg.RevenueTypeId = item.RecognitionDetail.RevenueTypeId Select bg).FirstOrDefault
                    item.BalanceBudget = budget.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = item.RecognitionDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RecognitionDetail.RevenueTypeId = revueneType.Id

                Next
            End If
            result.OriginalValue = (From e In _context.RecognitionModification.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = budgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New RecognitionModification
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Modificación reconocimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionModificationById(id As Integer) As RecognitionModification Implements IRecognitionModificationRepository.GetRecognitionModificationById
        Dim result = (From e In _context.RecognitionModification.Include("RecognitionModificationDetail").Include("RecognitionModificationDetail.RecognitionDetail")
                      Where e.Id = id Select e).FirstOrDefault
        If result IsNot Nothing Then
            result.OriginalValue = (From rm In _context.RecognitionModification.AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
            Return result
        Else
            Return New RecognitionModification
        End If
    End Function

    Public Function SP_SaveRecognitionModification(RecognitionModificationXml As String, RecognitionModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveRecognitionModification_Result Implements IRecognitionModificationRepository.SP_SaveRecognitionModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveRecognitionModification(RecognitionModificationXml, RecognitionModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
