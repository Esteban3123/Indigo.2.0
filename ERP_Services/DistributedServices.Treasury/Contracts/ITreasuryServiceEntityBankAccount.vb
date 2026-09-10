'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceEntityBankAccount

    ''' <summary>
    ''' guarda una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateEntityBankAccount(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Elimina una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una cuenta de entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetEntityBankAccount(code As String, audit As AuditMessage) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetEntityBankAccountById(ByVal Id As Integer, audit As AuditMessage) As EntityBankAccounts

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.EntityBankAccountUser))

    ''' <summary>
    ''' Lista todos los prefijos registrados en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    <OperationContract()>
    Function ListPrefixsEntityBankAccount() As List(Of String)

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="EntityBankAccountId">The identifier entity bank account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListUsersByEntityBankAccountId(EntityBankAccountId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Security.Entities.User))

End Interface
