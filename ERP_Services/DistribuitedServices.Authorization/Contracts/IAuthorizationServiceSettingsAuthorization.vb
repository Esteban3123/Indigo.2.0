Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IAuthorizationServiceSettingsAuthorization

    ''' <summary>
    ''' Obtiene el parámetro de autorización
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingsAuthorization() As ActionResult(Of SettingsAuthorization)

    ''' <summary>
    ''' Guarda el parámetro de autorización
    ''' </summary>
    ''' <param name="settingsAuthorization"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingsAuthorization(settingsAuthorization As SettingsAuthorization, audit As AuditMessage) As ActionResult(Of SettingsAuthorization)

End Interface
