#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class ImportunityCausesRepository
    Inherits GenericRepository(Of ImportunityCauses)
    Implements IImportunityCausesRepository

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
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code">codigo de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImportunityCausesByCode(code As String) As ImportunityCauses Implements IImportunityCausesRepository.GetImportunityCausesByCode
        Dim res = (From d As ImportunityCauses In Me._context.ImportunityCauses Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.ImportunityCauses.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New ImportunityCauses()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImportunityCausesById(id As Integer) As ImportunityCauses Implements IImportunityCausesRepository.GetImportunityCausesById
        Dim res = (From d In Me._context.ImportunityCauses Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As ImportunityCauses In Me._context.ImportunityCauses.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New ImportunityCauses()
        End If
    End Function

#End Region

End Class
