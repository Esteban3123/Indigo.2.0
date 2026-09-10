'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostDistributionManpowerRepository
    Inherits IRepository(Of CostDistributionManpower)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Function GetDistributionManpowerById(id As Integer) As CostDistributionManpower

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Function GetDistributionManpower(ByVal code As String) As CostDistributionManpower

    ''' <summary>
    ''' Obtener la distribución de mano de obra por periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="ManpowerType"></param>
    ''' <param name="EntityId"></param>
    ''' <returns></returns>
    Function SP_GetDistributionManpower(Year As Integer, Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As List(Of SP_GetDistributionManpower_Result)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por registro origen
    ''' </summary>
    Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(Year As Integer, Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As CostDistributionManpower

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionManpower)

    ''' <summary>
    ''' Me trae los datos necesarios para exportarlos a excel
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As List(Of SP_ExportExcelCostDistributionManPower_Result)

    ''' <summary>
    ''' Importa los movimientos de otro periodo
    ''' </summary>
    Function SP_ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportXml As String, CodeUser As String) As SP_ImportCostDistributionManpower_Result

    ''' <summary>
    ''' Guarda la distribucion de mano de obra
    ''' </summary>
    ''' <param name="CostDistributionManpowerXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveCostDistributionManpower(CostDistributionManpowerXml As String, CodeUser As String) As SP_SaveCostDistributionManpower_Result

    ''' <summary>
    ''' Guarda masivamente las distribuciones de mano de obra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmMasiveCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveCostDistributionManpower_Result

End Interface
