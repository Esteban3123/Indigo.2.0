'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteWorkCenter(workCenter As Domain.Payroll.Entities.WorkCenter, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.WorkCenter) Implements IPayrollWorkCenter.DeleteWorkCenter
        Using workCenterAdmin As IWorkCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IWorkCenterAdminService)()
            Return workCenterAdmin.DeleteWorkCenter(workCenter, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Centro de Trabajo
    ''' </summary>
    ''' <param name="code">Código del Centro de Trabajo</param>
    ''' <returns>Centro de Trabajo</returns>
    ''' <remarks></remarks>
    Public Function GetWorkCenter(code As String, session As SessionValues) As Domain.Payroll.Entities.WorkCenter Implements IPayrollWorkCenter.GetWorkCenter
        Using workCenterAdmin As IWorkCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IWorkCenterAdminService)()
            Return workCenterAdmin.GetWorkCenter(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista de Todos los Centros de Trabajo
    ''' </summary>
    ''' <returns>Lista de Centros de Trabajo</returns>
    ''' <remarks></remarks>
    Public Function ListAllWorkCenter(session As SessionValues) As List(Of Domain.Payroll.Entities.WorkCenter) Implements IPayrollWorkCenter.ListAllWorkCenter
        Using workCenterAdmin As IWorkCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IWorkCenterAdminService)()
            Return workCenterAdmin.ListAllWorkCenter()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveWorkCenter(workCenter As Domain.Payroll.Entities.WorkCenter, session As SessionValues) As Boolean Implements IPayrollWorkCenter.SaveWorkCenter
        Using workCenterAdmin As IWorkCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IWorkCenterAdminService)()
            Return workCenterAdmin.SaveWorkCenter(workCenter, session.AuditMessageWcf)
        End Using
    End Function


End Class
