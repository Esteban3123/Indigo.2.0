Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceLogisticsProductionCenterRecord

    Public Function DeleteCostLogisticsProductionCenterRecord(entity As Domain.Entities.CostLogisticsProductionCenterRecord, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostLogisticsProductionCenterRecord) Implements ICostServiceLogisticsProductionCenterRecord.DeleteCostLogisticsProductionCenterRecord
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.DeleteCostLogisticsProductionCenterRecord(entity, audit)
        End Using
        'Return _costLogisticsProductionCenterRecordAdminService.DeleteCostLogisticsProductionCenterRecord(entity, audit)
    End Function

    Public Function GetCostLogisticsProductionCenterRecordByCode(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostLogisticsProductionCenterRecord) Implements ICostServiceLogisticsProductionCenterRecord.GetCostLogisticsProductionCenterRecordByCode
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.GetCostLogisticsProductionCenterRecordByCode(code, audit)
        End Using
        'Return _costLogisticsProductionCenterRecordAdminService.GetCostLogisticsProductionCenterRecordByCode(code, audit)
    End Function

    Public Function GetCostLogisticsProductionCenterRecordById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostLogisticsProductionCenterRecord) Implements ICostServiceLogisticsProductionCenterRecord.GetCostLogisticsProductionCenterRecordById
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.GetCostLogisticsProductionCenterRecordById(id, audit)
        End Using
        'Return _costLogisticsProductionCenterRecordAdminService.GetCostLogisticsProductionCenterRecordById(id, audit)
    End Function

    Public Function SaveCostLogisticsProductionCenterRecord(entity As Domain.Entities.CostLogisticsProductionCenterRecord, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostLogisticsProductionCenterRecord) Implements ICostServiceLogisticsProductionCenterRecord.SaveCostLogisticsProductionCenterRecord
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.SaveCostLogisticsProductionCenterRecord(entity, audit, idSequence)
        End Using
        'Return _costLogisticsProductionCenterRecordAdminService.SaveCostLogisticsProductionCenterRecord(entity, audit, idSequence)
    End Function

    Public Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_CostReportConsolidatedCCLogisticA_Result) Implements ICostServiceLogisticsProductionCenterRecord.ListReportConsolidatedCCLogistic
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
        End Using
        'Return Me._costLogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
    End Function

    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_CostReportConsolidatedCCLogisticB_Result) Implements ICostServiceLogisticsProductionCenterRecord.ListReportConsolidatedCCLogisticB
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
        End Using
        'Return Me._costLogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
    End Function

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
    Public Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues) As DataSet Implements ICostServiceLogisticsProductionCenterRecord.GetListReportCostMeasurementUnit
        Using service As ICostLogisticsProductionCenterRecordAdminService = Container.Current.Resolve(Of ICostLogisticsProductionCenterRecordAdminService)()
            Return service.GetListReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId, Session)
        End Using
        'Return Me._costLogisticsProductionCenterRecordAdminService.GetListReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId, Session)
    End Function
End Class
