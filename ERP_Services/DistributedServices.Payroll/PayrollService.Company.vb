'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 25-04-2013
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
    ''' Elimina una Compañia
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteCompany(company As Domain.Payroll.Entities.Company, session As SessionValues) As ActionMessageResult(Of Company) Implements IPayrollCompany.DeleteCompany
        Using companyAdmin As ICompanyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICompanyAdminService)()
            Return companyAdmin.DeleteCompany(company, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Compañia en Específico
    ''' </summary>
    ''' <param name="code">Código de la Compañía</param>
    ''' <returns>Compañia</returns>
    Public Function GetCompany(nit As String, session As SessionValues) As Domain.Payroll.Entities.Company Implements IPayrollCompany.GetCompany
        Using companyAdmin As ICompanyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICompanyAdminService)()
            Return companyAdmin.GetCompany(nit)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todas las Compañias
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    ''' <remarks></remarks>
    Public Function ListAllCompany(session As SessionValues) As List(Of Domain.Payroll.Entities.Company) Implements IPayrollCompany.ListAllCompany
        Using companyAdmin As ICompanyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICompanyAdminService)()
            Return companyAdmin.ListAllCompany()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una Compañía
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveCompany(company As Domain.Payroll.Entities.Company, session As SessionValues) As Boolean Implements IPayrollCompany.SaveCompany
        Using companyAdmin As ICompanyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICompanyAdminService)()
            Return companyAdmin.SaveCompany(company, session.AuditMessageWcf)
        End Using
    End Function

End Class
