'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 16/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IContractServiceContractMinimumWage
    ''' <summary>
    ''' Guarda o Actualiza un salario
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractMinimumWage(ContractMinimumWage As Domain.Entities.ContractMinimumWage, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractMinimumWage)
    ''' <summary>
    ''' Elimina un salirio
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractMinimumWage(ContractMinimumWage As Domain.Entities.ContractMinimumWage, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' Obtiene un salario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractMinimumWage(code As String, audit As AuditMessage) As Domain.Entities.ContractMinimumWage
    ''' <summary>
    ''' Obtiene un salario por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetContractMinimumWageById(ByVal id As Integer) As ContractMinimumWage
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractMinimumWage(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractMinimumWage)
End Interface
