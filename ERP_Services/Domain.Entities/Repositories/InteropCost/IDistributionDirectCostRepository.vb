'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IDistributionDirectCostRepository
    Inherits IRepository(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Function GetDistributionDirectCost(ByVal code As String) As DistributionDirectCost


    Function ListDistributionDirectCostToReport(ByVal containerCost As String, ByVal distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result)


    Function ListGeneralExpenseByPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of GeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Function GetDistributionDirectCostById(id As Integer) As DistributionDirectCost

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Function ListDistributionDirectCostByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Function GetMainAccountValueByContainerNumberAccountYearAndMotn(ByVal container As String, ByVal numberaccount As String, ByVal year As String, ByVal month As Integer) As SP_MainAccountValue_Result

    Function GetHoursWorked(ByVal year As Int32, ByVal month As Int32) As IList

    Function CalculatePercentageByProductionCenter(pContainerNameDGEmpres As String, pDistributionBaseId As Int32, pTypeCalc As Int32, pYear As Int32, pMonth As Int32) As List(Of SP_CalculatePercentageByProductionCenter_Result)

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final
    ''' </summary>
    Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As SP_ReportGeneralProfitabilityTotalCost_Result

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As SP_ReportGeneralProfitabilityTotalCostB_Result

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGeneralExpenseById(GeneralExpenseId As Integer) As GeneralExpense

End Interface