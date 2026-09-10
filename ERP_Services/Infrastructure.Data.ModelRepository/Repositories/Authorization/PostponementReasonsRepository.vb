Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PostponementReasonsRepository
    Inherits GenericRepository(Of PostponementReasons)
    Implements IPostponementReasonsRepository

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

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPostponementReasonsById(id As Integer) As PostponementReasons Implements IPostponementReasonsRepository.GetPostponementReasonsById
        Dim res = (From bg In _context.PostponementReasons Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New PostponementReasons
        End If
    End Function

    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPostponementReasonsByCode(code As String) As PostponementReasons Implements IPostponementReasonsRepository.GetPostponementReasonsByCode
        Dim res = (From bg In _context.PostponementReasons.Include("PostponementReasonsUser") Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.PostponementReasons.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New PostponementReasons
        End If
    End Function

End Class
