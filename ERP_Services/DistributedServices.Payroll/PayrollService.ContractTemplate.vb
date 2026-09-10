'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Implements IPayrollContractTemplate

    ''' <summary>
    ''' Elimina una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractTemplate(contractTemplate As Domain.Payroll.Entities.ContractTemplate, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.ContractTemplate) Implements IPayrollContractTemplate.DeleteContractTemplate
        Using contractTemplateAdminService As IContractTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTemplateAdminService)()
            Return contractTemplateAdminService.DeleteContractTemplate(contractTemplate, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla de Contrati</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTemplate(code As String, session As SessionValues) As Domain.Payroll.Entities.ContractTemplate Implements IPayrollContractTemplate.GetContractTemplate
        Using contractTemplateAdminService As IContractTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTemplateAdminService)()
            Return contractTemplateAdminService.GetContractTemplate(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractTemplate(session As SessionValues) As List(Of Domain.Payroll.Entities.ContractTemplate) Implements IPayrollContractTemplate.ListAllContractTemplate
        Using contractTemplateAdminService As IContractTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTemplateAdminService)()
            Return contractTemplateAdminService.ListAllContractTemplate()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractTemplate(contractTemplate As Domain.Payroll.Entities.ContractTemplate, session As SessionValues) As Boolean Implements IPayrollContractTemplate.SaveContractTemplate
        Using contractTemplateAdminService As IContractTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTemplateAdminService)()
            Return contractTemplateAdminService.SaveContractTemplate(contractTemplate, session.AuditMessageWcf)
        End Using
    End Function
End Class
