'***********************************************************************
' Assembly         : Application.Budget
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2022-01-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPublicPolicyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una política pública
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Function GetPublicPolicy(code As String, audit As AuditMessage) As ActionResult(Of PublicPolicy)

    ''' <summary>
    ''' Guarda o Actualiza una política pública
    ''' </summary>
    ''' <param name="publicPolicy"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuense"></param>
    ''' <returns></returns>
    Function SavePublicPolicy(publicPolicy As PublicPolicy, audit As AuditMessage, Optional ByVal idSecuense As Int64 = 0) As ActionResult(Of PublicPolicy)

    ''' <summary>
    ''' Cambia el estado de una política pública
    ''' </summary>
    ''' <param name="publicPolicyId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStatePublicPolicy(publicPolicyId As Integer, audit As AuditMessage) As ActionResult

End Interface
