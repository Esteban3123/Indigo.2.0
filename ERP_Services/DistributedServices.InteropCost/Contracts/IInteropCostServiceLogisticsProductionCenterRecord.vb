'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Juan F. Tamayo Puertas
' Created          : 2016-11-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IInteropCostServiceLogisticsProductionCenterRecord

#Region "Methods"

    <OperationContract()>
    Function GetLogisticsProductionCenterRecordById(id As Integer, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)
    <OperationContract()>
    Function GetLogisticsProductionCenterRecordByCode(code As String, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)
    <OperationContract()>
    Function SaveLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage, idSequence As Int64) As ActionResult(Of LogisticsProductionCenterRecord)
    <OperationContract()>
    Function DeleteLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord)
    <OperationContract()>
    Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result)
    <OperationContract()>
    Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result)
#End Region

End Interface
