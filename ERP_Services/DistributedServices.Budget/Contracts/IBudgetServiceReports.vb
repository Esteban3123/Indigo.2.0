#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Text

#End Region

<ServiceContract()>
Public Interface IBudgetServiceReports

#Region "Income"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportBudgetExcutionIncome] realizado para cargar los datos del reporte de ejecucion presupuestal de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListReportBudgetExecutionIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportListDocumentIncome] realizado para cargar los datos de los reportes de presupuesto de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportListDocumentIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyIncomeExecution] realizado para cargar los datos de los reportes de ejecucion mensual de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportMonthlyIncomeExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportIncomeRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes de programaciónde ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeSchedule_Co] realizado para cargar los datos de los reportes de programaciónde ingresos y generar el archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateArchiveReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder


#End Region

#Region "Expense"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportBudgetExcutionExpense] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListReportBudgetExecutionExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos y exportar a archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportBudgetExecutionByCategoryThird] realizado para cargar los datos del reporte Ejecución presupuestal por rubro y por tercero
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportBudgetExecutionByCategoryThird(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportListDocumentExpense] realizado para cargar los datos de los reportes de presupuesto de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportListDocumentExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyExpenseExecution] realizado para cargar los datos de los reportes de ejecucion mensual de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportMonthlyExpenseExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportExpenseRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] realizado para cargar los datos del reporte de ejecucion de gastos para establecimientos publicos territoriales
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] y devuelve los datos en un StringBuilder
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder

#End Region

End Interface
