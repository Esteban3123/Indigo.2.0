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
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.DocumentalSystem.Entities
Imports Application.Base
Imports System.Data.SqlTypes

#End Region

''' <summary>
''' Servicio archivadores.
''' </summary>
''' <remarks></remarks>
Public Class FileContainersFormAdminService
    Implements IFileContainersFormAdminService


    Private _FileContainersformRepository As IFileContainersFormRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="FileContainersFormAdminService" />.
    ''' </summary>
    ''' <param name="FileContainersFormRepository">el repositorio para el manejo de los archivadores por formulario.</param>
    Public Sub New(ByVal FileContainersFormRepository As IFileContainersFormRepository)
        If FileContainersFormRepository Is Nothing Then
            Throw New ArgumentNullException("FileContainersformRepository Vacio")
        End If
        _FileContainersformRepository = FileContainersFormRepository
    End Sub

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <returns>Lista de FileContainersForm</returns>
    Public Function ListAllFileContainersForm(audit As AuditMessage) As List(Of FileContainersForm) Implements IFileContainersFormAdminService.ListAllFileContainersForm
        Try
            Return _FileContainersformRepository.ListAllFileContainersForm
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListFileContainersFormByIdFileContainer(IdFileContainer As Integer, audit As AuditMessage) As List(Of FileContainersForm) Implements IFileContainersFormAdminService.ListFileContainersFormByIdFileContainer
        If String.IsNullOrEmpty(IdFileContainer) = True Then
            Throw New ArgumentNullException("IdFileContainer vacío")
        End If
        Try
            Return _FileContainersformRepository.ListFileContainersFormByIdFileContainer(IdFileContainer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListFileContainersFormByIdForm(IdForm As Integer, audit As AuditMessage) As List(Of FileContainersForm) Implements IFileContainersFormAdminService.ListFileContainersFormByIdForm
        If String.IsNullOrEmpty(IdForm) = True Then
            Throw New ArgumentNullException("IdForm vacío")
        End If
        Try
            Return _FileContainersformRepository.ListFileContainersFormByIdForm(IdForm)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function


    Public Function DeleteFileContainerForms(fileContainerForm As FileContainersForm, audit As AuditMessage) As ActionResult(Of FileContainersForm) Implements IFileContainersFormAdminService.DeleteFileContainerForms

    End Function

    Public Function SaveFileContainerForms(fileContainerForm As FileContainersForm, audit As AuditMessage) As ActionResult(Of FileContainersForm) Implements IFileContainersFormAdminService.SaveFileContainerForms

    End Function
End Class

