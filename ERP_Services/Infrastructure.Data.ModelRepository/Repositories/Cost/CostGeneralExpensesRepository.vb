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
Imports System.Data.Entity.Infrastructure

Public Class CostGeneralExpensesRepository
    Inherits GenericRepository(Of CostGeneralExpense)
    Implements ICostGeneralExpensesRepository

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
    Public Function GetGeneralExpense(code As String) As CostGeneralExpense Implements ICostGeneralExpensesRepository.GetGeneralExpense
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From g In _context.CostGeneralExpense.Include("CostDistributionBase").Include("CostDistributionBase.CostDistributionBaseMeasurementUnit").Include("CostDistributionBase.CostDistributionBaseDetail").Include("JournalVoucherTypes") Where g.Code.Equals(code) Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.CategoryCodeName = (From e In _context.CostGeneralExpenseCategory.AsNoTracking() Where e.Id = query.CostGeneralExpenseCategoryId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
            query.OriginalValue = (From g In _context.CostGeneralExpense.AsNoTracking() Where g.Code.Equals(code) Select g).FirstOrDefault()
            query.DirectLaborDistributionCodeName = (From j In _context.JournalVoucherTypes.AsNoTracking() Where j.Id = query.JournalVoucherTypesId Select String.Concat(j.Code, " - ", j.Name)).FirstOrDefault()
            query.ReversalDirectLaborDistributionCodeName = (From r In _context.JournalVoucherTypes.AsNoTracking() Where r.Id = query.ReversalJournalVoucherTypesId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()

            For Each item As CostDistributionBase In query.CostDistributionBase
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
                For Each itemMeasureUnit In item.CostDistributionBaseMeasurementUnit
                    itemMeasureUnit.CodeNameMeasureUnit = (From e In _context.InventoryMeasurementUnit.AsNoTracking() Where e.Id = itemMeasureUnit.MeasurementUnitId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                Next
                If item.CostDistributionBaseDetail IsNot Nothing AndAlso item.CostDistributionBaseDetail.Count > 0 Then
                    For Each itemDistributionBaseDetail In item.CostDistributionBaseDetail
                        itemDistributionBaseDetail.CodeNameMainAccount = (From e In _context.MainAccounts.AsNoTracking() Where e.Id = itemDistributionBaseDetail.MainAccountId Select String.Concat(e.Number, " - ", e.Name)).FirstOrDefault()
                        If itemDistributionBaseDetail.CostCenterId IsNot Nothing Then
                            itemDistributionBaseDetail.CodeNameCostCenter = (From e In _context.CostCenter.AsNoTracking() Where e.Id = itemDistributionBaseDetail.CostCenterId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                        End If
                    Next
                End If
            Next

            Return query
        Else
            Return New CostGeneralExpense()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Function GetGeneralExpenseById(id As Integer) As CostGeneralExpense Implements ICostGeneralExpensesRepository.GetGeneralExpenseById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From g In _context.CostGeneralExpense Where g.Id = id Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From g In _context.CostGeneralExpense.AsNoTracking() Where g.Id = id Select g).FirstOrDefault()
            Return query
        Else
            Return New CostGeneralExpense()
        End If
    End Function

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As CostGeneralExpense Implements ICostGeneralExpensesRepository.GetGeneralExpenseByMainAccountId
        If MainAccountId = 0 Then
            Throw New ArgumentNullException("MainAccountId")
        End If
        'Dim query = (From ge In _context.CostGeneralExpense Where ge.MainAccountId = MainAccountId Select ge).FirstOrDefault()
        Dim query = (From ge In _context.CostGeneralExpense Select ge).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ge In _context.CostGeneralExpense.AsNoTracking() Select ge).FirstOrDefault()
            Return query
        Else
            Return New CostGeneralExpense()
        End If
    End Function

    ''' <summary>
    ''' Lista los elementos del costo con estado true
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCostGeneralExpenseByStatus(status As Boolean) As List(Of CostGeneralExpense) Implements ICostGeneralExpensesRepository.ListCostGeneralExpenseByStatus
        Return (From gx As CostGeneralExpense In _context.CostGeneralExpense.Include("CostDistributionBase").Include("CostDistributionBase.CostDistributionBaseDetail").Include("CostDistributionBase.CostDistributionBaseMeasurementUnit").Include("CostDistributionBase.CostDistributionBaseMeasurementUnit.InventoryMeasurementUnit").Include("CostDistributionBase.CostDistributionBaseDetail.CostProductionCenter") Select gx).ToList()
    End Function

    Public Function SP_ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, xmlListDistributionBaseDetail As String, xmlData As String) As List(Of SP_ImportDetailsToCostDistributionBase_Result) Implements ICostGeneralExpensesRepository.SP_ImportDetailsToCostDistributionBase
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportDetailsToCostDistributionBase(DistributionType, MeasurementUnit, xmlListDistributionBaseDetail, xmlData).ToList
    End Function

End Class