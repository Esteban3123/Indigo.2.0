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

Public Interface IApplicationSettingsAdminService
    Inherits IDisposable
    Function SaveApplicationSettings(ByVal applicationSettings As ApplicationSettings, ByVal audit As AuditMessage) As ActionResult(Of ApplicationSettings)
    Function GetApplicationSettingsByContainerId(containerId As Integer) As ApplicationSettings
End Interface
