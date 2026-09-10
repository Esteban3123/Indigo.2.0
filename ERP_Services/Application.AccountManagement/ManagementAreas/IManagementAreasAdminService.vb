'***********************************************************************
' Assembly         : Application.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-11-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IManagementAreasAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos las areas de gestion autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListManagementAreasByUserCode(userCode As String) As List(Of ManagementAreas)


    ''' <summary>
    ''' elimina una autorización
    ''' </summary>
    ''' <param name="ManagementAreas">The ManagementAreas authorization.</param>
    ''' <returns></returns>
    Function DeleteManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda una autorización
    ''' </summary>
    ''' <param name="ManagementAreas">The ManagementAreas authorization.</param>
    ''' <returns></returns>
    Function SaveManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene una autorizacion de la area de gestion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManagementAreasById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene una autorización del area de gestion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManagementAreasByCode(code As String, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateManagementAreas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene todas las areas de gestión activas
    ''' </summary>
    ''' <returns></returns>
    Function GetAllManagementAreas() As ActionResult(Of List(Of ManagementAreas))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="assignmentDate"></param>
    ''' <param name="managementAreaCode"></param>
    ''' <returns></returns>
    Function GetTimeStatus(assignmentDate As DateTime?, managementAreaCode As String) As Integer?

End Interface

