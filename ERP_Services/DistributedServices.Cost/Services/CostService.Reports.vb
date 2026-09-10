#Region "Imports"

Imports Application.Cost
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class CostService
    Implements ICostServiceReports

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportEstimateCosts] realizado para cargar los datos de la estimacion
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportEstimateCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ICostService.GetReportEstimateCosts
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetReportEstimateCosts(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_ReportActivityCosts] realizado para cargar los datos de costos por actividades
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportActivityCosts(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ICostServiceReports.GetListReportActivityCosts
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetListReportActivityCosts(filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[GetReportComparativeCosts] realizado para cargar los datos de la comparacion de costos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportComparativeCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ICostService.GetReportComparativeCosts
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetReportComparativeCosts(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[SP_CostReportResultProductionCostsExpenses] realizado para cargar los datos de los resultados de la operación
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResultProductionCostsExpenses(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ICostService.GetReportResultProductionCostsExpenses
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetReportResultProductionCostsExpenses(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Cost].[ReportResultProductionCostsExpensesDetail] realizado para cargar los datos de los resultados de la operación detallado
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResultProductionCostsExpensesDetail(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ICostService.GetReportResultProductionCostsExpensesDetail
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetReportResultProductionCostsExpensesDetail(criterias, Session)
        End Using
    End Function

End Class
