#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IAuthorizationServiceReports

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Authorization].[SP_ReportRequests] realizado para cargar los datos del reporte de autorizaciones (solicitudes)
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportRequests(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

End Interface
