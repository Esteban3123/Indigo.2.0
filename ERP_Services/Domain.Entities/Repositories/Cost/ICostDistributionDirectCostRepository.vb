'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICostDistributionDirectCostRepository
    Inherits IRepository(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Function GetDistributionDirectCost(ByVal code As String) As CostDistributionDirectCost

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Function GetDistributionDirectCostById(id As Integer) As CostDistributionDirectCost

    ''' <summary>
    ''' Obtiene una distribucion de elementos del costo por medio de la cuenta por pagar
    ''' </summary>
    Function GetDistributionDirectCostByAccountPayableId(AccountPayableId As Integer) As CostDistributionDirectCost

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Function ListDistributionDirectCostByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Function GetMainAccountValueByNumberAccountYearAndMotn(ByVal mainAccountId As Integer, ByVal year As String, ByVal month As Integer) As Decimal

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="CostGeneralExpenseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCostGeneralExpenseById(CostGeneralExpenseId As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Calcular distribución
    ''' </summary>
    ''' <param name="CostGeneralExpenseId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Function CalculateCostDistribution(CostGeneralExpenseId As Integer, Year As Integer, Month As Integer, Value As Decimal) As List(Of SP_CalculateCostDistribution_Result)

    ''' <summary>
    ''' Confirma las distribuciones de elementos del costo cuando son documentos tipo provisión
    ''' </summary>
    ''' <param name="IdProvisionDocument"></param>
    ''' <returns></returns>
    Function SP_ConfirmProvisionDocumentDistributionDirectCost(IdProvisionDocument As Integer, User As String) As List(Of SP_ConfirmProvisionDocumentDistributionDirectCost_Result)

    ''' <summary>
    ''' Reversa las distribuciones de elementos del costo cuando son documentos tipo provisión
    ''' </summary>
    ''' <param name="IdProvisionDocument"></param>
    ''' <returns></returns>
    Function SP_ReverseProvisionDocumentDistributionDirectCost(IdProvisionDocument As Integer, User As String, Type As Integer) As List(Of SP_ReverseProvisionDocumentDistributionDirectCost_Result)

    ''' <summary>
    ''' Copy And Paste Detalles de la distribucion de elementos del costo
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPasteCostDistributionDirectCostDetail(XmlObject As String) As List(Of SP_CopyAndPasteCostDistributionDirectCostDetail_Result)

End Interface