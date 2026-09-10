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
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class InteropCostService
    Implements IInteropCostServiceLogisticsProductionCenterRecord

#Region "Methods"

    Public Function DeleteLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements IInteropCostServiceLogisticsProductionCenterRecord.DeleteLogisticsProductionCenterRecord
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.DeleteLogisticsProductionCenterRecord(entity, audit)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.DeleteLogisticsProductionCenterRecord(entity, audit)
    End Function

    Public Function GetLogisticsProductionCenterRecordByCode(code As String, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements IInteropCostServiceLogisticsProductionCenterRecord.GetLogisticsProductionCenterRecordByCode
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.GetLogisticsProductionCenterRecordByCode(code, audit)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.GetLogisticsProductionCenterRecordByCode(code, audit)
    End Function

    Public Function GetLogisticsProductionCenterRecordById(id As Integer, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements IInteropCostServiceLogisticsProductionCenterRecord.GetLogisticsProductionCenterRecordById
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.GetLogisticsProductionCenterRecordById(id, audit)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.GetLogisticsProductionCenterRecordById(id, audit)
    End Function

    Public Function SaveLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage, idSequence As Int64) As ActionResult(Of LogisticsProductionCenterRecord) Implements IInteropCostServiceLogisticsProductionCenterRecord.SaveLogisticsProductionCenterRecord
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.SaveLogisticsProductionCenterRecord(entity, audit, idSequence)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.SaveLogisticsProductionCenterRecord(entity, audit, idSequence)
    End Function

    Public Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result) Implements IInteropCostServiceLogisticsProductionCenterRecord.ListReportConsolidatedCCLogistic
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
    End Function

    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result) Implements IInteropCostServiceLogisticsProductionCenterRecord.ListReportConsolidatedCCLogisticB
        Using service As ILogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ILogisticsProductionCenterRecordAdminService)()
            Return service.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
        End Using
        'Return Me._logisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
    End Function
#End Region

End Class
