'************************************************************
' Assembly         : Domain.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IAuthorizationRejectionRepository
    Inherits IRepository(Of AuthorizationRejection)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationRejectionByCode(code As String) As AuthorizationRejection
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationRejectionById(id As Integer) As AuthorizationRejection
End Interface
