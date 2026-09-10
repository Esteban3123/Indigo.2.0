#Region "Imports"

Imports Application.Portfolio
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.Text

#End Region

Partial Public Class PortfolioService

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportCircularAccountsReceivable] realizado para cargar los datos del reporte Circular 014 de cuentas por cobrar
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPortfolioServiceReports.GetListReportCircularAccountsReceivable
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportCircularAccountsReceivable(filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportCircularAccountsReceivable] realizado para generar el archivo plano del reporte Circular 014 de cuentas por cobrar
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GenerateFileCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As ActionResult(Of StringBuilder) Implements IPortfolioServiceReports.GenerateFileCircularAccountsReceivable
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GenerateFileCircularAccountsReceivable(filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioByAge] realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportPortfolioByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPortfolioServiceReports.GetListReportPortfolioByAge
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportPortfolioByAge(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportRadicateInvoice] realizado para cargar los datos del listado de radicados
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportRadicateInvoice(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPortfolioServiceReports.GetListReportRadicateInvoice
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportRadicateInvoice(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolio2193] realizado para cargar los datos del decreti 2193
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportPortfolio2193(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPortfolioServiceReports.GetListReportPortfolio2193
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportPortfolio2193(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioReconciliation] realizado para cargar los datos de conciliacion de cartera
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportPortfolioReconciliation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPortfolioServiceReports.GetListReportPortfolioReconciliation
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportPortfolioReconciliation(criterias, filters, Session)
        End Using
    End Function

#End Region

End Class
