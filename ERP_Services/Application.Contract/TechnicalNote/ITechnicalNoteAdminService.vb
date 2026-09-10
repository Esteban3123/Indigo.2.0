#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ITechnicalNoteAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una vigencia de nota técnica por id
    ''' </summary>
    ''' <returns></returns>
    Function GetTechnicalNoteById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Obtiene una vigencia de nota técnica por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetTechnicalNote(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Guarda o Actualiza una vigencia de nota técnica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveTechnicalNote(ByVal TechnicalNote As TechnicalNote, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Cambia el estado de la vigencia de nota técnica
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateTechnicalNote(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

End Interface
