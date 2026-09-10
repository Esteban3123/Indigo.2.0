'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/05/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository

#End Region

Public Class PAuthorizationSchedule

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de las plantillas de turno
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeScheduleTemplate() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationScheduleTemplateByStatus(True)
    End Function

    ''' <summary>
    ''' Carga los usuarios que esten asociados a la plantilla seleccionada
    ''' </summary>
    ''' <param name="authorizationScheduleTemplateId"></param>
    ''' <returns></returns>
    Public Function ListUsersForSchedule(authorizationScheduleTemplateId As Integer) As List(Of ViewListUsersForScheduleXpo)
        Dim filter As String = "AuthorizationScheduleTemplateId = " & authorizationScheduleTemplateId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListUsersForScheduleXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Obtiene la plantilla seleccionada
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationShceduleTemplateById(id As Integer) As AuthorizationScheduleTemplateXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationScheduleTemplateXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la entidad xpo para cargar el calendario en el loadControls
    ''' </summary>
    ''' <param name="authorizationScheduleTemplateId"></param>
    ''' <param name="month"></param>
    ''' <param name="year"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationScheduleXpo(authorizationScheduleTemplateId As Integer, month As Integer, year As Integer, userId As Integer) As AuthorizationScheduleXpo
        Dim filter As String = "AuthorizationScheduleTemplateId.Id = " & authorizationScheduleTemplateId & " and Month = " & month & " and Year = " & year & " and UserId = " & userId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationScheduleXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
