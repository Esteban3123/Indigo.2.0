'***********************************************************************
' Assembly         : Infrastructure.Data.AuthorizationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CancellationReasonsRepository
    Inherits GenericRepository(Of CancellationReasons)
    Implements ICancellationReasonsRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCancellationReasonsByCode(code As String) As CancellationReasons Implements ICancellationReasonsRepository.GetCancellationReasonsByCode
        Dim res = (From bg In _context.CancellationReasons.Include("CancellationReasonsUser") Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.CancellationReasons.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New CancellationReasons
        End If
    End Function

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCancellationReasonsById(id As Integer) As CancellationReasons Implements ICancellationReasonsRepository.GetCancellationReasonsById
        Dim res = (From bg In _context.CancellationReasons Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New CancellationReasons
        End If
    End Function
End Class
