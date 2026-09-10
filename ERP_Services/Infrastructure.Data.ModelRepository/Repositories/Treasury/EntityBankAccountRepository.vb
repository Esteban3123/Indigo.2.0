'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class EntityBankAccountRepository
    Inherits GenericRepository(Of EntityBankAccounts)
    Implements IEntityBankAccountRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una cuenta bancaria de la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetEntityBankAccount(code As String) As EntityBankAccounts Implements IEntityBankAccountRepository.GetEntityBankAccount
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As EntityBankAccounts In Me._context.EntityBankAccounts.Include("Checkbooks").Include("MainAccounts").Include("EntityBankAccountUser").Include("Currency") Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Dim entityBank As EntityBankAccounts = res(0)
            entityBank.hasMovements = (From tb In Me._context.TreasuryBalance Where tb.EntityBankAccountId = entityBank.Id)?.Any()
            If res(0).FinancialSourceId IsNot Nothing Then
                Dim financialSource = (From f In _context.FinancialSource.AsNoTracking Where f.Id = entityBank.FinancialSourceId Select f).FirstOrDefault
                res(0).FinancialSourceDescription = financialSource.Code + " - " + financialSource.Name
            End If
            If entityBank.CurrencyId IsNot Nothing Then
                entityBank.CurrencyAbbreviation = res(0).Currency.Abbreviation
            End If

            res(0).OriginalValue = (From d As EntityBankAccounts In Me._context.EntityBankAccounts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New EntityBankAccounts()
        End If
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetEntityBankAccountById(Id As Integer) As EntityBankAccounts Implements IEntityBankAccountRepository.GetEntityBankAccountById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As EntityBankAccounts In Me._context.EntityBankAccounts.Include("Checkbooks").Include("MainAccounts").Include("Bank").Include("Currency") Where d.Id.Equals(Id) Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As EntityBankAccounts In Me._context.EntityBankAccounts.AsNoTracking() Where d.Id.Equals(Id) Select d).FirstOrDefault()
            If res.CurrencyId IsNot Nothing Then
                res.CurrencyAbbreviation = res.Currency.Abbreviation
            End If
            Return res
        Else
            Return New EntityBankAccounts()
        End If
    End Function

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdEntityBankAccount</exception>
    Public Function ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount As Integer) As List(Of EntityBankAccountUser) Implements IEntityBankAccountRepository.ListEntityBankAccountUserByIdEntityBankiAccount
        If IdEntityBankAccount = 0 Then
            Throw New ArgumentNullException("IdEntityBankAccount")
        End If
        Return (From d As EntityBankAccountUser In Me._context.EntityBankAccountUser Where d.IdEntityBankAccount = IdEntityBankAccount Select d).ToList()
    End Function

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixs() As List(Of String) Implements IEntityBankAccountRepository.ListPrefixs
        Return (From b As EntityBankAccounts In Me._context.EntityBankAccounts Select b.Prefix).Distinct().ToList()
    End Function


    

    Public Function ListEntityBankAccountUserByIdEntityBankiAccountAndUserCode(IdEntityBankAccount As Integer, userCode As String) As List(Of EntityBankAccountUser) Implements IEntityBankAccountRepository.ListEntityBankAccountUserByIdEntityBankiAccountAndUserCode
        Return (From d As EntityBankAccountUser In Me._context.EntityBankAccountUser Where d.IdEntityBankAccount = IdEntityBankAccount And d.CodUser = userCode Select d).ToList()
    End Function

    
End Class
