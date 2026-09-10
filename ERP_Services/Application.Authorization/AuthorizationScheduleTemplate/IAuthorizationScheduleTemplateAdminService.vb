'***********************************************************************
' Assembly         : Application.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IAuthorizationScheduleTemplateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAuthorizationScheduleTemplate(ByVal AuthorizationScheduleTemplate As AuthorizationScheduleTemplate, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AuthorizationScheduleTemplate)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAuthorizationScheduleTemplate(ByVal AuthorizationScheduleTemplate As AuthorizationScheduleTemplate, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAuthorizationScheduleTemplate(ByVal code As String, ByVal audit As AuditMessage) As AuthorizationScheduleTemplate

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAuthorizationScheduleTemplateById(ByVal id As Integer) As AuthorizationScheduleTemplate

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateAuthorizationScheduleTemplate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AuthorizationScheduleTemplate)

End Interface
