'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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

Public Class DistributionLinesAdminService
    Implements IDistributionLinesAdminService
    Private Const FORM_NAME As String = "FrmDistributionLines"
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLinesRepository As IDistributionLinesRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal distributionLinesRepository As IDistributionLinesRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository)
        If distributionLinesRepository Is Nothing Then
            Throw New ArgumentNullException("distributionLinesRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository Vacio")
        End If
        _distributionLinesRepository = distributionLinesRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina una linea de distribucion 
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDistributionLines(distributionLines As DistributionLines, audit As AuditMessage) As ActionResult Implements IDistributionLinesAdminService.DeleteDistributionLines
        'If distributionLines Is Nothing Then
        '    Throw New ArgumentNullException("distributionLines")
        'End If
        'Dim unitOfWork As IUnitWork = Me._distributionLinesRepository.UnitWork
        'Try
        '    distributionLines.StartTracking()
        '    While distributionLines.DistributionLinesDetail.Count > 0
        '        distributionLines.DistributionLinesDetail.Item(0).MarkAsDeleted()
        '    End While
        '    While distributionLines.DistributionLinesICARetention.Count > 0
        '        distributionLines.DistributionLinesICARetention.Item(0).MarkAsDeleted()
        '    End While
        '    distributionLines.MarkAsDeleted()
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of DistributionLines)
        '    auditProcess = New IndigoAuditSimpleEntity(Of DistributionLines)(distributionLines, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._distributionLinesRepository.SaveEntity(distributionLines)
        '    unitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As DbUpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try



        If distributionLines Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionLinesRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                distributionLines.ModificationUser = audit.CodeUser
                distributionLines.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionLines)(distributionLines, audit, status)

                While distributionLines.DistributionLinesDetail.Count > 0
                    distributionLines.DistributionLinesDetail.Item(0).MarkAsDeleted()
                End While
                While distributionLines.DistributionLinesICARetention.Count > 0
                    distributionLines.DistributionLinesICARetention.Item(0).MarkAsDeleted()
                End While
                distributionLines.MarkAsDeleted()
                Me._distributionLinesRepository.SaveEntity(distributionLines)
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
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLines(code As String, audit As AuditMessage) As ActionResult(Of DistributionLines) Implements IDistributionLinesAdminService.GetDistributionLines
        If code = String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionLines As DistributionLines = Me._distributionLinesRepository.GetDistributionLines(code)
            If distributionLines IsNot Nothing AndAlso distributionLines.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DistributionLines)(distributionLines, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DistributionLines) With {.StateResult = True, .ObjectEmbbeded = distributionLines}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionLines) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLinesById(id As Integer, audit As AuditMessage) As ActionResult(Of DistributionLines) Implements IDistributionLinesAdminService.GetDistributionLinesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionLines As DistributionLines = Me._distributionLinesRepository.GetDistributionLinesById(id)
            If distributionLines IsNot Nothing AndAlso distributionLines.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DistributionLines)(distributionLines, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DistributionLines) With {.StateResult = True, .ObjectEmbbeded = distributionLines}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionLines) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una linea de distribucion
    ''' </summary>
    ''' <param name="distributionLines"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDistributionLines(distributionLines As DistributionLines, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DistributionLines) Implements IDistributionLinesAdminService.SaveDistributionLines


        If distributionLines Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionLinesRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(distributionLines.Code) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionLines.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DistributionLines) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionLines.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DistributionLines) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As DistributionLines = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DistributionLines)
                Dim status As Integer

                If distributionLines.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    distributionLines.CreationUser = audit.CodeUser
                    distributionLines.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _distributionLinesRepository.GetDistributionLines(distributionLines.Code)
                    distributionLines.ModificationUser = audit.CodeUser
                    distributionLines.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._distributionLinesRepository.SaveEntity(distributionLines)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DistributionLines)(distributionLines, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                distributionLines.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DistributionLines) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionLines, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DistributionLines) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionLines) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDistributionLines(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionLines) Implements IDistributionLinesAdminService.ChangeStateDistributionLines
        'Dim distributionLines As DistributionLines = _distributionLinesRepository.GetDistributionLines(code)
        'distributionLines.Status = state
        'Return SaveDistributionLines(distributionLines, audit)



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
            Dim distributionLines As DistributionLines = Me._distributionLinesRepository.GetDistributionLines(code.Trim())
            If distributionLines IsNot Nothing AndAlso distributionLines.Id > 0 Then
                distributionLines.Status = state
            End If
            Dim result = Me.SaveDistributionLines(distributionLines, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionLines) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _distributionLinesRepository = Nothing
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
