#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class DevolutionCauseRepository
    Inherits GenericRepository(Of DevolutionCause)
    Implements IDevolutionCauseRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una causa de devolución por codigo
    ''' </summary>
    ''' <param name="code">codigo de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDevolutionCauseByCode(code As String) As DevolutionCause Implements IDevolutionCauseRepository.GetDevolutionCauseByCode
        Dim res = (From d As DevolutionCause In Me._context.DevolutionCause Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.DevolutionCause.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New DevolutionCause()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una causa de devolución por id
    ''' </summary>
    ''' <param name="id">id de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDevolutionCauseById(id As Integer) As DevolutionCause Implements IDevolutionCauseRepository.GetDevolutionCauseById
        Dim res = (From d In Me._context.DevolutionCause Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As DevolutionCause In Me._context.DevolutionCause.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New DevolutionCause()
        End If
    End Function

#End Region

End Class
