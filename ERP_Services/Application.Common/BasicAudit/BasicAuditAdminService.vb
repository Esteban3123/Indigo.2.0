'***********************************************************************
' Assembly         : Application.Common
' Author           : Juan Diego Diaz
' Created          : 25-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Application.Base

Public Class BasicAuditAdminService
    Implements IBasicAuditAdminService

    ' Repositorio de auditoria basica
    Private _basicAuditRepository As IBasicAuditRepository

    Public Sub New(ByVal basicAuditRepository As IBasicAuditRepository)
        If basicAuditRepository Is Nothing Then
            Throw New ArgumentNullException("basicAuditRepository vacío")
        End If
        _basicAuditRepository = basicAuditRepository
    End Sub

    ''' <summary>
    ''' Obtiene una registro de auditoria basica.
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>Registro Auditoria Basica</returns>
    Public Function GetBasicAuditById(Code As String) As BasicAudit Implements IBasicAuditAdminService.GetBasicAuditById
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Código Vacío")
        End If
        Try
            Return _basicAuditRepository.GetBasicAuditById(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los registros de auditoria basica.
    ''' </summary>
    ''' <returns>Lista de Registros Auditoria Basica</returns>
    Public Function ListAllBasicAudit() As List(Of BasicAudit) Implements IBasicAuditAdminService.ListAllBasicAudit
        Try
            Return _basicAuditRepository.ListAllBasicAudit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de registros de auditoria basica.
    ''' </summary>
    ''' <param name="Entity">Entidad</param>
    ''' <returns>Lista de Registros Auditoria Basica</returns>
    Public Function ListBasicAuditByEntity(entity As String) As List(Of BasicAudit) Implements IBasicAuditAdminService.ListBasicAuditByEntity
        If String.IsNullOrEmpty(entity) Then
            Throw New ArgumentNullException("Registro Entidad Vacia")
        End If
        Try
            Return _basicAuditRepository.ListBasicAuditByEntity(entity)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una registro de auditoria basica.
    ''' </summary>
    ''' <param name="IdForm">Tag del formulario</param>
    ''' <param name="IdEntity">Id de la Entidad</param>
    ''' <returns>Lista de Registros Auditoria Basica</returns>
    Public Function ListBasicAuditByIdIdFormAndIdEntity(IdForm As String, IdEntity As String) As List(Of BasicAudit) Implements IBasicAuditAdminService.ListBasicAuditByIdIdFormAndIdEntity
        If String.IsNullOrEmpty(IdForm) Then
            Throw New ArgumentNullException("IdForm Vacío")
        End If
        If String.IsNullOrEmpty(IdEntity) Then
            Throw New ArgumentNullException("IdEntity Vacío")
        End If
        Try
            Return _basicAuditRepository.ListBasicAuditByIdIdFormAndIdEntity(IdForm, IdEntity)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una registro de auditoria basica.
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>Registro Auditoria Basica</returns>
    Public Function ListBasicAuditByIdUsuario(Code As String) As List(Of BasicAudit) Implements IBasicAuditAdminService.ListBasicAuditByIdUsuario
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Código Vacío")
        End If
        Try
            Return _basicAuditRepository.ListBasicAuditByIdUsuario(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtine registros de auditoria basica eliminados según formulario.
    ''' </summary>
    ''' <param name="Tag">Formulario</param>
    ''' <returns>Lista de Registros de Auditoria Basica</returns>
    Public Function ListBasicAuditByTag(Tag As String) As List(Of BasicAudit) Implements IBasicAuditAdminService.ListBasicAuditByTag
        If String.IsNullOrEmpty(Tag) Then
            Throw New ArgumentNullException("Tag Vacío")
        End If
        Try
            Return _basicAuditRepository.ListBasicAuditByTag(Tag)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "AplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para guardar la auditoria basica
    ''' </summary>
    ''' <param name="Id">Id de la entidad</param>
    ''' <param name="NameEntity">Nombre de la entidad</param>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBasicAudit(Id As String, NameEntity As String, Company As String, audit As AuditMessage, ActionAudit As ActionsAudit, Optional Parameters As String = "", Optional ReportName As String = "", Optional ByVal count As Integer = 1) As ActionResult(Of BasicAudit) Implements IBasicAuditAdminService.SaveBasicAudit
        Try
            IndigoAuditBasic.Execute(NameEntity, audit.Functional, Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionAudit, Company, audit.ContainerSecurity, Parameters, ReportName, count)
            Return New ActionResult(Of BasicAudit) With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult(Of BasicAudit) With {.StateResult = False}
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _basicAuditRepository = Nothing
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
