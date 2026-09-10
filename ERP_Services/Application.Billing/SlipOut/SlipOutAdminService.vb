'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal
Imports System.Text
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure

#End Region
Public Class SlipOutAdminService
    Implements ISlipOutAdminService
    Private Const FORM_NAME As String = "FrmSlipOut"

    Dim _slipOutRepository As ISlipOutRepository
    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenceDRepository As IBillingSequenceDetailRepository

    Public Sub New(slipOutRepository As ISlipOutRepository, secuenseDRepository As IBillingSequenceDetailRepository)
        _slipOutRepository = slipOutRepository
        _secuenceDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Obtiene una boleta de salida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByCode(code As String, ByVal audit As AuditMessage) As SlipOut Implements ISlipOutAdminService.GetSlipOutByCode
        Try
            Dim SlipOut = _slipOutRepository.GetSlipOutByCode(code)
            If SlipOut IsNot Nothing AndAlso SlipOut.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SlipOut)(SlipOut, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return SlipOut
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SlipOut
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutById(Id As Integer) As SlipOut Implements ISlipOutAdminService.GetSlipOutById
        Try
            Return _slipOutRepository.GetSlipOutById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SlipOut
        End Try
    End Function

    ''' <summary>
    ''' Guarda una boleta de salida
    ''' </summary>
    ''' <param name="SlipOut"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSlipOut(SlipOut As SlipOut, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of SlipOut) Implements ISlipOutAdminService.SaveSlipOut
        If SlipOut Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._slipOutRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(SlipOut.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            SlipOut.Code = res
                            seq.Next += 1
                            Me._secuenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of SlipOut) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), SlipOut.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of SlipOut) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As SlipOut = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of SlipOut)
                Dim status As Integer

                If SlipOut.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    SlipOut.CreationUser = audit.CodeUser
                    SlipOut.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = SlipOut.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._slipOutRepository.SaveEntity(SlipOut)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of SlipOut)(SlipOut, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                SlipOut.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of SlipOut) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = SlipOut, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SlipOut) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SlipOut) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="numberAdmission"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByNumberAdmission(numberAdmission As String, ByVal audit As AuditMessage) As SlipOut Implements ISlipOutAdminService.GetSlipOutByNumberAdmission
        Try
            Dim SlipOut = _slipOutRepository.GetSlipOutByAdmissionNumber(numberAdmission)
            If SlipOut IsNot Nothing AndAlso SlipOut.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SlipOut)(SlipOut, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return SlipOut
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SlipOut
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _slipOutRepository = Nothing
            _secuenceDRepository = Nothing
            Infrastructure.CrossCutting.Base.IndigoGC.Execute()
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
