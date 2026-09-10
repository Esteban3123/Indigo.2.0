'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IEntityBankAccountRepository
    Inherits IRepository(Of EntityBankAccounts)



    ''' <summary>
    ''' Obtiene una cuenta bancaria de la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetEntityBankAccount(ByVal code As String) As EntityBankAccounts

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetEntityBankAccountById(ByVal Id As Integer) As EntityBankAccounts

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    Function ListEntityBankAccountUserByIdEntityBankiAccount(ByVal IdEntityBankAccount As Integer) As List(Of EntityBankAccountUser)

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Function ListPrefixs() As List(Of String)


    Function ListEntityBankAccountUserByIdEntityBankiAccountAndUserCode(ByVal IdEntityBankAccount As Integer, userCode As String) As List(Of EntityBankAccountUser)

End Interface
