'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IHealthAdministratorAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una entidad administradora de salud
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveHealthAdministrator(ByVal HealthAdministrator As HealthAdministrator, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of HealthAdministrator)

    ''' <summary>
    ''' Elimina una ntidad administradora de salud
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteHealthAdministrator(ByVal HealthAdministrator As HealthAdministrator, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una ntidad administradora de salud por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetHealthAdministrator(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of HealthAdministrator)

    ''' <summary>
    ''' Obtiene una ntidad administradora de salud por id
    ''' </summary>
    ''' <returns></returns>
    Function GetHealthAdministratorById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of HealthAdministrator)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateHealthAdministrator(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of HealthAdministrator)

End Interface
