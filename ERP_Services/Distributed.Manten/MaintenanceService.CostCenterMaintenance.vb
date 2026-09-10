'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 26-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Maintenance.Entities

Partial Class MaintanceService

    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostCenter(costCenter As Domain.Maintenance.Entities.CostCenter, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult Implements ICostCenterMaintenanceService.DeleteCostCenter
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.DeleteCostCenter(costCenter, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String, session As SessionValues) As Domain.Maintenance.Entities.CostCenter Implements ICostCenterMaintenanceService.GetCostCenter
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.GetCostCenter(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter(session As SessionValues) As List(Of Domain.Maintenance.Entities.CostCenter) Implements ICostCenterMaintenanceService.ListAllCostCenter
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.ListAllCostCenter()
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveCostCenter(costCenter As Domain.Maintenance.Entities.CostCenter, session As SessionValues, Optional idSequence As Long = 0) As ActionResult(Of CostCenter) Implements ICostCenterMaintenanceService.SaveCostCenter
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.SaveCostCenter(costCenter, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, tracking As Boolean, Session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Maintenance.Entities.CostCenter Implements ICostCenterMaintenanceService.GetCostCenterById
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.GetCostCenterById(id, tracking)
        End Using
    End Function

    Public Function ChangeStateCostCenter(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of CostCenter) Implements ICostCenterMaintenanceService.ChangeStateCostCenter
        Using costCenterAdmin As ICostCenterMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterMaintenanceAdminService)()
            Return costCenterAdmin.ChangeStateCostCenter(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
