#Region "Imports"

Imports Domain.Base

#End Region

Public Interface ITechnicalNoteRepository
    Inherits IRepository(Of TechnicalNote), Inject

    ''' <summary>
    ''' Obtiene una nota técnica por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTechnicalNoteById(id As Integer) As TechnicalNote

    ''' <summary>
    ''' Obtiene una nota técnica con los agregados
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTechnicalNoteByIdWithAggregates(id As Integer) As TechnicalNote

    ''' <summary>
    ''' Obtiene una nota técnica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTechnicalNote(code As String) As TechnicalNote

End Interface
