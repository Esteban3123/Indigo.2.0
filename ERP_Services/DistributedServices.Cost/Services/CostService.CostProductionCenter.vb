Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostProductionCenter

    Public Function DeleteCostProductionCenter(productionCenter As Domain.Entities.CostProductionCenter, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostProductionCenter.DeleteCostProductionCenter
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.DeleteCostProductionCenter(productionCenter, audit)
        End Using
        'Return _costProductionCenterAdminService.DeleteCostProductionCenter(productionCenter, audit)
    End Function

    Public Function GetCostProductionCenter(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter) Implements ICostServiceCostProductionCenter.GetCostProductionCenter
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.GetCostProductionCenter(code, audit)
        End Using
        'Return _costProductionCenterAdminService.GetCostProductionCenter(code, audit)
    End Function

    Public Function GetCostProductionCenterById(id As Integer) As Domain.Entities.CostProductionCenter Implements ICostServiceCostProductionCenter.GetCostProductionCenterById
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.GetCostProductionCenterById(id)
        End Using
        'Return _costProductionCenterAdminService.GetCostProductionCenterById(id)
    End Function

    Public Function ListCostProductionCenter() As List(Of Domain.Entities.CostProductionCenter) Implements ICostServiceCostProductionCenter.ListCostProductionCenter
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.ListCostProductionCenter()
        End Using
        'Return _costProductionCenterAdminService.ListCostProductionCenter()
    End Function

    Public Function SaveCostProductionCenter(productionCenter As Domain.Entities.CostProductionCenter, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter) Implements ICostServiceCostProductionCenter.SaveCostProductionCenter
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.SaveCostProductionCenter(productionCenter, audit, idSequence)
        End Using
        'Return _costProductionCenterAdminService.SaveCostProductionCenter(productionCenter, audit, idSequence)
    End Function

    Public Function UpdateStateCostProductionCenter(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter) Implements ICostServiceCostProductionCenter.UpdateStateCostProductionCenter
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.UpdateStateCostProductionCenter(code, state, audit)
        End Using
        'Return _costProductionCenterAdminService.UpdateStateCostProductionCenter(code, state, audit)
    End Function

    Public Function GetProductionCenterByCostCenterId(costCenterId As Integer) As Domain.Entities.CostProductionCenter Implements ICostServiceCostProductionCenter.GetProductionCenterByCostCenterId
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.GetProductionCenterByCostCenterId(costCenterId)
        End Using
        'Return _costProductionCenterAdminService.GetProductionCenterByCostCenterId(costCenterId)
    End Function

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResult_Result) Implements ICostServiceCostProductionCenter.ListReportOperatingResult
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.ListReportOperatingResult(InitialMonth, EndMonth, Year, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._costProductionCenterAdminService.ListReportOperatingResult(InitialMonth, EndMonth, Year, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result) Implements ICostServiceCostProductionCenter.ListReportOperatingResultsByOrganizationalStructure
        Using service As ICostProductionCenterAdminService = DistributedServices.Cost.Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._costProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements ICostServiceCostProductionCenter.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Using service As ICostProductionCenterAdminService = DistributedServices.Cost.Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._costProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function

    Public Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ByVal ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result) Implements ICostServiceCostProductionCenter.ListReportCostCostMeasurementUnit
        Using service As ICostProductionCenterAdminService = Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.ListReportCostCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId)
        End Using
        'Return Me._costProductionCenterAdminService.ListReportCostCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_CostReportOperatingResultProductionCenter realizado para cargar los datos del reporte de Estructura Organizacional ProductionCenter de costo nativo
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="CodePCenterIni"></param>
    ''' <param name="CodePCenterFin"></param>
    ''' <param name="OrderBy"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetCostListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, OrderBy As Integer, Session As SessionValues) As DataSet Implements ICostServiceCostProductionCenter.GetCostListReportOperatingProductionCenter
        Using service As ICostProductionCenterAdminService = DistributedServices.Cost.Container.Current.Resolve(Of ICostProductionCenterAdminService)()
            Return service.GetCostListReportOperatingProductionCenter(InitialMonth, EndMonth, Year, CodePCenterIni, CodePCenterFin, OrderBy, Session)
        End Using
    End Function
End Class
