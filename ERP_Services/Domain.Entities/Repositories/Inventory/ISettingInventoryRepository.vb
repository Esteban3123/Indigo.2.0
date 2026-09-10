'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

Public Interface ISettingInventoryRepository
    Inherits IRepository(Of SettingInventory)
    ''' <summary>
    ''' obtiene una configuracion por año y mes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingInventory(OperatingUnitId As Integer) As SettingInventory

    ''' <summary>
    ''' Obtiene un parametro de inventario por unidad operativa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventorySettingsRegister(OperatingUnitId As Integer) As SettingInventory

    ''' <summary>
    ''' Realiza el proceso de cierre de inventario
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ClosedMonthInventory(MonthClosed As Integer, YearClosed As Integer, CodeUser As String) As SP_ClosedMonthInventory_Result

    ''' <summary>
    ''' Retorna la lista de los documentos de inventario que estan sin confirmar
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_VerifiyHasConfirmAllDocuments(MonthClosed As Integer, YearClosed As Integer) As List(Of SP_VerifiyHasConfirmAllDocuments_Result)

    ''' <summary>
    ''' Retorna la conciliación de los modulos de inventario y contabilidad
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConciliationInventoryVsAccounting(MonthClosed As Integer, YearClosed As Integer) As Task(Of List(Of SP_ConciliationInventoryVsAccounting_Result))

    ''' <summary>
    ''' Retorna la variacion del costo promedio de los productos
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ClosedMonthVariationCost(MonthClosed As Integer, YearClosed As Integer) As List(Of SP_ClosedMonthVariationCost_Result)

    ''' <summary>
    ''' obtiene una configuracion por unidad operativa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingInventoryByOperatingUnitId(OperatingUnitId As Integer) As SettingInventory

    ''' <summary>
    ''' Obtiene el reporte de inventario valorizado
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="range"></param>
    ''' <returns></returns>
    Function GetSP_ReportValuedInventory(filters As String, range As String) As List(Of SP_ReportValuedInventory_Result)



    ''' <summary>
    ''' Retorna la lista de los documentos de inventario que estan sin confirmar
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GetMonthlyClosureSummary(YearClosed As Integer, MonthClosed As Integer) As List(Of SP_GetMonthlyClosureSummary_Result)


    ''' <summary>
    ''' Obtiene cabecera del cierre por periodo
    ''' </summary>
    ''' <param name="yearClosed"></param>
    ''' <param name="monthClosed"></param>
    ''' <returns></returns>
    Function GetMonthlyClosed(yearClosed As Integer, monthClosed As Integer) As ClosedMonthInventoryHeader

    ''' <summary>
    ''' Obtiene el primer registro de parámetros de inventario
    ''' * ESTE MÉTODO SE USA ÚNICAMENTE PARA INTEGRACIÓN, DEBIDO A QUE NO SE ENVIAN DATOS DESDE EL CLIENTE Y NO SE PUEDE OBTENER LA UNIDAD OPERATIVA*
    ''' </summary>
    ''' <returns></returns>
    Function GetFirstOrDefaultSettingInventory() As SettingInventory
End Interface
