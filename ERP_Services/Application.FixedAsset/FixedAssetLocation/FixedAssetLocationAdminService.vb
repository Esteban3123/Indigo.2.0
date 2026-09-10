'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 24/09/2015
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
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FixedAssetLocationAdminService
    Implements IFixedAssetLocationAdminService


#Region "Constructor"

    'Repositorio de tipo de ubicacion
    Private _LocationRespository As IFixedAssetLocationRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="LocationRespository">Repositorio de ubicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal LocationRespository As IFixedAssetLocationRepository)
        If (LocationRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Ubicacion")
        End If
        _LocationRespository = LocationRespository
    End Sub

#End Region

#Region "Methods"

   

    Public Function DeleteLocation(Location As FixedAssetLocation, audit As AuditMessage) As Boolean Implements IFixedAssetLocationAdminService.DeleteLocation
        If Location Is Nothing Then
            Throw New ArgumentNullException("ubicacion vacio")
        End If
        Dim unitWork As IUnitWork = _LocationRespository.UnitWork
        Try
            _LocationRespository.DeleteEntity(Location)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetLocation)(Location, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function GetLocation(codeLocation As String) As FixedAssetLocation Implements IFixedAssetLocationAdminService.GetLocation
        If String.IsNullOrEmpty(codeLocation) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try

            Return _LocationRespository.GetLocation(codeLocation)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New FixedAssetLocation()
        End Try
    End Function

    Public Function ListAllLocation() As List(Of FixedAssetLocation) Implements IFixedAssetLocationAdminService.ListAllLocation
        Try
            Return _LocationRespository.ListAllLocation
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListLocation() As List(Of FixedAssetLocation) Implements IFixedAssetLocationAdminService.ListLocation
        Try
            Return _LocationRespository.ListLocation
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveLocation(Location As List(Of FixedAssetLocation), audit As AuditMessage) As Boolean Implements IFixedAssetLocationAdminService.SaveLocation
        If Location Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If

        Dim unitWork As IUnitWork = _LocationRespository.UnitWork
        Try
            For Each Objeto As FixedAssetLocation In Location

                _LocationRespository.SaveEntity(Objeto)

            Next

            unitWork.Commit()

            For Each Objeto As FixedAssetLocation In Location
                If Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetLocation)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetLocation)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Update, Objeto)
                    auditObject.Execute()
                End If
            Next


            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _LocationRespository = Nothing
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
