Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ISettingsAuthorizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el parametro de autorización
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingsAuthorization() As ActionResult(Of SettingsAuthorization)

    ''' <summary>
    ''' Guarda el parámetro de autorización
    ''' </summary>
    ''' <param name="settingsAuthorization"></param>
    ''' <returns></returns>
    Function SaveSettingsAuthorization(settingsAuthorization As SettingsAuthorization, audit As AuditMessage) As ActionResult(Of SettingsAuthorization)

End Interface
