'***********************************************************************
' Assembly         : Infrastructure.Data.AuthorizationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AuthorizationRejectionRepository
    Inherits GenericRepository(Of AuthorizationRejection)
    Implements IAuthorizationRejectionRepository

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
    Public Function GetAuthorizationRejectionByCode(code As String) As AuthorizationRejection Implements IAuthorizationRejectionRepository.GetAuthorizationRejectionByCode
        Dim res = (From bg In _context.AuthorizationRejection.Include("AuthorizationRejectionUser") Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.AuthorizationRejection.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New AuthorizationRejection
        End If
    End Function

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationRejectionById(id As Integer) As AuthorizationRejection Implements IAuthorizationRejectionRepository.GetAuthorizationRejectionById
        Dim res = (From bg In _context.AuthorizationRejection Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New AuthorizationRejection
        End If
    End Function

End Class
