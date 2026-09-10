'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Elimina una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    Public Function DeleteEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage) As ActionResult Implements ITreasuryServiceEntityBankAccount.DeleteEntityBankAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.DeleteEntityBankAccount(entityBankAccount, audit)
        End Using
        'Return Me._entityBankAdminService.DeleteEntityBankAccount(entityBankAccount, audit)
    End Function

    ''' <summary>
    ''' Obtiene una cuenta de entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetEntityBankAccount(code As String, audit As AuditMessage) As ActionResult(Of EntityBankAccounts) Implements ITreasuryServiceEntityBankAccount.GetEntityBankAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.GetEntityBankAccount(code, audit)
        End Using
        'Return Me._entityBankAdminService.GetEntityBankAccount(code, audit)
    End Function

    ''' <summary>
    ''' guarda una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    Public Function SaveEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of EntityBankAccounts) Implements ITreasuryServiceEntityBankAccount.SaveEntityBankAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.SaveEntityBankAccount(entityBankAccount, audit, idSequence)
        End Using
        'Return Me._entityBankAdminService.SaveEntityBankAccount(entityBankAccount, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateEntityBankAccount(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EntityBankAccounts) Implements ITreasuryServiceEntityBankAccount.UpdateStateEntityBankAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.UpdateStateEntityBankAccount(code, state, audit)
        End Using
        'Return Me._entityBankAdminService.UpdateStateEntityBankAccount(code, state, audit)
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetEntityBankAccountById(Id As Integer, audit As AuditMessage) As EntityBankAccounts Implements ITreasuryServiceEntityBankAccount.GetEntityBankAccountById
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.GetEntityBankAccountById(Id, audit)
        End Using
        'Return Me._entityBankAdminService.GetEntityBankAccountById(Id, audit)
    End Function

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    Public Function ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.EntityBankAccountUser)) Implements ITreasuryServiceEntityBankAccount.ListEntityBankAccountUserByIdEntityBankiAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount, audit)
        End Using
        'Return Me._entityBankAdminService.ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount, audit)
    End Function

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixsEntityBankAccount() As List(Of String) Implements ITreasuryServiceEntityBankAccount.ListPrefixsEntityBankAccount
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.ListPrefixs()
        End Using
        'Return Me._entityBankAdminService.ListPrefixs()
    End Function

    ''' <summary>
    ''' Consulta los usuarios por codigo
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUsersByEntityBankAccountId(EntityBankAccountId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Security.Entities.User)) Implements ITreasuryServiceEntityBankAccount.ListUsersByEntityBankAccountId
        Using service As IEntityBankAccountAdminService = Container.Current.Resolve(Of IEntityBankAccountAdminService)()
            Return service.ListUsersByEntityBankAccountId(EntityBankAccountId, audit)
        End Using
        'Return Me._entityBankAdminService.ListUsersByEntityBankAccountId(EntityBankAccountId, audit)
    End Function

End Class
