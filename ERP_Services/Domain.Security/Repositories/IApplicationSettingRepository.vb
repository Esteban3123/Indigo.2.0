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

Public Interface IApplicationSettingRepository
    Inherits IRepository(Of ApplicationSettings)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="containerId"></param>
    ''' <returns></returns>
    Function GetApplicationSettingsByContainerId(containerId As Integer) As ApplicationSettings
End Interface
