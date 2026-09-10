'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostDistributionManpower

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpowerById(id As Integer) As CostDistributionManpower

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpower(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionManpower)

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of List(Of CostDistributionManpower))

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    <OperationContract()>
    Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As ActionResult(Of CostDistributionManpower)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionManpower)

    ''' <summary>
    ''' Obtiene los datos necesarios para exportar a excel
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ExportExcelCostDistributionManPower_Result))

    ''' <summary>
    '''  Importa distribuciones de un periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionManpower(distributionManpower As Domain.Entities.CostDistributionManpower, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionManpower)

    ''' <summary>
    ''' Guarda masivamente los datos del form
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmMasive(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionManpower(distributionManpower As Domain.Entities.CostDistributionManpower, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface