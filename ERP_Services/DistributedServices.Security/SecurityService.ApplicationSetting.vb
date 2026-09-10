'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 19-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Application.Security
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
#End Region

Partial Public Class SecurityService

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationSettings"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveApplicationSettings(ByVal applicationSettings As ApplicationSettings, ByVal audit As AuditMessage) As ActionResult(Of ApplicationSettings) Implements ISecurityService.SaveApplicationSettings
        Using AdminSecurity As IApplicationSettingsAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IApplicationSettingsAdminService)()
            Return AdminSecurity.SaveApplicationSettings(applicationSettings, audit)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="containerId"></param>
    ''' <returns></returns>
    Public Function GetApplicationSettingsByContainerId(containerId As Integer) As ApplicationSettings Implements ISecurityService.GetApplicationSettingsByContainerId
        Using AdminSecurity As IApplicationSettingsAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IApplicationSettingsAdminService)()
            Return AdminSecurity.GetApplicationSettingsByContainerId(containerId)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGeneralConfiguration() As GeneralConfiguration Implements ISecurityService.GetGeneralConfiguration
        Using AdminSecurity As IGeneralConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IGeneralConfigurationAdminService)()
            Return AdminSecurity.GetGeneralConfiguration()
        End Using
    End Function

End Class
