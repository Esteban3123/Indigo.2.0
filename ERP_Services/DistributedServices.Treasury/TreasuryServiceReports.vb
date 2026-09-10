#Region "Imports"

Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class TreasuryService
    Implements ITreasuryServiceReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportReceiptsByRegime] realizado para cargar los datos del reporte listado de recibos de caja por regimen
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportReceiptsByRegime(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ITreasuryServiceReports.GetReportReceiptsByRegime
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportReceiptsByRegime(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliation] realizado para cargar los datos del reporte conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportBankReconciliation(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ITreasuryServiceReports.GetReportBankReconciliation
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportBankReconciliation(criterias, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliationAutomatic] realizado para cargar los datos del reporte conciliación bancaria automatica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportBankReconciliationAutomatic(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements ITreasuryServiceReports.GetReportBankReconciliationAutomatic
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportBankReconciliationAutomatic(criterias, Session)
        End Using
    End Function

#End Region

End Class
