'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractContract

    ''' <summary>
    ''' Guarda o Actualiza un contrato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContract(Contract As Domain.Entities.Contract, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Elimina un contrato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContract(Contract As Domain.Entities.Contract, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un contrato por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContract(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Gets the contract by identifier with aggregates.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractByIdWithAggregates(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContract(code As String, state As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract)

End Interface
