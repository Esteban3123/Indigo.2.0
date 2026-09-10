'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IContractServiceDefinitionRate

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDefinitionRate(DefinitionRate As Domain.Entities.DefinitionRate, ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), listDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition), Company As String, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDefinitionRate(DefinitionRate As Domain.Entities.DefinitionRate, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDefinitionRate(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate)

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDefinitionRateById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateDefinitionRate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate)

End Interface
