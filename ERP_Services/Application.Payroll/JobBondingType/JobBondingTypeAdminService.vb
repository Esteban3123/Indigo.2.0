'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
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
Imports System.Data.Entity.Infrastructure

Public Class JobBondingTypeAdminService

    Implements IJobBondingTypeAdminService

    'Repositorio de Tipos de Vinculación Laboral
    Private _JobBondingTypeRepository As IJobBondingTypeRepository

    ''' <summary>
    ''' inicia el repositorio de Tipos de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingTypeRepository">Repositorio de Tipos de Vinculación Laboral</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal jobBondingTypeRepository As IJobBondingTypeRepository)
        If (jobBondingTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Tipos de vinculación laboral vacio")
        End If
        _JobBondingTypeRepository = jobBondingTypeRepository
    End Sub

    ''' <summary>
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código del Tipo de Vinculación Laboral</param>
    ''' <returns>Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function GetJobBondingType(code As String) As JobBondingType Implements IJobBondingTypeAdminService.GetJobBondingType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _JobBondingTypeRepository.GetJobBondingType(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New JobBondingType()
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function ListAllJobBondingType() As List(Of JobBondingType) Implements IJobBondingTypeAdminService.ListAllJobBondingType
        Try
            Return _JobBondingTypeRepository.ListAllJobBondingType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveJobBondingType(jobBondingType As JobBondingType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IJobBondingTypeAdminService.SaveJobBondingType
        If jobBondingType Is Nothing Then
            Throw New ArgumentNullException("Tipo de vinculación vacio")
        End If
        Dim unitWork As IUnitWork = _JobBondingTypeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of JobBondingType)
            Dim auxJobBondingType As JobBondingType = Nothing
            Dim status As Integer

            If jobBondingType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                jobBondingType.ModificationUser = audit.CodeUser
                jobBondingType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxJobBondingType = _JobBondingTypeRepository.GetJobBondingType(jobBondingType.Code, False)
            Else
                jobBondingType.CreationUser = audit.CodeUser
                jobBondingType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _JobBondingTypeRepository.SaveEntity(jobBondingType)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of JobBondingType)(jobBondingType, audit, status, auxJobBondingType)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteJobBondingType(jobBondingType As JobBondingType, audit As AuditMessage) As Domain.Base.Entities.ActionMessageResult(Of JobBondingType) Implements IJobBondingTypeAdminService.DeleteJobBondingType
        Dim result As New ActionMessageResult(Of JobBondingType)
        result.StateResult = True
        If jobBondingType Is Nothing Then
            Throw New ArgumentNullException("Tipos de vinculación laboral vacio")
        End If
        Dim unitWork As IUnitWork = _JobBondingTypeRepository.UnitWork
        Try
            _JobBondingTypeRepository.DeleteEntity(jobBondingType)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(jobBondingType.GetType.Name, audit.Functional, jobBondingType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of JobBondingType)(jobBondingType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", jobBondingType.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
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
            _JobBondingTypeRepository = Nothing
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
