'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    Public Function DeleteGroup(Group As Domain.Payroll.Entities.Group, session As SessionValues) As ActionMessageResult(Of Group) Implements IPayrollService.DeleteGroup
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.DeleteGroup(Group, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetGroup(code As String, session As SessionValues) As Domain.Payroll.Entities.Group Implements IPayrollService.GetGroup
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.GetGroup(code)
        End Using
    End Function

    Public Function ListAllGroups(session As SessionValues) As List(Of Domain.Payroll.Entities.Group) Implements IPayrollService.ListAllGroups
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.ListAllGroups()
        End Using
    End Function

    Public Function SaveGroups(Group As Domain.Payroll.Entities.Group, session As SessionValues) As Boolean Implements IPayrollService.SaveGroup
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.SaveGroup(Group, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetGroupById(id As String, session As SessionValues) As Domain.Payroll.Entities.Group Implements IPayrollGroup.GetGroupById
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.GetGroupById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene unos grupos filtrado por empresa
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupsByCompanyId(ByVal companyId As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Group) Implements IPayrollGroup.GetGroupsByCompanyId
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.GetGroupsByCompanyId(companyId)
        End Using
    End Function

    Public Function GetGroupLiquidationById(ByVal GroupId As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Group) Implements IPayrollGroup.GetGroupLiquidationById
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.GetGroupLiquidationById(GroupId)
        End Using
    End Function

    Public Function GroupChangeState(code As String, state As Boolean, session As SessionValues) As Boolean Implements IPayrollGroup.GroupChangeState
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.GroupChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ListGroupsByStatus(Status As Boolean, session As SessionValues) As List(Of Domain.Payroll.Entities.Group) Implements IPayrollService.ListGroupsByStatus
        Using GroupAdmin As IGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupAdminService)()
            Return GroupAdmin.ListGroupsByStatus(Status)
        End Using
    End Function

End Class
