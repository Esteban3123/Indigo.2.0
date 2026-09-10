#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportCircularAccountsReceivable
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportCircularAccountsReceivable y genera el archivo plano
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GenerateFileCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As ActionResult(Of StringBuilder)

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioByAge] realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportPortfolioByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportRadicateInvoice
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportRadicateInvoice(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolio2193] realizado para cargar los datos del decreti 2193
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportPortfolio2193(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_ReportPortfolioReconciliation] realizado para cargar los datos de conciliacion de cartera
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportPortfolioReconciliation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet
#End Region

End Interface