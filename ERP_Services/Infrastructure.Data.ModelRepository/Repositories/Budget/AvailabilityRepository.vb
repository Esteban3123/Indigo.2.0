'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class AvailabilityRepository
    Inherits GenericRepository(Of Availability)
    Implements IAvailabilityRepository

    'Contexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una disponibilidad por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, Optional FlagTracking As Boolean = False) As Availability Implements IAvailabilityRepository.GetAvailability
        If FlagTracking = True Then
            Return (From d As Availability In Me._context.Availability.AsNoTracking().Include("AvailabilityDetail").AsNoTracking()
                    Where d.Code.Equals(Code.Trim()) AndAlso d.BudgetaryValidityId = BudgetaryValidityId Select d).FirstOrDefault
        Else
            Dim query = (From d As Availability In Me._context.Availability.Include("AvailabilityDetail")
                         Where d.Code.Equals(Code.Trim()) AndAlso d.BudgetaryValidityId = BudgetaryValidityId Select d).FirstOrDefault

            If query IsNot Nothing Then

                query.OriginalValue = (From d As Availability In Me._context.Availability.AsNoTracking Where d.Code.Equals(Code.Trim()) AndAlso d.BudgetaryValidityId = BudgetaryValidityId Select d).FirstOrDefault

                Dim dp = (From dy In Me._context.Dependency.AsNoTracking() Where query.DependencyId = dy.Id Select dy).FirstOrDefault()
                query.NameDependency = dp.Code + " - " + dp.Name

                If query.AvailabilityDetail IsNot Nothing AndAlso query.AvailabilityDetail.Count > 0 Then
                    For Each item In query.AvailabilityDetail

                        Dim budget = (From b In Me._context.Budget.AsNoTracking Where b.Id = item.BudgetId Select b).FirstOrDefault()
                        item.ValueBalance = budget.Balance

                        Dim ct = (From c In Me._context.Category.AsNoTracking Where c.Id = budget.CategoryId Select c).FirstOrDefault()
                        item.CategoryId = ct.Id
                        item.CodeCategory = ct.Code
                        item.NameCategory = ct.Name
                        item.CategoryCPCCodeId = ct.CPCCodeId

                        If query.Status = 1 AndAlso item.CPCCodeId.GetValueOrDefault = 0 AndAlso ct.CPCCodeId.GetValueOrDefault > 0 Then
                            item.CPCCodeId = ct.CPCCodeId.GetValueOrDefault
                        End If

                        If ct.CCPETCodeId.GetValueOrDefault() > 0 Then
                            Dim _ccpet = (From ccpet In Me._context.CCPET.AsNoTracking Where ccpet.Id = ct.CCPETCodeId Select ccpet).FirstOrDefault
                            If _ccpet IsNot Nothing Then
                                item.CCPETLinkAccount = _ccpet.LinkAccount
                            End If
                        End If

                        Dim revenueType = (From rt In Me._context.RevenueType.AsNoTracking Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault()
                        item.CodeNameRevenueType = revenueType.Code + " - " + revenueType.Name

                        Dim financialSource = (From fs In Me._context.FinancialSource.AsNoTracking Where fs.Id = ct.FinancialSourceId Select fs).FirstOrDefault()
                        item.FinancialSourceId = financialSource.Id
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    Next

                End If

                Return query
            Else
                Return New Availability()
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene una disponibilidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityById(Id As Integer, Optional FlagTracking As Boolean = False) As Availability Implements IAvailabilityRepository.GetAvailabilityById
        If FlagTracking Then
            Return (From d In Me._context.Availability.Include("AvailabilityDetail") Where d.Id = Id Select d).FirstOrDefault
        Else
            Dim query = (From d In Me._context.Availability.AsNoTracking().Include("AvailabilityDetail").AsNoTracking() Where d.Id = Id Select d).FirstOrDefault
            If query IsNot Nothing Then
                Dim dp = (From dy In Me._context.Dependency.AsNoTracking() Where query.DependencyId = dy.Id Select dy).FirstOrDefault()
                query.NameDependency = dp.Code + " - " + dp.Name
                If query.AvailabilityDetail IsNot Nothing AndAlso query.AvailabilityDetail.Count > 0 Then
                    For Each item In query.AvailabilityDetail

                        Dim budget = (From b In Me._context.Budget.AsNoTracking Where b.Id = item.BudgetId Select b).FirstOrDefault()
                        item.ValueBalance = budget.Balance

                        Dim ct = (From c In Me._context.Category.AsNoTracking Where c.Id = budget.CategoryId Select c).FirstOrDefault()
                        item.CategoryId = ct.Id
                        item.CodeCategory = ct.Code
                        item.NameCategory = ct.Name

                        Dim revenueType = (From rt In Me._context.RevenueType.AsNoTracking Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault()
                        item.CodeNameRevenueType = revenueType.Code + " - " + revenueType.Name

                        Dim financialSource = (From fs In Me._context.FinancialSource.AsNoTracking Where fs.Id = ct.FinancialSourceId Select fs).FirstOrDefault()
                        item.FinancialSourceId = financialSource.Id
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    Next
                End If
                query.OriginalValue = (From bm In _context.Availability.AsNoTracking Where bm.Id = Id Select bm).FirstOrDefault
                Return query
            Else
                Return New Availability
            End If
        End If
    End Function

#End Region

End Class
