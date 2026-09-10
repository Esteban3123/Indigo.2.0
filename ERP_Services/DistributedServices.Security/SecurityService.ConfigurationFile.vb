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
    ''' <param name="serviceConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveServiceConfiguration(ByVal serviceConfiguration As ServiceConfiguration, ByVal audit As AuditMessage) As ActionResult(Of ServiceConfiguration) Implements ISecurityService.SaveServiceConfiguration
        Using AdminSecurity As IServiceConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IServiceConfigurationAdminService)()
            Return AdminSecurity.SaveServiceConfiguration(serviceConfiguration, audit)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetServiceConfigurationById(id As Byte) As ServiceConfiguration Implements ISecurityService.GetServiceConfigurationById
        Using AdminSecurity As IServiceConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IServiceConfigurationAdminService)()
            Return AdminSecurity.GetServiceConfigurationById(id)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveUserConfiguration(ByVal userConfiguration As UserConfiguration, ByVal audit As AuditMessage) As ActionResult(Of UserConfiguration) Implements ISecurityService.SaveUserConfiguration
        Using AdminSecurity As IUserConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserConfigurationAdminService)()
            Return AdminSecurity.SaveUserConfiguration(userConfiguration, audit)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration Implements ISecurityService.GetUserConfigurationByUserId
        Using AdminSecurity As IUserConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserConfigurationAdminService)()
            Return AdminSecurity.GetUserConfigurationByUserId(userId)
        End Using
    End Function

    ''' <summary>
    ''' Update zona horaria y formatos
    ''' </summary>    
    ''' <returns></returns>
    Public Function UpdateUserConfiguration(UserConfigurationCulture As UserConfigurationCulture) As Boolean Implements ISecurityService.UpdateUserConfiguration
        Using AdminSecurity As IUserConfigurationAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserConfigurationAdminService)()
            Return AdminSecurity.UpdateUserConfiguration(UserConfigurationCulture)
        End Using
    End Function
End Class
