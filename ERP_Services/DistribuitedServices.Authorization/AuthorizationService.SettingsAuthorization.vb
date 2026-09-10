Imports Application.Authorization
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class AuthorizationService
    Implements IAuthorizationServiceSettingsAuthorization

    Public Function GetSettingsAuthorization() As ActionResult(Of SettingsAuthorization) Implements IAuthorizationServiceSettingsAuthorization.GetSettingsAuthorization
        Using service As ISettingsAuthorizationAdminService = Container.Current.Resolve(Of ISettingsAuthorizationAdminService)()
            Return service.GetSettingsAuthorization()
        End Using
    End Function

    Public Function SaveSettingsAuthorization(settingsAuthorization As SettingsAuthorization, audit As AuditMessage) As ActionResult(Of SettingsAuthorization) Implements IAuthorizationServiceSettingsAuthorization.SaveSettingsAuthorization
        Using service As ISettingsAuthorizationAdminService = Container.Current.Resolve(Of ISettingsAuthorizationAdminService)()
            Return service.SaveSettingsAuthorization(settingsAuthorization, audit)
        End Using
    End Function
End Class
