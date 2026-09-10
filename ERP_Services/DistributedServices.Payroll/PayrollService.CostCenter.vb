'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 20-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostCenter(costCenter As Domain.Payroll.Entities.CostCenter, session As SessionValues) As ActionMessageResult(Of CostCenter) Implements IPayrollCostCenter.DeleteCostCenter
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
            Return costCenterAdmin.DeleteCostCenter(costCenter, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String, session As SessionValues) As Domain.Payroll.Entities.CostCenter Implements IPayrollCostCenter.GetCostCenter
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
            Return costCenterAdmin.GetCostCenter(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter(session As SessionValues) As List(Of Domain.Payroll.Entities.CostCenter) Implements IPayrollCostCenter.ListAllCostCenter
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
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
    Public Function SaveCostCenter(costCenter As Domain.Payroll.Entities.CostCenter, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.CostCenter) Implements IPayrollCostCenter.SaveCostCenter
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
            Return costCenterAdmin.SaveCostCenter(costCenter, session.AuditMessageWcf, idSequense)
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
    Public Function GetCostCenterById(id As Integer, tracking As Boolean, Session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Payroll.Entities.CostCenter Implements IPayrollCostCenter.GetCostCenterById
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
            Return costCenterAdmin.GetCostCenterById(id, tracking)
        End Using
    End Function

    Public Function ChangeStateCostCenter(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Payroll.Entities.CostCenter) Implements IPayrollCostCenter.ChangeStateCostCenter
        Using costCenterAdmin As ICostCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostCenterAdminService)()
            Return costCenterAdmin.ChangeStateCostCenter(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
