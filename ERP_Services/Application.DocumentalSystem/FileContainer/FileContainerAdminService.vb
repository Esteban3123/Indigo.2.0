'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 22-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Domain.DocumentalSystem
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Audit
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.DocumentalSystem.Entities
Imports Application.Base
Imports System.Data.SqlTypes

#End Region

''' <summary>
''' Servicio archivadores.
''' </summary>
''' <remarks></remarks>
Public Class FileContainersAdminService
    Implements IFileContainerAdminService

    Private _FileContainerRepository As IFileContainerRepository
    Private _FileContainerFormsRepository As IFileContainersFormRepository
    Private _MetaDataRepository As IMetaDataRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="FileContainersFormAdminService" />.
    ''' </summary>
    ''' <param name="FileContainerRepository">el repositorio para el manejo de los archivadores.</param>
    ''' <param name="FileContainerFormsRepository">el repositorio para el manejo de los formularios por archivadores.</param>
    Public Sub New(ByVal FileContainerRepository As IFileContainerRepository, ByVal FileContainerFormsRepository As IFileContainersFormRepository, ByVal MetaDataRepository As IMetaDataRepository)
        If FileContainerRepository Is Nothing Then
            Throw New ArgumentNullException("FileContainerRepository Vacío")
        End If
        If FileContainerFormsRepository Is Nothing Then
            Throw New ArgumentNullException("FileContainerFormsRepository Vacío")
        End If
        _FileContainerRepository = FileContainerRepository
        _FileContainerFormsRepository = FileContainerFormsRepository
        _MetaDataRepository = MetaDataRepository
    End Sub


    ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <returns>Lista de Archivadores</returns>
    Public Function ListAllFileContainers(audit As AuditMessage) As List(Of FileContainer) Implements IFileContainerAdminService.ListAllFileContainers
        Try
            Return _FileContainerRepository.ListAllFileContainers()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="Id">Id del Archivador</param>
    ''' <returns>Objeto Archivador</returns>
    Function GetFileContainer(ByVal Id As Integer, audit As AuditMessage) As FileContainer Implements IFileContainerAdminService.GetFileContainer
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _FileContainerRepository.GetFileContainer(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para borrar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteFileContainer(fileContainer As FileContainer, audit As AuditMessage) As ActionResult(Of FileContainer) Implements IFileContainerAdminService.DeleteFileContainer
        If fileContainer Is Nothing Then
            Throw New ArgumentNullException("FileContainer vacío")
        End If
        Dim unitOfWork As IUnitWork = _FileContainerRepository.UnitWork
        Dim uWForms As IUnitWork = _FileContainerFormsRepository.UnitWork
        Dim uwMeta As IUnitWork = _MetaDataRepository.UnitWork
        Try
            Do While fileContainer.Metadata.Count > 0
                _MetaDataRepository.DeleteEntity(fileContainer.Metadata(0))
                uwMeta.CommitAndRefreshChanges()
            Loop

            Do While fileContainer.FileContainersForm.Count > 0
                _FileContainerFormsRepository.DeleteEntity(fileContainer.FileContainersForm(0))
                uWForms.CommitAndRefreshChanges()
            Loop

            unitOfWork.CommitAndRefreshChanges()

            _FileContainerRepository.DeleteEntity(fileContainer)
            unitOfWork.CommitAndRefreshChanges()
            Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainer)(fileContainer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult(Of FileContainer) With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({ex.Message})}
        End Try
    End Function

    ''' <summary>
    ''' Función para guardar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveFileContainer(fileContainer As FileContainer, audit As AuditMessage) As ActionResult(Of FileContainer) Implements IFileContainerAdminService.SaveFileContainer
        If fileContainer Is Nothing Then
            Throw New ArgumentNullException("FileContainer vacío")
        End If
        Dim unitOfWork As IUnitWork = _FileContainerRepository.UnitWork
        Dim unitOfWorkForms As IUnitWork = _FileContainerFormsRepository.UnitWork
        Dim AuxListFileContainerForm As New List(Of FileContainersForm)
        Dim AuxListMetadata As New List(Of Metadata)
        Try

            Dim AuxFileContainer As FileContainer = Nothing
            If fileContainer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                AuxFileContainer = _FileContainerRepository.GetFileContainer(fileContainer.Id, False)
            End If
            _FileContainerRepository.SaveEntity(fileContainer)
            unitOfWork.Commit()

            'Consulto los valores originales de la entidad FileContainersForm
            For i As Integer = 0 To fileContainer.FileContainersForm.Count - 1
                If fileContainer.FileContainersForm(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim AuxFileContainerForm = _FileContainerFormsRepository.ListFileContainersFormById(fileContainer.Id, False)
                    AuxListFileContainerForm.Add(AuxFileContainerForm)
                End If
            Next

            'Consulto los valores originales de la entidad metadata
            For i As Integer = 0 To fileContainer.Metadata.Count - 1
                If fileContainer.Metadata(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim AuxMetadata = _MetaDataRepository.getMetadataFormById(fileContainer.Metadata(i).Id, False)
                    AuxListMetadata.Add(AuxMetadata)
                End If
            Next

            'Audito el objeto contenedor
            If (fileContainer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added) Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainer)(fileContainer, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf fileContainer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainer)(fileContainer, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxFileContainer)
                auditObject.Execute()
            ElseIf fileContainer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainer)(fileContainer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
            End If


            'Audito la lista de Formulario del contenedor
            For i As Integer = 0 To fileContainer.FileContainersForm.Count - 1
                Dim x = i
                If fileContainer.FileContainersForm(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim result = AuxListFileContainerForm.Where(Function(c) c.Id = fileContainer.FileContainersForm(x).Id).SingleOrDefault
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainersForm)(fileContainer.FileContainersForm(i), audit, Infrastructure.CrossCutting.Audit.Actions.Update, result)
                    auditObject.Execute()
                ElseIf fileContainer.FileContainersForm(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainersForm)(fileContainer.FileContainersForm(i), audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf fileContainer.FileContainersForm(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of FileContainersForm)(fileContainer.FileContainersForm(i), audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                    auditObject.Execute()
                End If
            Next

            'Audito la lista de metadata
            For i As Integer = 0 To fileContainer.Metadata.Count - 1
                Dim x = i
                If fileContainer.Metadata(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim result = AuxListMetadata.Where(Function(c) c.Id = fileContainer.Metadata(x).Id).SingleOrDefault
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Metadata)(fileContainer.Metadata(i), audit, Infrastructure.CrossCutting.Audit.Actions.Update, result)
                    auditObject.Execute()
                ElseIf fileContainer.Metadata(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Metadata)(fileContainer.Metadata(i), audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf fileContainer.Metadata(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Metadata)(fileContainer.Metadata(i), audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                    auditObject.Execute()
                End If
            Next

            Return New ActionResult(Of FileContainer) With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FileContainer) With {.StateResult = False, .MessageResult = New List(Of String)({ex.Message})}
        End Try
    End Function
End Class

