'************************************************************
' Assembly         : Domain.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/03/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IAuthorizationSourceRepository
    Inherits IRepository(Of AuthorizationSource)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationSourceByCode(code As String) As AuthorizationSource
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationSourceById(id As Integer) As AuthorizationSource
End Interface
