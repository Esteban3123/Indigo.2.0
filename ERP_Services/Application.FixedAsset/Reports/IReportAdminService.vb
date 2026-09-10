#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportHistoricalDepreciation
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportHistoricalDepreciation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportHistoricalPhysicalAsset
    ''' </summary>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function SP_ReportHistoricalPhysicalAsset(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [FixedAsset].[SP_ReportResponsibleForFixedAssets]
    ''' </summary>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportResponsibleForFixedAssets(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet


    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportDepreciationMonth
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportDepreciationMonth(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet


#End Region

End Interface