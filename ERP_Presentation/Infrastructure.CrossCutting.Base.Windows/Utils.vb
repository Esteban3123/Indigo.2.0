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

Imports System.ComponentModel
Imports System.Configuration
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Text
Imports System.Web.Configuration
Imports DevExpress.Spreadsheet
Imports Domain.Base.Entities
#End Region

''' <summary>
''' Provee funciones utilitarias para distintas necesidades
''' </summary>
Public Class Utils

#Region "Fields"
    'Public Shared ReadOnly DefaultPath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Indigo Technologies", "ElectronicDocuments")
    ''' <summary>
    ''' Ruta por defecto usada para el almacén de documentos de facturacion electronica
    ''' </summary>
    Public Shared Function DefaultPath() As String
        Return System.IO.Path.Combine(LocalFolder(), "Vie HealtTeach", "ElectronicDocuments")
    End Function
#End Region

#Region "Cross-Thread"

    ''' <summary>
    ''' Asigna valor a una propiedad de un Control de forma segura
    ''' evitando el cruce de hilos
    ''' </summary>
    ''' <param name="ctr">Control que contiene la propiedad a asignar</param>
    ''' <param name="pathPropertyName">Ruta completa de la propiedad que se va a asignar</param>
    ''' <param name="value">Valor a asignar</param>
    Public Shared Sub SetValueToProperty(ByVal ctr As System.Windows.Forms.Control, ByVal pathPropertyName As String, ByVal value As Object)
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

    Public Shared Function AppFolder() As String
        'Windows.ApplicationModel.AppInfo.Current.PackageFamilyName
        'New Windows.ApplicationModel.AppService.AppServiceConnection()
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return AppDomain.CurrentDomain.BaseDirectory
        Else
            Return "" ' Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Presentation.Client\")
        End If
    End Function

    Public Shared Function LocalFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return "" 'Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If

    End Function

    Public Shared Function UserFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.   
            Return "" 'Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If

    End Function

    Public Shared Function TemporalFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Path.GetTempPath()
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return "" 'Windows.Storage.ApplicationData.Current.TemporaryFolder.Path
        End If
    End Function

    Public Shared Function DeskTopFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return "" 'Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If
    End Function

    Public Shared Function GetPathElectronicDocuments() As String
        'Obtenemos el archivo de configuración de la aplicación
        Dim appConfig As System.Configuration.Configuration = Nothing
        If System.IO.Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile).ToLower().Equals("web.config") Then
            appConfig = WebConfigurationManager.OpenWebConfiguration("~")
        Else
            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        End If
        Dim valueParam = appConfig.AppSettings.Settings(Base.Utils.PATH_ELECTRONIC_DOCUMENTS)
        If valueParam IsNot Nothing Then 'Si el parametro existe
            If Not valueParam.Value.Trim().Equals(String.Empty) Then
                Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value.Trim())
            Else
                valueParam.Value = DefaultPath()
                Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value)
            End If
        Else 'Si no existe
            valueParam = New KeyValueConfigurationElement(Base.Utils.PATH_ELECTRONIC_DOCUMENTS, DefaultPath)
            appConfig.AppSettings.Settings.Add(valueParam)
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value)
            appConfig.Save(ConfigurationSaveMode.Modified, True)
        End If
        Return valueParam.Value.Trim()
    End Function

    ''' <summary>
    ''' Obtiene el tipo de un reporte por su nombre de clase
    ''' </summary>
    ''' <param name="className">Nombre de la clase del reporte</param>
    ''' <returns>Tipo del reporte</returns>
    Public Shared Function GetReportTypeByClassName(ByVal className As String) As Type
        'Dim pathAssembly As String = Path.Combine(Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf("\")), "Presentation.Reporter.dll")
        Dim pathAssembly As String = String.Concat(AppFolder(), "Presentation.Reporter.dll")
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
            Return Base.Utils.GetHash(id.Trim())
        Catch ex As Exception
            Return String.Empty
        End Try
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
    ''' Genera un hash id de subscriptor para los servicios de notificación, a partir del nombre del proceso proporcionado
    ''' </summary>
    ''' <param name="processName">Nombre del proceso quien se subscribe al servicio de notificación</param>
    ''' <returns>El hash id generado</returns>
    Public Shared Function GetNotificationServiceHashId(ByVal processName As String) As String
        If processName IsNot Nothing AndAlso Not processName.Trim().Equals(String.Empty) Then
            '{Maquina(Id procesador)}¬{Usuario del sistema operativo}¬{Version de genesis}¬{Nombre de la base de datos}¬{Nombre del modulo quien realiza la subscripcion}¬{Id del proceso en el sistema}
            Return Base.Utils.MD5(Base.Utils.GetHash((String.Format("{0}¬{1}¬{2}¬{3}¬{4}¬{5}", GetIdProcessor(), Base.Utils.GetOSUsername(), Base.Utils.GetAppVersion(), SessionValues.Instance.IndigoCompany, processName.Trim(), Base.Utils.GetAppProcessId()).ToUpper())))
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos para el usuarios de la aplicacion
    ''' </summary>
    ''' <returns>Ruta de archivos para el usuarios de la aplicacion</returns>
    Public Shared Function GetPathApplicationFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(LocalFolder(), "\" & Base.Utils.GetCompanyName() & "\" & Base.Utils.GetAppName())
    End Function

    ''' <summary>
    ''' Obtiene la ruta a los archivos temporales generados por Vie
    ''' </summary>
    ''' <returns>Ruta a los archivos temporales</returns>
    Public Shared Function GetPathTempFiles() As String
        'Return Path.Combine(Path.GetTempPath(), "IndigoVieTempFiles")
        Return Path.Combine(TemporalFolder(), "IndigoVieTempFiles")
    End Function

    ''' <summary>
    ''' Obtiene la ruta del ejecutable de la aplicación
    ''' </summary>
    ''' <returns>La ruta del ejecutable de la aplicación</returns>
    Public Shared Function GetApplicationPath() As String
        'Return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Return AppFolder()
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos de configuración de la aplicacion
    ''' </summary>
    ''' <returns>Ruta de archivos de configuracion</returns>
    Public Shared Function GetPathConfigApplication() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return AppDomain.CurrentDomain.BaseDirectory
        Else
            Return String.Concat(GetPathApplicationFiles(), "\")
        End If
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(UserFolder(), "\" & Base.Utils.GetCompanyName() & "\" & Base.Utils.GetAppName())
    End Function


    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario del EHR de los Customizables
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFilesEHRCustomizableGridControl() As String
        Return Path.Combine(UserFolder(), Base.Utils.GetCompanyName(), Base.Utils.GetSessionValuesIndigoCompanyName(), "Xml", "Customizable", "GridControl")
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el perfil de impresión existe
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    ''' <param name="idForm">Id del formulario</param>
    ''' <returns>Valor que indica si el perfil existe</returns>
    Public Shared Function PrintProfileExists(ByVal userCode As String, ByVal idForm As Int32) As Boolean
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
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
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
        Dim skinNamePath As String = Path.Combine(GetPathApplicationFiles(), ("DefaultSkinName" & "(" & userCode & ").skin"))
        If File.Exists(skinNamePath) Then
            Dim body As String = File.ReadAllText(skinNamePath)
            If Not body.Trim().Equals(String.Empty) Then
                Return body.Trim()
            End If
        End If
        Return Base.Utils.DEFAULT_SKIN_NAME
    End Function

    ''' <summary>
    ''' Obtiene el cuerpo del archivo de favoritos para un usuario
    ''' </summary>
    ''' <param name="userCode">Usuario a consultar</param>
    ''' <returns>Cuerpo del archivo serializado</returns>
    Public Shared Function GetMyFavorites(ByVal userCode As String) As String
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
        Dim myFavoritesPath As String = Path.Combine(GetPathApplicationFiles(), ("MyFavorites" & "(" & userCode & ").fav"))
        If File.Exists(myFavoritesPath) Then
            Dim body As String = File.ReadAllText(myFavoritesPath)
            If Not body.Trim().Equals(String.Empty) Then
                Return body.Trim()
            End If
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Asigna el cuerpo al archivo de favoritos para un usuario
    ''' </summary>
    ''' <param name="userCode">Usuario a asignar</param>
    ''' <param name="favorites">Cuerpo de favoritos</param>
    Public Shared Sub SetMyFavorites(ByVal userCode As String, ByVal favorites As String)
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
        Dim myFavoritesPath As String = Path.Combine(GetPathApplicationFiles(), ("MyFavorites" & "(" & userCode & ").fav"))
        If File.Exists(myFavoritesPath) Then
            File.Delete(myFavoritesPath)
        End If
        File.WriteAllText(myFavoritesPath, favorites.Trim())
    End Sub

    ''' <summary>
    ''' Elimina el tema seleccionado
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    Public Shared Sub DeleteThemeSkinName(ByVal userCode As String)
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
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
        Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
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
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())

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
    ''' Elimina la preferencia de impresión de todos los formularios
    ''' </summary>
    ''' <param name="userCode">Código del usuario</param>
    Public Shared Sub DeletePrintProfiles(ByVal userCode As String)
        Try
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
            For Each f As String In Directory.GetFiles(GetPathApplicationFiles(), "PrintProfile*(" & userCode & ").Profile", SearchOption.TopDirectoryOnly)
                File.Delete(f)
            Next
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Elimina las preferencias del usuario
    ''' </summary>
    ''' <param name="userCode">Usuario a eliminar</param>
    Public Shared Sub DeleteCustomization(ByVal userCode As String)
        Try
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
            For Each f As String In Directory.GetFiles(GetPathApplicationFiles(), "*.IndigoUser_" & userCode & ".xml", SearchOption.AllDirectories)
                File.Delete(f)
            Next
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
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathApplicationFiles())
            Dim fl As String = Path.Combine(GetPathApplicationFiles(), ("PrintProfile" & idForm & "(" & userCode & ").Profile"))
            If File.Exists(fl) Then
                File.Delete(fl)
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Elimina las preferencias del usuario EHR
    ''' </summary>
    ''' <param name="userName">Nombre de Usario para eliminar</param> 
    Public Shared Sub DeleteCustomizationEHR(ByVal userName As String)
        Try
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(GetPathUserFilesEHRCustomizableGridControl())
            For Each fileToDelete As String In Directory.GetFiles(GetPathUserFilesEHRCustomizableGridControl(), userName & "*", SearchOption.AllDirectories)
                File.Delete(fileToDelete)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

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
            value = New DevExpress.Data.Filtering.Helpers.ExpressionEvaluator(TypeDescriptor.GetProperties(GetType(String)), DevExpress.Data.Filtering.CriteriaOperator.Parse(expresion)).Evaluate(expresion)
            result.ObjectEmbbeded = value
        Catch ex As Exception
            result.StateResult = False
            result.Message = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' metodo para setear el formato de la moneda en las columnas que se le indiquen
    ''' </summary>
    ''' <param name="Column"></param>
    Public Shared Function FormatGrid(Column As DevExpress.XtraGrid.Columns.GridColumn, ISO4217 As String, Optional currencyDecimalDigits As Integer = 2) As DevExpress.XtraGrid.Columns.GridColumn
        Dim fInfo As DevExpress.Utils.FormatInfo = Column.DisplayFormat
        fInfo.FormatType = DevExpress.Utils.FormatType.Custom
        fInfo.FormatString = "C" + currencyDecimalDigits.ToString()
        Dim Culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim format = New CultureInfo(ISO4217.GetCultureId).NumberFormat
        Culture.NumberFormat = SessionValues.Instance.ConfigurationSeparatorNumberFormat(format)
        fInfo.Format = Culture
        Column.SummaryItem.Format = Culture
        Return Column
    End Function


#Region "Excel"
    ''' <summary>
    ''' funcion que exporta Data en un archivo excel 
    ''' </summary>
    ''' <param name="listSheets"></param>
    ''' <returns></returns>
    Public Shared Function ExportDataToExcel(listSheets As List(Of ExcelSheetData)) As Byte()
        Try
            If listSheets Is Nothing OrElse Not listSheets.Any() Then
                Return Nothing
            End If

            Dim workbook As New Workbook With {
                .Unit = DevExpress.Office.DocumentUnit.Point
            }

            Dim author As String = workbook.CurrentAuthor
            Using stream As New MemoryStream()

                For sheetIndex = 0 To listSheets.Count - 1
                    workbook.BeginUpdate()

                    Dim sheet = listSheets(sheetIndex)

                    If sheetIndex > 0 Then
                        workbook.Worksheets.Add()
                    End If

                    Dim worksheet As Worksheet = workbook.Worksheets(sheetIndex)
                    If Not String.IsNullOrEmpty(sheet.Name) Then
                        worksheet.Name = sheet.Name
                    End If

                    ' Add column headers
                    For columnIndex = 0 To sheet.Columns.Count - 1
                        Dim column = sheet.Columns(columnIndex)
                        Dim headerCell As Cell = worksheet.Cells(0, columnIndex)
                        headerCell.Value = column.Name
                        headerCell.FillColor = Color.LightGray

                        If Not String.IsNullOrEmpty(column.Comment) Then
                            Dim cmt As Comment = worksheet.Comments.Add(headerCell, author, column.Comment)

                            ' Autosize “aproximado”
                            Dim widthPt As Single = 320.0F
                            Dim charsPerLine As Integer = 45
                            Dim textLen As Integer = cmt.Text.Length
                            Dim baseLines As Integer = Math.Max(1, CInt(Math.Ceiling(textLen / charsPerLine)))

                            ' Cuenta saltos de línea explícitos para no subestimar
                            Dim extraBreaks As Integer = cmt.Text.Split({vbCrLf, vbLf}, StringSplitOptions.None).Length - 1
                            Dim totalLines As Integer = baseLines + extraBreaks

                            Dim lineHeightPt As Single = 14.0F
                            cmt.Width = widthPt
                            cmt.Height = 20.0F + (totalLines * lineHeightPt)
                        End If

                        Select Case column.Type
                            Case ExcelColumnFormat.Text
                                worksheet.Columns(columnIndex).NumberFormat = "@"
                        End Select
                    Next


                    ' Add data rows in batches to handle large volumes of data
                    Dim batchSize As Integer = 1000
                    Dim totalRows As Integer = If(sheet?.Rows?.Count, 0)
                    If totalRows > 0 Then
                        For batchStart = 0 To totalRows - 1 Step batchSize
                            Dim batchEnd As Integer = Math.Min(batchStart + batchSize, totalRows)
                            For rowIndex = batchStart To batchEnd - 1
                                Dim columnMax = Math.Min(sheet.Columns.Count, sheet.Rows(rowIndex).Cells.Count)
                                For columnIndex = 0 To columnMax - 1
                                    Dim cellValue = sheet.Rows(rowIndex).Cells(columnIndex)
                                    Dim dataCell As Cell = worksheet.Cells(rowIndex + 1, columnIndex) ' +1 because headers are on row 0
                                    dataCell.Value = cellValue?.ToString()
                                Next
                            Next
                        Next
                    End If

                    workbook.Worksheets.ActiveWorksheet = workbook.Worksheets(0)
                    workbook.EndUpdate()
                    workbook.Calculate()
                    Using tempStream As New MemoryStream()
                        workbook.SaveDocument(tempStream, DocumentFormat.Xlsx)
                        tempStream.Seek(0, SeekOrigin.Begin)
                        ' Copiar el contenido del stream temporal al stream final
                        tempStream.CopyTo(stream, 81920) ' Copiar en bloques de 80 KB
                    End Using
                    GC.Collect()
                Next

                Return stream.ToArray()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar estructura excel apartir de un arreglo de string
    ''' </summary>
    ''' <param name="listNameColumns"></param>
    ''' <returns></returns>
    Public Shared Function ExportHeaderToExcel(ByVal ParamArray listNameColumns() As String) As Byte()
        Dim columns = New List(Of ExcelDataColumn)
        For Each nameColumn In listNameColumns
            columns.Add(New ExcelDataColumn With {.Name = nameColumn})
        Next
        Return ExportDataToExcel(New List(Of ExcelSheetData) From {New ExcelSheetData With {.Columns = columns}})
    End Function

#End Region

#Region "Integration HIS"

    ''' <summary>
    ''' Obtiene el nombre del archivo ejecutable del sistema asistencial
    ''' </summary>
    Public Shared Function GetHISAssemblyFileName() As String
        Try
            'Dim aux = ConfigurationManager.AppSettings("HISAssemblyFileName")
            Dim aux = ApplicationSetting.Instance.HISAssemblyFileName
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
                'Dim finf As FileVersionInfo = FileVersionInfo.GetVersionInfo(GetHISAssemblyFileName())
                Dim finf As FileVersionInfo = FileVersionInfo.GetVersionInfo(String.Concat(GetApplicationPath, GetHISAssemblyFileName()))
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
    Public Shared Function AutenticateUserHisAsync(ByVal userCode As String, ByVal companyCodeHis As String, ByVal containerCompanyVie As String) As Task(Of HisSessionValues)
        Return Task.Factory.StartNew(Of HisSessionValues)(Function()
                                                              Return AutenticateUserHis(userCode, companyCodeHis, containerCompanyVie)
                                                          End Function)
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
            Return New HisSessionValues() With {.IsLogin = False, .IsError = True, .ResponseMessage = IIf(ex.InnerException IsNot Nothing, ex.InnerException.Message, "") + "  -  " + ex.Message & vbCrLf & ex.StackTrace}
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
            'If File.Exists(assemblyName) Then
            '       Dim asm As Assembly = Assembly.LoadFrom(assemblyName)
            Dim archivo As String = String.Concat(GetApplicationPath, assemblyName)
            If File.Exists(archivo) Then
                Dim asm As Assembly = Assembly.LoadFrom(archivo)
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
            'If File.Exists(assemblyName) Then
            '       Dim asm As Assembly = Assembly.LoadFrom(assemblyName)
            Dim archivo As String = String.Concat(GetApplicationPath, assemblyName)
            If File.Exists(archivo) Then
                Dim asm As Assembly = Assembly.LoadFrom(archivo)
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

End Class
