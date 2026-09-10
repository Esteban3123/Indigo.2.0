'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-10
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
#End Region


Public Class PortfolioDeteriorationClassificationAdminService
    Implements IPortfolioDeteriorationClassificationAdminService



#Region "Variables"
    Private Const FORM_NAME As String = "FrmPortfolioDeteriorationClassification"
    Private _portfolioDeteriorationClassificationRepository As IPortfolioDeteriorationClassificationRepository
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
#End Region


#Region "Builder"
    Public Sub New(ByVal portfolioDeteriorationClassificationRepository As IPortfolioDeteriorationClassificationRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (portfolioDeteriorationClassificationRepository Is Nothing) Then
            Throw New ArgumentNullException(NameOf(portfolioDeteriorationClassificationRepository))
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException(NameOf(sequenseRepository))
        End If
        _portfolioDeteriorationClassificationRepository = portfolioDeteriorationClassificationRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region


#Region "Methods"

    ''' <summary>
    ''' Obtiene el registro por código.
    ''' </summary>
    ''' <param name="code">.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPortfolioDeteriorationClassificationByCode(code As String, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassificationAdminService.GetPortfolioDeteriorationClassificationByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim portfolioDeteriorationClassification As PortfolioDeteriorationClassification = _portfolioDeteriorationClassificationRepository.GetPortfolioDeteriorationClassificationByCode(code.Trim())
            If portfolioDeteriorationClassification IsNot Nothing AndAlso portfolioDeteriorationClassification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PortfolioDeteriorationClassification)(portfolioDeteriorationClassification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = True, .ObjectEmbbeded = portfolioDeteriorationClassification}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


    ''' <summary>
    ''' Guarda una clasificacion deterioro de Cartera
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification">.</param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SavePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, audit As AuditMessage, Optional idSequense As Integer = 0) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassificationAdminService.SavePortfolioDeteriorationClassification
        If portfolioDeteriorationClassification Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._portfolioDeteriorationClassificationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(portfolioDeteriorationClassification.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            portfolioDeteriorationClassification.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), portfolioDeteriorationClassification.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PortfolioDeteriorationClassification = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioDeteriorationClassification)
                Dim status As Integer

                If portfolioDeteriorationClassification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    portfolioDeteriorationClassification.CreationUser = audit.CodeUser
                    portfolioDeteriorationClassification.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = portfolioDeteriorationClassification.OriginalValue
                    portfolioDeteriorationClassification.ModificationUser = audit.CodeUser
                    portfolioDeteriorationClassification.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._portfolioDeteriorationClassificationRepository.SaveEntity(portfolioDeteriorationClassification)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioDeteriorationClassification)(portfolioDeteriorationClassification, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                portfolioDeteriorationClassification.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = portfolioDeteriorationClassification, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina una clasificacion deterioro de Cartera
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeletePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, audit As AuditMessage) As ActionResult Implements IPortfolioDeteriorationClassificationAdminService.DeletePortfolioDeteriorationClassification
        If portfolioDeteriorationClassification Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._portfolioDeteriorationClassificationRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                portfolioDeteriorationClassification.ModificationUser = audit.CodeUser
                portfolioDeteriorationClassification.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioDeteriorationClassification)(portfolioDeteriorationClassification, audit, status)
                portfolioDeteriorationClassification.MarkAsDeleted()
                Me._portfolioDeteriorationClassificationRepository.SaveEntity(portfolioDeteriorationClassification)
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
    ''' Cambia el estado de una clasificacion deterioro de Cartera
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassificationAdminService.ChangeStatePortfolioDeteriorationClassification
        If portfolioDeteriorationClassification Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            If portfolioDeteriorationClassification IsNot Nothing AndAlso portfolioDeteriorationClassification.Id > 0 Then
                portfolioDeteriorationClassification.Status = state
            End If
            Dim result = SavePortfolioDeteriorationClassification(portfolioDeteriorationClassification, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioDeteriorationClassification) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _portfolioDeteriorationClassificationRepository = Nothing
            _sequensePortfolioDRepository = Nothing
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
