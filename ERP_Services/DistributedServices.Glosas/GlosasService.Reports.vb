#Region "Imports"

Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class GlosasService

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Glosas].[SP_ReportListObjectionsReception] realizado para cargar los datos del reporte de recepción de glosas
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportListObjectionsReception(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IGlosasReports.GetReportListObjectionsReception
        Using service As IReportAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IReportAdminService)()
            Return service.GetReportListObjectionsReception(criterias, Session)
        End Using
    End Function

#End Region

End Class
