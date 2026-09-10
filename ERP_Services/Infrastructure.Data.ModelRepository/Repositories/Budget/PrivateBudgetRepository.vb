'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports System.Linq.Expressions
Imports System.Data.Entity.Infrastructure

Public Class PrivateBudgetRepository

    Inherits GenericRepository(Of PrivateBudget)
    Implements IPrivateBudgetRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetListPrivateBudget(Optional tracking As Boolean = True) As List(Of SP_ListPrivateBudget_Result) Implements IPrivateBudgetRepository.GetListPrivateBudget
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ListPrivateBudget().ToList()
    End Function

    Public Function GetListPrivateBudgetById(Id As Integer, Optional tracking As Boolean = True) As PrivateBudget Implements IPrivateBudgetRepository.GetListPrivateBudgetById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PrivateBudget Where d.Id = Id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As PrivateBudget In Me._context.PrivateBudget.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault()
            Return res
        Else
            Return New PrivateBudget()
        End If
    End Function

    Public Function GetListPrivateBudgetById(PrivateBudgetItemsStructureId As Integer, MainAccountId As Integer?, ThirdPartyId As Integer?, CostCenterId As Integer?, Month As Integer, Optional tracking As Boolean = True) As PrivateBudget Implements IPrivateBudgetRepository.GetListPrivateBudgetByData
        Dim res = (From d In Me._context.PrivateBudget Where d.PrivateBudgetItemsStructureId = PrivateBudgetItemsStructureId AndAlso d.MainAccountId = MainAccountId AndAlso d.ThirdPartyId = ThirdPartyId AndAlso d.CostCenterId = CostCenterId AndAlso d.Month = Month Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As PrivateBudget In Me._context.PrivateBudget.AsNoTracking() Where d.Id = res.Id Select d).FirstOrDefault()
            Return res
        Else
            Return New PrivateBudget()
        End If
    End Function
End Class
