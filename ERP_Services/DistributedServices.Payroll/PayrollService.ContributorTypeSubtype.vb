'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService

    ''' <summary>
    ''' Lista todas las combinaciones Tipo+Subtipo de cotizante
    ''' </summary>
    Public Function ListAllContributorTypeSubtype(session As SessionValues) As List(Of Domain.Payroll.Entities.ContributorTypeSubtype) Implements IPayrollContributorTypeSubtype.ListAllContributorTypeSubtype
        Using svc As IContributorTypeSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeSubtypeAdminService)()
            Return svc.ListAllContributorTypeSubtype()
        End Using
    End Function

    ''' <summary>
    ''' Lista los subtipos válidos para un Tipo de cotizante
    ''' </summary>
    Public Function ListByContributorTypeId(contributorTypeId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.ContributorTypeSubtype) Implements IPayrollContributorTypeSubtype.ListByContributorTypeId
        Using svc As IContributorTypeSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeSubtypeAdminService)()
            Return svc.ListByContributorTypeId(contributorTypeId)
        End Using
    End Function


    ''' <summary>
    ''' Guarda una nueva combinacion Tipo+Subtipo de cotizante
    ''' </summary>
    Public Function SaveContributorTypeSubtype(contributorTypeSubtype As Domain.Payroll.Entities.ContributorTypeSubtype, session As SessionValues) As Boolean Implements IPayrollContributorTypeSubtype.SaveContributorTypeSubtype
        Using svc As IContributorTypeSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeSubtypeAdminService)()
            Return svc.SaveContributorTypeSubtype(contributorTypeSubtype)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una combinacion Tipo+Subtipo de cotizante por Id
    ''' </summary>
    Public Function DeleteContributorTypeSubtype(id As Integer, session As SessionValues) As Boolean Implements IPayrollContributorTypeSubtype.DeleteContributorTypeSubtype
        Using svc As IContributorTypeSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeSubtypeAdminService)()
            Return svc.DeleteContributorTypeSubtype(id)
        End Using
    End Function

End Class
