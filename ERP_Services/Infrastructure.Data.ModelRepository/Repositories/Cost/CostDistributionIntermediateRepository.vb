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

Public Class CostDistributionIntermediateRepository
    Inherits GenericRepository(Of CostDistributionIntermediate)
    Implements ICostDistributionIntermediateRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.CostDistributionIntermediate.Include("CostDistributionIntermediateDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.CostDistributionIntermediate Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateRepository.ListDistributionIntermediateByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim ListDistribIntermediate = (From d In _context.CostDistributionIntermediate.Include("DistributionIntermediateDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
        For Each item As CostDistributionIntermediate In ListDistribIntermediate
            item.FullNameProductionCenter = (From t In _context.ProductionCenter Where t.Id = item.ProductionCenterId Select String.Concat(t.Code, " - ", t.Name)).FirstOrDefault()
        Next
        Return ListDistribIntermediate
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionIntermediateRepository.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim listData As List(Of CostDistributionIntermediate) = (From d In _context.CostDistributionIntermediate Where d.Year <= year AndAlso d.Month <= month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por el id del activo fijo, año y mes
    ''' </summary>
    Public Function GetDistributionIntermediateByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional tracking As Boolean = True) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediateByProductionCenterIdAndYearMonth
        If productionCenterId = 0 Then
            Throw New ArgumentNullException("productionCenterId")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As CostDistributionIntermediate = Nothing
        If tracking Then
            query = (From d In _context.CostDistributionIntermediate.Include("CostDistributionIntermediateDetail") Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        Else
            query = (From d In _context.CostDistributionIntermediate.AsNoTracking().Include("CostDistributionIntermediateDetail").AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

End Class