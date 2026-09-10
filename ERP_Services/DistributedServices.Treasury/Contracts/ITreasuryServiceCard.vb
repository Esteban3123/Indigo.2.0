'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCard

    ''' <summary>
    ''' Saves the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCard(card As Domain.Entities.Cards, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Cards)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateCard(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Cards)

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCard(card As Domain.Entities.Cards, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCard(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.Cards)

    ''' <summary>
    ''' obtener una tarjeta por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCardById(id As Integer) As Cards
End Interface
