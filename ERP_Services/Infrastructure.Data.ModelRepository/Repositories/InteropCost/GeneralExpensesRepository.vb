'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class GeneralExpensesRepository
    Inherits GenericRepository(Of GeneralExpense)
    Implements IGeneralExpensesRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Function GetGeneralExpense(code As String) As GeneralExpense Implements IGeneralExpensesRepository.GetGeneralExpense
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From g In _context.GeneralExpense.Include("DistributionBase").Include("DistributionBase.DistributionBaseMeasurementUnit").Include("DistributionBase.DistributionBaseDetail") Where g.Code.Equals(code) Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.CategoryCodeName = (From e In _context.GeneralExpenseCategory.AsNoTracking() Where e.Id = query.GeneralExpenseCategoryId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
            query.OriginalValue = (From g In _context.GeneralExpense.AsNoTracking() Where g.Code.Equals(code) Select g).FirstOrDefault()

            For Each item As DistributionBase In query.DistributionBase
                Select Case item.MultipleBase
                    Case 1
                        item.DistributionBaseName = "Distribución (A)"
                    Case 2
                        item.DistributionBaseName = "Distribución (B)"
                    Case 3
                        item.DistributionBaseName = "Distribución (C)"
                    Case 4
                        item.DistributionBaseName = "Distribución (D)"
                End Select
                Select Case item.DistributionType
                    Case 1
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeDirect", "InteropCost")
                    Case 2
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeCalculated", "InteropCost")
                    Case 3
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeSearch", "InteropCost")
                End Select
                Select Case item.MeasurementUnit
                    Case 1
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitProportion", "InteropCost")
                    Case 2
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitValue", "InteropCost")
                End Select
                For Each itemMeasureUnit In item.DistributionBaseMeasurementUnit
                    itemMeasureUnit.CodeNameMeasureUnit = (From e In _context.InventoryMeasurementUnit.AsNoTracking() Where e.Id = itemMeasureUnit.MeasurementUnitId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                Next
            Next

            Return query
        Else
            Return New GeneralExpense()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Function GetGeneralExpenseById(id As Integer) As GeneralExpense Implements IGeneralExpensesRepository.GetGeneralExpenseById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From g In _context.GeneralExpense Where g.Id = id Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From g In _context.GeneralExpense.AsNoTracking() Where g.Id = id Select g).FirstOrDefault()
            Return query
        Else
            Return New GeneralExpense()
        End If
    End Function

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense Implements IGeneralExpensesRepository.GetGeneralExpenseByMainAccountId
        'If MainAccountId = 0 Then
        '    Throw New ArgumentNullException("MainAccountId")
        'End If
        'Dim query = (From ge In _context.GeneralExpense Where ge.MainAccountId = MainAccountId Select ge).FirstOrDefault()
        'If query IsNot Nothing AndAlso query.Id > 0 Then
        '    query.OriginalValue = (From ge In _context.GeneralExpense.AsNoTracking() Where ge.MainAccountId = MainAccountId Select ge).FirstOrDefault()
        '    Return query
        'Else
        '    Return New GeneralExpense()
        'End If
        Return New GeneralExpense
    End Function

    Public Function ListGeneralExpenseByStatus(status As Boolean) As List(Of GeneralExpense) Implements IGeneralExpensesRepository.ListGeneralExpenseByStatus
        Return (From gx As GeneralExpense In _context.GeneralExpense.Include("DistributionBase").AsNoTracking().Include("DistributionBase.DistributionBaseDetail").AsNoTracking().Include("DistributionBase.DistributionBaseMeasurementUnit").AsNoTracking().Include("DistributionBase.DistributionBaseMeasurementUnit.InventoryMeasurementUnit").AsNoTracking().Include("DistributionBase.DistributionBaseDetail.ProductionCenter").AsNoTracking() Select gx).ToList()
    End Function
End Class
