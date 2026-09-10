

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class LocationAdminService
    Implements ILocationAdminService





    'Repositorio de tipo de ubicacion
    Private _LocationRespository As ILocationRespository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="LocationRespository">Repositorio de ubicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal LocationRespository As ILocationRespository)
        If (LocationRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Ubicacion")
        End If
        _LocationRespository = LocationRespository
    End Sub

    Public Function DeleteLocation(Location As Location, audit As AuditMessage) As Boolean Implements ILocationAdminService.DeleteLocation
        If Location Is Nothing Then
            Throw New ArgumentNullException("ubicacion vacio")
        End If
        Dim unitWork As IUnitWork = _LocationRespository.UnitWork
        Try
            _LocationRespository.DeleteEntity(Location)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Location)(Location, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function GetLocation(codeLocation As String) As Location Implements ILocationAdminService.GetLocation
        If String.IsNullOrEmpty(codeLocation) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try

            Return _LocationRespository.GetLocation(codeLocation)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New Location()
        End Try
    End Function

    Public Function ListAllLocation() As List(Of Location) Implements ILocationAdminService.ListAllLocation
        Try
            Return _LocationRespository.ListAllLocation
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListLocation() As List(Of Location) Implements ILocationAdminService.ListLocation
        Try
            Return _LocationRespository.ListLocation
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveLocation(Location As List(Of Location), audit As AuditMessage) As Boolean Implements ILocationAdminService.SaveLocation
        If Location Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If

        Dim unitWork As IUnitWork = _LocationRespository.UnitWork
        Try
            For Each Objeto As Location In Location

                _LocationRespository.SaveEntity(Objeto)

            Next

            unitWork.Commit()

            For Each Objeto As Location In Location
                If Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Location)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf Objeto.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Location)(Objeto, audit, Infrastructure.CrossCutting.Audit.Actions.Update, Objeto)
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
