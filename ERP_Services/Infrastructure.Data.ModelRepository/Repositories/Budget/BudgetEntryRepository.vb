'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Oscar Ivan Sierra Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class BudgetEntryRepository
    Inherits GenericRepository(Of Budget)
    Implements IBudgetEntryRepository

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
    ''' obtiene una categoria teniendo en cuenta el id de la vigencia.
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetCategory(ValidityId As Integer, RevenueType As String) As List(Of BudgetEntry) Implements IBudgetEntryRepository.GetBudgetCategory
        Dim Auxiliar As Boolean = True
        Dim BudgetEntry = From Category In _context.Category.Include("FinancialSource") Join
                          Validity In _context.BudgetaryValidity On Category.BudgetaryValidityId Equals Validity.Id
        Where Category.Auxiliary = Auxiliar And Category.BudgetaryValidityId = ValidityId
                                  Select New BudgetEntry With
                                         {
                                             .Id = Category.Id,
                                             .CodeBudget = Category.Code,
                                             .NameBudget = Category.Name,
                                             .Resource = Category.FinancialSource.Name,
                                             .ResolutionValue = 0
                                                                                        }
        Dim ListRevenueTypeClone As List(Of BudgetEntryDetail)
        Dim ListBugdetEntry As List(Of BudgetEntry) = BudgetEntry.ToList
        Dim ListRevenueType As List(Of BudgetEntryDetail) = (From e In _context.RevenueType Where e.Type = RevenueType Select New BudgetEntryDetail With {.Code = e.Code, .Name = e.Name, .Value = 0, .RevenueTypeId = e.Id}).ToList
        For i As Integer = 0 To ListBugdetEntry.Count - 1
            ListRevenueTypeClone = New List(Of BudgetEntryDetail)
            ListRevenueTypeClone = ListRevenueType
            For j As Integer = 0 To ListRevenueType.Count - 1

                Dim RevenueTypeId As Integer = ListRevenueTypeClone.Item(j).RevenueTypeId
                Dim CategoryId As Integer = ListBugdetEntry.Item(i).Id
                Dim Budget = (From e In _context.Budget Where e.RevenueTypeId = RevenueTypeId And e.CategoryId = CategoryId)
                If Budget.Count > 0 Then
                    If Object.Equals(Budget, Nothing) = False Then
                        ListRevenueTypeClone.Item(j).Value = Budget.Single.InitialValue
                        ListRevenueTypeClone.Item(j).Budget = Budget.Single
                        ListBugdetEntry.Item(i).ResolutionValue = ListBugdetEntry.Item(i).ResolutionValue + Budget.Single.InitialValue
                    End If
                Else
                    ListRevenueTypeClone.Item(j).Value = 0
                    ListRevenueTypeClone.Item(j).Budget = Nothing
                End If
                ListRevenueTypeClone.Item(j).CategoryId = ListBugdetEntry.Item(i).Id
            Next
            ListBugdetEntry.Item(i).ListBudgetEntry = New List(Of BudgetEntryDetail)
            For k As Integer = 0 To ListRevenueTypeClone.Count - 1
                ListBugdetEntry.Item(i).ListBudgetEntry.Add(New BudgetEntryDetail With {.Budget = ListRevenueTypeClone.Item(k).Budget, .CategoryId = ListRevenueTypeClone.Item(k).CategoryId, .Code = ListRevenueTypeClone.Item(k).Code, .Name = ListRevenueTypeClone.Item(k).Name, .RevenueTypeId = ListRevenueTypeClone.Item(k).RevenueTypeId, .Value = ListRevenueTypeClone.Item(k).Value})
            Next
        Next




        Return ListBugdetEntry
    End Function

    ''' <summary>
    ''' obtenga la viegncia para el presupuesto inicial
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetValidity(ValidityId As Integer) As BudgetaryValidity Implements IBudgetEntryRepository.GetBudgetValidity
        Dim Validity = (From Category In _context.BudgetaryValidity Where Category.Id = ValidityId).Single
        Return Validity
    End Function

    ''' <summary>
    ''' Obtener los rubros del presupuesto inicial 
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="itemType">Tipo de rubros, 1 = ingresos, 2 = gastos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudget(ValidityId As Integer, itemType As Integer) As List(Of Budget) Implements IBudgetEntryRepository.GetBudgetBudget
        Dim listBudget = (From e In _context.Budget.Include("Category").Include("Category.FinancialSource").Include("RevenueType")
                         Where e.Category.ItemType = itemType And e.Category.BudgetaryValidityId = ValidityId).ToList()

        If listBudget.Count > 0 Then
            Return listBudget.ToList
        Else
            Return New List(Of Budget)
        End If
    End Function

    ''' <summary>
    ''' Obtener los rubros del presupuesto inicial 
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="itemType">Tipo de rubros, 1 = ingresos, 2 = gastos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, FlagInitialValue As Boolean) As List(Of Budget) Implements IBudgetEntryRepository.GetBudgetBudgetInitialValueZero
        Dim listBudget As New List(Of Domain.Entities.Budget)
        If FlagInitialValue Then
            listBudget = (From e In _context.Budget.Include("Category").Include("Category.FinancialSource").Include("RevenueType")
                         Where e.Category.ItemType = itemType And e.Category.BudgetaryValidityId = ValidityId).ToList()
        Else
            listBudget = (From e In _context.Budget.Include("Category").Include("Category.FinancialSource").Include("RevenueType")
                         Where e.Category.ItemType = itemType And e.Category.BudgetaryValidityId = ValidityId And e.Balance > 0).ToList()
        End If


        If listBudget.Count > 0 Then
            Return listBudget.ToList
        Else
            Return New List(Of Budget)
        End If
    End Function

    Public Function SaveBudgetValidity(ValidityId As BudgetaryValidity) As Boolean Implements IBudgetEntryRepository.SaveBudgetValidity
        _context.SaveChangesEntity(ValidityId)
        Return True
    End Function

    ''' <summary>
    ''' Obtiene la cebecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId">Id de la vigencia</param>
    ''' <param name="type">tipo: Ingreso o Gasto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer) As BudgetHeader Implements IBudgetEntryRepository.GetBudgetHeader
        If budgetaryValidityId = 0 Then
            Throw New ArgumentNullException("budgetaryValidityId")
        End If
        Dim res = (From d In Me._context.BudgetHeader.Include("Budget") Where d.BudgetaryValidityId = budgetaryValidityId And d.Type = type Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As BudgetHeader In Me._context.BudgetHeader.AsNoTracking() Where d.BudgetaryValidityId = budgetaryValidityId Select d).FirstOrDefault
            Return res
        Else
            Return New BudgetHeader()
        End If
    End Function



#End Region

End Class
