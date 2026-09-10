'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IVieBotAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista la configuración de VieBot por formulario
    ''' </summary>
    ''' <param name="Form">Form</param>
    ''' <returns></returns>
    Function GetVieBotByForm(ByVal Form As String) As ActionResult(Of List(Of VieBot))

    ''' <summary>
    ''' Guarda o Actualiza un las configuraciones de VieBot
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveVieBot(ByVal ListVieBot As List(Of VieBot), ByVal audit As AuditMessage) As ActionResult(Of List(Of VieBot))

End Interface
