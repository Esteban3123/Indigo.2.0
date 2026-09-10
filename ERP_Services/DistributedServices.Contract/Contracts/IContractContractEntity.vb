'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractContractEntity

    ''' <summary>
    ''' Guarda o Actualiza una entidad de contrato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractEntity(ContractEntity As Domain.Entities.ContractEntity, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity)

    ''' <summary>
    ''' Elimina una entidad de contrato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractEntity(ContractEntity As Domain.Entities.ContractEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una entidad de contrato por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractEntity(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity)

    ''' <summary>
    ''' Obtiene una entidad de contrato por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractEntityById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractEntity(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity)

End Interface
