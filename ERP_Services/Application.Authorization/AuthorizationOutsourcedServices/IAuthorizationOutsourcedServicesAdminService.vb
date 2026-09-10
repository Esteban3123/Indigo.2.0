'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IAuthorizationOutsourcedServicesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAuthorizationOutsourcedServices(ByVal AuthorizationOutsourcedServices As AuthorizationOutsourcedServices, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AuthorizationOutsourcedServices)

    ''' <summary>
    ''' Obtiene por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAuthorizationOutsourcedServices(ByVal code As String) As ActionResult(Of AuthorizationOutsourcedServices)

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAuthorizationOutsourcedServicesById(ByVal id As Integer) As ActionResult(Of AuthorizationOutsourcedServices)

End Interface
