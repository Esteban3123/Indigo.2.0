'***********************************************************************
' Assembly         : Infrastructure.Data.AuthorizationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AuthorizationScheduleTemplateRepository
    Inherits GenericRepository(Of AuthorizationScheduleTemplate)
    Implements IAuthorizationScheduleTemplateRepository

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
    Public Function GetAuthorizationScheduleTemplateByCode(code As String) As AuthorizationScheduleTemplate Implements IAuthorizationScheduleTemplateRepository.GetAuthorizationScheduleTemplateByCode
        Dim res = (From bg In _context.AuthorizationScheduleTemplate.Include("AuthorizationScheduleTemplateUsers").Include("AuthorizationScheduleTemplateSchedule") Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.AuthorizationScheduleTemplate.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New AuthorizationScheduleTemplate
        End If
    End Function

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationScheduleTemplateById(id As Integer) As AuthorizationScheduleTemplate Implements IAuthorizationScheduleTemplateRepository.GetAuthorizationScheduleTemplateById
        Dim res = (From bg In _context.AuthorizationScheduleTemplate Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New AuthorizationScheduleTemplate
        End If
    End Function
End Class
