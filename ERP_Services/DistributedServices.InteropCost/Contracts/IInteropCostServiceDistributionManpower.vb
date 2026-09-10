'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceDistributionManpower

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionManpower(distributionManpower As DistributionManpower, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionManpower(distributionManpower As DistributionManpower, audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionManpower(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpower(code As String, audit As AuditMessage) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpowerByEmployeeIdAndYearMonth(ByVal employeeId As Integer, ByVal year As Integer, ByVal month As Integer) As ActionResult(Of DistributionManpower)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpowerById(id As Integer) As DistributionManpower

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionManpower)

    ''' <summary>
    ''' Guarda masivamente los datos del form
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmMasiveInteropCost(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_ExportExcelInteropCostDistributionManPower(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ExportExcelInteropCostDistributionManPower_Result))

End Interface