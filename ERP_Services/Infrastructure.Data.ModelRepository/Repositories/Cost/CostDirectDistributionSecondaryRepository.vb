'***********************************************************************
' Assembly         : Infrastructure.Data.CostRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class CostDirectDistributionSecondaryRepository
    Inherits GenericRepository(Of CostDirectDistributionSecondary)
    Implements ICostDirectDistributionSecondaryRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function CalculateDistributionSecondary(costDistributionSecondaryId As Integer, Year As Integer, Month As Integer) As List(Of SP_CalculateDistributionSecondary_Result) Implements ICostDirectDistributionSecondaryRepository.CalculateDistributionSecondary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CalculateDistributionSecondary(costDistributionSecondaryId, Year, Month).ToList()
    End Function

    Public Function GetCostDirectDistributionSecondary(code As String) As CostDirectDistributionSecondary Implements ICostDirectDistributionSecondaryRepository.GetCostDirectDistributionSecondary
        Dim res = (From e In _context.CostDirectDistributionSecondary.Include("CostDirectDistributionSecondaryDetail") Where e.Code = code Select e).FirstOrDefault
        If res IsNot Nothing Then
            For Each item In res.CostDirectDistributionSecondaryDetail
                item.CodeNameMeasureUnit = (From i In _context.InventoryMeasurementUnit.AsNoTracking() Where i.Id = item.MeasurementUnitId Select String.Concat(i.Code, " - ", i.Name)).FirstOrDefault()
                item.CodeNameProductionCenter = (From e In _context.CostProductionCenter.AsNoTracking() Where e.Id = item.ProductionCenterId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
            Next
            res.OriginalValue = (From e In _context.CostDirectDistributionSecondary.AsNoTracking() Where e.Code = code Select e).FirstOrDefault
            Return res
        Else
            Return New CostDirectDistributionSecondary
        End If
    End Function

    Public Function GetCostDirectDistributionSecondaryById(id As Integer) As CostDirectDistributionSecondary Implements ICostDirectDistributionSecondaryRepository.GetCostDirectDistributionSecondaryById
        Dim res = (From e In _context.CostDirectDistributionSecondary Where e.Id = id Select e).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From e In _context.CostDirectDistributionSecondary.AsNoTracking() Where e.Id = id Select e).FirstOrDefault
            Return res
        Else
            Return New CostDirectDistributionSecondary
        End If
    End Function

    Public Function GetCountByCostDistributionSecondaryId(idDistributionSecundary As Integer, month As Integer, year As Integer) As Integer Implements ICostDirectDistributionSecondaryRepository.GetCountByCostDistributionSecondaryId
        Dim result = (From e In _context.CostDirectDistributionSecondary.AsNoTracking() Where e.DistributionSecondaryId = idDistributionSecundary And e.Month = month And e.Year = year And e.Status <> 3 Select e.Code).Count()
        Return result
    End Function

    Public Function SP_UpdateFieldImportCost(ObjectXml As String) As SP_UpdateFieldImportCost_Result Implements ICostDirectDistributionSecondaryRepository.SP_UpdateFieldImportCost
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_UpdateFieldImportCost(ObjectXml).SingleOrDefault
    End Function

    Public Function SP_SaveDistributionSecondary(EntityXml As String, ListDeleteXml As String, ListLogisticsProductionCenterDetailXml As String, CodeUser As String) As SP_SaveDistributionSecondary_Result Implements ICostDirectDistributionSecondaryRepository.SP_SaveDistributionSecondary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveDistributionSecondary(EntityXml, ListDeleteXml, ListLogisticsProductionCenterDetailXml, CodeUser).SingleOrDefault()
    End Function

    Public Function SP_GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_GenerateDistributionSecondary_Result Implements ICostDirectDistributionSecondaryRepository.SP_GenerateDistributionSecondary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateDistributionSecondary(Year, Month, OperatingUnitId, CodeUser).SingleOrDefault()
    End Function

End Class
