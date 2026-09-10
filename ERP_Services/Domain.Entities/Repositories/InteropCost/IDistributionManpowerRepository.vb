'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IDistributionManpowerRepository
    Inherits IRepository(Of DistributionManpower)

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Function GetDistributionManpower(ByVal code As String) As DistributionManpower

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Function GetDistributionManpowerById(id As Integer) As DistributionManpower

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Function GetDistributionManpowerByEmployeeIdAndYearMonth(ByVal employeeId As Integer, ByVal year As Integer, ByVal month As Integer, Optional ByVal tracking As Boolean = True) As DistributionManpower

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionManpower)

    ''' <summary>
    ''' Guarda masivamente las distribuciones de mano de obra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmMasiveInteropCostDistributionManpower(XmlObject As String, Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveInteropCostDistributionManpower_Result

    ''' <summary>
    ''' Me trae los datos necesarios para exportarlos a excel
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ExportExcelInteropCostDistributionManPower(XmlObject As String, Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As List(Of SP_ExportExcelInteropCostDistributionManPower_Result)

End Interface