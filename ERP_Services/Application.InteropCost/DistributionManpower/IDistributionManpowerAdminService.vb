'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDistributionManpowerAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    Function SaveDistributionManpower(ByVal distributionManpower As DistributionManpower, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Function DeleteDistributionManpower(ByVal distributionManpower As DistributionManpower, ByVal audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    Function UpdateStateDistributionManpower(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Function GetDistributionManpower(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Function GetDistributionManpowerByEmployeeIdAndYearMonth(ByVal employeeId As Integer, ByVal year As Integer, ByVal month As Integer) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Function GetDistributionManpowerById(id As Integer) As DistributionManpower

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionManpower)

    ''' <summary>
    ''' Guarda masivamente los datos del form
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmMasiveInteropCost(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ExportExcelInteropCostDistributionManPower(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result))

End Interface