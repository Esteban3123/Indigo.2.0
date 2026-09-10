'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 05-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class CommitmentModificationRepository
    Inherits GenericRepository(Of CommitmentModification)
    Implements ICommitmentModificationRepository

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
    ''' obtiene un Modificación de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer) As CommitmentModification Implements ICommitmentModificationRepository.GetCommitmentModificationByCode
        Dim result = (From e In _context.CommitmentModification.Include("CommitmentModificationDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim commitment = (From cm In _context.Commitment.AsNoTracking() Where cm.Id = result.CommitmentId Select cm).FirstOrDefault
            result.CodeCommitment = commitment.Code
            result.CommitmentType = commitment.CommitmentType

            If result.CommitmentModificationDetail IsNot Nothing AndAlso result.CommitmentModificationDetail.Count > 0 Then
                For Each item In result.CommitmentModificationDetail
                    Dim commitmentDetail = (From cd In Me._context.CommitmentDetail.AsNoTracking() Where cd.Id = item.CommitmentDetailId Select cd).FirstOrDefault()
                    item.DateExpired = commitmentDetail.ExpiredDate
                    item.BalanceCommitment = commitmentDetail.Balance

                    Dim category = (From c In _context.Category.AsNoTracking Where c.Id = commitmentDetail.CategoryId Select c).FirstOrDefault()
                    item.CategoryId = category.Id
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = commitmentDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RevenueTypeId = revueneType.Id

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    If commitment.CommitmentType = 1 Then
                        Dim availabilityDetail = (From ad In Me._context.AvailabilityDetail.AsNoTracking() Where ad.Id = commitmentDetail.AvailabilityDetailId Select ad).FirstOrDefault()
                        Dim aviability = (From av In _context.Availability.AsNoTracking Where av.Id = availabilityDetail.AvailabilityId Select av).FirstOrDefault()
                        item.AvailabilityDetailId = availabilityDetail.Id
                        item.CodeAvailability = aviability.Code
                        item.BalanceAffects = availabilityDetail.Balance
                    Else
                        Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.CategoryId = commitmentDetail.CategoryId And bg.RevenueTypeId = commitmentDetail.RevenueTypeId Select bg).FirstOrDefault
                        item.BalanceAffects = budget.Balance
                    End If
                Next
            End If
            result.OriginalValue = (From e In _context.CommitmentModification.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New CommitmentModification
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Modificación de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentModificationById(id As Integer) As CommitmentModification Implements ICommitmentModificationRepository.GetCommitmentModificationById
        Dim result = (From rm In _context.CommitmentModification.AsNoTracking.Include("CommitmentModificationDetail").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If result IsNot Nothing Then
            Dim commitment = (From cm In _context.Commitment.AsNoTracking() Where cm.Id = result.CommitmentId Select cm).FirstOrDefault
            result.CodeCommitment = commitment.Code
            result.CommitmentType = commitment.CommitmentType

            If result.CommitmentModificationDetail IsNot Nothing AndAlso result.CommitmentModificationDetail.Count > 0 Then
                For Each item In result.CommitmentModificationDetail
                    Dim commitmentDetail = (From cd In Me._context.CommitmentDetail.AsNoTracking() Where cd.Id = item.CommitmentDetailId Select cd).FirstOrDefault()
                    item.DateExpired = commitmentDetail.ExpiredDate
                    item.BalanceCommitment = commitmentDetail.Balance

                    Dim category = (From c In _context.Category.AsNoTracking Where c.Id = commitmentDetail.CategoryId Select c).FirstOrDefault()
                    item.CategoryId = category.Id
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = commitmentDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RevenueTypeId = revueneType.Id

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    If commitment.CommitmentType = 1 Then
                        Dim availabilityDetail = (From ad In Me._context.AvailabilityDetail.AsNoTracking() Where ad.Id = commitmentDetail.AvailabilityDetailId Select ad).FirstOrDefault()
                        Dim aviability = (From av In _context.Availability.AsNoTracking Where av.Id = availabilityDetail.AvailabilityId Select av).FirstOrDefault()
                        item.AvailabilityDetailId = availabilityDetail.Id
                        item.CodeAvailability = aviability.Code
                        item.BalanceAffects = availabilityDetail.Balance
                    Else
                        Dim budget = (From bg In _context.Budget.AsNoTracking() Where bg.CategoryId = commitmentDetail.CategoryId And bg.RevenueTypeId = commitmentDetail.RevenueTypeId Select bg).FirstOrDefault
                        item.BalanceAffects = budget.Balance
                    End If
                Next
            End If
            result.OriginalValue = (From rm In _context.CommitmentModification.AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
            Return result
        Else
            Return New CommitmentModification
        End If
    End Function

    Public Function SP_SaveCommitmentModification(CommitmentModificationXml As String, CommitmentModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveCommitmentModification_Result Implements ICommitmentModificationRepository.SP_SaveCommitmentModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCommitmentModification(CommitmentModificationXml, CommitmentModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
