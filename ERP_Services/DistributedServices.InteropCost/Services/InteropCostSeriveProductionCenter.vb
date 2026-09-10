'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostSeriveProductionCenter

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Function DeleteProductionCenter(productionCenter As ProductionCenter, audit As AuditMessage) As ActionResult Implements IInteropCostSeriveProductionCenter.DeleteProductionCenter
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.DeleteProductionCenter(productionCenter, audit)
        End Using
        'Return Me._productionCenterAdminService.DeleteProductionCenter(productionCenter, audit)
    End Function

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetProductionCenter(code As String, audit As AuditMessage) As ActionResult(Of ProductionCenter) Implements IInteropCostSeriveProductionCenter.GetProductionCenter
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.GetProductionCenter(code, audit)
        End Using
        'Return Me._productionCenterAdminService.GetProductionCenter(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProductionCenterById(id As Integer) As ProductionCenter Implements IInteropCostSeriveProductionCenter.GetProductionCenterById
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.GetProductionCenterById(id)
        End Using
        'Return Me._productionCenterAdminService.GetProductionCenterById(id)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Function SaveProductionCenter(productionCenter As ProductionCenter, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ProductionCenter) Implements IInteropCostSeriveProductionCenter.SaveProductionCenter
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.SaveProductionCenter(productionCenter, audit, idSequence)
        End Using
        'Return Me._productionCenterAdminService.SaveProductionCenter(productionCenter, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state1.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateProductionCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionCenter) Implements IInteropCostSeriveProductionCenter.UpdateStateProductionCenter
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.UpdateStateProductionCenter(code, state, audit)
        End Using
        'Return Me._productionCenterAdminService.UpdateStateProductionCenter(code, state, audit)
    End Function

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenter() As List(Of ProductionCenter) Implements IInteropCostSeriveProductionCenter.ListProductionCenter
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListProductionCenter()
        End Using
        'Return Me._productionCenterAdminService.ListProductionCenter()
    End Function

    Public Function GetProductionCenterByCostCenterOid(costCenterOid As Integer) As ProductionCenter Implements IInteropCostSeriveProductionCenter.GetProductionCenterByCostCenterOid
        Using service As IProductionCenterAdminService = Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.GetProductionCenterByCostCenterOid(costCenterOid)
        End Using
        'Return Me._productionCenterAdminService.GetProductionCenterByCostCenterOid(costCenterOid)
    End Function

    ''' <summary>
    ''' Listado Reporte Resumido Operaciones de Costo
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="InitialCodeCenter"></param>
    ''' <param name="EndCodeCenter"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListrptResultProductionCostsExpenses(InitialMonth As Integer, EndMonth As Integer, Year As Integer, InitialCodeCenter As String, EndCodeCenter As String, Container As String) As List(Of SP_ReportResultProductionCostsExpenses_Result) Implements IInteropCostSeriveProductionCenter.ListrptResultProductionCostsExpenses
        Using service As IProductionCenterAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListrptResultProductionCostsExpenses(InitialMonth, EndMonth, Year, InitialCodeCenter, EndCodeCenter, Container)
        End Using
        'Return Me._productionCenterAdminService.ListrptResultProductionCostsExpenses(InitialMonth, EndMonth, Year, InitialCodeCenter, EndCodeCenter, Container)
    End Function

    ''' <summary>
    ''' Listado Reporte Detallado Operaciones de Costo
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="InitialCodeCenter"></param>
    ''' <param name="EndCodeCenter"></param>
    ''' <param name="Container"></param>
    ''' <param name="DetailType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListrptResultProductionCostsExpensesDetail(DateStart As Date, DateEnd As Date, InitialCodeCenter As String, EndCodeCenter As String, Container As String, DetailType As Integer) As List(Of SP_ReportResultProductionCostsExpensesDetail_Result) Implements IInteropCostSeriveProductionCenter.ListrptResultProductionCostsExpensesDetail
        Using service As IProductionCenterAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListrptResultProductionCostsExpensesDetail(DateStart, DateEnd, InitialCodeCenter, EndCodeCenter, Container, DetailType)
        End Using
        'Return Me._productionCenterAdminService.ListrptResultProductionCostsExpensesDetail(DateStart, DateEnd, InitialCodeCenter, EndCodeCenter, Container, DetailType)
    End Function

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResult_Result) Implements IInteropCostSeriveProductionCenter.ListReportOperatingResult
        Using service As IProductionCenterAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListReportOperatingResult(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._productionCenterAdminService.ListReportOperatingResult(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result) Implements IInteropCostSeriveProductionCenter.ListReportOperatingResultsByOrganizationalStructure
        Using service As IProductionCenterAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._productionCenterAdminService.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements IInteropCostSeriveProductionCenter.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Using service As IProductionCenterAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IProductionCenterAdminService)()
            Return service.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        End Using
        'Return Me._productionCenterAdminService.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
    End Function
End Class