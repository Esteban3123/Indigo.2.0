'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractModificationReason(contractModificationReason As Domain.Payroll.Entities.ContractModificationReason, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.ContractModificationReason) Implements IPayrollContractModificationReason.DeleteContractModificationReason
        Using contractAdminService As IContractModificationReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractModificationReasonAdminService)()
            Return contractAdminService.DeleteContractModificationReason(contractModificationReason, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    Public Function GetContractModificationReason(code As String, session As SessionValues) As Domain.Payroll.Entities.ContractModificationReason Implements IPayrollContractModificationReason.GetContractModificationReason
        Using contractAdminService As IContractModificationReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractModificationReasonAdminService)()
            Return contractAdminService.GetContractModificationReason(code)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractModificationReason(contractModificationReason As Domain.Payroll.Entities.ContractModificationReason, session As SessionValues) As Boolean Implements IPayrollContractModificationReason.SaveContractModificationReason
        Using contractAdminService As IContractModificationReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractModificationReasonAdminService)()
            Return contractAdminService.SaveContractModificationReason(contractModificationReason, session.AuditMessageWcf)
        End Using
    End Function
End Class
