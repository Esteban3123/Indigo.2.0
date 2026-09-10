#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface ICostServiceReports

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportEstimateCosts] realizado para cargar los datos de la estimacion
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportEstimateCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportActivityCosts] realizado para cargar los datos de costos por actividades
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportActivityCosts(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[GetReportComparativeCosts] realizado para cargar los datos de la comparacion de costos
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportComparativeCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_CostReportResultProductionCostsExpenses] realizado para cargar los datos de los resultados de la operación
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportResultProductionCostsExpenses(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[ReportResultProductionCostsExpensesDetail] realizado para cargar los datos de los resultados de la operación detallado
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportResultProductionCostsExpensesDetail(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

End Interface
