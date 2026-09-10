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

Public Class DeferredCausationAdminService
    Implements IDeferredCausationAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _deferredCausationRepository As IDeferredCausationRepository
    ''' <summary>
    ''' Variable tipo repositorio para causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _monthlyAmortizationRepository As IMonthlyAmortizationRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal deferredCausationRepository As IDeferredCausationRepository, ByVal monthlyAmortizationRepository As IMonthlyAmortizationRepository)
        If deferredCausationRepository Is Nothing Then
            Throw New ArgumentNullException("deferredCausationRepository Vacio")
        End If
        _deferredCausationRepository = deferredCausationRepository
        _monthlyAmortizationRepository = monthlyAmortizationRepository
    End Sub

    ''' <summary>
    ''' Elimina una causacion diferida
    ''' </summary>
    ''' <param name="deferredCausation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDeferredCausation(deferredCausation As DeferredCausation, audit As AuditMessage) As ActionResult Implements IDeferredCausationAdminService.DeleteDeferredCausation
        If deferredCausation Is Nothing Then
            Throw New ArgumentNullException("deferredCausation")
        End If
        Dim unitOfWork As IUnitWork = Me._deferredCausationRepository.UnitWork
        Try
            If deferredCausation.ChangeTracker.State = ObjectState.Deleted Then
                Me._deferredCausationRepository.DeleteEntity(deferredCausation)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(DeferredCausation).Name, audit.Functional, deferredCausation.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of DeferredCausation)(deferredCausation, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las causaciones diferidas por id de cuenta por pagar
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of DeferredCausation) Implements IDeferredCausationAdminService.GetDeferredCausationByIdAccountPayable
        If idAccountPayable = 0 Then
            Throw New ArgumentNullException("idAccountPayable")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim listDeferredCausation As List(Of DeferredCausation) = Me._deferredCausationRepository.GetDeferredCausationByIdAccountPayable(idAccountPayable)
            Return listDeferredCausation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las causaciones diferidas por el código de la cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByAccountPayableCode(code As String, audit As AuditMessage) As List(Of DeferredCausation) Implements IDeferredCausationAdminService.GetDeferredCausationByAccountPayableCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Return Me._deferredCausationRepository.GetDeferredCausationByAccountPayableCode(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una causacion diferida
    ''' </summary>
    ''' <param name="deferredCausation"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDeferredCausation(deferredCausation As DeferredCausation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DeferredCausation) Implements IDeferredCausationAdminService.SaveDeferredCausation
        If deferredCausation Is Nothing Then
            Throw New ArgumentNullException("deferredCausation")
        End If
        Dim unitOfWork As IUnitWork = Me._deferredCausationRepository.UnitWork
        Try
            Dim auxDeferredCausation As DeferredCausation = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of DeferredCausation)
            Dim status As Integer

            If deferredCausation.ChangeTracker.State <> ObjectState.Deleted Then
                Select Case deferredCausation.Status
                    Case 1
                        If deferredCausation.ChangeTracker.State = ObjectState.Added Then
                            deferredCausation.CreationUser = audit.CodeUser
                            deferredCausation.CreationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        Else
                            auxDeferredCausation = _monthlyAmortizationRepository.GetDeferredCausationById(deferredCausation.Id)
                            deferredCausation.ModificationUser = audit.CodeUser
                            deferredCausation.ModificationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        End If

                    Case 2
                        If deferredCausation.Id = 0 Then
                            deferredCausation.CreationUser = audit.CodeUser
                            deferredCausation.CreationDate = DateTime.Now
                        End If
                        deferredCausation.ConfirmationUser = audit.CodeUser
                        deferredCausation.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    Case 3
                        deferredCausation.AnnulmentUser = audit.CodeUser
                        deferredCausation.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                End Select
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Delete
            End If

            Me._deferredCausationRepository.SaveEntity(deferredCausation)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of DeferredCausation)(deferredCausation, audit, status, auxDeferredCausation)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            deferredCausation.MarkAsUnchanged()

            Return New ActionResult(Of DeferredCausation) With {.StateResult = True, .ObjectEmbbeded = deferredCausation}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DeferredCausation) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeferredCausation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _deferredCausationRepository = Nothing
            _monthlyAmortizationRepository = Nothing
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
