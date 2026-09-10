'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un tipo de contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo contribuyente</param>
    ''' <returns></returns>
    Public Function DeleteContributorType(contributorType As Domain.Payroll.Entities.ContributorType, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.ContributorType) Implements IPayrollContributorType.DeleteContributorType
        Using contributorTypeAdmin As IContributorTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeAdminService)()
            Return contributorTypeAdmin.DeleteContributorType(contributorType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Código del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    Public Function GetContributorType(code As String, session As SessionValues) As Domain.Payroll.Entities.ContributorType Implements IPayrollContributorType.GetContributorType
        Using contributorTypeAdmin As IContributorTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeAdminService)()
            Return contributorTypeAdmin.GetContributorType(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los tipos de contribuyentes
    ''' </summary>
    ''' <returns>Lista de tipos de contribuyentes</returns>
    Public Function ListAllContributorType(session As SessionValues) As List(Of Domain.Payroll.Entities.ContributorType) Implements IPayrollContributorType.ListAllContributorType
        Using contributorTypeAdmin As IContributorTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeAdminService)()
            Return contributorTypeAdmin.ListAllContributorType()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un tipo contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo Contribuyente</param>
    ''' <returns></returns>
    Public Function SaveContributorType(contributorType As Domain.Payroll.Entities.ContributorType, session As SessionValues) As Boolean Implements IPayrollContributorType.SaveContributorType
        Using contributorTypeAdmin As IContributorTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorTypeAdminService)()
            Return contributorTypeAdmin.SaveContributorType(contributorType, session.AuditMessageWcf)
        End Using
    End Function
End Class
