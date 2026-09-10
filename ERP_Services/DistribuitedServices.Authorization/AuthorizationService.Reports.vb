#Region "Imports"

Imports Application.Authorization
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class AuthorizationService
    Implements IAuthorizationServiceReports

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Authorization].[SP_ReportRequests] realizado para cargar los datos del reporte de autorizaciones (solicitudes)
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportRequests(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAuthorizationServiceReports.GetReportRequests
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportRequests(criterias, Session)
        End Using
    End Function

End Class
