'***********************************************************************
' Assembly         : Application.Auhtorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IAuthorizationSourceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAuthorizationSource(ByVal AuthorizationSource As AuthorizationSource, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AuthorizationSource)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAuthorizationSource(ByVal AuthorizationSource As AuthorizationSource, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAuthorizationSource(ByVal code As String, ByVal audit As AuditMessage) As AuthorizationSource

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAuthorizationSourceById(ByVal id As Integer) As AuthorizationSource

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateAuthorizationSource(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AuthorizationSource)

End Interface
