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

Public Class DistributionFixedAssetRepository
    Inherits GenericRepository(Of DistributionFixedAsset)
    Implements IDistributionFixedAssetRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Public Function GetDistributionFixedAsset(code As String) As DistributionFixedAsset Implements IDistributionFixedAssetRepository.GetDistributionFixedAsset
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.DistributionFixedAsset.Include("DistributionFixedAssetDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionFixedAsset.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionFixedAsset()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Public Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset Implements IDistributionFixedAssetRepository.GetDistributionFixedAssetById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.DistributionFixedAsset Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionFixedAsset.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionFixedAsset()
        End If
    End Function

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of DistributionFixedAsset) Implements IDistributionFixedAssetRepository.ListDistributionFixedAssetByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Return (From d In _context.DistributionFixedAsset.Include("DistributionFixedAssetDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IDistributionFixedAssetRepository.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim listData As List(Of DistributionFixedAsset) = (From d In _context.DistributionFixedAsset Where d.Year * 100 + d.Month <= year * 100 + month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por el id del activo fijo, año y mes
    ''' </summary>
    Public Function GetDistributionFixedAssetByFixedAssetIdAndYearMonth(fixedassetId As Integer, year As Integer, month As Integer, Optional tracking As Boolean = True) As DistributionFixedAsset Implements IDistributionFixedAssetRepository.GetDistributionFixedAssetByFixedAssetIdAndYearMonth
        If fixedassetId = 0 Then
            Throw New ArgumentNullException("fixedassetId")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As DistributionFixedAsset = Nothing
        If tracking Then
            query = (From d In _context.DistributionFixedAsset.Include("DistributionFixedAssetDetail") Where d.FixedAssetId = fixedassetId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        Else
            query = (From d In _context.DistributionFixedAsset.AsNoTracking().Include("DistributionFixedAssetDetail").AsNoTracking() Where d.FixedAssetId = fixedassetId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionFixedAsset.AsNoTracking() Where d.FixedAssetId = fixedassetId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionFixedAsset()
        End If
    End Function

    Public Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As DistributionFixedAsset Implements IDistributionFixedAssetRepository.GetDistributionFixedAssetByActivoAndYearMonth
        If activoOid = 0 Then
            Throw New ArgumentNullException("activoOid")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As DistributionFixedAsset = (From d In _context.DistributionFixedAsset.Include("DistributionFixedAssetDetail") Where d.FixedAssetId = activoOid AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionFixedAsset.AsNoTracking() Where d.FixedAssetId = activoOid AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()

            Return query
        Else
            Return New DistributionFixedAsset()
        End If
    End Function

    Public Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, CodeUser As String) As SP_ConfirmMasiveDistributionFixedAsset_Result Implements IDistributionFixedAssetRepository.SP_ConfirmMasiveDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmMasiveDistributionFixedAsset(Container, Year, Month, CodeUser).SingleOrDefault
    End Function

    Public Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As List(Of SP_ExportExcelDistributionFixedAsset_Result) Implements IDistributionFixedAssetRepository.SP_ExportExcelDistributionFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ExportExcelDistributionFixedAsset(Container, Year, Month).ToList
    End Function

End Class