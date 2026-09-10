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

Public Class BudgetRepository
    Inherits GenericRepository(Of Budget)
    Implements IBudgetRepository


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
    ''' Obtiene un presupuesto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetById(Id As Integer) As Budget Implements IBudgetRepository.GetBudgetById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Budget Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As Budget In Me._context.Budget.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault

            Return res
        Else
            Return New Budget
        End If
    End Function

    ''' <summary>
    ''' obtiene un presupuesto por id del rubro y el tipo de ingreso
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    Public Function GetBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Budget Implements IBudgetRepository.GetBudgetByCategoryIdAndRevenueTypeId
        Dim res = (From b In _context.Budget.Include("Category") Where b.CategoryId = categoryId And b.RevenueTypeId = revenueTypeId Select b).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New Budget
    End Function

    ''' <summary>
    ''' Obtiene el saldo de un presupuesto por id del rubro y del tipo
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Decimal Implements IBudgetRepository.GetBalanceBudgetByCategoryIdAndRevenueTypeId
        Dim res = (From b In _context.Budget Where b.CategoryId = categoryId And b.RevenueTypeId = revenueTypeId Select b.Balance).FirstOrDefault()
        If res <> 0 Then
            Return res
        End If
        Return New Decimal
    End Function

    ''' <summary>
    ''' Obtiene un presupuesto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetByIdAsNotTracking(id As Integer) As Budget Implements IBudgetRepository.GetBudgetByIdAsNotTracking
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Return (From d In Me._context.Budget.AsNoTracking() Where d.Id = id Select d).FirstOrDefault
    End Function

    Public Function GetThirdPartyByNit(nit As String) As String Implements IBudgetRepository.GetThirdPartyByNit

        Dim thirdParty = (From d In Me._context.ThirdParty.AsNoTracking() Where d.Nit = nit Select d.CodeDivipola).FirstOrDefault
        If thirdParty IsNot Nothing OrElse thirdParty <> "" Then
            Return thirdParty
        End If
        Return ""
    End Function
#End Region

End Class
