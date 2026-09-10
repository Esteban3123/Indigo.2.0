'***********************************************************************
' Assembly         : Domain.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region

Public Interface IUserConfigurationRepository
    Inherits IRepository(Of UserConfiguration)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration

    ''' <summary>
    ''' Actualizar zona horaria usuario
    ''' </summary>
    ''' <returns></returns>
    Function UpdateUserConfiguration(UserConfigurationCulture As UserConfigurationCulture) As Boolean

End Interface
