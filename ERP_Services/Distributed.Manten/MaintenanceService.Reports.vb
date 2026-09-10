#Region "Imports"

Imports Application.Maintenance
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class MaintanceService

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportFixedAssetsMetrology realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportFixedAssetsMetrology(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IMaintenanceServiceReports.GetListReportFixedAssetsMetrology
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportFixedAssetsMetrology(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportMaintenanceRepairs realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportMaintenanceRepairs(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IMaintenanceServiceReports.GetListReportMaintenanceRepairs
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportMaintenanceRepairs(criterias, filters, Session)
        End Using
    End Function

#End Region

End Class