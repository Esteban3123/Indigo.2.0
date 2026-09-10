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

Public Class DistributionDirectCostRepository
    Inherits GenericRepository(Of DistributionDirectCost)
    Implements IDistributionDirectCostRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Function GetDistributionDirectCost(code As String) As DistributionDirectCost Implements IDistributionDirectCostRepository.GetDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.DistributionDirectCost.Include("DistributionDirectCostDetail").Include("DistributionDirectCostValues") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            If query.DistributionDirectCostDetail IsNot Nothing AndAlso query.DistributionDirectCostDetail.Count > 0 Then
                For Each item In query.DistributionDirectCostDetail
                    item.ProductionCenterCodeName = (From ge In _context.ProductionCenter.AsNoTracking Where ge.Id = item.ProductionCenterId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
                    If item.MeasurementUnitId IsNot Nothing Then
                        item.MeasurementUnitCodeName = (From ge In _context.InventoryMeasurementUnit.AsNoTracking Where ge.Id = item.MeasurementUnitId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
                    End If
                Next
            End If
            query.FullNameGeneralExpense = (From ge In _context.GeneralExpense Where ge.Id = query.GeneralExpenseId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
            query.OriginalValue = (From d In _context.DistributionDirectCost.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionDirectCost()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Function GetDistributionDirectCostById(id As Integer) As DistributionDirectCost Implements IDistributionDirectCostRepository.GetDistributionDirectCostById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.DistributionDirectCost Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionDirectCost.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionDirectCost()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Public Function GetMainAccountValueByContainerNumberAccountYearAndMotn(container As String, numberaccount As String, year As String, month As Integer) As SP_MainAccountValue_Result Implements IDistributionDirectCostRepository.GetMainAccountValueByContainerNumberAccountYearAndMotn
        Return _context.SP_MainAccountValue(container, numberaccount, year, month).ToList().ElementAt(0)
    End Function

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of DistributionDirectCost) Implements IDistributionDirectCostRepository.ListDistributionDirectCostByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Return (From d In _context.DistributionDirectCost.Include("DistributionDirectCostDetail") Where d.Year * 100 + d.Month <= year * 100 + month Select d).ToList()
    End Function

    Public Function GetHoursWorked(year As Integer, month As Integer) As IList Implements IDistributionDirectCostRepository.GetHoursWorked
        'Dim res = (From detail As DistributionManpowerDetail In _context.DistributionManpowerDetail.Include("DistributionManpower") Where detail.DistributionManpower.Year = year And detail.DistributionManpower.Month = month And detail.DistributionManpower.Status = True Select detail).ToList()
        Dim res = (From detail As DistributionManpowerDetail In _context.DistributionManpowerDetail.Include("DistributionManpower")
                   Where detail.DistributionManpower.Year = year And detail.DistributionManpower.Month = month
                   Group By ProductionCenterId = detail.ProductionCenterId Into Sum(detail.HoursQuantity)).ToList()
        Return res
    End Function

    Public Function CalculatePercentageByProductionCenter(pContainerNameDGEmpres As String, pDistributionBaseId As Integer, pTypeCalc As Integer, pYear As Integer, pMonth As Integer) As List(Of SP_CalculatePercentageByProductionCenter_Result) Implements IDistributionDirectCostRepository.CalculatePercentageByProductionCenter
        Return _context.SP_CalculatePercentageByProductionCenter(pContainerNameDGEmpres, pDistributionBaseId, pTypeCalc, pYear, pMonth).ToList()
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As SP_ReportGeneralProfitabilityTotalCost_Result Implements IDistributionDirectCostRepository.GetPrimaryEstimateOrFinal
        Return _context.SP_ReportGeneralProfitabilityTotalCost(InitialMonth, InitialYear, LastMonth, LastYear).ToList().ElementAt(0)
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As SP_ReportGeneralProfitabilityTotalCostB_Result Implements IDistributionDirectCostRepository.GetPrimaryEstimateOrFinalB
        Return _context.SP_ReportGeneralProfitabilityTotalCostB(InitialMonth, InitialYear, LastMonth, LastYear).ToList().ElementAt(0)
    End Function

    Public Function ListGeneralExpenseByPeriod(year As Integer, month As Integer) As List(Of GeneralExpense) Implements IDistributionDirectCostRepository.ListGeneralExpenseByPeriod
        Dim result = (From d As DistributionDirectCost In _context.DistributionDirectCost.Include("GeneralExpense") Where d.Year = year And d.Month = month And d.Status <> 3 Select d.GeneralExpense).ToList()
        Return result
    End Function

    Public Function ListDistributionDirectCostToReport(containerCost As String, distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result) Implements IDistributionDirectCostRepository.ListDistributionDirectCostToReport
        Dim result = _context.SP_ReportDistributionDirectCost(containerCost, distributionDirectCostId).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGeneralExpenseById(GeneralExpenseId As Integer) As GeneralExpense Implements IDistributionDirectCostRepository.GetGeneralExpenseById
        Return (From e In _context.GeneralExpense.AsNoTracking.Include("DistributionBase").AsNoTracking.Include("DistributionBase.DistributionBaseDetail").AsNoTracking.Include("DistributionBase.DistributionBaseDetail.ProductionCenter").AsNoTracking Where e.Id = GeneralExpenseId Select e).FirstOrDefault()
    End Function

End Class