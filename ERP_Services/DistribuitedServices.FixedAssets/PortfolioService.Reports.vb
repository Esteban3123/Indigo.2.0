#Region "Imports"

Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class FixedAssetService

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [FixedAsset].[SP_ReportHistoricalDepreciation] realizado para cargar los datos del reporte de depreciaciones
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportHistoricalDepreciation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IFixedAssetServiceReports.GetListReportHistoricalDepreciation
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportHistoricalDepreciation(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Método que realiza el llamado del store [FixedAsset].[SP_ReportHistoricalPhysicalAsset]
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function SP_ReportHistoricalPhysicalAsset(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IFixedAssetServiceReports.SP_ReportHistoricalPhysicalAsset
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.SP_ReportHistoricalPhysicalAsset(filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [FixedAsset].[SP_ReportResponsibleForFixedAssets]
    ''' </summary>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResponsibleForFixedAssets(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IFixedAssetServiceReports.GetReportResponsibleForFixedAssets
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetReportResponsibleForFixedAssets(criterias, filters, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [FixedAsset].[SP_ReportDepreciationMonth] realizado para cargar los datos del reporte de depreciaciones del mes
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportDepreciationMonth(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IFixedAssetServiceReports.GetListReportDepreciationMonth
        Using service As IReportAdminService = Container.Current.Resolve(Of IReportAdminService)()
            Return service.GetListReportDepreciationMonth(criterias, filters, Session)
        End Using
    End Function

#End Region

End Class
