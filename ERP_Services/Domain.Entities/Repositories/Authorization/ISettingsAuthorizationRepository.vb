Imports Domain.Base

Public Interface ISettingsAuthorizationRepository
    Inherits IRepository(Of SettingsAuthorization)

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    Function GetSettingsAuthorizationById(id As Integer) As SettingsAuthorization

    ''' <summary>
    ''' Obtiene el parámetro de autorización
    ''' </summary>
    Function GetSettingsAuthorization() As SettingsAuthorization

End Interface