#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Authorization].[SP_ReportRequests] realizado para cargar los datos del reporte de autorizaciones (solicitudes)
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportRequests(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface