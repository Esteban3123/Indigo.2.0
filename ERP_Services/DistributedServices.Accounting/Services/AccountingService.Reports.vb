#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Accounting
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class AccountingService

#Region "Functions"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportBalances] realizado para cargar los datos del reporte balance de pruebas
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportBalances(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportBalances
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportBalances(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportThirdPartyBalance] realizado para cargar los datos del reporte saldos de terceros
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportThirdPartyBalance(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportThirdPartyBalance
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportThirdPartyBalance(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatus] realizado para cargar los datos del reporte de resultados
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResulStatus(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportResulStatus
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportResulStatus(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatusComparative] realizado para cargar los datos del reporte de resultados comparativo
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportResulStatusComparative(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportResulStatusComparative
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportResulStatusComparative(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportExogenousFormat(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportExogenousFormat
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportExogenousFormat(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GenerateExogenaFormats(criterias As Dictionary(Of String, String), Session As SessionValues) As Domain.Base.Entities.ActionResult(Of String) Implements IAccountingReports.GenerateExogenaFormats
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GenerateExogenaFormats(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para generar la circular unica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportSingleCircular(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReports.GetReportSingleCircular
        Using service As IAccountingReportAdminService = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportSingleCircular(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para consultar el reporte de conciliación de módulos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportReconcileModule(criterias As Dictionary(Of String, String), Session As SessionValues) As System.Data.DataSet Implements IAccountingReports.GetReportReconcileModule
        Using service = Container.Current.Resolve(Of IAccountingReportAdminService)()
            Return service.GetReportReconcileModule(criterias, Session)
        End Using
    End Function

#End Region

End Class
