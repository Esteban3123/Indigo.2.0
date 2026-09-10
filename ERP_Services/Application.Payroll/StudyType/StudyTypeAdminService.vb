Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class StudyTypeAdminService
    Implements IStudyTypeAdminService

    'Repositorio de tipos de estudio
    Private _StudyTypeRepository As IStudyTypeRepository

    ''' <summary>
    ''' inicia el repositorio de tipos de estudio
    ''' </summary>
    ''' <param name="repository">Repositorio de tipos de estudios</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IStudyTypeRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de tipos de estudio vacio")
        End If
        _StudyTypeRepository = repository
    End Sub

    ''' <summary>
    ''' Elimina un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Public Function DeleteStudyType(studyType As StudyType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of StudyType) Implements IStudyTypeAdminService.DeleteStudyType
        Dim result As New ActionMessageResult(Of StudyType)
        result.StateResult = True
        If studyType Is Nothing Then
            Throw New ArgumentNullException("Tipo de estudios es vacio")
        End If
        Dim unitWork As IUnitWork = _StudyTypeRepository.UnitWork
        Try
            _StudyTypeRepository.DeleteEntity(studyType)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(studyType.GetType.Name, audit.Functional, studyType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of StudyType)(studyType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", studyType.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de estudio especifico
    ''' </summary>
    ''' <param name="code">Código de el tipo de estudio</param>
    ''' <returns> Tipo de estudio</returns>
    Public Function GetStudyType(code As String) As StudyType Implements IStudyTypeAdminService.GetStudyType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _StudyTypeRepository.GetStudyType(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New StudyType()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns>Lista de tipos de estudio</returns>
    Public Function ListAllStudyType() As List(Of StudyType) Implements IStudyTypeAdminService.ListAllStudyType
        Try
            Return _StudyTypeRepository.ListAllStudyType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Public Function SaveStudyType(studyType As StudyType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IStudyTypeAdminService.SaveStudyType
        If studyType Is Nothing Then
            Throw New ArgumentNullException("Tipo de estudio vacio")
        End If
        Dim unitWork As IUnitWork = _StudyTypeRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of StudyType)
            Dim auxStudyType As StudyType = Nothing
            Dim status As Integer

            If studyType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                studyType.ModificationUser = audit.CodeUser
                studyType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxStudyType = _StudyTypeRepository.GetStudyType(studyType.Code, False)
            Else
                studyType.CreationUser = audit.CodeUser
                studyType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _StudyTypeRepository.SaveEntity(studyType)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of StudyType)(studyType, audit, status, auxStudyType)
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
            _StudyTypeRepository = Nothing
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
