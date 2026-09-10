'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Juan Carlos Bermudez
' Created          : 12-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources


#End Region

Public Class AccountReceivableDocumentAdminService
    Implements IAccountReceivableDocumentAdminService

#Region "Fields"

    Private _accountReceivableDocumentRepository As IAccountReceivableDocumentRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal accountReceivableDocumentRepository As IAccountReceivableDocumentRepository)
        If accountReceivableDocumentRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableDocumentRepository")
        End If
        _accountReceivableDocumentRepository = accountReceivableDocumentRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentById(id As Integer) As AccountReceivableDocument Implements IAccountReceivableDocumentAdminService.GetAccountReceivableDocumentById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim accountReceivableDocument = Me._accountReceivableDocumentRepository.GetAccountReceivableDocumentById(id)
            Return accountReceivableDocument
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por el codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentByCode(code As String, audit As AuditMessage) As AccountReceivableDocument Implements IAccountReceivableDocumentAdminService.GetAccountReceivableDocumentByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountReceivableDocument = _accountReceivableDocumentRepository.GetAccountReceivableDocumentByCode(code.Trim())
            If accountReceivableDocument IsNot Nothing AndAlso accountReceivableDocument.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountReceivableDocument)(accountReceivableDocument, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return accountReceivableDocument
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un documento de cuenta x cobrar 
    ''' </summary>
    ''' <param name="accountReceivableDocument"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountReceivableDocument(accountReceivableDocument As AccountReceivableDocument, audit As AuditMessage) As ActionResult(Of AccountReceivableDocument) Implements IAccountReceivableDocumentAdminService.SaveAccountReceivableDocument
        If accountReceivableDocument Is Nothing Then
            Throw New ArgumentNullException("accountReceivableDocument")
        End If
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim AccountReceivableDocumentXml As String = accountReceivableDocument.ToXML()
                Dim auditStatus = CType(Utils.GetAuditStatus(accountReceivableDocument.Id, accountReceivableDocument.Status), Infrastructure.CrossCutting.Audit.Actions)

                Dim result = Me._accountReceivableDocumentRepository.SP_SaveAccountReceivableDocument(AccountReceivableDocumentXml, audit.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of AccountReceivableDocument) With {.StateResult = False, .Message = result.MessageResult}
                End If

                accountReceivableDocument.Id = result.Id
                accountReceivableDocument.Code = result.Code

                Dim auditProcess As New IndigoAuditSimpleEntity(Of Domain.Entities.AccountReceivableDocument)(accountReceivableDocument, audit, auditStatus, accountReceivableDocument.OriginalValue)
                auditProcess.Execute()

                accountReceivableDocument.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AccountReceivableDocument) With {.StateResult = True, .ObjectEmbbeded = accountReceivableDocument, .Message = result.MessageResult}
            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountReceivableDocument) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountReceivableDocument) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountReceivableDocument) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _accountReceivableDocumentRepository = Nothing
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
