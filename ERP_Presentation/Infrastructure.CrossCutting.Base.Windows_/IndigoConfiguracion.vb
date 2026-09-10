
Public NotInheritable Class IndigoConfiguracion

    Private Shared NombreArchivoReal As String
    Private Shared NombreArchivo As String
    Private Shared dtConfiguracion As DataTable

    Shared Sub New()
        'NombreArchivoReal = Application.StartupPath & "\AppConfiguration.IndigoNet"
        NombreArchivoReal = Utils.AppFolder() & "AppConfiguration.IndigoNet"
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            NombreArchivo = NombreArchivoReal
        Else
            NombreArchivo = Window.Utils.LocalFolder() & "\" & GetCompanyName() & "\" & GetAppName() & "\AppConfiguration.IndigoNet"
        End If
        dtConfiguracion = New DataTable("Configuracion")
        With dtConfiguracion.Columns
            .Add("CacheLocal", GetType(String))
            .Add("RutaReportes", GetType(String))
            .Add("ProtocoloWeb", GetType(String))
            .Add("ServicioWeb", GetType(String))
            .Add("ServicioNotificaciones", GetType(String))
            .Add("ProtocoloXPO", GetType(String))
            .Add("ServicioXPO", GetType(String))
            .Add("BuscaActualizaciones", GetType(String))
            .Add("RutaActualizacion", GetType(String))
            .Add("BloqueoSesion", GetType(String))
            .Add("EmpresaAplicacionCalidad", GetType(String))
        End With
    End Sub

    Public Shared Sub VerificarArchivo()
        If (Not IO.File.Exists(NombreArchivo)) AndAlso IO.File.Exists(NombreArchivoReal) Then
            dtConfiguracion.Rows.Clear()
            dtConfiguracion.ReadXml(NombreArchivoReal)
            dtConfiguracion.WriteXml(NombreArchivo)
        End If
    End Sub

    ''' <summary>
    ''' devuelve el binding del archivo de configuracion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function MapeaArchivoConfiguracion() As DataTable
        Return dtConfiguracion
    End Function

    ''' <summary>
    ''' retorna el archivo de configuracion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function LeerArchivoConfiguracion() As DataTable
        VerificarArchivo()
        If IO.File.Exists(NombreArchivo) = True Then
            dtConfiguracion.Rows.Clear()
            dtConfiguracion.ReadXml(NombreArchivo)
        End If
        Return dtConfiguracion
    End Function

    ''' <summary>
    ''' retorna el archivo de configuracion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function LeerArchivoConfiguracion(Of Tipodato)(ByVal Parametro As String) As Tipodato
        VerificarArchivo()
        If IO.File.Exists(NombreArchivo) = False Then
            Return Nothing
        End If
        If dtConfiguracion.Columns.Contains(Parametro) = True Then
            dtConfiguracion.Rows.Clear()
            dtConfiguracion.ReadXml(NombreArchivo)
            Return dtConfiguracion.Rows(0).Item(Parametro)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' escribe el archivo de configuracion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Sub EscribirArchivoConfiguracion(ByVal dtArchivo As DataTable)
        dtArchivo.WriteXml(NombreArchivo)
    End Sub

    ''' <summary>
    ''' verifica la existencia del archivo de configuracion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ExisteArchivoConfiguracion() As Boolean
        VerificarArchivo()
        Return IO.File.Exists(NombreArchivo)
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la compañia quien desarrollo la aplicación
    ''' </summary>
    ''' <returns>Nombre de la compañia</returns>
    Public Shared Function GetCompanyName() As String
        Return ValidateString(My.Application.Info.CompanyName)
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la aplicacion
    ''' </summary>
    ''' <returns>Nombre de la aplicacion</returns>
    Public Shared Function GetAppName() As String
        Return ValidateString(My.Application.Info.ProductName)
    End Function

    ''' <summary>
    ''' Remueve los caracteres no validos
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Cadena valida</returns>
    Private Shared Function ValidateString(ByVal str As String) As String
        Dim pattern As String = "áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_\/ "
        Dim res As String = String.Empty
        For Each c As Char In str
            If pattern.Contains(c) Then
                res &= c
            End If
        Next
        Return res
    End Function
End Class
