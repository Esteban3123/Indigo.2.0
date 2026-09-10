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

Public Class CostDistributionManpowerRepository
    Inherits GenericRepository(Of CostDistributionManpower)
    Implements ICostDistributionManpowerRepository

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

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Function GetDistributionManpowerById(id As Integer) As CostDistributionManpower Implements ICostDistributionManpowerRepository.GetDistributionManpowerById
        Dim query = (From d In _context.CostDistributionManpower Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionManpower.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Function GetDistributionManpower(code As String) As CostDistributionManpower Implements ICostDistributionManpowerRepository.GetDistributionManpower
        Dim query = (From d In _context.CostDistributionManpower.Include("CostDistributionManpowerDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionManpower.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Obtener la distribución de mano de obra por periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="ManpowerType"></param>
    ''' <param name="EntityId"></param>
    ''' <returns></returns>
    Public Function SP_GetDistributionManpower(Year As Integer, Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As List(Of SP_GetDistributionManpower_Result) Implements ICostDistributionManpowerRepository.SP_GetDistributionManpower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetDistributionManpower(Year, Month, ManpowerType, EntityId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As CostDistributionManpower Implements ICostDistributionManpowerRepository.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId
        Dim query = (From d In _context.CostDistributionManpower.Include("CostDistributionManpowerDetail") Where d.Year = Year AndAlso d.Month = Month AndAlso d.ManpowerType = ManpowerType AndAlso d.EntityId = EntityId Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionManpower.AsNoTracking() Where d.Year = Year AndAlso d.Month = Month AndAlso d.ManpowerType = ManpowerType AndAlso d.EntityId = EntityId Select d).FirstOrDefault()
            If query.EntityCurrencyId IsNot Nothing Then
                query.CostDistributionManpowerDetail.ToList().ForEach(Sub(x)
                                                                          x.EntityCurrencyId = query.EntityCurrencyId
                                                                      End Sub)
            End If
            Return query
        Else
            Return New CostDistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionManpowerRepository.ListPeriodWithDataByMaximumPeriod
        Dim listData As List(Of CostDistributionManpower) = (From d In _context.CostDistributionManpower Where d.Year <= year AndAlso d.Month <= month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionManpower) Implements ICostDistributionManpowerRepository.ListDistributionManpowerByYearMonth
        Dim listCostDistributionManpower = (From d In _context.CostDistributionManpower Where d.Year = year AndAlso d.Month = month Select d).ToList()

        If listCostDistributionManpower IsNot Nothing AndAlso listCostDistributionManpower.Count > 0 Then
            'Dictionaries
            Dim dictionaryPositions As New Dictionary(Of Integer, String)
            'Individual Items
            Dim codeName As String = Nothing

            For Each item As CostDistributionManpower In listCostDistributionManpower
                If item.ThirdPartyId IsNot Nothing Then
                    item.ThirdPartyNitName = (From t In _context.ThirdParty Where t.Id = item.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                End If

                If item.PositionId IsNot Nothing Then
                    If Not dictionaryPositions.ContainsKey(item.PositionId) Then
                        codeName = (From p In _context.Position.AsNoTracking Where p.Id = item.PositionId Select p.Code + " - " + p.Name).FirstOrDefault
                        dictionaryPositions.Add(item.PositionId, codeName)
                    Else
                        codeName = dictionaryPositions(item.PositionId)
                    End If
                    item.PositionCodeName = codeName
                End If
            Next
        End If

        Return listCostDistributionManpower
    End Function

    ''' <summary>
    ''' Obtiene los datos necesarios para poder exportar a excel
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As List(Of SP_ExportExcelCostDistributionManPower_Result) Implements ICostDistributionManpowerRepository.SP_ExportExcelCostDistributionManPower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ExportExcelCostDistributionManPower(Year, Month).ToList
    End Function

    ''' <summary>
    ''' Guarda la distribucion de mano de obra
    ''' </summary>
    ''' <param name="CostDistributionManpowerXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveCostDistributionManpower(CostDistributionManpowerXml As String, CodeUser As String) As SP_SaveCostDistributionManpower_Result Implements ICostDistributionManpowerRepository.SP_SaveCostDistributionManpower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCostDistributionManpower(CostDistributionManpowerXml, CodeUser).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Importa distribuciones de mano de obra
    ''' </summary>
    Public Function SP_ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportXml As String, CodeUser As String) As SP_ImportCostDistributionManpower_Result Implements ICostDistributionManpowerRepository.SP_ImportCostDistributionManpower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportCostDistributionManpower(Year, Month, OperatingUnitId, ImportXml, CodeUser).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Guarda masivamente las distribuciones de mano de obra
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmMasiveCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveCostDistributionManpower_Result Implements ICostDistributionManpowerRepository.SP_ConfirmMasiveCostDistributionManpower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmMasiveCostDistributionManpower(Year, Month, OperatingUnitId, CodeUser).SingleOrDefault
    End Function

#End Region

End Class