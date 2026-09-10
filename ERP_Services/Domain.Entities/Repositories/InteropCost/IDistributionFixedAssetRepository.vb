'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IDistributionFixedAssetRepository
    Inherits IRepository(Of DistributionFixedAsset)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Function GetDistributionFixedAsset(ByVal code As String) As DistributionFixedAsset

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por el id del activo fijo, año y mes
    ''' </summary>
    Function GetDistributionFixedAssetByFixedAssetIdAndYearMonth(fixedassetId As Integer, year As Integer, month As Integer, Optional ByVal tracking As Boolean = True) As DistributionFixedAsset

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionFixedAsset)

    Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As DistributionFixedAsset

    Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, CodeUser As String) As SP_ConfirmMasiveDistributionFixedAsset_Result

    Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As List(Of SP_ExportExcelDistributionFixedAsset_Result)

End Interface