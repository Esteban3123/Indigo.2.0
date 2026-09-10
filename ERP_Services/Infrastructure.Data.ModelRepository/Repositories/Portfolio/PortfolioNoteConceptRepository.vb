'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class PortfolioNoteConceptRepository
    Inherits GenericRepository(Of PortfolioNoteConcept)
    Implements IPortfolioNoteConceptRepository

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConcept(code As String, Optional tracking As Boolean = True) As PortfolioNoteConcept Implements IPortfolioNoteConceptRepository.GetPortfolioNoteConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As PortfolioNoteConcept In _context.PortfolioNoteConcept.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res.Count > 0 Then
                res(0).OriginalValue = (From d As PortfolioNoteConcept In _context.PortfolioNoteConcept.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New PortfolioNoteConcept()
            End If
        Else
            Dim res = (From d As PortfolioNoteConcept In _context.PortfolioNoteConcept Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res.Count > 0 Then
                res(0).OriginalValue = (From d As PortfolioNoteConcept In _context.PortfolioNoteConcept Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New PortfolioNoteConcept()
            End If
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConceptById(id As Integer) As PortfolioNoteConcept Implements IPortfolioNoteConceptRepository.GetPortfolioNoteConceptById
        Dim res = (From pnc In _context.PortfolioNoteConcept.Include("MainAccounts") Where pnc.Id = id Select pnc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pnc In _context.PortfolioNoteConcept.AsNoTracking() Where pnc.Id = id Select pnc).FirstOrDefault()
            Return res
        Else
            Return New PortfolioNoteConcept()
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de nota por un filtro
    ''' </summary>
    ''' <param name="noteType"></param>
    ''' <param name="idAccout"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConceptByFilter(noteType As Integer, idAccout As Integer, status As Integer) As PortfolioNoteConcept Implements IPortfolioNoteConceptRepository.GetPortfolioNoteConceptByFilter
        Dim res = (From pnc In _context.PortfolioNoteConcept Where pnc.NoteType = noteType Select pnc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pnc In _context.PortfolioNoteConcept.AsNoTracking() Where pnc.NoteType = noteType AndAlso pnc.IdAccount = idAccout AndAlso pnc.Status = status Select pnc).FirstOrDefault()
            Return res
        Else
            Return New PortfolioNoteConcept()
        End If
    End Function

#End Region

End Class
