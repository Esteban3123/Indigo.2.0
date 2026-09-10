'***********************************************************************
' Assembly         : Application.Common
' Author           : Daniel Eduardo Arévalo
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class TimeUnitAdminService

    Implements ITimeUnitAdminService

    ' Repositorio de tipo de telefono
    Private _timeUnitRepository As ITimeUnitRepository

    Public Sub New(ByVal timeUnitRepository As ITimeUnitRepository)
        If timeUnitRepository Is Nothing Then
            Throw New ArgumentNullException("TimeUnitRepository vacio")
        End If
        _timeUnitRepository = timeUnitRepository
    End Sub

    ''' <summary>
    ''' Elimina una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteTimeUnit(timeUnit As TimeUnit, audit As AuditMessage) As ActionMessageResult(Of TimeUnit) Implements ITimeUnitAdminService.DeleteTimeUnit
        Dim result As New ActionMessageResult(Of TimeUnit)
        result.StateResult = True
        If timeUnit Is Nothing Then
            Throw New ArgumentNullException("timeUnit Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _timeUnitRepository.UnitWork
        Try
            _timeUnitRepository.DeleteEntity(timeUnit)
            UnitOfWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of TimeUnit)(timeUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(timeUnit.GetType.Name, audit.Functional, timeUnit.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", timeUnit.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de la Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnit(code As String) As TimeUnit Implements ITimeUnitAdminService.GetTimeUnit
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _timeUnitRepository.GetTimeUnit(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TimeUnit()
        End Try
    End Function

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function ListAllTimeUnit() As List(Of TimeUnit) Implements ITimeUnitAdminService.ListAllTimeUnit
        Try
            Return _timeUnitRepository.ListAllTimeUnit()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTimeUnit(timeUnit As TimeUnit, audit As AuditMessage) As Boolean Implements ITimeUnitAdminService.SaveTimeUnit
        If timeUnit Is Nothing Then
            Throw New ArgumentNullException("timeUnit Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _timeUnitRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of TimeUnit)
            Dim AuxTimeUnit As TimeUnit = Nothing
            Dim status As Integer

            If timeUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                timeUnit.ModificationUser = audit.CodeUser
                timeUnit.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxTimeUnit = _timeUnitRepository.GetTimeUnitByCode(timeUnit.Code, False)
            Else
                timeUnit.CreationUser = audit.CodeUser
                timeUnit.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _timeUnitRepository.SaveEntity(timeUnit)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of TimeUnit)(timeUnit, audit, status, AuxTimeUnit)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
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
            _timeUnitRepository = Nothing
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
