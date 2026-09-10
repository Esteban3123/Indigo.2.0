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
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Management
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web.Configuration
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Root
Imports Newtonsoft.Json
Imports DevExpress.Spreadsheet
Imports System.Drawing
#End Region

''' <summary>
''' Provee funciones utilitarias para distintas necesidades
''' </summary>
Public Class Utils
    Implements IDisposable
#Region "Consts"

    ''' <summary>
    ''' Formato de la ruta de definiciones
    ''' </summary>
    Public Const PATHDEF As String = "{0}\Xml\Customizable\{1}\{2}\{3}"
    ''' <summary>
    ''' Nombre del skin por defecto
    ''' </summary>
    Public Const DEFAULT_SKIN_NAME As String = "Office 2010 Blue"
    ''' <summary>
    ''' Nombre del parámetro en el archivo de configuración, el cual
    ''' contiene la ruta del almacén de documentos de facturacion electronica
    ''' </summary>
    Public Const PATH_ELECTRONIC_DOCUMENTS As String = "_PathElectronicDocuments_"

#End Region

#Region "Fields"
    'Public Shared ReadOnly DefaultPath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Indigo Technologies", "ElectronicDocuments")
    ''' <summary>
    ''' Ruta por defecto usada para el almacén de documentos de facturacion electronica
    ''' </summary>
    Public Shared Function DefaultPath() As String
        Return System.IO.Path.Combine(LocalFolder(), "Vie HealtTeach", "ElectronicDocuments")
    End Function
#End Region

#Region "Properties"
    ''' <summary>
    ''' Lista de metodos de pagos; se crea este Utils para Consumir en el frontEnd
    ''' </summary>
    Private Shared _listPaymentMethod As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property PaymentMethodTypes As List(Of Tuple(Of Byte, String))
        Get
            If _listPaymentMethod Is Nothing Then
                _listPaymentMethod = New List(Of Tuple(Of Byte, String))()
                _listPaymentMethod.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("EffectivePaymentMethod", "Treasury")))
                _listPaymentMethod.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("PaymentMethodCheck", "Treasury")))
                _listPaymentMethod.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("CardPaymentMethod", "Treasury")))
                _listPaymentMethod.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("ConsignmentPaymentMethod", "Treasury")))
            End If
            Return _listPaymentMethod
        End Get
    End Property

    ''' <summary>
    ''' lista de tipos de folios; se crea utils para obtener el nombre
    ''' </summary>
    Private Shared _folioMasterAccountNames As Dictionary(Of Integer, String)
    Public Shared ReadOnly Property FolioMasterAccountNames As Dictionary(Of Integer, String)
        Get
            If _folioMasterAccountNames Is Nothing Then
                _folioMasterAccountNames = New Dictionary(Of Integer, String)
                _folioMasterAccountNames.Add(eMasterAccount.MasterAccount, String.Format(ResourceManager.GetString("MasterAccount", "CtrFolio")))
                _folioMasterAccountNames.Add(eMasterAccount.EntityAccount, String.Format(ResourceManager.GetString("MasterAccountInsurer", "CtrFolio")))
                _folioMasterAccountNames.Add(eMasterAccount.PatientAccount, String.Format(ResourceManager.GetString("MasterAccountPatient", "CtrFolio")))
                _folioMasterAccountNames.Add(eMasterAccount.NotMasterAccount, String.Format(ResourceManager.GetString("NotMasterAccount", "CtrFolio")))
            End If
            Return _folioMasterAccountNames
        End Get
    End Property

    ''' <summary>
    ''' lista de los tipos de estado que tiene un ingreso
    ''' </summary>
    Private Shared _DicAdmisionStatus As Dictionary(Of String, String)
    Public Shared ReadOnly Property DicAdmisionStatus As Dictionary(Of String, String)
        Get
            If _DicAdmisionStatus Is Nothing Then
                _DicAdmisionStatus = New Dictionary(Of String, String)
                _DicAdmisionStatus.Add("F", ResourceManager.GetString("Invoiced", "Crystal"))
                _DicAdmisionStatus.Add("A", ResourceManager.GetString("Canceled", "Crystal"))
                _DicAdmisionStatus.Add("C", ResourceManager.GetString("Closed", "Crystal"))
                _DicAdmisionStatus.Add("P", ResourceManager.GetString("Partial", "Crystal"))
                _DicAdmisionStatus.Add("B", ResourceManager.GetString("Locked", "Crystal"))
                _DicAdmisionStatus.Add("", ResourceManager.GetString("Open", "Crystal"))

            End If
            Return _DicAdmisionStatus
        End Get
    End Property

    Private Shared _UnitTypes As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' lista de tuplas de tipo de unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property UnitTypes As List(Of Tuple(Of Byte, String))
        Get
            If _UnitTypes Is Nothing Then
                _UnitTypes = New List(Of Tuple(Of Byte, String))
                _UnitTypes.Add(New Tuple(Of Byte, String)(1, "Urgencias"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(2, "Hospitalizacion"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(3, "Apoyo Dx"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(4, "Apoyo Terapeutico"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(5, "Unidades de Cuidado Intensivo Adulto"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(6, "Unidades de Cuidado Intermedio Adulto"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(7, "Unidades de Cuidado Intensivo Pediatrica"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(8, "Unidades de Cuidado Intermedio Pediatrica"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(9, "Unidades de Cuidado Intensivo Neonatal"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(10, "Unidades de Cuidado Intermedio Neonatal"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(11, "Unidades de Cuidado Basico Neonatal"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(12, "Unidad Renal"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(13, "Unidad Oncologica"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(14, "Unidad Medicina Nuclear"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(15, "Consulta Externa"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(16, "Unidad Mental"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(17, "Unidad de Quemados"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(18, "Unidad de Cuidado Paliativo"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(19, "Cirugia"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(20, "Laboratorio"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(21, "Cardiologia No Invasiva"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(22, "Cardiologia Invasiva"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(23, "Gineco-Obstetricia"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(24, "Consulta Externa - Gineco-Obstetricia"))
                _UnitTypes.Add(New Tuple(Of Byte, String)(25, "Otras"))
            End If
            Return _UnitTypes
        End Get
    End Property

    Private Shared _listLogicOperator As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' lista de tupla de operadores logicos
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ListLogicOperator As List(Of Tuple(Of Integer, String))
        Get
            If _listLogicOperator Is Nothing Then
                _listLogicOperator = New List(Of Tuple(Of Integer, String))
                _listLogicOperator.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("LogicOperatorNever", "Contract")))
                _listLogicOperator.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("LogicOperatorAnd", "Contract")))
                _listLogicOperator.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("LogicOperatorOr", "Contract")))
            End If
            Return _listLogicOperator
        End Get
    End Property

    Private Shared _rateManualType As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Lista de tupla de tipos de manual tarifario
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property RateManualType As List(Of Tuple(Of Byte, String))
        Get
            If _rateManualType Is Nothing Then
                _rateManualType = New List(Of Tuple(Of Byte, String))
                _rateManualType.Add(New Tuple(Of Byte, String)(1, "ISS 2001"))
                _rateManualType.Add(New Tuple(Of Byte, String)(2, "ISS 2004"))
                _rateManualType.Add(New Tuple(Of Byte, String)(3, "SOAT"))
            End If
            Return _rateManualType
        End Get
    End Property


    Private Shared _equalsOperator As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' lista de tuplas de operador de igualdad
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property EqualsOperator As List(Of Tuple(Of Byte, String))
        Get
            If _equalsOperator Is Nothing Then
                _equalsOperator = New List(Of Tuple(Of Byte, String))
                _equalsOperator.Add(New Tuple(Of Byte, String)(1, "="))
                _equalsOperator.Add(New Tuple(Of Byte, String)(2, "<>"))
            End If
            Return _equalsOperator
        End Get
    End Property

    ''' <summary>
    ''' lista tipos de medicamento de la tabla del SISPRO
    ''' </summary>
    Private Shared _medicationTypesSISPRO As Dictionary(Of String, String)
    Public Shared ReadOnly Property MedicationTypesSISPRO As Dictionary(Of String, String)
        Get
            If _medicationTypesSISPRO Is Nothing Then
                _medicationTypesSISPRO = New Dictionary(Of String, String)
                _medicationTypesSISPRO.Add("01", String.Format(ResourceManager.GetString("MedicationType1", "Inventory")))
                _medicationTypesSISPRO.Add("02", String.Format(ResourceManager.GetString("MedicationType2", "Inventory")))
                _medicationTypesSISPRO.Add("03", String.Format(ResourceManager.GetString("MedicationType3", "Inventory")))
                _medicationTypesSISPRO.Add("04", String.Format(ResourceManager.GetString("MedicationType4", "Inventory")))
                _medicationTypesSISPRO.Add("05", String.Format(ResourceManager.GetString("MedicationType5", "Inventory")))
            End If
            Return _medicationTypesSISPRO
        End Get
    End Property
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

#Region "ToXML"

    Public Shared Function DictionaryToXML(data As Dictionary(Of String, String))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            Dim dateValue As Date = Nothing
            Dim value As String = item.Value

            If value Is Nothing Then
                Continue For
            ElseIf Date.TryParse(value.Replace(", ", "").Replace(",", ""), dateValue) Then
                value = dateValue.ToString("dd/MM/yyyy HH:mm:ss")
            Else
                value = item.Value
            End If

            builder.Append(String.Format("<{0}>{1}</{0}>", item.Key, value))
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "JSON Serializer"

    ''' <summary>
    ''' Serializa un objeto a una cadena en formato JSON
    ''' </summary>
    ''' <param name="obj">Objeto a serializar</param>
    ''' <returns>Objeto serializado</returns>
    Public Shared Function SerializeObjectToJson(ByVal obj As Object) As String
        Return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
    End Function

    ''' <summary>
    ''' Deserializa una cadena JSON a un objeto dinámico
    ''' </summary>
    ''' <param name="json">Cadena JSON a deserializar</param>
    ''' <returns>Objeto dinámico deserializado</returns>
    Public Shared Function DeserializeJsonToObject(ByVal json As String) As Object
        Return JsonConvert.DeserializeObject(Of ExpandoObject)(json, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
    End Function

    Public Shared Function DeserializeJsonToEntity(Of T)(json As String) As T
        Return JsonConvert.DeserializeObject(Of T)(json, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
    End Function

#End Region

#Region "Datatable to Object"

    Public Shared Function ConvertDataTable(Of T)(dt As DataTable) As List(Of T)
        Dim data As List(Of T) = New List(Of T)
        For Each row As DataRow In dt.Rows
            Dim item As T = GetItem(Of T)(row)
            data.Add(item)
        Next
        Return data
    End Function

    Public Shared Function GetItem(Of T)(dr As DataRow) As T
        Dim temp As Type = GetType(T)
        Dim obj As T = Activator.CreateInstance(Of T)
        For Each column As DataColumn In dr.Table.Columns
            For Each pro As PropertyInfo In temp.GetProperties()
                If pro.Name = column.ColumnName Then
                    pro.SetValue(obj, dr(column.ColumnName), Nothing)
                End If
            Next
        Next
        Return obj
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
        ''' <summary>
        ''' Redondea a los millones
        ''' </summary>
        Millions = -6
        ''' <summary>
        ''' Redondea a 2  Decimales
        ''' </summary>
        TwoDecimal = 6
    End Enum

    ''' <summary>
    ''' Redondea valores a 0.01, 0.1, 1, 10, 100, 1000
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="roundLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RoundValue(ByVal value As Decimal, ByVal roundLevel As Decimal) As Decimal
        Select Case roundLevel
            Case 1
                Return RoundValue(value, Utils.RoundLevel.Unit)
            Case 2
                Return RoundValue(value, Utils.RoundLevel.Ten)
            Case 3
                Return RoundValue(value, Utils.RoundLevel.Hundred)
            Case 4
                Return RoundValue(value, Utils.RoundLevel.Thousands)
            Case 5, 0.1
                Return Math.Round(value, 1, MidpointRounding.AwayFromZero)
            Case 6, 0.01, 0
                Return Math.Round(value, 2, MidpointRounding.AwayFromZero)
            Case 7
                Return Math.Round(value, 3, MidpointRounding.AwayFromZero)
            Case 8
                Return Math.Round(value, 4, MidpointRounding.AwayFromZero)
            Case 10
                Return RoundValue(value, Utils.RoundLevel.Ten)
            Case 100
                Return RoundValue(value, Utils.RoundLevel.Hundred)
            Case 1000
                Return RoundValue(value, Utils.RoundLevel.Thousands)
            Case Else
                Return value
        End Select
    End Function

    ''' <summary>
    ''' Redondea los valores dependiendo del tipo de redondeo de la moneda
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="roundingType"></param>
    ''' <returns></returns>
    Public Shared Function RoundValueByTypeCurrency(ByVal value As Decimal, ByVal roundingType As Integer) As Decimal
        Select Case roundingType
            Case ECurrencyRoundingType.TwoDecimals
                Return Math.Round(value, 2, MidpointRounding.AwayFromZero)
            Case ECurrencyRoundingType.OneDecimal
                Return Math.Round(value, 1, MidpointRounding.AwayFromZero)
            Case ECurrencyRoundingType.None
                Return Math.Round(value, 0, MidpointRounding.AwayFromZero)
            Case ECurrencyRoundingType.Tens
                Return RoundValue(value, RoundLevel.Ten)
            Case ECurrencyRoundingType.Hundreds
                Return RoundValue(value, RoundLevel.Hundred)
            Case ECurrencyRoundingType.Thousands
                Return RoundValue(value, RoundLevel.Thousands)
            Case Else
                Return value
        End Select
    End Function

    ''' <summary>
    ''' Redondea valores de moneda a un nivel específico
    ''' </summary>
    ''' <param name="value">Valor a redondear</param>
    ''' <param name="roundLevel">Nivel al que se va a redondear el valor</param>
    ''' <returns>Valor redondeado</returns>
    Public Shared Function RoundValue(ByVal value As Decimal, Optional ByVal roundLevel As RoundLevel = Utils.RoundLevel.Unit) As Double
        Dim m As Double
        m = 10 ^ roundLevel
        If value < 0 Then
            Return Fix(value * m - 0.5) / m
        Else
            Return Fix(value * m + 0.5) / m
        End If
    End Function

    ''' <summary>
    ''' Redondea la parte decimal significativa de un valor maximo a 4 digitos
    ''' </summary>
    Public Shared Function SetPartDecimalToValue(ByVal decimalValue As Decimal)
        Try
            Dim result As String

            If decimalValue = Math.Floor(decimalValue) Then 'Si el valor es entero (como 0.5 o 1), mostrarlo sin decimales
                result = decimalValue.ToString("0")

            Else 'Si el valor tiene decimales, mostrar hasta 4 decimales
                result = decimalValue.ToString("0.####")
            End If

            Return result
        Catch ex As Exception
            Return Decimal.Zero
        End Try
    End Function

    Public Shared Function RoundDown(ByVal value As Double, ByVal decimalPlaces As Integer) As Double
        Dim factor As Integer = 10 ^ decimalPlaces
        Return Math.Floor(value * factor) / factor
    End Function

#End Region

#Region "Integration HIS"
    Public Shared Function AppFolder() As String
        'Windows.ApplicationModel.AppInfo.Current.PackageFamilyName
        'New Windows.ApplicationModel.AppService.AppServiceConnection()
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return AppDomain.CurrentDomain.BaseDirectory
        Else
            Return Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Presentation.Client\")
        End If
    End Function

    Public Shared Function LocalFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If

    End Function

    Public Shared Function UserFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If

    End Function

    Public Shared Function TemporalFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Path.GetTempPath()
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return Windows.Storage.ApplicationData.Current.TemporaryFolder.Path
        End If
    End Function

    Public Shared Function DeskTopFolder() As String
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Else
            ' Change this to Dim roamingFolder As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
            ' to use the RoamingFolder instead, for example.
            Return Windows.Storage.ApplicationData.Current.LocalFolder.Path
        End If
    End Function

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

#Region "Electronic Documents For Electronic Billing"

    Public Shared Function GetMinutesToAdd(retry As Integer) As Integer
        Dim minuteToAdd As Int32 = 0
        Select Case retry
            Case 0
                minuteToAdd = 0
            Case 1
                minuteToAdd = 1
            Case 2
                minuteToAdd = 2
            Case 3
                minuteToAdd = 5
            Case 4
                minuteToAdd = 10
            Case 5
                minuteToAdd = 15
            Case 6
                minuteToAdd = 30
            Case 7
                minuteToAdd = 60
            Case 8
                minuteToAdd = 120
            Case 9
                minuteToAdd = 360
            Case 10
                minuteToAdd = 720
            Case Else
                minuteToAdd = 1440 * If(retry > 10, retry - 10, 1)
        End Select
        Return minuteToAdd
    End Function

    Public Shared Function SerializeToXmlElement(o As Object) As Xml.XmlElement
        Return Root.Utils.SerializeToXmlElement(o)
    End Function

    Public Shared Function SerializeToXmlString(o As Object) As String
        Return Root.Utils.SerializeToXmlString(o)
    End Function

    Public Shared Function GetFormatNumberWithTwoDecimals(value As Decimal) As String
        Return Infrastructure.CrossCutting.Root.Utils.GetFormatNumberWithTwoDecimals(value)
    End Function

    Public Shared Function GetPathElectronicDocuments() As String
        'Obtenemos el archivo de configuración de la aplicación
        Dim appConfig As System.Configuration.Configuration = Nothing
        If System.IO.Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile).ToLower().Equals("web.config") Then
            appConfig = WebConfigurationManager.OpenWebConfiguration("~")
        Else
            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        End If
        Dim valueParam = appConfig.AppSettings.Settings(PATH_ELECTRONIC_DOCUMENTS)
        If valueParam IsNot Nothing Then 'Si el parametro existe
            If Not valueParam.Value.Trim().Equals(String.Empty) Then
                Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value.Trim())
            Else
                valueParam.Value = DefaultPath()
                Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value)
            End If
        Else 'Si no existe
            valueParam = New KeyValueConfigurationElement(PATH_ELECTRONIC_DOCUMENTS, DefaultPath)
            appConfig.AppSettings.Settings.Add(valueParam)
            Infrastructure.CrossCutting.Root.Utils.EnsurePathExists(valueParam.Value)
            appConfig.Save(ConfigurationSaveMode.Modified, True)
        End If
        Return valueParam.Value.Trim()
    End Function

    Public Shared Function Sha1Encode(strToHash As String) As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha1Encode(strToHash)
    End Function

    Public Shared Function Sha384Encode(strToHash As String) As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha384Encode(strToHash)
    End Function

    Public Shared Function Sha1Base64Encode(nb As Byte(), DateSend As String, Key As String) As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha1Base64Encode(nb, DateSend, Key)
    End Function

    Public Shared Function Sha256Encode(strToHash As String) As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha256Encode(strToHash)
    End Function

    Public Shared Function FileBase64Encode(filePath As String, fileName As String) As String
        Dim path = System.IO.Path.Combine(filePath, fileName)
        Return Convert.ToBase64String(File.ReadAllBytes(path))
    End Function

    Public Shared Function FileReadAllBytes(filePath As String, fileName As String) As Byte()
        Dim path = System.IO.Path.Combine(filePath, fileName)
        Return File.ReadAllBytes(path)
    End Function

    Public Shared Function ValidateFileExists(ByVal filePath As String, ByVal fileName As String) As Boolean
        Return Infrastructure.CrossCutting.Root.Utils.ValidateFileExists(filePath, fileName)
    End Function

    Public Shared Sub CompressFile(filePath As String, fileNameXml As String, fileNameZip As String)
        Dim fileZip = System.IO.Path.Combine(filePath, fileNameZip)
        File.Delete(fileZip)
        Dim fileXml = System.IO.Path.Combine(filePath, fileNameXml)
        Dim fileInfo As New FileInfo(fileXml)
        Using destination As ZipArchive = ZipFile.Open(fileZip, ZipArchiveMode.Create)
            destination.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name, CompressionLevel.Optimal)
        End Using
    End Sub

    ''' <summary>
    ''' funcion que se encarga de comprimir en zip el archivo, retornando otro arreglo de byte
    ''' </summary>
    ''' <param name="fileXmlBytes"></param>
    ''' <param name="fileNameXml"></param>
    ''' <returns></returns>
    Public Shared Function CompressFileStream(fileXmlBytes As Byte(), fileNameXml As String) As Byte()
        Using memoryStream As New MemoryStream()
            Using archive As New ZipArchive(memoryStream, ZipArchiveMode.Create, True)
                Dim entry As ZipArchiveEntry = archive.CreateEntry(fileNameXml, CompressionLevel.Optimal)
                Using entryStream As Stream = entry.Open()
                    entryStream.Write(fileXmlBytes, 0, fileXmlBytes.Length)
                End Using
            End Using
            Return memoryStream.ToArray()
        End Using
    End Function

    ''' <summary>
    ''' funcion que se encarga de comprimir varios archivos en zip,haciendo uso de stream
    ''' </summary>
    ''' <param name="filesBytes"></param>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Public Shared Function CompressMultipleFileStream(filesBytes As Dictionary(Of String, Byte()), filePath As String) As Byte()
        Using memoryStream As New MemoryStream()
            Using archive As New ZipArchive(memoryStream, ZipArchiveMode.Create, True)
                For Each item In filesBytes
                    Dim entry As ZipArchiveEntry = archive.CreateEntry(item.Key, CompressionLevel.Optimal)
                    Using entryStream As Stream = entry.Open()
                        entryStream.Write(item.Value, 0, item.Value.Length)
                    End Using
                Next
            End Using
            Return memoryStream.ToArray()
        End Using
    End Function

    Public Shared Sub CompressMultipleFile(fileZip As String, filePath As String, files As String())
        fileZip = System.IO.Path.Combine(filePath, fileZip)
        File.Delete(fileZip)
        For Each fileName As String In files
            If Not Utils.ValidateFileExists(filePath, fileName) Then
                Dim fileFullPath = System.IO.Path.Combine(filePath, fileName)
                Using archive As ZipArchive = ZipFile.Open(fileZip, ZipArchiveMode.Update)
                    archive.CreateEntryFromFile(fileFullPath, Path.GetFileName(fileFullPath), CompressionLevel.Optimal)
                End Using
            End If
        Next
    End Sub

    Public Shared Sub DeleteFile(filePath As String, fileName As String)
        Dim path = System.IO.Path.Combine(filePath, fileName)
        File.Delete(path)
    End Sub

    Public Shared Function GetXmlStringFromHttpContentString(content As String) As String
        Dim startPosition = content.IndexOf("<")
        Dim endPosition = content.LastIndexOf(">") + 1
        Dim xml = content.Substring(startPosition, endPosition - startPosition)
        Return xml
    End Function

    Public Shared Function Deserialize(Of T)(xmlStr As String) As T
        Return Infrastructure.CrossCutting.Root.Utils.Deserialize(Of T)(xmlStr)
    End Function

    Public Shared Function Deserialize(Of T)(doc As XDocument) As T
        Return Infrastructure.CrossCutting.Root.Utils.Deserialize(Of T)(doc)
    End Function

    Public Shared Function GetDescriptionTypeDocumentElectronic(documentType As TypeElectronicDocument) As String
        Select Case documentType
            Case TypeElectronicDocument.DocumentoSoporte
                Return "Documento Soporte"
            Case TypeElectronicDocument.NotaDeAjuste
                Return "Nota de ajuste"
            Case Else
                Return String.Empty
        End Select
    End Function

#End Region

#Region "Others"

    ''' <summary>
    ''' Convierte el elemento de entrada a decimal
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Shared Function ConvertToDecimal(ByVal data As Object) As Decimal
        Dim result As Decimal = 0
        If data Is Nothing Then
            Return result
        End If
        Dim convertedValue As Decimal = Nothing
        If Decimal.TryParse(data.ToString(), convertedValue) Then
            result = convertedValue
            Return result
        End If
        Return result
    End Function

    ''' <summary>
    ''' Convierte el elemento de entrada a entero
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Shared Function ConvertToInt(ByVal data As Object) As Integer
        Dim result As Integer = 0
        If data Is Nothing Then
            Return result
        End If
        Dim convertedValue As Integer = Nothing
        If Integer.TryParse(data.ToString, convertedValue) Then
            Return convertedValue
        End If
        Return result
    End Function

    ''' <summary>
    ''' Retornar el formato de la fecha
    ''' </summary>
    Public Shared Function GetCustomDateFormat(DateFormat As CustomFormatDate) As String
        Select Case DateFormat
            Case CustomFormatDate.dd_MM_yyyy
                Return "dd/MM/yyyy"
            Case CustomFormatDate.MM_dd_yyyy
                Return "MM/dd/yyyy"
            Case CustomFormatDate.yyyy_MM_dd
                Return "yyy/MM/dd"
            Case CustomFormatDate.dd_MMM_yyyy
                Return "dd/MMM/yyyy"
            Case CustomFormatDate.dd_de_MMMM_de_yyyy
                Return "dd \de MMMM \de yyyy"
            Case Else
                Return "dd/MM/yyyy"
        End Select
    End Function

    ''' <summary>
    ''' Retornar formato de hora
    ''' </summary>
    Public Shared Function GetCustomTimeFormat(TimeFormat As CustomFormatTime) As String
        Select Case TimeFormat
            Case CustomFormatTime.hh_mm_ss_tt
                Return "hh:mm:ss tt"
            Case CustomFormatTime.HH_mm_ss
                Return "HH:mm:ss"
            Case Else
                Return "hh:mm:ss tt"
        End Select
    End Function

    ''' <summary>
    ''' Método que se encarga de obtener el tipo de unidad
    ''' </summary>
    ''' <param name="unitType"></param>
    ''' <returns></returns>
    Public Shared Function GetFunctionalUnitType(unitType As Integer) As Integer
        Dim unitTypeFilter As Integer

        If unitType = 1 Or unitType = 23 Then
            unitTypeFilter = 1 '1 - Urgencias
        ElseIf unitType = 2 Or unitType = 5 Or unitType = 6 Or unitType = 7 Or unitType = 8 Or unitType = 9 Or unitType = 10 Or unitType = 11 Or unitType = 16 Or unitType = 17 Or unitType = 18 Then
            unitTypeFilter = 2 '2 - Hospitalizacion
        ElseIf unitType = 19 Then
            unitTypeFilter = 3 '3 - Quirofanos
        Else
            unitTypeFilter = 4 '4 - Servicios Ambulatorios
        End If

        Return unitTypeFilter
    End Function

    Public Shared Function GetStrHours(hours As Integer, minutes As Integer) As Tuple(Of Integer, Integer, String)
        If minutes > 59 Then
            Dim h As Integer = Math.Truncate(minutes / 60)
            minutes = minutes - (h * 60)
            hours += h
        End If
        Return New Tuple(Of Integer, Integer, String)(hours, minutes, $"{hours.ToString()}:{If(minutes < 10, "0" & minutes, minutes.ToString())} Horas")
    End Function

    Public Shared Function RemoveSpecialCharacters(ByVal str As String) As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each c As Char In str
            If (c >= "0"c AndAlso c <= "9"c) OrElse (c >= "A"c AndAlso c <= "Z"c) OrElse (c >= "a"c AndAlso c <= "z"c) OrElse c = "."c OrElse c = "_"c Then
                sb.Append(c)
            End If
        Next

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Calcula el porcentage de todas las distribuciones de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalculatePercentage(TotalValue As Decimal, PartValue As Decimal) As Decimal
        If TotalValue = 0 Then
            Return 0
        End If
        Dim returnValue As Decimal = 0
        returnValue = (PartValue * 100) / TotalValue
        Return RoundValue(returnValue, RoundLevel.Unit)
    End Function

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

        Dim units As New UnitStay With {
            .Days = endDate.Subtract(initDate).Days,
            .Hours = endDate.Subtract(initDate).Hours
        }

        Dim oldDays = units.Days

        If units.Days > 0 Then
            units.Days = 0

            While initDate < endDate
                Dim medianoche = New Date(initDate.Year, initDate.Month, initDate.Day, limitTime.Hours, limitTime.Minutes, limitTime.Seconds)

                If medianoche >= initDate And medianoche < endDate Then
                    units.Days += 1
                End If

                initDate = initDate.AddDays(1)
            End While
        End If

        If oldDays < units.Days Then
            units.Hours = 0
        End If

        Return units
    End Function

    ''' <summary>
    ''' Obtiene una cadena de texto formateada con la cantidad de
    ''' años, meses y dias que hay entre la fecha dada y la actual
    ''' </summary>
    ''' <param name="birth">Fecha de nacimiento (Fecha inicial)</param>
    ''' <returns>Edad formateada</returns>
    Public Shared Function AgeToString(ByVal birth As Date) As String
        Dim tday As TimeSpan = Date.Now.Subtract(birth)
        Dim years As Integer, months As Integer, days As Integer
        months = 12 * (Date.Now.Year - birth.Year) + (Date.Now.Month - birth.Month)

        If Date.Now.Day < birth.Day Then
            months -= 1
            days = Date.DaysInMonth(birth.Year, birth.Month) - birth.Day + Date.Now.Day
        Else
            days = Date.Now.Day - birth.Day
        End If

        years = Math.Floor(months / 12)
        months -= years * 12

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
    ''' Obtiene recursivamente la secuencia de excepciones heredadas
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <param name="list">Lista de mensajes</param>
    Private Shared Sub GetInnerExceptionMessage(ByVal ex As Exception, ByRef list As List(Of String))
        If ex.InnerException IsNot Nothing Then
            GetInnerExceptionMessage(ex.InnerException, list)
        Else
            list.Add(ex.Message & vbCrLf & ex.StackTrace & vbCrLf & "===============================" & vbCrLf)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene recursivamente la secuencia de excepciones heredadas
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <returns>Message Error</returns>
    Public Shared Function GetInnerExceptionMessageToString(ByVal ex As Exception) As String
        Return Infrastructure.CrossCutting.Root.Utils.GetInnerExceptionMessageToString(ex)
    End Function

    ''' <summary>
    ''' Valida si el error presentado corresponde a un código de los enviados
    ''' </summary>
    ''' <param name="ex">Excepcion a validar</param>
    ''' <param name="status">Codigo de los estados a validar</param>
    ''' <returns>Verdadero si encuentra alguno de los códigos relacionados</returns>
    Public Shared Function ValidateSqlCodeExceptions(ByVal ex As SqlException, ByVal status As Integer()) As Boolean
        Return Infrastructure.CrossCutting.Root.Utils.ValidateSqlCodeExceptions(ex, status)
    End Function

    '''' <summary>
    '''' Corrige el numero de factura para poder enviarlo al SP de consulta
    '''' </summary>
    '''' <param name="number">Numero de factura a arreglar</param>
    '''' de lo contrario se corrige para .Net</param>
    '''' <returns>Numero de factura corregido</returns>
    Public Shared Function FixInvoiceNumber(ByVal number As String, ByVal typeInterface As Integer) As String
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
    ''' Lista el nombre de los decimanles del tipo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListDecimalCurrency(ISO4217 As String) As String
        Select Case ISO4217
            Case "CRC", "EUR" : ListDecimalCurrency = "CENTIMOS"
            Case Else : ListDecimalCurrency = "CENTAVOS"
        End Select
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
            Case Is < 0 : Num2Text = "MENOS " & Num2Text(Math.Abs(value))
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
        Return BitConverter.ToString(md5Obj.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.Trim()))).Replace("-", "")
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
            Return Convert.ToBase64String(Encoding.UTF8.GetBytes(data.Trim())).ToUpper()
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
        Dim pattern As String = "áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_\/ "
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
    ''' Obtiene el nombre de la empresa Indigo  a la cual esta conectado el usuario.
    ''' </summary>
    ''' <returns>Nombre de la compañia</returns>
    Public Shared Function GetSessionValuesIndigoCompanyName() As String
        Return ValidateString(SessionValues.Instance.IndigoCompanyName)
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
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(LocalFolder(), "\" & GetCompanyName() & "\" & GetAppName())
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
        Return String.Concat(UserFolder(), "\" & GetCompanyName() & "\" & GetAppName())
    End Function


    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario del EHR de los Customizables
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFilesEHRCustomizableGridControl() As String
        Return Path.Combine(UserFolder(), GetCompanyName(), GetSessionValuesIndigoCompanyName(), "Xml", "Customizable", "GridControl")
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
        Return DEFAULT_SKIN_NAME
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
    ''' Obtiene un valor que indica si se muestra el selector de temas
    ''' </summary>
    ''' <returns>Valor que indica si se muestra el selector de temas</returns>
    Public Shared Function ShowThemeSkinSelector() As Boolean
        'If ConfigurationManager.AppSettings("ShowThemeSkinSelector") IsNot Nothing AndAlso Not ConfigurationManager.AppSettings("ShowThemeSkinSelector").ToString().Trim().Equals(String.Empty) Then
        '    Return If(ConfigurationManager.AppSettings("ShowThemeSkinSelector").ToString().Trim().ToLower().Equals("true"), True, False)
        'End If
        'Return False
        Return ApplicationSetting.Instance.ShowThemeSkinSelector
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

    Public Shared Function IsFoundational(MyTag As String, SessionValue As SessionValues) As Boolean
        If Not String.IsNullOrEmpty(SessionValue.FoundationalContainer) Then
            Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = MyTag)
            If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                Return True
            End If
        End If
        Return False
    End Function

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
    Public Shared Function StringPad(input As String, padLength As Integer, padString As String, Optional padType As PadType = PadType.STR_PAD_RIGHT) As String
        Dim output As String = ""
        Dim difference As Integer = padLength - Len(input)
        If difference > 0 Then
            Select Case padType
                Case PadType.STR_PAD_LEFT
                    For x = 1 To difference Step Len(padString)
                        output = output & padString
                    Next
                    output = Right(output & input, padLength)
                Case PadType.STR_PAD_RIGHT
                    For x = 1 To difference Step Len(padString)
                        output = output & padString
                    Next
                    output = Left(input & output, padLength)
                Case PadType.STR_PAD_BOTH
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
    ''' devuelve la cadena hasta la cantidad maxima de caracteres que se establezca 
    ''' si el string supera el largo maximo la devuelve cortada, de lo contrario lo retorna tal cual
    ''' </summary>
    ''' <param name="input"></param>
    ''' <param name="padLength"></param>
    ''' <returns></returns>
    Public Shared Function StringMaxLenth(input As String, padLength As Integer, Optional separator As String = "") As String
        Dim outPut As String = input
        If Len(input) > padLength Then
            outPut = input.Substring(0, padLength)
        End If
        outPut &= separator
        Return outPut
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
            value = New DevExpress.Data.Filtering.Helpers.ExpressionEvaluator(TypeDescriptor.GetProperties(GetType(String)), DevExpress.Data.Filtering.CriteriaOperator.Parse(expresion)).Evaluate(expresion)
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
    Public Shared Function GetDistributedValues(value As Decimal, percent1 As Decimal) As Tuple(Of Decimal, Decimal)
        Dim value1 As Decimal = value * percent1 / 100
        Dim value2 As Decimal = value - value1
        Return New Tuple(Of Decimal, Decimal)(value1, value2)
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

    ''' <summary>
    ''' Obtiene el estado que se usará en el proceso de auditoria
    ''' </summary>
    ''' <param name="id">id del registro(es 0 cuando se esta insertando)</param>
    ''' <param name="status">estado del registro (1 registrado, 2 confirmado, 3 anulado)</param>
    ''' <returns>De acuerdo a los action del proyecto de auditoria</returns>
    Public Shared Function GetAuditStatus(id As Integer, status As Integer) As Integer
        Dim auditStatus As Integer
        If status = 2 Then
            auditStatus = 5
        ElseIf status = 3 Then
            auditStatus = 6
        ElseIf id = 0 Then
            auditStatus = 1
        Else
            auditStatus = 2
        End If
        Return auditStatus
    End Function

    Public Shared Function CalculateAdjustedValue(Difference As Decimal, ValueIfDifferenceNegative As Decimal, ValueIfDifferencePositive As Decimal) As Decimal
        Return Root.Utils.CalculateAdjustedValue(Difference, ValueIfDifferenceNegative, ValueIfDifferencePositive)
    End Function

    Public Shared Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("dd/MM/yyyy hh:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars()
    End Function

    Public Shared Function GetAppSettingValueByKey(key As String)
        Dim appSettings = ConfigurationManager.AppSettings

        If appSettings.Count = 0 Then
            Throw New Exception("El archivo de configuración esta vacio.")
        End If

        If String.IsNullOrEmpty(appSettings(key)) Then
            Throw New Exception(String.Format("No se encontró la key: '{0}' en el archivo de configuración.", key))
        End If

        Return appSettings(key)
    End Function

    Public Shared Function GetDefaultReport(reportsPath As String, formId As Integer, className As String) As String
        Dim nameRepDefault As String = "Report"

        Dim pathRepDefault As String = Path.Combine(reportsPath, formId & "Repx.Default")
        If File.Exists(pathRepDefault) Then
            nameRepDefault = File.ReadAllText(pathRepDefault)
            nameRepDefault = nameRepDefault.Trim()
        End If

        Return Path.Combine(reportsPath, String.Format("{0}.{1}.{2}.repx", formId, className, nameRepDefault))
    End Function

    Public Shared Function ValidateItem(item As List(Of String), index As Integer, QuantityItems As Integer) As String
        If item.Count = QuantityItems Then
            Return item(index)
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetNumberFromString(value As String) As String
        Dim numberPart As Integer = 0
        Dim numberString As String = System.Text.RegularExpressions.Regex.Match(value, "\d+").Value
        If numberString <> "" Then
            numberPart = Convert.ToInt32(numberString)
        End If
        Return numberPart.ToString()
    End Function

#End Region

#Region "Conversion"
    ''' <summary>
    ''' Conversión de unidades
    ''' </summary>
    ''' <param name="measurementUnit1"></param>
    ''' <param name="measurementUnit2"></param>
    ''' <returns></returns>
    Public Shared Function MeasureUnitConvert(measurementUnit1 As String, measurementUnit2 As String) As Decimal
        measurementUnit1 = measurementUnit1.ToLower()
        measurementUnit2 = measurementUnit2.ToLower()

        If measurementUnit1 = measurementUnit2 Then Return 1

        If measurementUnit1 = "g" AndAlso measurementUnit2 = "mg" Then Return 1000
        If measurementUnit1 = "g" AndAlso measurementUnit2 = "kg" Then Return 0.001
        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "kg" Then Return 0.000001

        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "g" Then Return 0.001
        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "g" Then Return 1000
        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "mg" Then Return 1000000

        If measurementUnit1 = "ml" AndAlso measurementUnit2 = "l" Then Return 0.001
        If measurementUnit1 = "l" AndAlso measurementUnit2 = "ml" Then Return 1000
        If measurementUnit1 = "ml" AndAlso measurementUnit2 = "cc" Then Return 1

        If measurementUnit1 = "cc" AndAlso measurementUnit2 = "l" Then Return 0.001
        If measurementUnit1 = "l" AndAlso measurementUnit2 = "cc" Then Return 1000

        If measurementUnit1 = "g" AndAlso measurementUnit2 = "g/g" Then Return 1
        If measurementUnit1 = "g" AndAlso measurementUnit2 = "mg/ml" Then Return 1000
        If measurementUnit1 = "g" AndAlso measurementUnit2 = "g/mg" Then Return 1
        If measurementUnit1 = "g" AndAlso measurementUnit2 = "g/ml" Then Return 1

        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "g/g" Then Return 0.001
        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "mg/ml" Then Return 1
        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "g/mg" Then Return 0.001
        If measurementUnit1 = "mg" AndAlso measurementUnit2 = "g/ml" Then Return 0.001

        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "g/g" Then Return 1000
        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "mg/ml" Then Return 0.000001
        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "g/mg" Then Return 1000
        If measurementUnit1 = "kg" AndAlso measurementUnit2 = "g/ml" Then Return 1000

        Return 0
    End Function

    ''' <summary>
    ''' Función que devuelve la unidad de tiempo según parámetros, pudiendo ser singular o plural
    ''' </summary>
    ''' <param name="timeUnit"></param>
    ''' <param name="unit"></param>
    ''' <returns></returns>
    Public Shared Function TimeUnitConvert(timeUnit As Byte, Optional unit As Integer = 0)

        Dim timeUnitDescription = ""

        Select Case timeUnit
            Case 1
                If unit = 1 Then
                    timeUnitDescription = "dia"
                Else
                    timeUnitDescription = "dias"
                End If
            Case 2
                If unit = 1 Then
                    timeUnitDescription = "mes"
                Else
                    timeUnitDescription = "meses"
                End If
            Case 3
                If unit = 1 Then
                    timeUnitDescription = "año"
                Else
                    timeUnitDescription = "años"
                End If
        End Select

        Return timeUnitDescription
    End Function


    ''' <summary>
    ''' Función que devuelve la unidad de peso según parámetros, pudiendo ser singular o plural
    ''' </summary>
    ''' <param name="weightUnit"></param>
    ''' <param name="unit"></param>
    ''' <returns></returns>
    Public Shared Function WeightUnitConvert(weightUnit As Byte, Optional unit As Integer = 0)

        Dim weightUnitDescription = ""

        Select Case weightUnit
            Case 1
                If unit = 1 Then
                    weightUnitDescription = "miligramo"
                Else
                    weightUnitDescription = "miligramos"
                End If
            Case 2
                If unit = 1 Then
                    weightUnitDescription = "gramo"
                Else
                    weightUnitDescription = "gramos"
                End If
            Case 3
                If unit = 1 Then
                    weightUnitDescription = "kilogramo"
                Else
                    weightUnitDescription = "kilogramos"
                End If
        End Select

        Return weightUnitDescription
    End Function
#End Region

#Region "ISO4217"
    ''' <summary>
    ''' Retorna un valor con formato moneda dependiendo del tipo de moneda de la ISO 4217
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ISO4217"></param>
    ''' <returns></returns>
    Public Shared Function GetMoneyWithISO4217(Value As Decimal, ISO4217 As String, Optional currencyDecimalDigits As Integer = 2) As String
        Dim mask = "C" + currencyDecimalDigits.ToString()

        If String.IsNullOrEmpty(ISO4217) Then
            Return Value.ToString(mask)
        End If

        Dim cultureId As Integer
        Dim _region = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(Function(ct)
                                                                                        cultureId = ct.LCID
                                                                                        Return New RegionInfo(ct.LCID)
                                                                                    End Function).
                                                                                Where(Function(ri) ri.ISOCurrencySymbol = ISO4217).FirstOrDefault
        If _region IsNot Nothing Then
            Dim culture As CultureInfo = New CultureInfo(cultureId)
            culture.NumberFormat = SessionValues.Instance.ConfigurationSeparatorNumberFormat(culture.NumberFormat)
            Return Value.ToString(mask, culture)
        Else
            Return String.Format("{0} {1}", Value, ISO4217)
        End If
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
#End Region

#Region "TaxControls"
    ''' <summary>
    ''' enumerado que establece el nombre del subtotal, valor bruto e impuesto
    ''' </summary>
    Public Enum eTypeTaxControl
        SubtotalSalesPrice
        GrossValue
        TaxValue
    End Enum

    Public Enum eTypeValue
        GrossValue
        DiscountValue
        NetValue
        TaxValue
        TotalValue
    End Enum

    ''' <summary>
    ''' funcion para retornar el subtotal, el valor bruto y el impuesto
    ''' </summary>
    ''' <param name="flagTaxInclude">Bandera que define si incluye o no impuestos</param>
    ''' <param name="salesPrice">Valor del servicio o producto calculado por la tarifa</param>
    ''' <param name="taxPercent">porcentaje del impuesto</param>
    ''' <returns></returns>
    Public Shared Function SetValueSalesPrice(flagTaxInclude As Boolean, salesPrice As Decimal, taxPercent As Decimal) As Dictionary(Of eTypeTaxControl, Decimal)
        Dim dictionary = New Dictionary(Of eTypeTaxControl, Decimal)
        Dim subtotalSalesPrice As Decimal
        Dim grossValue As Decimal
        Dim taxValue As Decimal

        If flagTaxInclude Then
            subtotalSalesPrice = salesPrice
            dictionary.Add(eTypeTaxControl.SubtotalSalesPrice, subtotalSalesPrice)

            grossValue = Math.Round((subtotalSalesPrice / ((taxPercent / 100) + 1)), 2, MidpointRounding.AwayFromZero)
            dictionary.Add(eTypeTaxControl.GrossValue, grossValue)

            taxValue = Math.Round((subtotalSalesPrice - grossValue), 2, MidpointRounding.AwayFromZero)
            dictionary.Add(eTypeTaxControl.TaxValue, taxValue)
        Else
            grossValue = salesPrice
            dictionary.Add(eTypeTaxControl.GrossValue, grossValue)

            taxValue = Math.Round((grossValue * (taxPercent / 100)), 2, MidpointRounding.AwayFromZero)
            dictionary.Add(eTypeTaxControl.TaxValue, taxValue)

            subtotalSalesPrice = grossValue + taxValue
            dictionary.Add(eTypeTaxControl.SubtotalSalesPrice, subtotalSalesPrice)
        End If
        Return dictionary
    End Function

    ''' <summary>
    ''' funcion para retornar el valor bruto(sin iva y sin desc), el valor neto (con desc y sin IVA), valor del descuento, valor total(con desc y IVA)
    ''' </summary>
    ''' <param name="typeOfValue">Bandera que define si el valor que se esta pasando es el bruto, el neto o el total que incluye IVA </param>
    ''' <param name="value">Valor (que puede ser el bruto, neto o [con desc e iva (total)]) </param>
    ''' <param name="taxPercent">porcentaje del impuesto</param>
    ''' <param name="percentDiscount">porcentaje del descuento</param>
    ''' <param name="discountIsValue">especifica si el procentaje de descuento es por valor o por porcentaje (el valor es el valor a tomar como descuento)</param>
    ''' <returns></returns>
    Public Shared Function SetValueSalesPriceWithNet(typeOfValue As eTypeValue, value As Decimal, taxPercent As Decimal, Optional percentDiscount As Decimal = 0, Optional discountIsValue As Boolean = False) As Dictionary(Of eTypeValue, Decimal)
        Dim _dictionary = New Dictionary(Of eTypeValue, Decimal)
        Dim discountValue As Decimal = 0
        Dim netValue As Decimal = 0
        Dim taxValue As Decimal = 0
        Dim totalValue As Decimal = 0
        Dim grossValue As Decimal = 0

        Select Case typeOfValue
            Case eTypeValue.GrossValue
                ' Añadir valor bruto
                _dictionary.Add(eTypeValue.GrossValue, value)

                ' Calcular y añadir descuento
                discountValue = If(Not discountIsValue, (value * (percentDiscount / 100)), percentDiscount)
                discountValue = Math.Round(discountValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.DiscountValue, discountValue)

                ' Calcular y añadir valor neto
                netValue = value - discountValue
                netValue = Math.Round(netValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.NetValue, netValue)

                ' Calcular y añadir impuesto
                taxValue = netValue * (taxPercent / 100)
                taxValue = Math.Round(taxValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.TaxValue, taxValue)

                ' Calcular y añadir valor total
                totalValue = netValue + taxValue
                _dictionary.Add(eTypeValue.TotalValue, totalValue)

            Case eTypeValue.TotalValue
                ' Añadir valor total
                _dictionary.Add(eTypeValue.TotalValue, value)

                ' Calcular y añadir valor neto
                netValue = (value / ((taxPercent / 100) + 1))
                netValue = Math.Round(netValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.NetValue, netValue)

                ' Calcular y añadir impuesto
                taxValue = value - netValue
                taxValue = Math.Round(taxValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.TaxValue, taxValue)

                ' Calcular y añadir valor bruto
                grossValue = If(Not discountIsValue, (netValue / (1 - (percentDiscount / 100))), netValue + percentDiscount)
                grossValue = Math.Round(grossValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.GrossValue, grossValue)

                ' Calcular y añadir descuento
                discountValue = If(Not discountIsValue, grossValue - netValue, percentDiscount)
                discountValue = Math.Round(discountValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.DiscountValue, discountValue)

            Case eTypeValue.NetValue
                ' Añadir valor neto
                _dictionary.Add(eTypeValue.NetValue, value)

                ' Calcular y añadir impuesto
                taxValue = (value * (taxPercent / 100))
                taxValue = Math.Round(taxValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.TaxValue, taxValue)

                ' Calcular y añadir valor total
                totalValue = value + taxValue
                totalValue = Math.Round(totalValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.TotalValue, totalValue)

                'Calcular y añadir valor bruto
                grossValue = If(Not discountIsValue, (value / (1 - (percentDiscount / 100))), value + percentDiscount)
                grossValue = Math.Round(grossValue, 2, MidpointRounding.AwayFromZero)
                _dictionary.Add(eTypeValue.GrossValue, grossValue)

                ' Calcular y añadir descuento 
                discountValue = If(Not discountIsValue, grossValue - value, percentDiscount)
                _dictionary.Add(eTypeValue.DiscountValue, discountValue)
        End Select

        Return _dictionary
    End Function

#End Region

#Region "TRM"
    ''' <summary>
    ''' funcion que siempre retorna la tasa de cambio en el factor de conversion de mayor numero
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function VisibleTRM(value As Decimal) As Decimal
        If value = 0 Then
            Return value
        End If
        Dim Value2 = (1 / value)

        Return Math.Round(If(value >= Value2, value, Value2), 2)
    End Function
#End Region

#Region "Points and Commas For Thousands a decimal"
    ''' <summary>
    ''' funcion que retorna un decimal con su formato segun la localización
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function correctDecimalFormat(value As String) As Decimal
        Dim culture As NumberFormatInfo = CultureInfo.CurrentCulture.NumberFormat.Clone()
        Dim listChar = New List(Of Char)
        If value.Contains(",") Or value.Contains(".") Then
            For Each c As Char In value
                If c = ","c Or c = "."c Then
                    listChar.Add(c)
                End If
            Next
            If listChar.Count > 1 Then
                If listChar(0) = culture.CurrencyGroupSeparator Then
                    Return Convert.ToDecimal(value)
                Else
                    Return CDec(Convert.ToDecimal(value, CultureInfo.InvariantCulture))
                End If
            Else

                If listChar(0) = culture.CurrencyDecimalSeparator Then
                    Return Convert.ToDecimal(value)
                Else
                    Return CDec(Convert.ToDecimal(value, CultureInfo.InvariantCulture))
                End If

            End If

        End If
        Return Convert.ToDecimal(value)
    End Function

#End Region

#Region "Mask By Currency Decimals"
    ''' <summary>
    ''' funcion que retorna entero para la mascara segun los decimales de redondeo que maneje la moneda
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function MaskByCurrencyRounding(value As Decimal, Optional CurrencyNumbertFormat As Globalization.NumberFormatInfo = Nothing) As Integer
        Select Case value
            Case 0.01, ECurrencyRoundingType.TwoDecimals 'Dos decimales
                Return 2
            Case 0.1, ECurrencyRoundingType.OneDecimal 'Un decimal
                Return 1
            Case 1  ' Ningun decimal
                If CurrencyNumbertFormat IsNot Nothing Then
                    Return CurrencyNumbertFormat.CurrencyDecimalDigits
                Else
                    Return 0
                End If
            Case ECurrencyRoundingType.None ' Ningun decimal
                Return 0
            Case 10, ECurrencyRoundingType.Tens 'Decenas
                Return 0
            Case 100, ECurrencyRoundingType.Hundreds 'Centenas
                Return 0
            Case 1000, ECurrencyRoundingType.Thousands 'Miles
                Return 0
            Case Else
                Return 0
        End Select
    End Function

#End Region

#Region "IdentificationType"
    ''' <summary>
    ''' metodo que retorna el tipo de identificacion antigua, por sigla o id
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    <Obsolete()>
    Public Shared Function IdentificationTypeObsolete(value As String) As Tuple(Of Integer, String, String)

        Select Case value
            Case "CC", "0"
                Return New Tuple(Of Integer, String, String)(0, "Cédula de Ciudadanía", "CC")
            Case "TI", "1"
                Return New Tuple(Of Integer, String, String)(1, "Cédula de Extranjería", "CE")
            Case "TI", "2"
                Return New Tuple(Of Integer, String, String)(2, "Tarjeta de Identidad", "TI")
            Case "RC", "3"
                Return New Tuple(Of Integer, String, String)(3, "Registro Civil", "RC")
            Case "PA", "4"
                Return New Tuple(Of Integer, String, String)(4, "Pasaporte", "PA")
            Case "AS", "5"
                Return New Tuple(Of Integer, String, String)(5, "Adulto Sin Identificación", "AS")
            Case "MS", "6"
                Return New Tuple(Of Integer, String, String)(6, "Menor Sin Identificación", "MS")
            Case "NI", "7"
                Return New Tuple(Of Integer, String, String)(7, "Nit", "NI")
            Case "NU", "8"
                Return New Tuple(Of Integer, String, String)(8, "Número único de identificación personal", "NU")
            Case "CN", "9"
                Return New Tuple(Of Integer, String, String)(9, "Certificado Nacido Vivo", "CN")
            Case "CD", "10"
                Return New Tuple(Of Integer, String, String)(10, "Carnet Diplomático", "CD")
            Case "SC", "11"
                Return New Tuple(Of Integer, String, String)(11, "Salvoconducto", "SC")
            Case "PE", "12"
                Return New Tuple(Of Integer, String, String)(12, "Permiso especial de Permanencia", "PE")
            Case "PT", "13"
                Return New Tuple(Of Integer, String, String)(13, "Permiso temporal de Permanencia", "PT")
            Case "DE", "14"
                Return New Tuple(Of Integer, String, String)(14, "Documento extranjero", "DE")
            Case "SI", "15"
                Return New Tuple(Of Integer, String, String)(15, "Sin identificación", "SI")
            Case Else
                Return New Tuple(Of Integer, String, String)(20, "Otro", "OT")
        End Select
    End Function
#End Region

#Region "UtilsContractItemsRestriction"
#Region "enums"
    Public Enum EItemsRestrictionRuleType
        CUPS = 1
        SubGroupCUPS = 2
        GroupCUPS = 3
        Product = 4
        SubGroupProduct = 5
        GroupProduct = 6
        General = 7
    End Enum

    Public Enum ELogicalOperators
        [Nothing] = 1
        [And] = 2
        [Or] = 3
    End Enum

    Public Enum EItemsRestrictionConditionType
        [Nothing] = 1
        FunctionalUnit = 2
        FunctionalUnitType = 3
        StayType = 4
        RateManualType = 5
        QxGroup = 6
        UVRRange = 7
    End Enum
#End Region
#End Region

#Region "ElectronicRIPS"

    Private Shared ReadOnly _eRIPSEntityNameDictionary As New Dictionary(Of String, EEntityNameERIPS) From {
        {"Invoice", EEntityNameERIPS.Invoice},
        {"BillingNote", EEntityNameERIPS.BillingNote},
        {"BillingNoteAdjustment", EEntityNameERIPS.BillingNoteAdjustment},
        {"ResendInvoice", EEntityNameERIPS.ResendInvoice},
        {"ResendBillingNote", EEntityNameERIPS.ResendBillingNote},
        {"ResendBillingNoteAdjustment", EEntityNameERIPS.ResendBillingNoteAdjustment},
        {"InvoiceFixedAmount", EEntityNameERIPS.InvoiceFixedAmount},
        {"ResendInvoiceFixedAmount", EEntityNameERIPS.ResendInvoiceFixedAmount},
        {"InvoiceEntityCapitated", EEntityNameERIPS.InvoiceEntityCapitated},
        {"ResendInvoiceEntityCapitated", EEntityNameERIPS.ResendInvoiceEntityCapitated}
    }
    Private Shared _electronicDocumentTypeNotAllowRIPS As HashSet(Of EElectronicDocumentType)
    Private disposedValue As Boolean

    ''' <summary>
    ''' metodo para homologar los nombre de los entity name en enums
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function ERIPSEntityNameHologation(value As String) As EEntityNameERIPS
        If _eRIPSEntityNameDictionary.ContainsKey(value) Then
            Return _eRIPSEntityNameDictionary(value)
        Else
            Dim ex As New ArgumentException($"Error en la configuración del proceso RIPS")
            ex.Data.Add("InvalidEntityName", value)
            ex.Data.Add("ValidEntityNames", String.Join(", ", _eRIPSEntityNameDictionary.Keys))
            ex.Data.Add("ErrorCode", "RIPS_INVALID_ENTITY")
            Throw ex
        End If

    End Function

    ''' <summary>
    ''' propiedad que retorna los tipos de documentos Electronicos Que No deben generar RIPS electronico
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ElectronicDocumentTypeNotAllowRIPS As HashSet(Of EElectronicDocumentType)
        Get
            If _electronicDocumentTypeNotAllowRIPS Is Nothing Then
                _electronicDocumentTypeNotAllowRIPS = New HashSet(Of EElectronicDocumentType)
                _electronicDocumentTypeNotAllowRIPS.Add(EElectronicDocumentType.ParticularInvoice)
                _electronicDocumentTypeNotAllowRIPS.Add(EElectronicDocumentType.BasicBilling)
                _electronicDocumentTypeNotAllowRIPS.Add(EElectronicDocumentType.ProductSalesInvoice)
            End If
            Return _electronicDocumentTypeNotAllowRIPS
        End Get
    End Property
#End Region

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

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
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


