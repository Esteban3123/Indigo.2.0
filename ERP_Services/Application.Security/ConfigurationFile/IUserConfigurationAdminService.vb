'***********************************************************************
' Assembly         : Application.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IUserConfigurationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveUserConfiguration(ByVal userConfiguration As UserConfiguration, ByVal audit As AuditMessage) As ActionResult(Of UserConfiguration)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration

    ''' <summary>
    ''' Actualizar zona horaria y formatos
    ''' </summary>
    ''' <returns></returns>
    Function UpdateUserConfiguration(ByVal UserConfigurationCulture As UserConfigurationCulture) As Boolean

End Interface
