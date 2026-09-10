#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Text

#End Region

<ServiceContract()>
Public Interface IFixedAssetServiceReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [FixedAsset].[SP_ReportHistoricalDepreciation] realizado para cargar los datos del reporte de depreciaciones
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportHistoricalDepreciation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportHistoricalPhysicalAsset
    ''' </summary>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ReportHistoricalPhysicalAsset(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [FixedAsset].[SP_ReportResponsibleForFixedAssets]
    ''' </summary>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportResponsibleForFixedAssets(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [FixedAsset].[SP_ReporttDepreciationMonth] realizado para cargar los datos del reporte de depreciaciones del mes
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportDepreciationMonth(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet


#End Region

End Interface