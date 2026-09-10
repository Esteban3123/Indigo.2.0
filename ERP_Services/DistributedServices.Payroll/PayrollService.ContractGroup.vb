'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
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
    ''' Elimina un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Public Function DeleteContractGroup(contractGroup As Domain.Payroll.Entities.ContractGroup, session As SessionValues) As ActionMessageResult(Of ContractGroup) Implements IPayrollContractGroup.DeleteContractGroup
        Using contractGroupAdminService As IContractGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractGroupAdminService)()
            Return contractGroupAdminService.DeleteContractGroup(contractGroup, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de contrato
    ''' </summary>
    ''' <param name="code">Código de grupo de contrato</param>
    ''' <returns> Grupo de contrato</returns>
    Public Function GetContractGroup(code As String, session As SessionValues) As Domain.Payroll.Entities.ContractGroup Implements IPayrollContractGroup.GetContractGroup
        Using contractGroupAdminService As IContractGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractGroupAdminService)()
            Return contractGroupAdminService.GetContractGroup(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los Grupos de contratos
    ''' </summary>
    ''' <returns>Lista los grupos de contratos</returns>
    Public Function ListAllContractGroup(session As SessionValues) As List(Of Domain.Payroll.Entities.ContractGroup) Implements IPayrollContractGroup.ListAllContractGroup
        Using contractGroupAdminService As IContractGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractGroupAdminService)()
            Return contractGroupAdminService.ListAllContractGroup()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Public Function SaveContractGroup(contractGroup As Domain.Payroll.Entities.ContractGroup, session As SessionValues) As Boolean Implements IPayrollContractGroup.SaveContractGroup
        Using contractGroupAdminService As IContractGroupAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractGroupAdminService)()
            Return contractGroupAdminService.SaveContractGroup(contractGroup, session.AuditMessageWcf)
        End Using
    End Function
End Class
