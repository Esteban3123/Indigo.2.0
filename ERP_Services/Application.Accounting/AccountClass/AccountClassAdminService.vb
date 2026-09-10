'***********************************************************************
' Assembly         : Domain.Seedwork
' Author           : Sergio Fernandez
' Created          : 2014-11-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class AccountClassAdminService
    Implements IAccountClassAdminService

    Private _Repository As IAccountClassRepository

    Private _sequenceDRepository As ISequenseAccountingDRepository

    Private _pucRepository As IPUCRepository

    Private Const FORM_NAME As String = "Clases Contables"

#Region "Builder"
    Public Sub New(ByVal Repository As IAccountClassRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository, ByVal pucRepository As IPUCRepository)
        If Repository Is Nothing Or secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _Repository = Repository
        _sequenceDRepository = secuenseDRepository
        _pucRepository = pucRepository
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Elimina una clase contable
    ''' </summary>
    ''' <param name="doc">Clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>
    ''' Resultado de la acción
    ''' </returns>
    Public Function DeleteAccountClass(doc As Domain.Entities.MainAccountClasses, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IAccountClassAdminService.DeleteAccountClass
        If doc Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                doc.ModificationUser = audit.CodeUser
                doc.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MainAccountClasses)(doc, audit, status)

                doc.MarkAsDeleted()
                Me._Repository.SaveEntity(doc)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una clase contable
    ''' </summary>
    ''' <param name="code">Código de la clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>
    ''' Clase contable
    ''' </returns>
    Public Function GetAccountClassByCode(code As String, Optional Tracking As Boolean = True) As Domain.Entities.MainAccountClasses Implements IAccountClassAdminService.GetAccountClassByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _Repository.GetAccountClassByCode(code, Tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccountClasses()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una clase contable
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="Tracking"></param>
    ''' <returns>
    ''' Clase contable
    ''' </returns>
    Public Function GetAccountClassById(id As Integer, Optional Tracking As Boolean = True) As MainAccountClasses Implements IAccountClassAdminService.GetAccountClassById
        Try
            Return _Repository.GetAccountClassById(id, Tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccountClasses()
        End Try
    End Function

    ''' <summary>
    ''' Gets all account class.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountClass() As List(Of MainAccountClasses) Implements IAccountClassAdminService.GetAllAccountClass
        Try
            Return _Repository.GetAllAccountClass()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba una una clase contable
    ''' </summary>
    ''' <param name="doc">Clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <param name="idSequence"></param>
    Public Function SaveAccountClass(doc As MainAccountClasses, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of MainAccountClasses) Implements IAccountClassAdminService.SaveAccountClass
        If doc Is Nothing Then
            Throw New ArgumentNullException("clase cueneta Vacia")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty


                If doc.Code Is Nothing OrElse doc.Code.Trim().Equals(String.Empty) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._sequenceDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            doc.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MainAccountClasses) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), doc.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MainAccountClasses) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim AuxClass As MainAccountClasses = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccountClasses)
                Dim status As Integer

                If doc.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    doc.CreationDate = DateTime.Now
                    doc.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    'Valido que la clase contable no este amarrada a una cuenta contable
                    If _pucRepository.ValidateMainAccountByIdAccountClass(doc.Id) = False Then
                        UnitOfWork.Commit()
                        scope.Dispose()
                        Return New ActionResult(Of MainAccountClasses) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"MAASOCIATED"}.ToList(), .Message = "La Clase Contable no se puede Actualizar porque ya tiene una Cuenta Contable asociada."}
                    End If
                    doc.ModificationDate = DateTime.Now
                    doc.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    AuxClass = doc.OriginalValue
                End If
                Me._Repository.SaveEntity(doc)
                UnitOfWork.Commit()
                unitWorkSequence.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MainAccountClasses)(doc, audit, status, AuxClass)
                auditProcess.Execute()
                doc.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MainAccountClasses) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = doc, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of MainAccountClasses) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccountClasses) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateAccountClass(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MainAccountClasses) Implements IAccountClassAdminService.UpdateStateAccountClass
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim accountClass As MainAccountClasses = Me._Repository.GetAccountClassByCode(code.Trim())
            If accountClass IsNot Nothing AndAlso accountClass.Id > 0 Then
                accountClass.Status = state
                accountClass.MarkAsModified()
            End If
            Return Me.SaveAccountClass(accountClass, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccountClasses) With {.StateResult = False}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _Repository = Nothing
            _sequenceDRepository = Nothing
            _pucRepository = Nothing
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