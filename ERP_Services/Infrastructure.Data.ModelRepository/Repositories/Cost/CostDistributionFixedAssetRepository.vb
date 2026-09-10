'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class CostDistributionFixedAssetRepository
    Inherits GenericRepository(Of CostDistributionFixedAsset)
    Implements ICostDistributionFixedAssetRepository

#Region "Builder"

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostDistributionFixedAssetById(id As Integer) As CostDistributionFixedAsset Implements ICostDistributionFixedAssetRepository.GetCostDistributionFixedAssetById
        Dim query = (From d In _context.CostDistributionFixedAsset Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionFixedAsset.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionFixedAsset()
        End If
    End Function

    Public Function GetCostDistributionFixedAsset(code As String) As CostDistributionFixedAsset Implements ICostDistributionFixedAssetRepository.GetCostDistributionFixedAsset
        Dim query = (From d In _context.CostDistributionFixedAsset.Include("CostDistributionFixedAssetDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionFixedAsset.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionFixedAsset()
        End If
    End Function

    Public Function SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId(year As Integer, month As Integer, physicalId As Integer) As List(Of SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId_Result) Implements ICostDistributionFixedAssetRepository.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId(year, month, physicalId).ToList()
    End Function

    Public Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As CostDistributionFixedAsset Implements ICostDistributionFixedAssetRepository.GetCostDistributionFixedAssetByPhysicalIdAndYearMonth
        Dim query = (From d In _context.CostDistributionFixedAsset.Include("CostDistributionFixedAssetDetail") Where d.FixedAssetPhysicalAssetId = physicalId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionFixedAsset.AsNoTracking() Where d.FixedAssetPhysicalAssetId = physicalId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionFixedAsset()
        End If
    End Function

    Public Function ListPeriodWithDistributionFixedDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionFixedAssetRepository.ListPeriodWithDistributionFixedDataByMaximumPeriod
        Dim listData As List(Of CostDistributionFixedAsset) = (From d In _context.CostDistributionFixedAsset Where d.Year <= year AndAlso d.Month <= month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetRepository.ListDistributionFixedAssetByYearMonth
        Dim listCostDistributionFixedAsset = (From d In _context.CostDistributionFixedAsset.AsNoTracking.Include("FixedAssetPhysicalAsset").AsNoTracking Where d.Year = year AndAlso d.Month = month Select d).ToList()

        If listCostDistributionFixedAsset IsNot Nothing AndAlso listCostDistributionFixedAsset.Count > 0 Then
            'Dictionaries
            Dim dictionaryItems As New Dictionary(Of Integer, String)
            Dim dictionaryLocations As New Dictionary(Of Integer, String)
            Dim dictionaryResponsibles As New Dictionary(Of Integer, String)
            'Individual Items
            Dim codeName As String = Nothing

            For Each item As CostDistributionFixedAsset In listCostDistributionFixedAsset
                If Not dictionaryLocations.ContainsKey(item.FixedAssetPhysicalAsset.ItemId) Then
                    codeName = (From p In _context.FixedAssetItem.AsNoTracking Where p.Id = item.FixedAssetPhysicalAsset.ItemId Select p.Code + " - " + p.Description).FirstOrDefault
                    dictionaryLocations.Add(item.FixedAssetPhysicalAsset.ItemId, codeName)
                Else
                    codeName = dictionaryLocations(item.FixedAssetPhysicalAsset.ItemId)
                End If
                item.FixedAssetItemCodeDescription = codeName

                If Not dictionaryLocations.ContainsKey(item.FixedAssetPhysicalAsset.LocationId) Then
                    codeName = (From p In _context.Position.AsNoTracking Where p.Id = item.FixedAssetPhysicalAsset.LocationId Select p.Code + " - " + p.Name).FirstOrDefault
                    dictionaryLocations.Add(item.FixedAssetPhysicalAsset.LocationId, codeName)
                Else
                    codeName = dictionaryLocations(item.FixedAssetPhysicalAsset.LocationId)
                End If
                item.FixedAssetLocationCodeName = codeName

                If Not dictionaryLocations.ContainsKey(item.FixedAssetPhysicalAsset.ResponsibleId) Then
                    codeName = (From p In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where p.Id = item.FixedAssetPhysicalAsset.ResponsibleId Select p.ThirdParty.Nit + " - " + p.ThirdParty.Name).FirstOrDefault
                    dictionaryLocations.Add(item.FixedAssetPhysicalAsset.ResponsibleId, codeName)
                Else
                    codeName = dictionaryLocations(item.FixedAssetPhysicalAsset.ResponsibleId)
                End If
                item.ThirdPartyNitName = codeName
            Next
        End If

        Return listCostDistributionFixedAsset
    End Function

    Public Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As List(Of SP_ExportExcelCostDistributionFixedAsset_Result) Implements ICostDistributionFixedAssetRepository.SP_ExportExcelCostDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ExportExcelCostDistributionFixedAsset(Year, Month).ToList
    End Function

    Public Function SP_ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportXml As String, CodeUser As String) As SP_ImportCostDistributionFixedAsset_Result Implements ICostDistributionFixedAssetRepository.SP_ImportCostDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportCostDistributionFixedAsset(Year, Month, OperatingUnitId, ImportXml, CodeUser).SingleOrDefault()
    End Function

    Public Function SP_SaveCostDistributionFixedAsset(CostDistributionFixedAssetXml As String, CodeUser As String) As SP_SaveCostDistributionFixedAsset_Result Implements ICostDistributionFixedAssetRepository.SP_SaveCostDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCostDistributionFixedAsset(CostDistributionFixedAssetXml, CodeUser).SingleOrDefault()
    End Function

    Public Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveCostDistributionFixedAsset_Result Implements ICostDistributionFixedAssetRepository.SP_ConfirmMasiveCostDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmMasiveCostDistributionFixedAsset(Year, Month, OperatingUnitId, CodeUser).SingleOrDefault
    End Function

#End Region

End Class
