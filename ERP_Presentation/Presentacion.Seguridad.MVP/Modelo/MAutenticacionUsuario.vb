'**********************************************************************
' Assembly         : Presentacion.Security.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-04-2021
'
' Description      : Formulario para espacio de trabajo del usuario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class MAutenticacionUsuario
    Public Class Base
        Property Id As Integer
    End Class

    Public Class ParametroFuncion
        Property Code As String
        Property Funcion As String
        Property Asincrono As Boolean
    End Class

    Public Class Parametros
        Property Par1 As String
        Property Par2 As String
    End Class

    Public Class ParametrosLogin
        Property Par1 As String
        Property Par2 As String
        Property Par3 As String
        Property Par4 As String
        Property Par5 As String
        Property Par6 As String
        Property Par7 As String
    End Class

    Public Class RespuestaLogin
        Property Id As Integer
        Property Message As String
        Property UserLogin As Domain.Security.Entities.UserLogin
    End Class

    Public Class Respuesta
        Property Fallo As Boolean
        Property Mensaje As String
        Property Result As Object
    End Class

    Public Class Usuario
        Property Id As Integer
        Property IdPerson As Integer
        Property UserCode As String
        Property UserType As Byte
        Property ProfileType As String
        Property Email As String
    End Class

    Public Class ParametrosAzureFunctions
        Property AppFunctionURL As String
        Property GetPerfilUbicacionKey As String
        Property GetProfesionalKey As String
        Property SaveUserConfigurationKey As String
        Property GetApplicationSettingsByContainerIdKey As String
        Property LoginUserCompanyKey As String
        Property GetSuscriptionsKey As String
    End Class

    Public Class Suscriptions
        Property Id As Int32
        Property TenantId As Int16
        Property ProductCatalogId As Int16
        Property SuscriptionID As Guid
        Property TypeOfLicensing As Byte
        Property NumberOfUsers As Int16
        Property NumberOfDevices As Int16
        Property BTIuV1Id As Int16
        Property BTIuV2Id As Int16
    End Class
End Class
