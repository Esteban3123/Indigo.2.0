#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class PortfolioConciliationConceptsRepository
    Inherits GenericRepository(Of PortfolioConciliationConcepts)
    Implements IPortfolioConciliationConceptsRepository

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
    Public Function GetPortfolioConciliationConceptsByCode(code As String) As PortfolioConciliationConcepts Implements IPortfolioConciliationConceptsRepository.GetPortfolioConciliationConceptsByCode
        Dim res = (From d As PortfolioConciliationConcepts In Me._context.PortfolioConciliationConcepts Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.PortfolioConciliationConcepts.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New PortfolioConciliationConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioConciliationConceptsById(id As Integer) As PortfolioConciliationConcepts Implements IPortfolioConciliationConceptsRepository.GetPortfolioConciliationConceptsById
        Dim res = (From d In Me._context.PortfolioConciliationConcepts Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As PortfolioConciliationConcepts In Me._context.PortfolioConciliationConcepts.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New PortfolioConciliationConcepts()
        End If
    End Function

#End Region

End Class
