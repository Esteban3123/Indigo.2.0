#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class TechnicalNoteRepository
    Inherits GenericRepository(Of TechnicalNote)
    Implements ITechnicalNoteRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicializa el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetTechnicalNoteById(id As Integer) As TechnicalNote Implements ITechnicalNoteRepository.GetTechnicalNoteById
        Dim TechnicalNote = (From rmv In Me._context.TechnicalNote Where rmv.Id = id Select rmv).FirstOrDefault()
        If TechnicalNote Is Nothing Then
            Return New TechnicalNote
        End If
        TechnicalNote.OriginalValue = (From rmv In Me._context.TechnicalNote.AsNoTracking() Where rmv.Id = id Select rmv).FirstOrDefault()
        Return TechnicalNote
    End Function

    Public Function GetTechnicalNoteByIdWithAggregates(id As Integer) As TechnicalNote Implements ITechnicalNoteRepository.GetTechnicalNoteByIdWithAggregates
        Dim TechnicalNote = (From rmv In Me._context.TechnicalNote.AsNoTracking().Include("TechnicalNoteDetail").AsNoTracking() Where rmv.Id = id Select rmv).FirstOrDefault()
        If TechnicalNote Is Nothing Then
            Return New TechnicalNote
        End If
        Return TechnicalNote
    End Function

    Public Function GetTechnicalNote(code As String) As TechnicalNote Implements ITechnicalNoteRepository.GetTechnicalNote
        Dim TechnicalNote = (From rmv In Me._context.TechnicalNote.Include("TechnicalNoteDetail") Where rmv.Code = code Select rmv).FirstOrDefault()
        If TechnicalNote Is Nothing Then
            Return New TechnicalNote
        End If

        For Each detail In TechnicalNote.TechnicalNoteDetail
            detail.GrouperDescription = (From p In _context.Groupers.AsNoTracking Where detail.GrouperId = p.Id Select p.Code + " - " + p.Description).FirstOrDefault
        Next

        TechnicalNote.OriginalValue = (From rmv In Me._context.TechnicalNote.AsNoTracking() Where rmv.Id = TechnicalNote.Id Select rmv).FirstOrDefault()
        Return TechnicalNote
    End Function

#End Region

End Class
