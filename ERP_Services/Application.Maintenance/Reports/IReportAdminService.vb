#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportFixedAssetsMetrology realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportFixedAssetsMetrology(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportMaintenanceRepairs realizado para cargar los datos de la cartera por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportMaintenanceRepairs(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface