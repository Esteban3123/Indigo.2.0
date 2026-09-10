'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceDistributionDirectCost

    <OperationContract()>
    Function ListGeneralExpenseByPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of GeneralExpense)

    <OperationContract()>
    Function ListDistributionDirectCostToReport(ByVal containerCost As String, ByVal distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result)

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionDirectCost(distributionDirectCost As DistributionDirectCost, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionDirectCost)

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionDirectCost(distributionDirectCost As DistributionDirectCost, audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Actualiza el estado
    ' ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionDirectCost(code As String, audit As AuditMessage) As ActionResult(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionDirectCostById(id As Integer) As DistributionDirectCost

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionDirectCostByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionDirectCost)

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    <OperationContract()>
    Function GetMainAccountValueByContainerNumberAccountYearAndMotn(ByVal container As String, ByVal numberaccount As String, ByVal year As String, ByVal month As Integer) As ActionResult(Of SP_MainAccountValue_Result)

    <OperationContract()>
    Function CalculateDistribution(GeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of DistributionDirectCostDetail))

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final
    ''' </summary>
    <OperationContract()>
    Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result)

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    <OperationContract()>
    Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result)

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "InteropCost.SP_ReportGeneralProfitabilityTotalCostC"
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPrimaryEstimateOrFinalC(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, session As SessionValues) As DataSet
End Interface