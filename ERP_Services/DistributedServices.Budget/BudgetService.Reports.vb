#Region "Imports"

Imports System.Text
Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

#Region "Income"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportBudgetExcutionIncome] realizado para cargar los datos del reporte de ejecucion presupuestal de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportBudgetExecutionIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetListReportBudgetExecutionIncome
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportBudgetExecutionIncome(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportListDocumentIncome] realizado para cargar los datos de los reportes de presupuesto de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportListDocumentIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportListDocumentIncome
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportListDocumentIncome(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyIncomeExecution] realizado para cargar los datos de los reportes de ejecucion mensual de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportMonthlyIncomeExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportMonthlyIncomeExecution
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportMonthlyIncomeExecution(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportIncomeRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportIncomeRecordBook
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportIncomeRecordBook(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeRecordBook] realizado para cargar los datos de los reportes de programación de ingresos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportIncomeProgramming
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportIncomeProgramming(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportIncomeSchedule_Co] realizado para cargar los datos de los reportes de programación de ingresos y generar el archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GenerateArchiveReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IBudgetServiceReports.GenerateArchiveReportIncomeProgramming
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GenerateArchiveReportIncomeProgramming(criterias, Session)
        End Using
    End Function

#End Region

#Region "Expense"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportBudgetExcutionExpense] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportBudgetExecutionExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetListReportBudgetExecutionExpense
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportBudgetExecutionExpense(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetExpendituresProgrammingForTerritorialPublicEstablisments
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseSchedule_Co] realizado para cargar los datos del reporte de ejecucion presupuestal de gastos y exportar a archivo plano
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IBudgetServiceReports.GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportBudgetExecutionByCategoryThird] realizado para cargar los datos del reporte Ejecución presupuestal por rubro y por tercero
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportBudgetExecutionByCategoryThird(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportBudgetExecutionByCategoryThird
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportBudgetExecutionByCategoryThird(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportListDocumentExpense] realizado para cargar los datos de los reportes de presupuesto de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportListDocumentExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportListDocumentExpense
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportListDocumentExpense(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportMonthlyExpenseExecution] realizado para cargar los datos de los reportes de ejecucion mensual de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportMonthlyExpenseExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportMonthlyExpenseExecution
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportMonthlyExpenseExecution(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseRecordBook] realizado para cargar los datos de los reportes del libro de ejecución de gastos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportExpenseRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetReportExpenseRecordBook
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportExpenseRecordBook(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] realizado para cargar los datos del reporte de ejecucion de gastos para establecimientos publicos territoriales
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IBudgetServiceReports.GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Budget].[SP_ReportExpenseExecution_Co] y devuelve los datos en un StringBuilder
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IBudgetServiceReports.GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias, Session)
        End Using
    End Function

#End Region

End Class
