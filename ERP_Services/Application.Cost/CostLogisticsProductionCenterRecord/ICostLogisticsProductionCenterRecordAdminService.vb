'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICostLogisticsProductionCenterRecordAdminService
    Inherits IDisposable

#Region "Methods"

    Function GetCostLogisticsProductionCenterRecordById(ByVal id As Int32, audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord)

    Function GetCostLogisticsProductionCenterRecordByCode(ByVal code As String, audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord)

    Function SaveCostLogisticsProductionCenterRecord(ByVal entity As CostLogisticsProductionCenterRecord, ByVal audit As AuditMessage, idSequense As Int64) As ActionResult(Of CostLogisticsProductionCenterRecord)

    Function DeleteCostLogisticsProductionCenterRecord(ByVal entity As CostLogisticsProductionCenterRecord, ByVal audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord)

    Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_CostReportConsolidatedCCLogisticA_Result)

    Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_CostReportConsolidatedCCLogisticB_Result)

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure spReportCostMeasurementUnit
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues) As DataSet
#End Region

End Interface
