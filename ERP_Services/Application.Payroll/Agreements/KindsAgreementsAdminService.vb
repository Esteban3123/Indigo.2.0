'***********************************************************************
' Assembly         : Application.Payrol
' Author           : Rafael Patiño
' Created          : 03-01-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class KindsAgreementsAdminService
    Implements IKindsAgreementsAdminService


    Private _KindsAgreementsRepository As IKindsAgreementsRepository

    Public Sub New(ByVal IKindsAgreementsRepository As IKindsAgreementsRepository)
        If IKindsAgreementsRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _KindsAgreementsRepository = IKindsAgreementsRepository
    End Sub

    ''' <summary>
    ''' Funcion para cargar un objeto de clases de convenios
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKindsAgreements(code As String, audit As AuditMessage) As KindsAgreements Implements IKindsAgreementsAdminService.GetKindsAgreements
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Dim KindsAgreements = _KindsAgreementsRepository.GetKindsAgreements(code, True)
            If KindsAgreements.Code > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of KindsAgreements)(KindsAgreements, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return KindsAgreements
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para guardar una nueva clases de convenios
    ''' </summary>
    ''' <param name="KindsAgreements"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveKindsAgreements(ByVal KindsAgreements As KindsAgreements, ByVal audit As AuditMessage) As ActionResult(Of KindsAgreements) Implements IKindsAgreementsAdminService.SaveKindsAgreements
        If KindsAgreements Is Nothing Then
            Throw New ArgumentNullException("Clase de convenio vacio")
        End If
        Dim _unitWork As IUnitWork = _KindsAgreementsRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of KindsAgreements)
            Dim _KindsAgreementsAux As KindsAgreements = Nothing
            Dim status As Integer

            If KindsAgreements.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                KindsAgreements.ModificationUser = audit.CodeUser
                KindsAgreements.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                '_KindsAgreementsAux = _KindsAgreementsRepository.GetKindsAgreements(KindsAgreements.Code, False)
            Else
                KindsAgreements.CreationUser = audit.CodeUser
                KindsAgreements.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _KindsAgreementsRepository.SaveEntity(KindsAgreements)
            _unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of KindsAgreements)(KindsAgreements, audit, status, _KindsAgreementsAux)
            auditProcess.Execute()
            Return New ActionResult(Of KindsAgreements) With {.StateResult = True, .ObjectEmbbeded = KindsAgreements}
        Catch ex As OptimisticConcurrencyException
            _unitWork.RollbackChanges()
            Return New ActionResult(Of KindsAgreements) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            _unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of KindsAgreements) With {.StateResult = False, .MessageResult = New List(Of String)()}
        End Try
    End Function

    ''' <summary>
    ''' Lista clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListKindsAgreements(ByVal audit As AuditMessage) As List(Of KindsAgreements) Implements IKindsAgreementsAdminService.ListKindsAgreements
        Try
            Dim ListKinds = _KindsAgreementsRepository.ListKindsAgreements()
            If ListKinds.Count > 0 Then
                For Each item As KindsAgreements In ListKinds
                    Dim auditObject As New IndigoAuditSimpleEntity(Of KindsAgreements)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                    auditObject.Execute()
                Next
            End If
            Return ListKinds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para eliminar una clase de convenio
    ''' </summary>
    ''' <param name="KindsAgreements">clase de convenio</param>
    ''' <param name="audit">Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteKindsAgreements(KindsAgreements As KindsAgreements, audit As AuditMessage) As ActionMessageResult(Of KindsAgreements) Implements IKindsAgreementsAdminService.DeleteKindsAgreements
        Dim result As New ActionMessageResult(Of KindsAgreements)
        result.StateResult = True
        If KindsAgreements Is Nothing Then
            Throw New ArgumentNullException("Clase de convenios vacio")
        End If
        Dim _unitWork As IUnitWork = _KindsAgreementsRepository.UnitWork
        Try
            _KindsAgreementsRepository.SaveEntity(KindsAgreements)
            _unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of KindsAgreements)(KindsAgreements, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            _unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", KindsAgreements.Code))
            Return result
        Catch ex As Exception
            _unitWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _KindsAgreementsRepository = Nothing
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
