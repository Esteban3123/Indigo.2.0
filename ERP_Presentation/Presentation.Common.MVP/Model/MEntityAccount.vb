'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Presentation.Security.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MEntityAccount
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Async Function GetAccountById(id As Integer, Optional tracking As Boolean = True) As Task(Of Domain.Entities.MainAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByIdAsync(id, tracking, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.TreasurySequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Saves the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult(Of BlockRecordTreasury))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBlockRecordTreasuryAsync(record)
    End Function

    ''' <summary>
    ''' Deletes the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteBlockRecordTreasuryAsync(record)
    End Function

    ''' <summary>
    ''' Gets the block record treasury.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordTreasury(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordTreasury)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBlockRecordTreasuryByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUser(ByVal container As String) As DevExpress.Data.Linq.LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(container).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(container).SecurityService.ListUserByContainer(_indigoSessionValues.IndigoContainerId)
    End Function

    ''' <summary>
    ''' Saves the entity bank account.
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    Public Async Function SaveEntityBankAccount(ByVal entityBankAccount As EntityBankAccounts, ByVal idSequence As Int64) As Task(Of ActionResult(Of EntityBankAccounts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveEntityBankAccountAsync(entityBankAccount, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateEntityBankAccount(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of EntityBankAccounts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.UpdateStateEntityBankAccountAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the entity bank account.
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <returns></returns>
    Public Async Function DeleteEntityBankAccount(ByVal entityBankAccount As EntityBankAccounts) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteEntityBankAccountAsync(entityBankAccount, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the entity bank account.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetEntityBankAccount(ByVal code As String) As Task(Of ActionResult(Of EntityBankAccounts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetEntityBankAccountAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetUserByCode(ByVal code As String) As Task(Of Domain.Security.Entities.User)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserAsync(code, _indigoSessionValues, Nothing)
        If res Is Nothing OrElse res.Id = 0 Then
            res = New Domain.Security.Entities.User()
            res.UserCode = code
        Else
            res.PersonFullName = res.Person.Fullname
        End If
        Return res
    End Function

    Public Async Function GetUserAuthorizedByCode(ByVal code As String) As Task(Of Domain.Security.Entities.User)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserAsync(code, _indigoSessionValues, Nothing)
        If res Is Nothing OrElse res.Id = 0 Then
            res = New Domain.Security.Entities.User()
            res.UserCode = code
        Else
            res.PersonFullName = res.Person.Fullname
        End If
        Return res
    End Function

    Public Async Function GetUserById(ByVal Id As String) As Task(Of Domain.Security.Entities.User)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByIdAsync(Id, _indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res = New Domain.Security.Entities.User()
            res.UserCode = String.Empty
        Else
            res.PersonFullName = res.Person.Fullname
        End If
        Return res
    End Function

    ''' <summary>
    ''' Lists the entity bank account user by identifier entity banki account.
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    ''' <returns></returns>
    Public Async Function ListEntityBankAccountUserByIdEntityBankiAccount(ByVal IdEntityBankAccount As Integer) As Task(Of ActionResult(Of List(Of EntityBankAccountUser)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListEntityBankAccountUserByIdEntityBankiAccountAsync(IdEntityBankAccount, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetEntityBankAccountById(ByVal Id As Integer) As Task(Of EntityBankAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetEntityBankAccountByIdAsync(Id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetEntityBankAccountByIdSimple(ByVal Id As Integer) As EntityBankAccounts
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetEntityBankAccountById(Id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the user by identifier cash register.
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier cash register.</param>
    ''' <returns></returns>
    Public Async Function ListUserByIdEntityBankAccount(ByVal IdEntityBankAccount As String) As Task(Of List(Of Domain.Security.Entities.User))
        Dim result = Await ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount)
        Dim UserCash As List(Of EntityBankAccountUser) = result.ObjectEmbbeded
        Dim ListUser As New List(Of Domain.Security.Entities.User)
        For Each UserCashReg As EntityBankAccountUser In UserCash
            Using ModeloUsuario As New MUsuario()
                Dim user As Domain.Security.Entities.User = Await ModeloUsuario.ConsultarUsuarioCodigoContainerId(CStr(UserCashReg.CodUser))
                ListUser.Add(user)
            End Using
        Next
        Return ListUser
    End Function

    ''' <summary>
    ''' Lists the entity bank account user by identifier entity banki account.
    ''' </summary>
    ''' <param name="EntityBankAccountId">The identifier entity bank account.</param>
    ''' <returns></returns>
    Public Async Function ListUsersByEntityBankAccountId(ByVal EntityBankAccountId As Integer) As Task(Of ActionResult(Of List(Of Domain.Security.Entities.User)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListUsersByEntityBankAccountIdAsync(EntityBankAccountId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
