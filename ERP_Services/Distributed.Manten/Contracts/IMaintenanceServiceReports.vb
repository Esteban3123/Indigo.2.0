#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IMaintenanceServiceReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportFixedAssetsMetrology realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportFixedAssetsMetrology(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportMaintenanceRepairs realizado
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportMaintenanceRepairs(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface