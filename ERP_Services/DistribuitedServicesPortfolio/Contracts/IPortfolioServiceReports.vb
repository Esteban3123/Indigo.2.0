#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Text

#End Region

<ServiceContract()>
Public Interface IPortfolioServiceReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportCircularAccountsReceivable] realizado para cargar los datos del reporte Circular 014 de cuentas por cobrar
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportCircularAccountsReceivable] realizado para generar el archivo plano del reporte Circular 014 de cuentas por cobrar
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateFileCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As ActionResult(Of StringBuilder)

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioByAge] realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPortfolioByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportRadicateInvoice] realizado para cargar los datos del listado de radicados
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportRadicateInvoice(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolio2193] realizado para cargar los datos del decreti 2193
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPortfolio2193(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioReconciliation] realizado para cargar los datos de conciliacion de cartera
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPortfolioReconciliation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface