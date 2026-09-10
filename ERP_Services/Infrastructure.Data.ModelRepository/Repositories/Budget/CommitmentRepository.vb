'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 02-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class CommitmentRepository
    Inherits GenericRepository(Of Commitment)
    Implements ICommitmentRepository

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
    ''' obtiene un compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentByCode(code As String, BudgetaryValidityId As Integer) As Commitment Implements ICommitmentRepository.GetCommitmentByCode
        Dim result = (From e In _context.Commitment.Include("CommitmentDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault
        '.Include("CommitmentDetail.AvailabilityDetail").Include("CommitmentDetail.AvailabilityDetail.Budget").Include("CommitmentDetail.AvailabilityDetail.Availability")
        If result IsNot Nothing Then
            Dim thirdParty = (From tp In _context.ThirdParty Where tp.Id = result.ThirdPartyId Select tp).FirstOrDefault()
            result.NameThirdParty = thirdParty.Nit + " - " + thirdParty.Name

            If result.CommitmentDetail IsNot Nothing AndAlso result.CommitmentDetail.Count > 0 Then
                For Each item In result.CommitmentDetail
                    Dim availabilityDetail = (From ad In _context.AvailabilityDetail.AsNoTracking() Where ad.Id = item.AvailabilityDetailId).FirstOrDefault()
                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = availabilityDetail.BudgetId).FirstOrDefault()
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceBudget = budget.Balance
                    item.BalanceAffects = availabilityDetail.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    'Dim availability = (From av In _context.Availability.AsNoTracking() Join avd In _context.AvailabilityDetail.AsNoTracking() On av.Id Equals avd.AvailabilityId Where avd.Id = item.AvailabilityDetailId Select av).FirstOrDefault

                    Dim availability = (From a In _context.Availability.AsNoTracking() Where a.Id = availabilityDetail.AvailabilityId).FirstOrDefault()
                    item.CodeAvailability = availability.Code
                    item.DateAvailability = availability.DocumentDate
                    item.DateExpirationAvailability = availability.ExpirationDate

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RevenueTypeId = revueneType.Id

                Next
            End If
            result.OriginalValue = (From e In _context.Commitment.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New Commitment
        End If
    End Function

    ''' <summary>
    ''' Obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentById(id As Integer) As Commitment Implements ICommitmentRepository.GetCommitmentById
        Dim result = (From rm In _context.Commitment.AsNoTracking.Include("CommitmentDetail").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If result IsNot Nothing Then
            Dim thirdParty = (From tp In _context.ThirdParty Where tp.Id = result.ThirdPartyId Select tp).FirstOrDefault()
            result.NameThirdParty = thirdParty.Nit + " - " + thirdParty.Name

            If result.CommitmentDetail IsNot Nothing AndAlso result.CommitmentDetail.Count > 0 Then
                For Each item In result.CommitmentDetail
                    Dim availabilityDetail = (From ad In _context.AvailabilityDetail.AsNoTracking() Where ad.Id = item.AvailabilityDetailId).FirstOrDefault()
                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = availabilityDetail.BudgetId).FirstOrDefault()
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceBudget = budget.Balance
                    item.BalanceAffects = availabilityDetail.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim availability = (From a In _context.Availability.AsNoTracking() Where a.Id = availabilityDetail.AvailabilityId).FirstOrDefault()
                    item.CodeAvailability = availability.Code
                    item.DateAvailability = availability.DocumentDate
                    item.DateExpirationAvailability = availability.ExpirationDate

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name
                    item.RevenueTypeId = revueneType.Id

                Next
            End If
            result.OriginalValue = (From e In _context.Commitment.AsNoTracking() Where e.Id = id Select e).FirstOrDefault()
            Return result
        Else
            Return New Commitment
        End If
    End Function

    Public Function SP_SaveCommitment(CommitmentXml As String, CommitmentDetailForDeleteXml As String, CodeUser As String) As SP_SaveCommitment_Result Implements ICommitmentRepository.SP_SaveCommitment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCommitment(CommitmentXml, CommitmentDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
