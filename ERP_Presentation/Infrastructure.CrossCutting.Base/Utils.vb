'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-08-06
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-08-06
' Description      : Conjunto de funciones utilitarias
'
' Last Modified By : Cristhian Salazar
' Last Modified On : 2014-01-15
' Description      : Creo la funcion de String Pad y RoundValueNearestThousand
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Management
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports System.Configuration
Imports Domain.Base.Entities
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Data.Filtering
Imports System.ComponentModel
Imports System.Dynamic
Imports Newtonsoft.Json
Imports Infrastructure.CrossCutting.Resources
Imports System.Reflection
Imports System.Text

#End Region

''' <summary>
''' Provee funciones utilitarias para distintas necesidades
''' </summary>
Public Class Utils

#Region "Consts"

    ''' <summary>
    ''' Formato de la ruta de definiciones
    ''' </summary>
    Public Const PATHDEF As String = "{0}\Xml\Customizable\{1}\{2}\{3}"
    ''' <summary>
    ''' Nombre del skin por defecto
    ''' </summary>
    Public Const DEFAULT_SKIN_NAME As String = "Indigo MetroStyle Azul"

#End Region

#Region "Functions"

#Region "Dic Type Files"

    ''' <summary>
    ''' Diccionario de tipos de archivos
    ''' </summary>
    Private Shared dicNameTypeFiles As New Dictionary(Of String, String) From
        {
            {".exe", "Programa ejecutable de Windows"},
            {".jpg", "Imagen JPG"},
            {".jpge", "Imagen JPGE"},
            {".png", "Imagen PNG"},
            {".bmp", "Imagen BMP"},
            {".gif", "Imagen GIF"},
            {".doc", "Microsoft Word"},
            {".docx", "Microsoft Word"},
            {".xls", "Microsoft Excel"},
            {".xlsx", "Microsoft Excel"},
            {".pdf", "Portable Document Format"},
            {".txt", "Archivo de Texto"},
            {".cvs", "Archivo de Texto"}
        }

#End Region

#Region "Cross-Thread"

    ''' <summary>
    ''' Asigna valor a una propiedad de un Control de forma segura
    ''' evitando el cruce de hilos
    ''' </summary>
    ''' <param name="ctr">Control que contiene la propiedad a asignar</param>
    ''' <param name="pathPropertyName">Ruta completa de la propiedad que se va a asignar</param>
    ''' <param name="value">Valor a asignar</param>
    Public Shared Sub SetValueToProperty(ByVal ctr As Windows.Forms.Control, ByVal pathPropertyName As String, ByVal value As Object)
        If ctr.InvokeRequired Then
            ctr.BeginInvoke(Sub()
                                InternalSetValueToProperty(ctr, pathPropertyName, value)
                            End Sub)
        Else
            InternalSetValueToProperty(ctr, pathPropertyName, value)
        End If
    End Sub
    Private Shared Sub InternalSetValueToProperty(ByVal obj As Object, ByVal pathPropertyName As String, ByVal value As Object)
        Dim props() As String = pathPropertyName.Split(".")
        Dim listProps As New List(Of String)(props)
        If props.Length = 0 Then Return
        If props IsNot Nothing AndAlso props.Length > 1 Then
            listProps.RemoveAt(0)
            Dim newPathPropertyName As String = String.Join(".", listProps.ToArray())
            Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    If p IsNot Nothing Then
                        Dim objAux = p.GetValue(obj)
                        If objAux IsNot Nothing Then
                            InternalSetValueToProperty(objAux, newPathPropertyName, value)
                        End If
                    End If
                Next
            End If
        Else
            Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    If p IsNot Nothing Then
                        p.SetValue(obj, value)
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region "JSON Serializer"

    ''' <summary>
    ''' Serializa un objeto a una cadena en formato JSON
    ''' </summary>
    ''' <param name="obj">Objeto a serializar</param>
    ''' <returns>Objeto serializado</returns>
    Public Shared Function SerializeObjectToJson(ByVal obj As Object) As String
        Return JsonConvert.SerializeObject(obj, Formatting.None, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
    End Function

    ''' <summary>
    ''' Deserializa una cadena JSON a un objeto dinámico
    ''' </summary>
    ''' <param name="json">Cadena JSON a deserializar</param>
    ''' <returns>Objeto dinámico deserializado</returns>
    Public Shared Function DeserializeJsonToObject(ByVal json As String) As Object
        Return JsonConvert.DeserializeObject(Of ExpandoObject)(json, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
    End Function

#End Region

#Region "Round"

    ''' <summary>
    ''' Tipo de redondeo
    ''' </summary>
    Public Enum RoundLevel As Integer
        ''' <summary>
        ''' Redondea al peso o unidad
        ''' </summary>
        Unit = 0
        ''' <summary>
        ''' Redondea a la decena
        ''' </summary>
        Ten = -1
        ''' <summary>
        ''' Redondea a la centena
        ''' </summary>
        Hundred = -2
        ''' <summary>
        ''' Redondea a los miles
        ''' </summary>
        Thousands = -3
    End Enum
    ''' <summary>
    ''' Redondea valores a 1,10,100,1000
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="roundLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RoundValue(ByVal value As Double, ByVal roundLevel As Int32) As Double
        Select Case roundLevel
            Case 1
                Return RoundValue(value, Utils.RoundLevel.Unit)
            Case 10
                Return RoundValue(value, Utils.RoundLevel.Ten)
            Case 100
                Return RoundValue(value, Utils.RoundLevel.Hundred)
            Case 1000
                Return RoundValue(value, Utils.RoundLevel.Thousands)
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' Redondea valores de moneda a un nivel específico
    ''' </summary>
    ''' <param name="value">Valor a redondear</param>
    ''' <param name="roundLevel">Nivel al que se va a redondear el valor</param>
    ''' <returns>Valor redondeado</returns>
    Public Shared Function RoundValue(ByVal value As Double, Optional ByVal roundLevel As RoundLevel = Utils.RoundLevel.Unit) As Double
        Dim m As Double
        m = 10 ^ roundLevel
        If value < 0 Then
            Return Fix(value * m - 0.5) / m
        Else
            Return Fix(value * m + 0.5) / m
        End If
    End Function

#End Region

#Region "Integration HIS"

    ''' <summary>
    ''' Obtiene el nombre del archivo ejecutable del sistema asistencial
    ''' </summary>
    Public Shared Function GetHISAssemblyFileName() As String
        Try
            Dim aux = ConfigurationManager.AppSettings("HISAssemblyFileName")
            If aux IsNot Nothing Then
                Return aux.ToString().Trim()
            Else
                Return String.Empty
            End If
        Catch
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la versión del ensamblado principal
    ''' del sistema asistencial
    ''' </summary>
    ''' <returns>Version del ensamblado del sistema asistencial</returns>
    Public Shared Function GetHisVersion() As FileVersionInfo
        Try
            If Not GetHISAssemblyFileName().Equals(String.Empty) Then
                Dim finf As FileVersionInfo = FileVersionInfo.GetVersionInfo(GetHISAssemblyFileName())
                Return finf
            End If
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Realiza el proceso de autenticación en el sistema asistencial
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="companyCodeHis">Código de la empresa</param>
    ''' <param name="containerCompanyVie">Nombre del contenedor Vie</param>
    ''' <returns>Valores de sesión</returns>
    Public Shared Function AutenticateUserHis(ByVal userCode As String, ByVal companyCodeHis As String, ByVal containerCompanyVie As String) As HisSessionValues
        Try
            Dim loginHis As Object = GetInstanceHisSystem(GetHISAssemblyFileName(), "IndigoLogin")
            If loginHis IsNot Nothing Then
                Dim CargarParametrosConfiguracionLocal As MethodInfo = loginHis.GetType().GetMethod("CargarParametrosConfiguracionLocal", BindingFlags.NonPublic Or BindingFlags.Static)
                If CargarParametrosConfiguracionLocal IsNot Nothing Then
                    CargarParametrosConfiguracionLocal.Invoke(loginHis, Nothing)
                    Dim AutenticarUsuarioVie As MethodInfo = loginHis.GetType().GetMethod("AutenticarUsuarioVie")
                    If AutenticarUsuarioVie IsNot Nothing Then
                        Dim result As Object = AutenticarUsuarioVie.Invoke(loginHis, New Object() {userCode, companyCodeHis, containerCompanyVie})
                        If Not Object.Equals(result, Nothing) Then
                            Dim sessionResult As New HisSessionValues()
                            sessionResult.UserName = result.NombreUsuario
                            sessionResult.UserRol = result.RolUsuario
                            sessionResult.UserGroup = result.GrupoUsuario
                            sessionResult.UserPosition = result.CargoUsuario
                            sessionResult.ResponseMessage = result.MensajeAutenticacion
                            sessionResult.UserStatus = result.EstadoUsuario
                            sessionResult.IsLogin = result.AutenticacionSatisfactoria
                            Dim ver = GetHisVersion()
                            sessionResult.Version = ver.FileVersion

                            Return sessionResult
                        End If
                    End If
                End If
            End If
            Return New HisSessionValues() With {.IsLogin = False, .IsError = False, .ResponseMessage = "El tipo invocado no existe"}
        Catch ex As Exception
            Return New HisSessionValues() With {.IsLogin = False, .IsError = True, .ResponseMessage = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo del sistema asistencial
    ''' </summary>
    ''' <param name="assemblyName">Nombre del ensamblado donde se encuentra el tipo</param>
    ''' <param name="typeName">Nombre del tipo</param>
    ''' <returns>Tipo obtenido</returns>
    Public Shared Function GetTypeHisSystem(ByVal assemblyName As String, ByVal typeName As String) As Type
        Try
            If File.Exists(assemblyName) Then
                Dim asm As Assembly = Assembly.LoadFrom(assemblyName)
                Dim t As Type = asm.GetTypes().Where(Function(f) f.Name.Equals(typeName)).FirstOrDefault()
                Return t
            End If
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una instancia de un tipo en el sistema asistencial
    ''' </summary>
    ''' <param name="assemblyName">Nombre del ensamblado donde se encuentra el tipo</param>
    ''' <param name="typeName">Nombre del tipo</param>
    ''' <returns>Instancia del tipo</returns>
    Public Shared Function GetInstanceHisSystem(ByVal assemblyName As String, ByVal typeName As String) As Object
        Try
            If File.Exists(assemblyName) Then
                Dim asm As Assembly = Assembly.LoadFrom(assemblyName)
                Dim t As Type = asm.GetTypes().Where(Function(f) f.Name.Equals(typeName)).FirstOrDefault()
                If t IsNot Nothing Then
                    Dim instance = Activator.CreateInstance(t)
                    Return instance
                End If
            End If
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Others"

    ''' <summary>
    ''' Obtiene el nombre del tipo de archivo por extensión
    ''' </summary>
    ''' <param name="extension">Extensión del archivo</param>
    ''' <returns>El nombre del tipo de archivo</returns>
    Public Shared Function GetNameTypeFile(ByVal extension As String) As String
        If dicNameTypeFiles.ContainsKey(extension.Trim().ToLower()) Then
            Return dicNameTypeFiles(extension.Trim().ToLower())
        End If
        Return extension.Trim().ToLower()
    End Function

    ''' <summary>
    ''' Calcula y obtiene una cadena con el tamaño de un total de bytes
    ''' </summary>
    ''' <param name="totalBytes">Total de bytes a calcular</param>
    ''' <returns>Cadena con el tamaño calculado del total de bytes</returns>
    Public Shared Function CalcSizeFile(ByVal totalBytes As Int64) As String
        Dim result As Int64 = (((totalBytes \ 1024) \ 1024) \ 1024) 'Calcula GB
        If result > 0 Then
            Return (Math.Round((((totalBytes / 1024) / 1024) / 1024), 2).ToString() & " GB")
        Else
            result = ((totalBytes \ 1024) \ 1024) 'Calcula MB
            If result > 0 Then
                Return (Math.Round(((totalBytes / 1024) / 1024), 2).ToString() & " MB")
            Else
                result = (totalBytes \ 1024) 'Calcula KB
                If result > 0 Then
                    Return (Math.Round((totalBytes / 1024), 2).ToString() & " KB")
                Else
                    Return (totalBytes.ToString() & " Bytes")
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene el tipo de un reporte por su nombre de clase
    ''' </summary>
    ''' <param name="className">Nombre de la clase del reporte</param>
    ''' <returns>Tipo del reporte</returns>
    Public Shared Function GetReportTypeByClassName(ByVal className As String) As Type
        Dim pathAssembly As String = Path.Combine(Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf("\")), "Presentation.Reporter.dll")
        If File.Exists(pathAssembly) Then
            Dim asm As Assembly = Assembly.LoadFile(pathAssembly)
            Dim res = asm.GetTypes().Where(Function(t) t.Name.Equals(className)).ToList()
            If res.Count > 0 Then
                Return res(0)
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Calcula la cantidad de unidades de estancia entre dos fechas
    ''' </summary>
    ''' <param name="initDate">Fecha inicial</param>
    ''' <param name="endDate">Fecha final</param>
    ''' <param name="limitTime">Hora de corte</param>
    ''' <returns>Cantidad de unidades de estancia</returns>
    Public Shared Function CalcUnitStay(ByVal initDate As Date, ByVal endDate As Date, ByVal limitTime As TimeSpan) As UnitStay
        If initDate > endDate Then
            Return New UnitStay
        End If

        Dim units As New UnitStay
        units.Days = endDate.Subtract(initDate).Days
        units.Hours = endDate.Subtract(initDate).Hours

        If units.Days > 0 Then
            units.Days = 0
            While initDate < endDate
                Dim medianoche = New DateTime(initDate.Year, initDate.Month, initDate.Day, limitTime.Hours, limitTime.Minutes, limitTime.Seconds)
                If medianoche >= initDate And medianoche < endDate Then
                    units.Days += 1
                End If
                initDate = initDate.AddDays(1)
            End While
        End If

        Return units
    End Function

    ''' <summary>
    ''' Obtiene una cadena de texto formateada con la cantidad de
    ''' años, meses y dias que hay entre la fecha dada y la actual
    ''' </summary>
    ''' <param name="birth">Fecha de nacimiento (Fecha inicial)</param>
    ''' <returns>Edad formateada</returns>
    Public Shared Function AgeToString(ByVal birth As DateTime) As String
        Dim today As DateTime = DateTime.Now
        Dim years As Integer = today.Year - birth.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        If today.Month < birth.Month OrElse (today.Month = birth.Month And today.Day < birth.Day) Then
            years -= 1
            months = today.Month
        Else
            months = today.Month - birth.Month
            If today.Day < birth.Day Then
                months -= 1
            End If
        End If
        If today.Day > birth.Day Then
            days = today.Day - birth.Day
        End If
        Return String.Format(ResourceManager.GetString("AgeToStringFormat"), years, months, days)
    End Function

    ''' <summary>
    ''' Obtiene la lista de mensajes de excepcion
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <returns>Lista de mensajes</returns>
    Public Shared Function GetInnerExceptionMessages(ByVal ex As Exception) As List(Of String)
        Dim list As New List(Of String)()
        GetInnerExceptionMessage(ex, list)
        Return list
    End Function

    ''' <summary>
    ''' Obtiene la recursivamente la secuencia de excepciones heredadas
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <param name="list">Lista de mensajes</param>
    Private Shared Sub GetInnerExceptionMessage(ByVal ex As Exception, ByRef list As List(Of String))
        If ex.InnerException IsNot Nothing Then
            GetInnerExceptionMessage(ex, list)
        Else
            list.Add(ex.Message & vbCrLf & ex.StackTrace & vbCrLf & "===============================" & vbCrLf)
        End If
    End Sub

    ''' <summary>
    ''' Corrige el numero de factura para poder enviarlo al SP de consulta
    ''' </summary>
    ''' <param name="number">Numero de factura a arreglar</param>
    ''' de lo contrario se corrige para .Net</param>
    ''' <returns>Numero de factura corregido</returns>
    Public Shared Function FixInvoiceNumber(ByVal number As String, ByVal typeInterface As Integer)
        If number Is Nothing OrElse number.Trim().Equals(String.Empty) Then
            Return String.Empty
        End If
        If typeInterface = 1 Or typeInterface = 2 Then
            If number.Trim().Length > 14 Then
                Return String.Empty
            End If
        End If
        Dim invoiceNumber = number.Trim()
        If invoiceNumber.Contains(" ") Then 'Verificamos si el numero contiene prefijo
            Dim prefix As String = invoiceNumber.Substring(0, invoiceNumber.IndexOf(" ")).Trim()
            Dim body As String = invoiceNumber.Substring(invoiceNumber.IndexOf(" ") + 1).Trim()
            If typeInterface = 1 Or typeInterface = 2 Then 'En Fox la longitud es de 14
                prefix = StringPad(prefix, 4, " ") 'El prefijo en Fox tiene un largo de 4 caracteres
                body = StringPad(body, 10, "0", PadType.STR_PAD_LEFT)
                invoiceNumber = prefix & body
            ElseIf typeInterface = 3 Then
                ' En Net privado la longitud es 12
                body = StringPad(body, (12 - prefix.Length), "0", PadType.STR_PAD_LEFT)
                invoiceNumber = prefix & body
            ElseIf typeInterface = 4 Then
                ' En Net publico la longitud es 14 0 13
                Return invoiceNumber
            End If
        Else 'No tiene prefijo
            If typeInterface = 1 Or typeInterface = 2 Then 'En Fox la longitud es de 14
                invoiceNumber = StringPad(invoiceNumber, 14, "0", PadType.STR_PAD_LEFT)
            ElseIf typeInterface = 3 Then
                ' En Net privado la longitud es 12
                invoiceNumber = StringPad(invoiceNumber, 12, "0", PadType.STR_PAD_LEFT)
            ElseIf typeInterface = 4 Then
                ' En Net publico la longitud es 14 0 13
                Return invoiceNumber
            End If
        End If
        Return invoiceNumber
    End Function

    ''' <summary>
    ''' convierte un valor double a letras
    ''' </summary>
    ''' <param name="value">valor a convertir</param>
    ''' <returns>numero en letras</returns>
    ''' <remarks></remarks>
    Public Shared Function Num2Text(ByVal value As Double) As String
        Dim values = Math.Round(value, 0, MidpointRounding.AwayFromZero)
        Select Case values
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON DE"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES"
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000) Else Num2Text &= " DE"
            Case 1000000000000.0# : Num2Text = "UN BILLON DE"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#) Else Num2Text &= " DE"
        End Select


    End Function

    ''' <summary>
    ''' Obtiene y compone la cadena de conexion usada en EntityFramework
    ''' </summary>
    ''' <param name="connectionName">Nombre del contenedor</param>
    ''' <param name="model">Modelo usado por el contexto</param>
    ''' <param name="company">Numero del contenedor de la compañia a conectar</param>
    ''' <returns>Cadena de conexión compuesta</returns>
    Public Shared Function GetEntityConnectionString(ByVal connectionName As String, ByVal model As String, ByVal company As String, Optional ByVal isEntity As Boolean = True) As String
        Dim strEntity = ConfigurationManager.ConnectionStrings(connectionName)
        If strEntity IsNot Nothing Then
            Return String.Format(strEntity.ConnectionString, model, company) & If(isEntity, ";multipleactiveresultsets=True;App=EntityFramework""", String.Empty)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Obtiene el hash MD5 a partir de una secuencia de datos
    ''' </summary>
    ''' <param name="data">Secuencia de datos</param>
    ''' <returns>Hash MD5 generado</returns>
    Public Shared Function MD5(ByVal data As String) As String
        Dim md5Obj As MD5 = MD5CryptoServiceProvider.Create()
        Return BitConverter.ToString(md5Obj.ComputeHash(Text.ASCIIEncoding.ASCII.GetBytes(data.Trim()))).Replace("-", "")
    End Function

    ''' <summary>
    ''' Obtiene el hash MD5 a partir de una secuencia de datos
    ''' </summary>
    ''' <param name="data">Secuencia de datos</param>
    ''' <returns>Hash MD5 generado</returns>
    Public Shared Function MD5(ByVal data As Byte()) As String
        Dim md5Obj As MD5 = MD5CryptoServiceProvider.Create()
        Return BitConverter.ToString(md5Obj.ComputeHash(data))
    End Function

    ''' <summary>
    ''' Genera un hash a partir de la secuencia de datos proporcionados
    ''' </summary>
    ''' <param name="data">Secuencia de datos a partir del cual se genera el hash</param>
    ''' <returns>Hash generado a partir de la secuencia de datos proporcionados</returns>
    ''' <remarks>El parámetro <paramref name="data" /> es opcional, y en caso de no ser proporcionado
    ''' se genera un Guid</remarks>
    Public Shared Function GetHash(Optional ByVal data As String = "") As String
        If data IsNot Nothing AndAlso Not data.Equals("") Then
            Return Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(data.Trim())).ToUpper()
        Else
            Return System.Guid.NewGuid().ToString().ToUpper()
        End If
    End Function

    ''' <summary>
    ''' Genera un hash a partir de la secuencia de bytes proporcionados
    ''' </summary>
    ''' <param name="data">Secuencia de bytes a partir del cual se genera el hash</param>
    ''' <returns>Hash generado a partir de la secuencia de bytes proporcionados</returns>
    Public Shared Function GetHash(ByVal data As Byte()) As String
        Return Convert.ToBase64String(data)
    End Function

    ''' <summary>
    ''' Obtiene un Id de la concatenación de todos los id's de los procesadores en la maquina local
    ''' </summary>
    ''' <returns>Id único del conjuntos de procesadores</returns>
    Public Shared Function GetIdProcessor() As String
        Try
            Dim objMOS As ManagementObjectSearcher
            Dim objMOC As ManagementObjectCollection
            objMOS = New ManagementObjectSearcher("Select * From Win32_Processor")
            objMOC = objMOS.Get
            Dim id As String = ""
            For Each objMO As ManagementObject In objMOC
                id &= IIf(objMO("ProcessorID") Is Nothing, String.Empty, objMO("ProcessorID"))
            Next
            objMOS.Dispose()
            objMOS = Nothing
            Return GetHash(id.Trim())
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el usuario logueado en el sistema operativo
    ''' </summary>
    ''' <returns>El usuario logueado</returns>
    Public Shared Function GetOSUsername() As String
        Return ValidateString(System.Security.Principal.WindowsIdentity.GetCurrent().Name)
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la maquina
    ''' </summary>
    ''' <returns>Nombre de la maquina</returns>
    Public Shared Function GetHostname() As String
        Return ValidateString(My.Computer.Name)
    End Function

    ''' <summary>
    ''' Obtiene una lista de todas las IP's asignadas al host local
    ''' </summary>
    ''' <returns>Lista de IP's</returns>
    Public Shared Function GetLocalHostIPs() As List(Of String)
        Dim IPs() As System.Net.IPAddress = System.Net.Dns.GetHostEntry("").AddressList
        Dim list As New List(Of String)()
        For Each ip As System.Net.IPAddress In IPs
            list.Add(ip.ToString())
        Next
        Return list
    End Function

    ''' <summary>
    ''' Obtiene la lista de direcciones MAC asignadas al host local
    ''' </summary>
    ''' <returns>Lista de direcciones MAC</returns>
    Public Shared Function GetLocalHostMACsAddress() As List(Of String)
        Try
            Dim objMOS As ManagementObjectSearcher
            Dim objMOC As ManagementObjectCollection
            objMOS = New ManagementObjectSearcher("Select * From Win32_NetworkAdapter where MACAddress is not null")
            objMOC = objMOS.Get
            Dim list As New List(Of String)()
            For Each objMO As ManagementObject In objMOC
                list.Add(IIf(objMO("MACAddress") Is Nothing, String.Empty, objMO("MACAddress")))
            Next
            objMOS.Dispose()
            objMOS = Nothing
            Return list
        Catch ex As Exception
            Return New List(Of String)()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el nombre del sistema operativo
    ''' </summary>
    ''' <returns>Nombre del sistema operativo</returns>
    Public Shared Function GetOSName() As String
        Return ValidateString(My.Computer.Info.OSFullName)
    End Function

    ''' <summary>
    ''' Remueve los caracteres no validos
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Cadena valida</returns>
    Private Shared Function ValidateString(ByVal str As String) As String
        Dim pattern As String = "áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_ "
        Dim res As String = String.Empty
        For Each c As Char In str
            If pattern.Contains(c) Then
                res &= c
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el sistema operativo es windows 7 o superior
    ''' </summary>
    ''' <returns>Valor que indica si el OS es win7 o superior</returns>
    Public Shared Function Win7OrHigher() As Boolean
        Dim OSV As System.OperatingSystem = System.Environment.OSVersion
        If OSV.Version.Major >= 6 AndAlso OSV.Version.Minor >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si la maquina se encuentra conectada a una red
    ''' </summary>
    ''' <returns>Valor que indica si la maquina se encuentra conectada a una red</returns>
    Public Shared Function NetworkConnectionIsAvailable() As Boolean
        Return My.Computer.Network.IsAvailable
    End Function

    ''' <summary>
    ''' Ontiene la versión de la aplicación
    ''' </summary>
    ''' <param name="format">Formato en que se quiere mostrar la versión, donde el primer parametro es el número de la versión y el segundo el número de revisión. ej {0}-{1}</param>
    ''' <returns>Versión de la aplicación</returns>
    Public Shared Function GetAppVersion() As Version
        Return My.Application.Info.Version
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la aplicacion
    ''' </summary>
    ''' <returns>Nombre de la aplicacion</returns>
    Public Shared Function GetAppName() As String
        Return ValidateString(My.Application.Info.ProductName)
    End Function

    ''' <summary>
    ''' Obtiene el id del proceso asociado a la aplicacion
    ''' </summary>
    ''' <returns>Id del proceso</returns>
    Public Shared Function GetAppProcessId() As Integer
        Return Process.GetCurrentProcess().Id
    End Function

    ''' <summary>
    ''' Genera un hash id de subscriptor para los servicios de notificación, a partir del nombre del proceso proporcionado
    ''' </summary>
    ''' <param name="processName">Nombre del proceso quien se subscribe al servicio de notificación</param>
    ''' <returns>El hash id generado</returns>
    Public Shared Function GetNotificationServiceHashId(ByVal processName As String) As String
        If processName IsNot Nothing AndAlso Not processName.Trim().Equals(String.Empty) Then
            '{Maquina(Id procesador)}¬{Usuario del sistema operativo}¬{Version de genesis}¬{Nombre de la base de datos}¬{Nombre del modulo quien realiza la subscripcion}¬{Id del proceso en el sistema}
            Return MD5(GetHash((String.Format("{0}¬{1}¬{2}¬{3}¬{4}¬{5}", GetIdProcessor(), GetOSUsername(), GetAppVersion(), SessionValues.Instance.IndigoCompany, processName.Trim(), GetAppProcessId()).ToUpper())))
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la compañia quien desarrollo la aplicación
    ''' </summary>
    ''' <returns>Nombre de la compañia</returns>
    Public Shared Function GetCompanyName() As String
        Return ValidateString(My.Application.Info.CompanyName)
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos para el usuarios de la aplicacion
    ''' </summary>
    ''' <returns>Ruta de archivos para el usuarios de la aplicacion</returns>
    Public Shared Function GetPathApplicationFiles() As String
        Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

    ''' <summary>
    ''' Obtiene la ruta del ejecutable de la aplicación
    ''' </summary>
    ''' <returns>La ruta del ejecutable de la aplicación</returns>
    Public Shared Function GetApplicationPath()
        Return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos de configuración de la aplicacion
    ''' </summary>
    ''' <returns>Ruta de archivos de configuracion</returns>
    Public Shared Function GetPathConfigApplication() As String
        Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFiles() As String
        Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

    ''' <summary>
    ''' Asegura que la ruta a directorio exista. Si no existe la crea
    ''' </summary>
    ''' <param name="path">Ruta a directorio</param>
    Public Shared Sub EnsurePathExists(ByVal path As String)
        Try
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si el perfil de impresión existe
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="idForm">Id del formulario</param>
    ''' <returns>Valor que indica si el perfil existe</returns>
    Public Shared Function PrintProfileExists(ByVal userCode As String, ByVal idForm As Int32) As Boolean
        EnsurePathExists(GetPathApplicationFiles())
        Try
            Dim fl As String = Path.Combine(GetPathApplicationFiles(), ("PrintProfile" & idForm & "(" & userCode & ").Profile"))
            If File.Exists(fl) Then
                Dim ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(fl)))
                Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(PrintProfile))
                Dim obj = CType(xs.Deserialize(ms), PrintProfile)
                Return True
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el perfil de impresión para un usuario y frontal determinado
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="idForm">Id del formulario</param>
    ''' <returns>El perfil de impresión</returns>
    Public Shared Function GetPrintProfile(ByVal userCode As String, ByVal idForm As Int32) As PrintProfile
        Dim obj As New PrintProfile With {.IsPrint = True}
        Try
            Dim fl As String = Path.Combine(GetPathApplicationFiles(), ("PrintProfile" & idForm & "(" & userCode & ").Profile"))
            If File.Exists(fl) Then
                Dim ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(fl)))
                Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(PrintProfile))
                obj = CType(xs.Deserialize(ms), PrintProfile)
            End If
            Return obj
        Catch
            Return obj
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el nombre del skin para el usuario
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <returns>Nombre del skin usado por el usuario</returns>
    Public Shared Function GetThemeSkinName(ByVal userCode As String) As String
        EnsurePathExists(GetPathApplicationFiles())
        Dim skinNamePath As String = Path.Combine(GetPathApplicationFiles(), ("DefaultSkinName" & "(" & userCode & ").skin"))
        If File.Exists(skinNamePath) Then
            Dim body As String = File.ReadAllText(skinNamePath)
            If Not body.Trim().Equals(String.Empty) Then
                Return body.Trim()
            End If
        End If
        Return DEFAULT_SKIN_NAME
    End Function

    ''' <summary>
    ''' Elimina el tema seleccionado
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    Public Shared Sub DeleteThemeSkinName(ByVal userCode As String)
        EnsurePathExists(GetPathApplicationFiles())
        Dim skinNamePath As String = Path.Combine(GetPathApplicationFiles(), ("DefaultSkinName" & "(" & userCode & ").skin"))
        If File.Exists(skinNamePath) Then
            File.Delete(skinNamePath)
        End If
    End Sub

    ''' <summary>
    ''' Asigna el nombre del skin seleccionado
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="skinName">Nombre del skin</param>
    Public Shared Sub SetThemeSkinName(ByVal userCode As String, ByVal skinName As String)
        EnsurePathExists(GetPathApplicationFiles())
        Dim skinNamePath As String = Path.Combine(GetPathApplicationFiles(), ("DefaultSkinName" & "(" & userCode & ").skin"))
        If File.Exists(skinNamePath) Then
            File.Delete(skinNamePath)
        End If
        File.WriteAllText(skinNamePath, skinName.Trim())
    End Sub

    ''' <summary>
    ''' Crea un archivo de perfil de impresión para un frontal y usuario
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="idForm">Id del formulario</param>
    ''' 
    Public Shared Sub CreatePrintProfile(ByVal userCode As String, ByVal idForm As Int32, ByVal obj As PrintProfile)
        Try
            EnsurePathExists(GetPathApplicationFiles())

            Dim fl As String = Path.Combine(GetPathApplicationFiles(), ("PrintProfile" & idForm & "(" & userCode & ").Profile"))
            DeletePrintProfile(userCode, idForm)
            Using fls As FileStream = File.OpenWrite(fl)
                Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(PrintProfile))
                xs.Serialize(fls, obj)
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Elimina la preferencia de impresión
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="idForm">Id del formulario</param>
    Public Shared Sub DeletePrintProfile(ByVal userCode As String, ByVal idForm As Int32)
        Try
            EnsurePathExists(GetPathApplicationFiles())
            Dim fl As String = Path.Combine(GetPathApplicationFiles(), ("PrintProfile" & idForm & "(" & userCode & ").Profile"))
            If File.Exists(fl) Then
                File.Delete(fl)
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el selector de temas
    ''' </summary>
    ''' <returns>Valor que indica si se muestra el selector de temas</returns>
    Public Shared Function ShowThemeSkinSelector() As Boolean
        If ConfigurationManager.AppSettings("ShowThemeSkinSelector") IsNot Nothing AndAlso Not ConfigurationManager.AppSettings("ShowThemeSkinSelector").ToString().Trim().Equals(String.Empty) Then
            Return If(ConfigurationManager.AppSettings("ShowThemeSkinSelector").ToString().Trim().ToLower().Equals("true"), True, False)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Obtiene la cultura configurada por defecto en el app.config
    ''' </summary>
    ''' <returns>Cultura configurada por defecto</returns>
    Public Shared Function GetDefaultCulture() As CultureInfo
        If ConfigurationManager.AppSettings("Idioma") IsNot Nothing AndAlso Not ConfigurationManager.AppSettings("Idioma").ToString().Trim().Equals(String.Empty) Then
            Return New CultureInfo(ConfigurationManager.AppSettings("Idioma").ToString())
        Else
            Return New CultureInfo("es-CO")
        End If
    End Function

    Enum PadType
        STR_PAD_LEFT = 1
        STR_PAD_RIGHT = 2
        STR_PAD_BOTH = 3
    End Enum

    ''' <summary>
    ''' Redondea un valor al mil mas cercano
    ''' </summary>
    ''' <param name="value">valor a redondear</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RoundValueNearestThousand(value As Double) As Double
        Dim valueReturn As Double = 0
        If value > 0 Then
            If Right(value.ToString(), 3) = "000" Then
                valueReturn = value
            Else
                valueReturn = Math.Round(value / 1000) * 1000
            End If
        End If
        Return valueReturn
    End Function

    ''' <summary>
    ''' Función para Redondear Valores al cien mas cercano
    ''' </summary>
    ''' <param name="ValueIn">Valor a Redondear</param>
    ''' <returns>Valor Redondeado</returns>
    ''' <remarks></remarks>
    Public Shared Function RoundedValuesNearestHundred(ByVal ValueIn As Double) As Double

        Dim ValueReturn As Double

        If ValueIn > 0 Then
            If Right(ValueIn, 2) = "00" Then
                ValueReturn = ValueIn
            Else
                ValueReturn = Math.Round(ValueIn / 100) * 100
            End If
        Else
            ValueReturn = 0
        End If

        Return ValueReturn

    End Function

    ''' <summary>
    ''' Funcion la cual rellena los caracteres restantes de una cadena dependiendo del tipo lo rellena hacia la derecha o izquierda
    ''' nOTA: Si la longitud de la cadena(input) es mayor a la longitud del pad(padLength) se devolvera la cadena cortada
    ''' </summary>
    ''' <param name="input">La cadena que se va rellenar</param>
    ''' <param name="padLength">El tamaño maximo de la cadena resultante</param>
    ''' <param name="padString">La cadena con la que se va rellenar</param>
    ''' <param name="padType">El tipo de relleno que va mos a usar</param>
    ''' <returns>Returna una cadena con el tamaño especificado y rellenado con el pad usado</returns>
    ''' <remarks></remarks>
    Public Shared Function StringPad(input As String, padLength As Integer, padString As String, Optional padType As PadType = PadType.STR_PAD_RIGHT)
        Dim output As String = ""
        Dim difference As Integer = padLength - Len(input)
        If difference > 0 Then
            Select Case padType
                Case padType.STR_PAD_LEFT
                    For x = 1 To difference Step Len(padString)
                        output = output & padString
                    Next
                    output = Right(output & input, padLength)
                Case padType.STR_PAD_RIGHT
                    For x = 1 To difference Step Len(padString)
                        output = output & padString
                    Next
                    output = Left(input & output, padLength)
                Case padType.STR_PAD_BOTH
                    output = input
                    For x = 1 To difference Step Len(padLength) * 2
                        output = padString & output & padString
                    Next
                    If Len(output) < padLength Then
                        output = output & padString
                    End If
                    output = Left(output, padLength)
            End Select
        Else
            output = input
        End If
        output = output.Substring(0, padLength)
        Return output
    End Function

    ''' <summary>
    ''' Función para Redondear Valores de acuerdo la Tasa de Aproximación
    ''' </summary>
    ''' <param name="ValueIn">Valor a Redondear</param>
    ''' <param name="Rate">Factor a Redondear (0, 10, 100, 1000)</param>
    ''' <returns>Valor Redondeado</returns>
    ''' <remarks></remarks>
    Public Shared Function RoundedValuesByRate(ByVal ValueIn As Double, Rate As Integer) As Double

        Dim ValueReturn As Double

        If ValueIn > 0 Then
            Select Case Rate
                Case 0
                    ValueReturn = ValueIn
                Case 1
                    ValueReturn = Math.Round(ValueIn)
                Case 10
                    If Right(ValueIn, 1) = "0" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 1)
                        ValueReturn = Math.Round((ValueIn / 10) * 10, 0)
                    End If
                Case 100
                    If Right(ValueIn, 2) = "00" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 2)
                        ValueReturn = Math.Round((ValueIn / 100) * 100, 0)
                    End If
                Case 1000
                    If Right(ValueIn, 3) = "000" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 3)
                        ValueReturn = Math.Round((ValueIn / 1000) * 1000, 0)
                    End If

            End Select
        Else
            ValueReturn = 0
        End If

        Return ValueReturn

    End Function

    ''' <summary>
    ''' Funcio para ejecutar las expresiones de los conceptos de nominas
    ''' </summary>
    ''' <param name="expresion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EvalExpression(ByVal expresion As String) As ActionResult(Of Object)
        Dim result As New ActionResult(Of Object)
        result.StateResult = True
        Dim value As Decimal
        Try
            value = New ExpressionEvaluator(TypeDescriptor.GetProperties(GetType(String)), CriteriaOperator.Parse(expresion)).Evaluate(expresion)
            result.ObjectEmbbeded = value
        Catch ex As Exception
            result.StateResult = False
            result.Message = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Compara dos versiones convertidas a String y retorna una valor indicando
    ''' cual es mayor, menor o iguales. 0 = v1 igual a v2, 1 = v1 mayor que v2, -1 = v1 menor que v2
    ''' </summary>
    ''' <param name="v1">Versión 1</param>
    ''' <param name="v2">Versión 2</param>
    ''' <returns>Valor que indica el resultado de la comparación</returns>
    Public Shared Function CompareVersions(ByVal v1 As String, ByVal v2 As String) As Int32
        Dim v11 As New Version(v1)
        Dim v22 As New Version(v2)
        Return v11.CompareTo(v22)
    End Function

    ''' <summary>
    ''' Obtiene los valores a distribuir
    ''' </summary>
    ''' <param name="value">valor a distribuir</param>
    ''' <returns></returns>
    Public Shared Function GetDistributedValues(value As Long, percent1 As Decimal) As Tuple(Of Long, Long)
        Dim value1 As Long = value * percent1 / 100
        Dim value2 As Long = value - value1
        Return New Tuple(Of Long, Long)(value1, value2)
    End Function

    Public Shared Function GetDistributedValuesByValue(current As Decimal, valueToDistribute As Decimal) As Tuple(Of Decimal, Decimal)
        If valueToDistribute < current Then
            Return New Tuple(Of Decimal, Decimal)(valueToDistribute, current - valueToDistribute)
        Else
            If (current - valueToDistribute) < 0 Then
                Return New Tuple(Of Decimal, Decimal)(current, 0)
            Else
                Return New Tuple(Of Decimal, Decimal)(current - valueToDistribute, 0)
            End If
        End If
    End Function

#End Region

#End Region

End Class

<Serializable()>
Public Class PrintProfile

    Public Property IsPrint As Boolean
    Public Property OnSave As Boolean
    Public Property OnUpdate As Boolean
    Public Property OnConfirm As Boolean
    Public Property OnCancel As Boolean

End Class

''' <summary>
''' Encapsula la cantidad de dias y horas de estancia de un paciente
''' </summary>
Public Class UnitStay

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el número de dias de estancias
    ''' </summary>
    ''' <value>Número de dias de estancia</value>
    ''' <returns>Número de dias de estancias</returns>
    Public Property Days As Integer

    ''' <summary>
    ''' Obtiene o asigna el número de horas de estancias
    ''' </summary>
    ''' <value>Número de horas de estancia</value>
    ''' <returns>Número de horas de estancias</returns>
    Public Property Hours As Integer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.New(0, 0)
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="days">Número de dias de la estancias</param>
    ''' <param name="hours">Número de horas de la estancias</param>
    Public Sub New(ByVal days As Integer, ByVal hours As Integer)
        Me.Days = days
        Me.Hours = hours
    End Sub

#End Region

End Class
