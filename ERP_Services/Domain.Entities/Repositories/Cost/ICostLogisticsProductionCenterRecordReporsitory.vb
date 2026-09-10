'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ICostLogisticsProductionCenterRecordReporsitory
    Inherits IRepository(Of CostLogisticsProductionCenterRecord)

#Region "Methods"

    Function GetCostLogisticsProductionCenterRecordById(ByVal id As Int32) As CostLogisticsProductionCenterRecord

    Function GetCostLogisticsProductionCenterRecordByCode(ByVal code As String) As CostLogisticsProductionCenterRecord

    Function GetCostLogisticsProductionCenterPendingImport(ByVal ProductionCenterId As Integer, ByVal Year As Integer, ByVal month As Integer) As List(Of CostLogisticsProductionCenterRecordDetail)

    Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_CostReportConsolidatedCCLogisticA_Result)

    Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_CostReportConsolidatedCCLogisticB_Result)

#End Region

End Interface
