'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/09/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostDistributionFixedAssetRepository
    Inherits IRepository(Of CostDistributionFixedAsset)

    Function GetCostDistributionFixedAssetById(id As Integer) As CostDistributionFixedAsset

    Function GetCostDistributionFixedAsset(ByVal code As String) As CostDistributionFixedAsset

    Function SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId(year As Integer, month As Integer, physicalId As Integer) As List(Of SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId_Result)

    Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As CostDistributionFixedAsset

    Function ListPeriodWithDistributionFixedDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionFixedAsset)

    Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As List(Of SP_ExportExcelCostDistributionFixedAsset_Result)

    Function SP_ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportXml As String, CodeUser As String) As SP_ImportCostDistributionFixedAsset_Result

    Function SP_SaveCostDistributionFixedAsset(CostDistributionFixedAssetXml As String, CodeUser As String) As SP_SaveCostDistributionFixedAsset_Result

    Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveCostDistributionFixedAsset_Result

End Interface