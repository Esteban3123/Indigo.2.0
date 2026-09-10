'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Ernesto Cordoba
' Created          : 16/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function ChangeStateContractMinimumWage(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractMinimumWage) Implements IContractServiceContractMinimumWage.ChangeStateContractMinimumWage
        Using service As IContractMinimumWageAdminService = Container.Current.Resolve(Of IContractMinimumWageAdminService)()
            Return service.ChangeStateContractMinimumWage(code, state, audit)
        End Using
        'Return Me._contractMinimumWageAdminService.ChangeStateContractMinimumWage(code, state, audit)
    End Function

    ''' <summary>
    ''' Elimina un salirio
    ''' </summary>
    ''' <param name="ContractMinimumWage"></param>
    ''' <returns></returns>
    Public Function DeleteContractMinimumWage(ContractMinimumWage As Domain.Entities.ContractMinimumWage, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceContractMinimumWage.DeleteContractMinimumWage
        Using service As IContractMinimumWageAdminService = Container.Current.Resolve(Of IContractMinimumWageAdminService)()
            Return service.DeleteContractMinimumWage(ContractMinimumWage, audit)
        End Using
        'Return Me._contractMinimumWageAdminService.DeleteContractMinimumWage(ContractMinimumWage, audit)
    End Function

    ''' <summary>
    ''' Obtiene un salario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetContractMinimumWage(code As String, audit As AuditMessage) As Domain.Entities.ContractMinimumWage Implements IContractServiceContractMinimumWage.GetContractMinimumWage
        Using service As IContractMinimumWageAdminService = Container.Current.Resolve(Of IContractMinimumWageAdminService)()
            Return service.GetContractMinimumWage(code, audit)
        End Using
        'Return Me._contractMinimumWageAdminService.GetContractMinimumWage(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un salario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetContractMinimumWageById(id As Integer) As Domain.Entities.ContractMinimumWage Implements IContractServiceContractMinimumWage.GetContractMinimumWageById
        Using service As IContractMinimumWageAdminService = Container.Current.Resolve(Of IContractMinimumWageAdminService)()
            Return service.GetContractMinimumWageById(id)
        End Using
        'Return Me._contractMinimumWageAdminService.GetContractMinimumWageById(id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un salario
    ''' </summary>
    ''' <param name="ContractMinimumWage"></param>
    ''' <returns></returns>
    Public Function SaveContractMinimumWage(ContractMinimumWage As Domain.Entities.ContractMinimumWage, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractMinimumWage) Implements IContractServiceContractMinimumWage.SaveContractMinimumWage
        Using service As IContractMinimumWageAdminService = Container.Current.Resolve(Of IContractMinimumWageAdminService)()
            Return service.SaveContractMinimumWage(ContractMinimumWage, audit, idSequense)
        End Using
        'Return Me._contractMinimumWageAdminService.SaveContractMinimumWage(ContractMinimumWage, audit, idSequense)
    End Function
End Class
