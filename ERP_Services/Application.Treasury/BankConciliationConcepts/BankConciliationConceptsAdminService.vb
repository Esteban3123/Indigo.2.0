#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region
Public Class BankConciliationConceptsAdminService

    Implements IBankConciliationConceptsAdminService
    Private Const FORM_NAME As String = "FrmBankConciliationConcepts"

    'Repositorio de tipo de ubicacion
    Private _BankConciliationConceptsRespository As IBankConciliationConceptsRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="BankConciliationConceptsRespository">Repositorio de  conceptos de conciliación bancaria</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As ISequenseTreasuryDRepository, ByVal BankConciliationConceptsRespository As IBankConciliationConceptsRepository)
        If (BankConciliationConceptsRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de BankConciliationConceptsRespository")
        End If
        _BankConciliationConceptsRespository = BankConciliationConceptsRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    ''' <summary>
    ''' Función que obtiene un concepto de conciliación bancaria por Código
    ''' </summary>
    ''' <param name="Code">Código de los conceptos de conciliación bancaria</param>
    ''' <returns>BankConciliationConcepts</returns>
    ''' <remarks></remarks>
    Public Function GetBankConciliationConceptsByCode(Code As String) As BankConciliationConcepts Implements IBankConciliationConceptsAdminService.GetBankConciliationConceptsByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de  conceptos de conciliación bancaria vacio")
        End If
        Try
            Return _BankConciliationConceptsRespository.GetBankConciliationConceptsByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New BankConciliationConcepts()
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todos los conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns>Lista de  conceptos de conciliación bancaria</returns>
    ''' <remarks></remarks>
    Public Function ListAllBankConciliationConcepts() As List(Of BankConciliationConcepts) Implements IBankConciliationConceptsAdminService.ListAllBankConciliationConcepts
        Try
            Return _BankConciliationConceptsRespository.ListAllBankConciliationConcepts()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Eliminar el concepto de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteBankConciliationConcepts(BankConciliationConcepts As BankConciliationConcepts, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBankConciliationConceptsAdminService.DeleteBankConciliationConcepts
        If BankConciliationConcepts Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._BankConciliationConceptsRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                BankConciliationConcepts.ModificationUser = audit.CodeUser
                BankConciliationConcepts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BankConciliationConcepts)(BankConciliationConcepts, audit, status)
                BankConciliationConcepts.MarkAsDeleted()
                Me._BankConciliationConceptsRespository.SaveEntity(BankConciliationConcepts)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
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
    ''' Función para Almacenar conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveBankConciliationConcepts(BankConciliationConcepts As BankConciliationConcepts, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BankConciliationConcepts) Implements IBankConciliationConceptsAdminService.SaveBankConciliationConcepts
        If BankConciliationConcepts Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._BankConciliationConceptsRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(BankConciliationConcepts.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            BankConciliationConcepts.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BankConciliationConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), BankConciliationConcepts.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BankConciliationConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As BankConciliationConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BankConciliationConcepts)
                Dim status As Integer

                If BankConciliationConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    BankConciliationConcepts.CreationUser = audit.CodeUser
                    BankConciliationConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = BankConciliationConcepts.OriginalValue
                    BankConciliationConcepts.ModificationUser = audit.CodeUser
                    BankConciliationConcepts.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._BankConciliationConceptsRespository.SaveEntity(BankConciliationConcepts)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BankConciliationConcepts)(BankConciliationConcepts, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se  conceptos de conciliación bancaria la entidad como sin cambios
                BankConciliationConcepts.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BankConciliationConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = BankConciliationConcepts, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BankConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Function ChangeBankConciliationConceptsStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BankConciliationConcepts) Implements IBankConciliationConceptsAdminService.ChangeBankConciliationConceptsStatus
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
            Dim BankConciliationConcepts As BankConciliationConcepts = GetBankConciliationConceptsByCode(code.Trim())
            If BankConciliationConcepts IsNot Nothing AndAlso BankConciliationConcepts.Id > 0 Then
                BankConciliationConcepts.Status = state
            End If
            Dim result = Me.SaveBankConciliationConcepts(BankConciliationConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _BankConciliationConceptsRespository = Nothing
            _secuenseDRepository = Nothing
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
