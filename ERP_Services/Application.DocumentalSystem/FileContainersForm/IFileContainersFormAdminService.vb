'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 21-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz para el servicio de archivador por formulario.
''' </summary>
Public Interface IFileContainersFormAdminService

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListAllFileContainersForm(audit As AuditMessage) As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdFileContainer(ByVal IdFileContainer As Integer, audit As AuditMessage) As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del Formulario</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdForm(ByVal IdForm As Integer, audit As AuditMessage) As List(Of FileContainersForm)
    ''' <summary>
    ''' Función para guardar un formulario asociado a un archivador
    ''' </summary>
    ''' <param name="fileContainerForm">Objeto Formulario Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveFileContainerForms(ByVal fileContainerForm As FileContainersForm, audit As AuditMessage) As ActionResult(Of FileContainersForm)
    ''' <summary>
    ''' Función para borrar un formulario asociado a archivador
    ''' </summary>
    ''' <param name="fileContainerForm">Objeto Formulario Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Function DeleteFileContainerForms(ByVal fileContainerForm As FileContainersForm, audit As AuditMessage) As ActionResult(Of FileContainersForm)


End Interface
