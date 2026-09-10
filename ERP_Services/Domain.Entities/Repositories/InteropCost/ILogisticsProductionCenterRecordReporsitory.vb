'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Juan F. Tamayo Puertas
' Created          : 2016-11-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ILogisticsProductionCenterRecordReporsitory
    Inherits IRepository(Of LogisticsProductionCenterRecord)

#Region "Methods"

    Function GetLogisticsProductionCenterRecordById(ByVal id As Int32) As LogisticsProductionCenterRecord

    Function GetLogisticsProductionCenterRecordByCode(ByVal code As String) As LogisticsProductionCenterRecord

    Function GetLogisticsProductionCenterPendingImport(ByVal ProductionCenterId As Integer, ByVal Year As Integer, ByVal month As Integer) As List(Of LogisticsProductionCenterRecordDetail)

    Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result)

    Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result)
#End Region

End Interface
