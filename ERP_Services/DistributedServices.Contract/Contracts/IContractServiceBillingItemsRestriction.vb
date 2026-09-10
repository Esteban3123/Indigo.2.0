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
Public Interface IContractServiceBillingItemsRestriction

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBillingItemsRestriction(BillingItemsRestriction As Domain.Entities.BillingItemsRestriction, ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteBillingItemsRestriction(BillingItemsRestriction As Domain.Entities.BillingItemsRestriction, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBillingItemsRestriction(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction)

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBillingItemsRestrictionById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateBillingItemsRestriction(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction)

    ''' <summary>
    ''' obtiene detalles por id de cabecera
    ''' </summary>
    ''' <param name="idHeader"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As ActionResult(Of List(Of BillingItemsRestrictionDetail))

End Interface
