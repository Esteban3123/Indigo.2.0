'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 05-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region

Public Class MaintenanceParameterAdminService

    Implements IMaintenanceParameterAdminService

    'Repositorio de tipo de MaintenanceParameter
    Private _maintenanceParameterRespository As IMaintenanceParameterRepository

    ''' <summary>
    ''' inicia el repositorio de MaintenanceParameter
    ''' </summary>
    ''' <param name="MaintenanceParameterRepository">Repositorio de MaintenanceParameter</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal MaintenanceParameterRepository As IMaintenanceParameterRepository)
        If (MaintenanceParameterRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de MaintenanceParameterRepository")
        End If
        _maintenanceParameterRespository = MaintenanceParameterRepository
    End Sub

    ''' <summary>
    ''' Función para Eliminar los parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, audit As AuditMessage) As Boolean Implements IMaintenanceParameterAdminService.DeleteMaintenanceParameter
        If MaintenanceParameter Is Nothing Then
            Throw New ArgumentNullException("Trademark vacio")
        End If
        Dim unitWork As IUnitWork = _maintenanceParameterRespository.UnitWork
        Try
            _maintenanceParameterRespository.DeleteEntity(MaintenanceParameter)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of MaintenanceParameter)(MaintenanceParameter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene los parámetros de Mantenimiento
    ''' </summary>
    ''' <returns>MaintenanceParameter</returns>
    ''' <remarks></remarks>
    Public Function ListMaintenanceParameter() As MaintenanceParameter Implements IMaintenanceParameterAdminService.ListMaintenanceParameter
        Try
            Return _maintenanceParameterRespository.ListMaintenanceParameter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New MaintenanceParameter()
        End Try
    End Function

    ''' <summary>
    ''' Función para Almacenar los Parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, audit As AuditMessage) As Boolean Implements IMaintenanceParameterAdminService.SaveMaintenanceParameter
        If MaintenanceParameter Is Nothing Then
            Throw New ArgumentNullException("MaintenanceParameter Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _maintenanceParameterRespository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceParameter)
            Dim auxMaintenanceParameter As MaintenanceParameter = Nothing
            Dim status As Integer
            If MaintenanceParameter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                MaintenanceParameter.ModificationUser = audit.CodeUser
                MaintenanceParameter.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxMaintenanceParameter = _maintenanceParameterRespository.ListMaintenanceParameter(False)
            Else
                MaintenanceParameter.CreationUser = audit.CodeUser
                MaintenanceParameter.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            'Valido si se va a guardar o a eliminar
            _maintenanceParameterRespository.SaveEntity(MaintenanceParameter)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceParameter)(MaintenanceParameter, audit, status, auxMaintenanceParameter)
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
            _maintenanceParameterRespository = Nothing
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
