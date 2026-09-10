#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class BrandAdminService
    Implements IBrandAdminService

    'Repositorio de la marca
    Private _BrandRepository As IBrandRepository

    ''' <summary>
    ''' inicia el repositorio de la marca
    ''' </summary>
    ''' <param name="BrandRepository">Repositorio de la marca</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal BrandRepository As IBrandRepository)
        If (BrandRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de la marca")
        End If
        _BrandRepository = BrandRepository
    End Sub

    Public Function DeleteBrand(Brand As Brand, audit As AuditMessage) As Boolean Implements IBrandAdminService.DeleteBrand
        If Brand Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _BrandRepository.UnitWork
        Try
            Brand.MarkAsDeleted()
            _BrandRepository.DeleteEntity(Brand)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Brand)(Brand, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function GetBrand(codeBrand As String) As Brand Implements IBrandAdminService.GetBrand
        If String.IsNullOrEmpty(codeBrand) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _BrandRepository.GetBrand(codeBrand)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New Brand()
        End Try
    End Function

    Public Function ListAllBrand() As List(Of Brand) Implements IBrandAdminService.ListAllBrand
        Try
            Return _BrandRepository.ListAllBrand
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveBrand(Brand As Brand, audit As AuditMessage) As Boolean Implements IBrandAdminService.SaveBrand
        If Brand Is Nothing Then
            Throw New ArgumentNullException("tipo de marca")
        End If
        Dim unitWork As IUnitWork = _BrandRepository.UnitWork
        Try
            'Valido si se guarda o se edita
            If Brand.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                _BrandRepository.SaveEntity(Brand)
            ElseIf Brand.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                _BrandRepository.UpdateEntity(Brand)
            End If
            unitWork.Commit()
            If Brand.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Brand)(Brand, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Brand.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Brand)(Brand, audit, Infrastructure.CrossCutting.Audit.Actions.Update, Brand)
                auditObject.Execute()
            End If
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
            _BrandRepository = Nothing
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
