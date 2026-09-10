#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

#Region "Income"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportBudgetExcutionIncome] realizado para cargar los datos del reporte de ejecucion presupuestal de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportBudgetExecutionIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportListDocumentIncome] realizado para cargar los datos de los reportes de presupuesto de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportListDocumentIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyIncomeExecution] realizado para cargar los datos de los reportes de ejecucion mensual de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportMonthlyIncomeExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportIncomeRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

#Region "Expense"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportBudgetExcutionExpense] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportBudgetExecutionExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure[Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportBudgetExecutionByCategoryThird] realizado para cargar los datos del reporte Ejecución presupuestal por rubro y por tercero
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportBudgetExecutionByCategoryThird(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportListDocumentExpense] realizado para cargar los datos de los reportes de presupuesto de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportListDocumentExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyExpenseExecution] realizado para cargar los datos de los reportes de ejecucion mensual de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportMonthlyExpenseExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportExpenseRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] realizado para cargar los datos del reporte de ejecucion de gastos para establecimientos publicos territoriales
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de programación de gastos para establecimientos publicos territoriales y exportar a archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] realizado para cargar los datos del reporte de ejecución de gastos para establecimientos publicos territoriales y exportar a archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes de programación de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeSchedule_Co] realizado para cargar los datos del reporte de programación de ingresos y generar el archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder

#End Region

#End Region

End Interface