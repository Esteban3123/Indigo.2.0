'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Pablo Alexander Salazar Sanchez
' Created          : 15/12/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMainAccountLevelsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los Niveles de cuntas contables
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function GetAllMainAccountLevels(ByVal audit As AuditMessage) As List(Of MainAccountLevels)

    ''' <summary>
    ''' Guarda o Actualiza los Niveles de cuntas contables
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MainAccountLevels)

    ''' <summary>
    ''' Elimina un Nivele de cunta contable
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene los Niveles de cuntas contables por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetMainAccountLevelsByCode(code As String, audit As AuditMessage) As ActionResult(Of MainAccountLevels)

    ''' <summary>
    ''' Obtiene los Niveles de cuntas contables por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMainAccountLevelsById(id As Integer, audit As AuditMessage) As ActionResult(Of MainAccountLevels)

    ' <summary>
    ' obtiene un Nivele de cuntas contables por digitos
    ' </summary>
    ' <returns></returns>
    ' <remarks></remarks>
    ' Function GetMainAccountLevelsByDigits(ByVal digits As Integer, ByVal audit As AuditMessage) As ActionResult(Of MainAccountLevels)

End Interface
