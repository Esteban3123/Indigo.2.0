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
''' Interfaz para el servicio de archivadores.
''' </summary>
Public Interface IFileContainerAdminService
     ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <returns>Lista de Archivadores</returns>
    Function ListAllFileContainers(audit As AuditMessage) As List(Of FileContainer)
    ''' <summary>
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="Id">Id del Archivador</param>
    ''' <returns>Objeto Archivador</returns>
    Function GetFileContainer(ByVal Id As Integer, audit As AuditMessage) As FileContainer
    ''' <summary>
    ''' Función para borrar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Function DeleteFileContainer(fileContainer As FileContainer, audit As AuditMessage) As ActionResult(Of FileContainer)
    ''' <summary>
    ''' Función para guardar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveFileContainer(fileContainer As FileContainer, audit As AuditMessage) As ActionResult(Of FileContainer)

End Interface
