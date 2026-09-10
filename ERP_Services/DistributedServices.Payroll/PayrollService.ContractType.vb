'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractType(contractType As Domain.Payroll.Entities.ContractType, session As SessionValues) As Boolean Implements IPayrollContractType.DeleteContractType
        Using contractTypeAdminService As IContractTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTypeAdminService)()
            Return contractTypeAdminService.DeleteContractType(contractType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractType(code As String, session As SessionValues) As Domain.Payroll.Entities.ContractType Implements IPayrollContractType.GetContractType
        Using contractTypeAdminService As IContractTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTypeAdminService)()
            Return contractTypeAdminService.GetContractType(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los Tipos de Contrato
    ''' </summary>
    ''' <returns>Tipos de Contratos</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractType(session As SessionValues) As List(Of Domain.Payroll.Entities.ContractType) Implements IPayrollContractType.ListAllContractType
        Using contractTypeAdminService As IContractTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTypeAdminService)()
            Return contractTypeAdminService.ListAllContractType()
        End Using
    End Function

    ''' <summary>
    ''' Almacena un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractType(contractType As Domain.Payroll.Entities.ContractType, session As SessionValues) As Boolean Implements IPayrollContractType.SaveContractType
        Using contractTypeAdminService As IContractTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTypeAdminService)()
            Return contractTypeAdminService.SaveContractType(contractType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="ID">ID del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTypeById(ID As String, session As SessionValues) As Domain.Payroll.Entities.ContractType Implements IPayrollContractType.GetContractTypeById
        Using contractTypeAdminService As IContractTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractTypeAdminService)()
            Return contractTypeAdminService.GetContractTypeById(ID)
        End Using
    End Function
End Class
