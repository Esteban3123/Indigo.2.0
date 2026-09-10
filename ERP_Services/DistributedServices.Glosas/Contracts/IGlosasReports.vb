Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IGlosasReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Glosas].[SP_ReportListObjectionsReception] realizado para cargar los datos del reporte de recepción de glosas
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetReportListObjectionsReception(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface
