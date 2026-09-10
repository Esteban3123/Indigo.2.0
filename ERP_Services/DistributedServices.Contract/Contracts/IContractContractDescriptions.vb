'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractContractDescriptions

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractDescriptions(ContractDescriptions As Domain.Entities.ContractDescriptions, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractDescriptions(ContractDescriptions As Domain.Entities.ContractDescriptions, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado grupo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractDescriptions(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions)

    ''' <summary>
    ''' Obtiene un determinado grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractDescriptionsById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractDescriptions(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions)

End Interface
