'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDistributionDirectCostAdminService
    Inherits IDisposable

    Function ListGeneralExpenseByPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of GeneralExpense)


    Function ListDistributionDirectCostToReport(ByVal containerCost As String, ByVal distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result)

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Function SaveDistributionDirectCost(ByVal distributionDirectCost As DistributionDirectCost, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DistributionDirectCost)

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Function DeleteDistributionDirectCost(ByVal distributionDirectCost As DistributionDirectCost, ByVal audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    Function UpdateStateDistributionDirectCost(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Function GetDistributionDirectCost(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionDirectCost)

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
    Function GetMainAccountValueByContainerNumberAccountYearAndMotn(ByVal container As String, ByVal numberaccount As String, ByVal year As String, ByVal month As Integer) As ActionResult(Of SP_MainAccountValue_Result)

    ''' <summary>
    ''' Calcula la distribución del valor entre los distintos centros de producción
    ''' </summary>
    ''' <param name="generalExpense">Elemento del gasto</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de detalles de distribución de costos</returns>
    Function CalculateDistribution(ByVal GeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of DistributionDirectCostDetail))

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final
    ''' </summary>
    Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result)

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result)

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "InteropCost.SP_ReportGeneralProfitabilityTotalCostC"
    ''' </summary>
    ''' <returns></returns>
    Function GetPrimaryEstimateOrFinalC(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, session As SessionValues) As DataSet

End Interface