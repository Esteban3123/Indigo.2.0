'**********************************************************************
' Assembly         : Presentacion.Security.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-04-2021
'
' Description      : Formulario para espacio de trabajo del usuario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Newtonsoft.Json
Imports Presentation.CloudAgent

Public Class PAutenticacionUsuario
    Implements IDisposable

    Dim client As HttpClient
    Dim response As HttpResponseMessage
    Dim Indigo As SessionValues = SessionValues.Instance
    Dim _SecurityDefault As Boolean
    Dim _datosProceso As MAutenticacionUsuario.ParametrosAzureFunctions

    Sub New(ByVal datosProceso As MAutenticacionUsuario.ParametrosAzureFunctions)
        _datosProceso = datosProceso
        If _datosProceso IsNot Nothing Then
            client = New HttpClient
            client.BaseAddress = New Uri(_datosProceso.AppFunctionURL)
            ServicePointManager.SecurityProtocol = (ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls13)
            ServicePointManager.ServerCertificateValidationCallback = Function(sender1, certificate, chain, sslPolicyErrors)
                                                                          Return True
                                                                      End Function
        End If
    End Sub

    Public Async Function SendMessageNotification(Of T, T1)(ByVal parametroFuncion As MAutenticacionUsuario.ParametroFuncion, ByVal parametro As T1) As Task(Of MAutenticacionUsuario.Respuesta)

        client.DefaultRequestHeaders.Clear()
        client.DefaultRequestHeaders.Add("x-functions-key", parametroFuncion.Code)

        Dim json = JsonConvert.SerializeObject(parametro)

        If parametroFuncion.Asincrono Then
            response = Await client.PostAsync(parametroFuncion.Funcion, New StringContent(json, Text.Encoding.UTF8, "application/json"))
        Else
            response = client.PostAsync(parametroFuncion.Funcion, New StringContent(json, Text.Encoding.UTF8, "application/json")).Result
        End If

        If response.StatusCode <> HttpStatusCode.OK Then
            Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Se presento un error al enviar el mensaje en la función " + parametroFuncion.Funcion}
        End If

        Return New MAutenticacionUsuario.Respuesta With {.Fallo = False, .Result = JsonConvert.DeserializeObject(Of T)(response.Content.ReadAsStringAsync().Result)}
    End Function

    Public Async Function GetUserByEmail(email As String) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros()
        parametro.Par1 = email
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = ApplicationSetting.Instance.GetUserByEmailKey '"tVrs832LdqHo2PwRx/qohGNZTRij/5b6sX71a6JBcurzHvwf5wwe0A=="
        parametroFuncion.Funcion = "/api/GetUserByEmail"
        parametroFuncion.Asincrono = True
        Dim result = Await SendMessageNotification(Of MAutenticacionUsuario.Usuario, MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)
        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, MAutenticacionUsuario.Usuario)
            If obj.Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se encontró la información del usuario"}
            Else
                Return result
            End If
        End If

    End Function

    Public Async Function GetUserConfigurationByUserId(ByVal userId As Integer) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros()
        parametro.Par1 = userId
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = ApplicationSetting.Instance.GetUserConfigurationByUserIdKey
        parametroFuncion.Funcion = "/api/GetUserConfigurationByUserId"
        parametroFuncion.Asincrono = True
        Dim result = Await SendMessageNotification(Of Domain.Security.Entities.UserConfiguration, MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, Domain.Security.Entities.UserConfiguration)
            If obj.Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = False, .Result = New Domain.Security.Entities.UserConfiguration() With {
                .UserId = userId, .ActualCity = 368148, .CustomReportPath = "C:\Reportes\", .LanguageCulture = "es-CO", .LightweightVersion = False,
                .ReporteadorActivo = True, .ShowThemeSkinSelector = True, .TypeAlertControl = 1, .ReportPathType = 1, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added}}}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function GetPerfilUbicacion(ByVal usuario As String) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros()
        parametro.Par1 = usuario
        parametro.Par2 = Infrastructure.CrossCutting.Base.SessionValues.Instance.HisContainer
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.GetPerfilUbicacionKey '"gDc9o3vtc4P1MktoReVYXy5IrHxPUpiqheXNfL1NQl7GaFUuDtaz9w=="
        parametroFuncion.Funcion = "/api/GetPerfilUbicacion"
        parametroFuncion.Asincrono = False
        Dim result = Await SendMessageNotification(Of Domain.Crystal.Entities.SP_SEG_AutenticarUsuario_Result, MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, Domain.Crystal.Entities.SP_SEG_AutenticarUsuario_Result)
            If obj.CODIGO = "-1" Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.CODIGO = "0" Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se encontró la información de perfil y ubicación"}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function GetProfesional(ByVal usuario As String) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros()
        parametro.Par1 = usuario
        parametro.Par2 = Infrastructure.CrossCutting.Base.SessionValues.Instance.HisContainer
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.GetProfesionalKey '"90IVO7R5O55gxUANp0aMAugMC81Zv3sAjid9DvTABIqKLTZPPmq9Jg=="
        parametroFuncion.Funcion = "/api/GetProfesional"
        parametroFuncion.Asincrono = False
        Dim result = Await SendMessageNotification(Of Domain.Crystal.Entities.SP_SEG_AutenticarDatosProfesional_Result, MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, Domain.Crystal.Entities.SP_SEG_AutenticarDatosProfesional_Result)
            If obj.CODIGO = "-1" Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.CODIGO = "0" Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se encontró la información del profesional"}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function SaveUserConfiguration(ByVal _userConfiguration As Domain.Security.Entities.UserConfiguration) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.SaveUserConfigurationKey '"xUIavHU0QusxZD1yU2HYDxBFJChdM2544JNgLcLY0KnXKY1qJ58f6A=="
        parametroFuncion.Funcion = "/api/SaveUserConfiguration"
        parametroFuncion.Asincrono = True
        Dim result = Await SendMessageNotification(Of MAutenticacionUsuario.Base, Domain.Security.Entities.UserConfiguration)(parametroFuncion, _userConfiguration)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, MAutenticacionUsuario.Base)
            If obj.Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se guardo o almaceno la información de configuración del usuario"}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function GetApplicationSettingsByContainerId(containerId As Integer) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros()
        parametro.Par1 = containerId
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.GetApplicationSettingsByContainerIdKey '"SdWRQt7OVra2DbslmZ4KPeNjvo2bdJaVUneJPaCQUOGR/da4iiAMlg=="
        parametroFuncion.Funcion = "/api/GetApplicationSettingsByContainerId"
        parametroFuncion.Asincrono = False
        Dim result = Await SendMessageNotification(Of Domain.Security.Entities.ApplicationSettings, MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, Domain.Security.Entities.ApplicationSettings)
            If obj.Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se encontró la información de configuración de la empresa"}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function LoginUserCompany(ByVal idUser As Integer, ByVal userCode As String, ByVal idCompany As Integer, ByVal companyCode As String, ByVal appVersion As Version,
                                           ByVal userType As Short) As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.ParametrosLogin()
        parametro.Par1 = idUser
        parametro.Par2 = userCode
        parametro.Par3 = idCompany
        parametro.Par4 = companyCode
        parametro.Par5 = appVersion.ToString()
        parametro.Par6 = "1"
        parametro.Par7 = userType
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.LoginUserCompanyKey '"ngW0gjTFM1ydkMG0q4WQBR3BNxr1bYAxorifmGKyIiNMab54sCLIPw=="
        parametroFuncion.Funcion = "/api/LoginUserCompany"
        parametroFuncion.Asincrono = False
        Dim result = Await SendMessageNotification(Of MAutenticacionUsuario.RespuestaLogin, MAutenticacionUsuario.ParametrosLogin)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, MAutenticacionUsuario.RespuestaLogin)
            If obj.Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj.Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = obj.Message}
            Else
                Return result
            End If
        End If
    End Function

    Public Async Function GetSuscriptions() As Task(Of MAutenticacionUsuario.Respuesta)
        Dim parametro = New MAutenticacionUsuario.Parametros
        parametro.Par1 = SessionValues.Instance.TenantId
        Dim parametroFuncion = New MAutenticacionUsuario.ParametroFuncion()
        parametroFuncion.Code = _datosProceso.GetSuscriptionsKey '"GZFCoanQ/m4G0hCKGIvC67si/F82HB5qjegJFMPEZe0TZWoV4oHkUA==
        parametroFuncion.Funcion = "/api/GetSuscriptions"
        parametroFuncion.Asincrono = True
        Dim result = Await SendMessageNotification(Of List(Of MAutenticacionUsuario.Suscriptions), MAutenticacionUsuario.Parametros)(parametroFuncion, parametro)

        If result.Fallo Then
            Return result
        Else
            Dim obj = CType(result.Result, List(Of MAutenticacionUsuario.Suscriptions))
            If obj(0).Id = -1 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "Falta enviar algunos parámetros"}
            ElseIf obj(0).Id = 0 Then
                Return New MAutenticacionUsuario.Respuesta With {.Fallo = True, .Mensaje = "No se encontró la información de licenciamiento"}
            Else
                Return result
            End If
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetServiceConfiguration(ByVal id As Byte) As Task(Of ServiceConfiguration)
        _SecurityDefault = True
        Return Await IndigoConecta.InstanciaDefault.CurrentCloud.IndigoSeguridadDefault.GetServiceConfigurationByIdAsync(id)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetServiceConfigurationSeg(ByVal id As Byte) As Task(Of ServiceConfiguration)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetServiceConfigurationByIdAsync(id)
    End Function

    ''' <summary>
    '''  Obtiene la ruta del informe para un usuario específico, un contenedor dado y una unidad operativa.
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <param name="idContainer"></param>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns>Ruta del informe como cadena.</returns>
    Public Function GetReportPathByUser(idUser As Integer, Optional idContainer As Integer = 0, Optional idOperatingUnit As Integer = 0) As String
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetReportPathByUser(idUser, idContainer, idOperatingUnit)
    End Function


    Public Sub IndigoConectaReset()
        IndigoConecta.Reset()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                client = Nothing
            End If
            If _SecurityDefault Then
                IndigoConecta.Reset()
            End If
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

