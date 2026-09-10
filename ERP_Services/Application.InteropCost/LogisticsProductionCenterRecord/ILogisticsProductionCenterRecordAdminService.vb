'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ILogisticsProductionCenterRecordAdminService
    Inherits IDisposable

#Region "Methods"

    Function GetLogisticsProductionCenterRecordById(ByVal id As Int32, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)

    Function GetLogisticsProductionCenterRecordByCode(ByVal code As String, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)

    Function SaveLogisticsProductionCenterRecord(ByVal entity As LogisticsProductionCenterRecord, ByVal audit As AuditMessage, idSequense As Int64) As ActionResult(Of LogisticsProductionCenterRecord)

    Function DeleteLogisticsProductionCenterRecord(ByVal entity As LogisticsProductionCenterRecord, ByVal audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)

    Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result)

    Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result)
#End Region

End Interface