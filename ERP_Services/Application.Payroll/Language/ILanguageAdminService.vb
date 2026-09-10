'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ILanguageAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    Function ListAllLanguage() As List(Of Language)

    ''' <summary>
    ''' Obtiene un idioma especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    Function GetLanguage(ByVal code As String) As Language

    ''' <summary>
    ''' Graba o Actualiza un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveLanguage(ByVal language As Language, ByVal audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of Language)

    ''' <summary>
    ''' Elimina un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteLanguage(ByVal language As Language, ByVal audit As AuditMessage) As ActionMessageResult(Of Language)
    Function UpdateStateLanguage(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Language)
End Interface
