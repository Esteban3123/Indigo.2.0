'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDistributionManpowerAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Function GetDistributionManpowerById(id As Integer) As CostDistributionManpower

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Function GetDistributionManpower(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionManpower)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por periodo y de ser necesario un tercero
    ''' </summary>
    Function GetDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of List(Of CostDistributionManpower))

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por registro origen
    ''' </summary>
    Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of CostDistributionManpower)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionManpower)

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result))

    ''' <summary>
    '''  Importa distribuciones de un periodo
    ''' </summary>
    Function ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    Function SaveDistributionManpower(ByVal distributionManpower As CostDistributionManpower, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostDistributionManpower)

    ''' <summary>
    ''' Guarda masivamente los datos del form
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmMasive(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Function DeleteDistributionManpower(ByVal distributionManpower As CostDistributionManpower, ByVal audit As AuditMessage) As ActionResult

End Interface
