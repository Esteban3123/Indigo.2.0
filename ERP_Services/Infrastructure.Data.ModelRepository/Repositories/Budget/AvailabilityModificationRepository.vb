'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 01-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class AvailabilityModificationRepository
    Inherits GenericRepository(Of AvailabilityModification)
    Implements IAvailabilityModificationRepository

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
    ''' obtiene una modificacion de disponibilidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer) As AvailabilityModification Implements IAvailabilityModificationRepository.GetAvailabilityModificationByCode
        Dim result = (From e In _context.AvailabilityModification.Include("AvailabilityModificationDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim availability = (From rc In _context.Availability.AsNoTracking() Where rc.Id = result.AvailabilityId Select rc).FirstOrDefault()
            result.CodeAvailability = availability.Code

            If result.AvailabilityModificationDetail IsNot Nothing AndAlso result.AvailabilityModificationDetail.Count > 0 Then
                For Each item In result.AvailabilityModificationDetail
                    Dim availabilityDetail = (From ad In _context.AvailabilityDetail.AsNoTracking() Where ad.Id = item.AvailabilityDetailId Select ad).FirstOrDefault()
                    item.BalanceAvailability = availabilityDetail.Balance

                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = availabilityDetail.BudgetId Select b).FirstOrDefault()
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault()
                    item.CategoryId = category.Id
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.CategoryCPCCodeId = category.CPCCodeId

                    If category.CCPETCodeId.GetValueOrDefault() > 0 Then
                        Dim _ccpet = (From ccpet In Me._context.CCPET.AsNoTracking Where ccpet.Id = category.CCPETCodeId Select ccpet).FirstOrDefault
                        If _ccpet IsNot Nothing Then
                            item.CCPETLinkAccount = _ccpet.LinkAccount
                        End If
                    End If

                    Dim cpc = (From c In _context.CPCCatalog.AsNoTracking() Where c.Id = availabilityDetail.CPCCodeId Select c).FirstOrDefault()
                    If cpc IsNot Nothing Then
                        item.CPCCodeId = cpc.Id
                        item.CPCCode = cpc.Code + " - " + cpc.Name
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault()
                    item.RevenueTypeId = revueneType.Id
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault()
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    item.BalanceBudget = budget.Balance
                Next
            End If
            result.OriginalValue = (From e In _context.AvailabilityModification.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New AvailabilityModification
        End If
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityModificationById(id As Integer) As AvailabilityModification Implements IAvailabilityModificationRepository.GetAvailabilityModificationById
        Dim result = (From rm In _context.AvailabilityModification.AsNoTracking.Include("AvailabilityModificationDetail").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If result IsNot Nothing Then
            Dim availability = (From rc In _context.Availability.AsNoTracking() Where rc.Id = result.AvailabilityId Select rc).FirstOrDefault()
            result.CodeAvailability = availability.Code

            If result.AvailabilityModificationDetail IsNot Nothing AndAlso result.AvailabilityModificationDetail.Count > 0 Then
                For Each item In result.AvailabilityModificationDetail
                    Dim availabilityDetail = (From ad In _context.AvailabilityDetail.AsNoTracking() Where ad.Id = item.AvailabilityDetailId Select ad).FirstOrDefault()
                    item.BalanceAvailability = availabilityDetail.Balance

                    Dim budget = (From b In _context.Budget.AsNoTracking() Where b.Id = availabilityDetail.BudgetId Select b).FirstOrDefault()
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault()
                    item.CategoryId = category.Id
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    If category.CCPETCodeId.GetValueOrDefault() > 0 Then
                        Dim _ccpet = (From ccpet In Me._context.CCPET.AsNoTracking Where ccpet.Id = category.CCPETCodeId Select ccpet).FirstOrDefault
                        If _ccpet IsNot Nothing Then
                            item.CCPETLinkAccount = _ccpet.LinkAccount
                        End If
                    End If

                    Dim cpc = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CPCCodeId Select c).FirstOrDefault()
                    item.CPCCodeId = cpc.Id
                    item.CPCCode = cpc.Code + " - " + cpc.Name

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault()
                    item.RevenueTypeId = revueneType.Id
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault()
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    item.BalanceBudget = budget.Balance
                Next
            End If
            Return result
        Else
            Return New AvailabilityModification
        End If
    End Function

    Public Function SP_SaveAvailabilityModification(AvailabilityModificationXml As String, AvailabilityModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveAvailabilityModification_Result Implements IAvailabilityModificationRepository.SP_SaveAvailabilityModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAvailabilityModification(AvailabilityModificationXml, AvailabilityModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
