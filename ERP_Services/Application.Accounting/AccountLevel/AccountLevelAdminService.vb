'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Exceptions

#End Region
Public Class AccountLevelAdminService
    Implements IAccountLevelAdminService
    Private _Repository As IAccountLevelRepository

#Region "Builder"
    Public Sub New(ByVal Repository As IAccountLevelRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _Repository = Repository
    End Sub
#End Region

#Region "implements"
    ''' <summary>
    ''' Deletes the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteAccountLevel(doc As MainAccountLevels, audit As AuditMessage) As ActionMessageResult(Of MainAccountLevels) Implements IAccountLevelAdminService.DeleteAccountLevel
        Dim result As New ActionMessageResult(Of MainAccountLevels)
        result.StateResult = True
        If doc Is Nothing Then
            Throw New ArgumentNullException("nivel Vacia")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            doc.ModificationDate = DateTime.Now
            doc.ModificationUser = audit.CodeUser
            Dim auditProcess As New IndigoAuditSimpleEntity(Of MainAccountLevels)(doc, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            _Repository.DeleteEntity(doc)
            UnitOfWork.Commit()
            auditProcess.Execute()
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            ' result.MessageResult.Add(New MessageResult("c-0000", doc.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the account Level by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelByCode(Code As String, Optional tracking As Boolean = True) As MainAccountLevels Implements IAccountLevelAdminService.GetAccountLevelByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _Repository.GetAccountLevelByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccountLevels()
        End Try
    End Function

    ''' <summary>
    ''' Gets the account Level by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelById(id As Integer, Optional tracking As Boolean = True) As MainAccountLevels Implements IAccountLevelAdminService.GetAccountLevelById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Return _Repository.GetAccountLevelById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccountLevels()
        End Try
    End Function

    ''' <summary>
    ''' Saves the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveAccountLevel(doc As MainAccountLevels, audit As AuditMessage) As Boolean Implements IAccountLevelAdminService.SaveAccountLevel
        If doc Is Nothing Then
            Throw New ArgumentNullException("nivel  Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccountLevels)
            Dim status As Integer
            Dim AuxLevel As MainAccountLevels = Nothing
            If doc.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                doc.CreationUser = audit.CodeUser
                doc.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                doc.ModificationDate = DateTime.Now
                doc.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxLevel = doc.OriginalValue
            End If

            _Repository.SaveEntity(doc)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of MainAccountLevels)(doc, audit, status, AuxLevel)
            auditProcess.Execute()

            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets all account level.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountLevel() As List(Of MainAccountLevels) Implements IAccountLevelAdminService.GetAllAccountLevel

        Try
            Return _Repository.GetAllAcountLevel()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _Repository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
