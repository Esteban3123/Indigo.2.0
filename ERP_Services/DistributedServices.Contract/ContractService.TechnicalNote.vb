#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

#End Region

Partial Class ContractService

    ''' <summary>
    ''' Obtiene una nota técnica por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTechnicalNoteById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements IContractTechnicalNote.GetTechnicalNoteById
        Using service As ITechnicalNoteAdminService = Container.Current.Resolve(Of ITechnicalNoteAdminService)()
            Return service.GetTechnicalNoteById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una nota técnica por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetTechnicalNote(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements IContractTechnicalNote.GetTechnicalNote
        Using service As ITechnicalNoteAdminService = Container.Current.Resolve(Of ITechnicalNoteAdminService)()
            Return service.GetTechnicalNote(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una nota técnica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveTechnicalNote(ByVal TechnicalNote As TechnicalNote, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TechnicalNote) Implements IContractTechnicalNote.SaveTechnicalNote
        Using service As ITechnicalNoteAdminService = Container.Current.Resolve(Of ITechnicalNoteAdminService)()
            Return service.SaveTechnicalNote(TechnicalNote, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la nota técnica
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateTechnicalNote(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements IContractTechnicalNote.ChangeStateTechnicalNote
        Using service As ITechnicalNoteAdminService = Container.Current.Resolve(Of ITechnicalNoteAdminService)()
            Return service.ChangeStateTechnicalNote(code, state, audit)
        End Using
    End Function

End Class
