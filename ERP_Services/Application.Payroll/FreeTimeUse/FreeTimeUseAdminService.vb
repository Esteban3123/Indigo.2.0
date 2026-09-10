'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Data.Entity.Core
Imports Application.Payroll

Public Class FreeTimeUseAdminService
    Implements IFreeTimeUseAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio de las Actividades de Tiempo Libre
    ''' </summary>
    Private _FreeTimeUseRepository As IFreeTimeUseRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IPayrollSequenceDetailRepository

#End Region

    ''' <summary>
    ''' inicia el repositorio de Actividades de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUseRepository">Repositorio de Actividades de Tiempo Libre</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal freeTimeUseRepository As IFreeTimeUseRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If (freeTimeUseRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Actividades de Tiempo Libre vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _FreeTimeUseRepository = freeTimeUseRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FreeTimeUse) Implements IFreeTimeUseAdminService.ChangeState
        Dim freeTimeUse As FreeTimeUse = _FreeTimeUseRepository.GetFreeTimeUse(code, True)
        freeTimeUse.State = state
        Return SaveFreeTimeUse(freeTimeUse, audit)
    End Function

    ''' <summary>
    ''' Elimina una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFreeTimeUse(freeTimeUse As FreeTimeUse, audit As AuditMessage) As ActionResult Implements IFreeTimeUseAdminService.DeleteFreeTimeUse
        If freeTimeUse Is Nothing Then
            Throw New ArgumentNullException("freeTimeUse")
        End If
        Dim unitOfWork As IUnitWork = Me._FreeTimeUseRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of FreeTimeUse)
            auditProcess = New IndigoAuditSimpleEntity(Of FreeTimeUse)(freeTimeUse, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._FreeTimeUseRepository.DeleteEntity(freeTimeUse)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUse(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of FreeTimeUse) Implements IFreeTimeUseAdminService.GetFreeTimeUse
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim freeTimeUse As FreeTimeUse = Me._FreeTimeUseRepository.GetFreeTimeUse(code.Trim(), tracking)
            If freeTimeUse IsNot Nothing AndAlso freeTimeUse.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FreeTimeUse)(freeTimeUse, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = True, .ObjectEmbbeded = freeTimeUse}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    '''  Obtiene una Actividad de Tiempo Libre por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUseById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of FreeTimeUse) Implements IFreeTimeUseAdminService.GetFreeTimeUseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim freeTimeUse As FreeTimeUse = Me._FreeTimeUseRepository.GetFreeTimeUseById(id, tracking)
            If freeTimeUse IsNot Nothing AndAlso freeTimeUse.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FreeTimeUse)(freeTimeUse, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = True, .ObjectEmbbeded = freeTimeUse}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


    ''' <summary>
    ''' Guarda o actualiza una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFreeTimeUse(freeTimeUse As FreeTimeUse, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FreeTimeUse) Implements IFreeTimeUseAdminService.SaveFreeTimeUse
        If freeTimeUse Is Nothing Then
            Throw New ArgumentNullException("freeTimeUse")
        End If
        Dim unitOfWork As IUnitWork = Me._FreeTimeUseRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If freeTimeUse.Code Is Nothing OrElse freeTimeUse.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        freeTimeUse.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxFreeTimeUse As FreeTimeUse = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of FreeTimeUse)
            Dim status As Integer

            If freeTimeUse.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                freeTimeUse.CreationUser = audit.CodeUser
                freeTimeUse.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxFreeTimeUse = _FreeTimeUseRepository.GetFreeTimeUse(freeTimeUse.Code, False)
                freeTimeUse.ModificationUser = audit.CodeUser
                freeTimeUse.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._FreeTimeUseRepository.SaveEntity(freeTimeUse)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of FreeTimeUse)(freeTimeUse, audit, status, auxFreeTimeUse)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            freeTimeUse.MarkAsUnchanged()

            Return New ActionResult(Of FreeTimeUse) With {.StateResult = True, .ObjectEmbbeded = freeTimeUse}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FreeTimeUse) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _FreeTimeUseRepository = Nothing
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
