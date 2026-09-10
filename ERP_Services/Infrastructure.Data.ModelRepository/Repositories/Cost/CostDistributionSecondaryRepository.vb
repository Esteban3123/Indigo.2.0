'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class CostDistributionSecondaryRepository
    Inherits GenericRepository(Of CostDistributionSecondary)
    Implements ICostDistributionSecondaryRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    Public Function GetDistributionSecondary(code As String) As CostDistributionSecondary Implements ICostDistributionSecondaryRepository.GetDistributionSecondary
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From g In _context.CostDistributionSecondary.Include("CostDistributionSecondaryBase").Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail").Include("CostDistributionSecondaryBase.CostDistributionSecondaryMeasurementUnit") Where g.Code.Equals(code) Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From g In _context.CostDistributionSecondary.AsNoTracking() Where g.Code.Equals(code) Select g).FirstOrDefault()
            query.FullNameProductionCenter = (From p In _context.CostProductionCenter Where p.Id = query.ProductionCenterId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
            For Each item As CostDistributionSecondaryBase In query.CostDistributionSecondaryBase
                Select Case item.MultipleBase
                    Case 1
                        item.DistributionBaseName = "Distribución (A)"
                    Case 2
                        item.DistributionBaseName = "Distribución (B)"
                    Case 3
                        item.DistributionBaseName = "Distribución (C)"
                    Case 4
                        item.DistributionBaseName = "Distribución (D)"
                End Select
                Select Case item.DistributionType
                    Case 1
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeDirect", "InteropCost")
                    Case 2
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeCalculated", "InteropCost")
                    Case 3
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeSearch", "InteropCost")
                End Select
                Select Case item.MeasurementUnit
                    Case 1
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitProportion", "InteropCost")
                    Case 2
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitValue", "InteropCost")
                End Select
                For Each itemMeasureUnit In item.CostDistributionSecondaryMeasurementUnit
                    itemMeasureUnit.CodeNameMeasureUnit = (From e In _context.InventoryMeasurementUnit.AsNoTracking() Where e.Id = itemMeasureUnit.MeasurementUnitId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                Next
            Next
            Return query
        Else
            Return New CostDistributionSecondary()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    Public Function GetDistributionSecondaryById(id As Integer) As CostDistributionSecondary Implements ICostDistributionSecondaryRepository.GetDistributionSecondaryById
        Return (From g In _context.CostDistributionSecondary.AsNoTracking.Include("CostDistributionSecondaryBase").AsNoTracking.Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail").AsNoTracking.Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail.CostProductionCenter").AsNoTracking Where g.Id = id Select g).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionSecondary) Implements ICostDistributionSecondaryRepository.ListDistributionSecondaryByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        'Dim ListDistribSecondary As List(Of CostDistributionSecondary) = (From d In _context.CostDistributionSecondary.Include("CostDistributionSecondaryBase").Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
        'For Each item As CostDistributionSecondary In ListDistribSecondary
        '    item.FullNameProductionCenter = (From t In _context.CostProductionCenter Where t.Id = item.ProductionCenterId Select String.Concat(t.Code, " - ", t.Name)).FirstOrDefault()
        'Next
        'Return ListDistribSecondary
        Return Nothing
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionSecondaryRepository.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        'Dim listData As List(Of CostDistributionSecondary) = (From d In _context.CostDistributionSecondary Where d.Year * 100 + d.Month <= year * 100 + month Select d).ToList()
        'Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the distribution intermediate by production center identifier and year month.
    ''' </summary>
    Public Function GetDistributionSecondaryByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional tracking As Boolean = True) As CostDistributionSecondary Implements ICostDistributionSecondaryRepository.GetDistributionSecondaryByProductionCenterIdAndYearMonth
        If productionCenterId = 0 Then
            Throw New ArgumentNullException("productionCenterId")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As CostDistributionSecondary = Nothing
        'If tracking Then
        '    query = (From d In _context.CostDistributionSecondary.Include("CostDistributionSecondaryBase").Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail") Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        'Else
        '    query = (From d In _context.CostDistributionSecondary.AsNoTracking().Include("CostDistributionSecondaryBase").Include("CostDistributionSecondaryBase.CostDistributionSecondaryBaseDetail").AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        'End If
        'If query IsNot Nothing AndAlso query.Id > 0 Then
        '    query.OriginalValue = (From d In _context.CostDistributionSecondary.AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        '    Return query
        'Else
        '    Return New CostDistributionSecondary()
        'End If
        Return Nothing
    End Function

    Public Function SP_CopyPasteCostSecondaryDistribution(XmlObject As String, DistributionSecondaryId As Integer) As List(Of SP_CopyPasteCostSecondaryDistribution_Result) Implements ICostDistributionSecondaryRepository.SP_CopyPasteCostSecondaryDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyPasteCostSecondaryDistribution(XmlObject, DistributionSecondaryId).ToList
    End Function

    Public Function SP_ImportDetailsToCostDistributionSecondaryBase(DistributionType As Byte, MeasurementUnit As Byte, xmlListDistributionSecondaryBaseDetail As String, xmlData As String) As List(Of SP_ImportDetailsToCostDistributionSecondaryBase_Result) Implements ICostDistributionSecondaryRepository.SP_ImportDetailsToCostDistributionSecondaryBase
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportDetailsToCostDistributionSecondaryBase(DistributionType, MeasurementUnit, xmlListDistributionSecondaryBaseDetail, xmlData).ToList
    End Function

End Class