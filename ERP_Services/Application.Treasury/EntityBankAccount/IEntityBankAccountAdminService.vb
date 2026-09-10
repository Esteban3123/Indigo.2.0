'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEntityBankAccountAdminService
    Inherits IDisposable

    ''' <summary>
    ''' guarda una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveEntityBankAccount(ByVal entityBankAccount As EntityBankAccounts, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateEntityBankAccount(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Elimina una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteEntityBankAccount(ByVal entityBankAccount As EntityBankAccounts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una cuenta de entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetEntityBankAccount(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of EntityBankAccounts)

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetEntityBankAccountById(ByVal Id As Integer, ByVal audit As AuditMessage) As EntityBankAccounts

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    Function ListEntityBankAccountUserByIdEntityBankiAccount(ByVal IdEntityBankAccount As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of EntityBankAccountUser))

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Function ListPrefixs() As List(Of String)

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="EntityBankAccountId">The identifier entity bank account.</param>
    ''' <returns></returns>
    Function ListUsersByEntityBankAccountId(ByVal EntityBankAccountId As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of Domain.Security.Entities.User))

End Interface
