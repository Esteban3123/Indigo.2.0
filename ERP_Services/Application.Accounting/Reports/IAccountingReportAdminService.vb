Public Interface IAccountingReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportBalances] realizado para cargar los datos del reporte balance de pruebas
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportBalances(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportThirdPartyBalance] realizado para cargar los datos del reporte saldos de terceros
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportThirdPartyBalance(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatus] realizado para cargar los datos del reporte de resultados
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportResulStatus(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatusComparative] realizado para cargar los datos del reporte de resultados comparativo
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportResulStatusComparative(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportExogenousFormat(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GenerateExogenaFormats(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of String)

    ''' <summary>
    ''' Funcion para generar la circular unica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportSingleCircular(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para consultar el reporte de conciliación de módulos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportReconcileModule(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet

#End Region

End Interface
