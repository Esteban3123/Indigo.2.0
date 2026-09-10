'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure

Public Class StudyCenterAdminService
    Implements IStudyCenterAdminService

    'Repositorio de Centros de Estudio
    Private _StudyCenterRepository As IStudyCenterRepository

    ''' <summary>
    ''' inicia el repositorio de Centros de estudio
    ''' </summary>
    ''' <param name="repository">Repositorio de Centros de estudios</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IStudyCenterRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de centros de estudio vacio")
        End If
        _StudyCenterRepository = repository
    End Sub

    ''' <summary>
    ''' Elimina un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteStudyCenter(studyCenter As StudyCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of StudyCenter) Implements IStudyCenterAdminService.DeleteStudyCenter
        Dim result As New ActionMessageResult(Of StudyCenter)
        result.StateResult = True
        If studyCenter Is Nothing Then
            Throw New ArgumentNullException("Centros de estudios es vacio")
        End If
        Dim unitWork As IUnitWork = _StudyCenterRepository.UnitWork
        Try
            _StudyCenterRepository.DeleteEntity(studyCenter)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(studyCenter.GetType.Name, audit.Functional, studyCenter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of StudyCenter)(studyCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", studyCenter.Code))
            Return result
            unitWork.RollbackChanges()
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Centro de Estudio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    Public Function GetStudyCenter(code As String) As StudyCenter Implements IStudyCenterAdminService.GetStudyCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _StudyCenterRepository.GetStudyCenter(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New StudyCenter()
        End Try
    End Function

    ''' <summary>
    ''' Lista de Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudio</returns>
    ''' <remarks></remarks>
    Public Function ListAllStudyCenter() As List(Of StudyCenter) Implements IStudyCenterAdminService.ListAllStudyCenter
        Try
            Return _StudyCenterRepository.ListAllStudyCenter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actuliza un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveStudyCenter(studyCenter As StudyCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IStudyCenterAdminService.SaveStudyCenter
        If studyCenter Is Nothing Then
            Throw New ArgumentNullException("Centro de estudio vacio")
        End If
        Dim unitWork As IUnitWork = _StudyCenterRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of StudyCenter)
            Dim auxStudyCenter As StudyCenter = Nothing
            Dim status As Integer

            If studyCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                studyCenter.ModificationUser = audit.CodeUser
                studyCenter.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxStudyCenter = _StudyCenterRepository.GetStudyCenter(studyCenter.Code, False)
            Else
                studyCenter.CreationUser = audit.CodeUser
                studyCenter.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _StudyCenterRepository.SaveEntity(studyCenter)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of StudyCenter)(studyCenter, audit, status, auxStudyCenter)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _StudyCenterRepository = Nothing
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
