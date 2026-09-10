'************************************************************
' Assembly         : Domain.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IAuthorizationScheduleTemplateRepository
    Inherits IRepository(Of AuthorizationScheduleTemplate)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationScheduleTemplateByCode(code As String) As AuthorizationScheduleTemplate
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationScheduleTemplateById(id As Integer) As AuthorizationScheduleTemplate
End Interface
