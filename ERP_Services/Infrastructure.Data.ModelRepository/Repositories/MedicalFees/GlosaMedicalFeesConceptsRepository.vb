#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class GlosaMedicalFeesConceptsRepository
    Inherits GenericRepository(Of GlosaMedicalFeesConcepts)
    Implements IGlosaMedicalFeesConceptsRepository

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
    Public Function GetGlosaMedicalFeesConceptsByCode(code As String) As GlosaMedicalFeesConcepts Implements IGlosaMedicalFeesConceptsRepository.GetGlosaMedicalFeesConceptsByCode
        Dim res = (From d As GlosaMedicalFeesConcepts In Me._context.GlosaMedicalFeesConcepts Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.GlosaMedicalFeesConcepts.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New GlosaMedicalFeesConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaMedicalFeesConceptsById(id As Integer) As GlosaMedicalFeesConcepts Implements IGlosaMedicalFeesConceptsRepository.GetGlosaMedicalFeesConceptsById
        Dim res = (From d In Me._context.GlosaMedicalFeesConcepts Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As GlosaMedicalFeesConcepts In Me._context.GlosaMedicalFeesConcepts.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New GlosaMedicalFeesConcepts()
        End If
    End Function

#End Region

End Class
