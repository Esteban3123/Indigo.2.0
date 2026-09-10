'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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

Public Class PortfolioDemandStatusAdminService
    Implements IPortfolioDemandStatusAdminService

    'Repositorio de la aseguradora
    Private _DemandStatusRepository As IPortfolioDemandStatusRepository

    'repositorio de la secuencia
    Private _sequenceRepository As ISequensePortfolioDRepository

    Private Const FORM_NAME As String = "FrmDemandStatus"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="PortfolioDemandStatusRepository"></param>
    ''' <param name="sequenceRepository"></param>
    Public Sub New(PortfolioDemandStatusRepository As IPortfolioDemandStatusRepository, sequenceRepository As ISequensePortfolioDRepository)
        If (PortfolioDemandStatusRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de DemandStatusRepository vacio")
        End If
        _sequenceRepository = sequenceRepository
        _DemandStatusRepository = PortfolioDemandStatusRepository
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteDemandStatus(_DemandStatus As DemandStatus, audit As AuditMessage) As ActionResult Implements IPortfolioDemandStatusAdminService.DeleteDemandStatus

        If _DemandStatus Is Nothing Then
            Throw New ArgumentNullException("_DemandStatus")
        End If
        Dim unitOfWork As IUnitWork = Me._DemandStatusRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                _DemandStatus.ModificationUser = audit.CodeUser
                _DemandStatus.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DemandStatus)(_DemandStatus, audit, status)

                _DemandStatus.MarkAsDeleted()
                Me._DemandStatusRepository.SaveEntity(_DemandStatus)
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
    ''' 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetDemandStatus(Code As String) As DemandStatus Implements IPortfolioDemandStatusAdminService.GetDemandStatus
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Return _DemandStatusRepository.GetDemandStatusByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New DemandStatus()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllDemandStatus() As List(Of DemandStatus) Implements IPortfolioDemandStatusAdminService.ListAllDemandStatus
        Try
            Return _DemandStatusRepository.ListAllDemandStatus()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveDemandStatus(_DemandStatus As DemandStatus, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DemandStatus) Implements IPortfolioDemandStatusAdminService.SaveDemandStatus
        
        If _DemandStatus Is Nothing Then
            Throw New ArgumentNullException("_DemandStatus")
        End If
        Dim unitOfWork As IUnitWork = Me._DemandStatusRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(_DemandStatus.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequenceRepository.GetSequenseDById(cint(idSequense))
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            _DemandStatus.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                            sequenseUnitOfWork.Commit()
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DemandStatus) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), _DemandStatus.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DemandStatus) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As DemandStatus = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DemandStatus)
                Dim status As Integer

                If _DemandStatus.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _DemandStatus.CreationUser = audit.CodeUser
                    _DemandStatus.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _DemandStatus.OriginalValue
                    _DemandStatus.ModificationUser = audit.CodeUser
                    _DemandStatus.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._DemandStatusRepository.SaveEntity(_DemandStatus)
                unitOfWork.Commit()
                
                auditProcess = New IndigoAuditSimpleEntity(Of DemandStatus)(_DemandStatus, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                _DemandStatus.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DemandStatus) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = _DemandStatus, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DemandStatus) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DemandStatus) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DemandStatusChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DemandStatus) Implements IPortfolioDemandStatusAdminService.DemandStatusChangeState
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
            Dim ObjDemandStatus As DemandStatus = Me._DemandStatusRepository.GetDemandStatusByCode(code.Trim())
            If ObjDemandStatus IsNot Nothing AndAlso ObjDemandStatus.Id > 0 Then
                ObjDemandStatus.Status = state
            End If
            Dim result = Me.SaveDemandStatus(ObjDemandStatus, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DemandStatus) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _DemandStatusRepository = Nothing
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
