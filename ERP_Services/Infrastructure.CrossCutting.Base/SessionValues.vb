
'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base.SessionValues
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Security.Entities
Imports System.Runtime.Serialization
Imports System.Globalization
Imports System.ServiceModel
Imports System.Linq
Imports System.Threading


#End Region

''' <summary>
''' 	Esta clase  hace uso del patron singleton y contiene cada una de las 
''' propiedades de valores de Session a las cuales pueden acceder en cualquier momento de la aplicacion.
''' </summary>
<DataContract()>
Public Class SessionValues

#Region "Builders"

    Private Shared _instance As SessionValues
    ''' <summary>
    ''' Esta Propiedad instancia la clase IndigoSingleton por una unica vez.	
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance As SessionValues
        Get
            If _instance Is Nothing Then
                _instance = New SessionValues()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Globalization"

    Private _culture As CultureInfo
    ''' <summary>
    ''' Obtiene o asigna la cultura configurada para la aplicación
    ''' </summary>
    ''' <value>Cultura</value>
    ''' <returns>La cultura</returns>
    Public Property Culture As CultureInfo
        Get
            Return Me._culture
        End Get
        Set(value As CultureInfo)
            Me._culture = value
        End Set
    End Property

    Private _City As String
    ''' <summary>
    ''' Esta propiedad contiene el WOIED de la ciudad.
    ''' </summary>
    ''' <value></value>
    <DataMember>
    Property City As String
        Get
            Return _City
        End Get
        Set(ByVal value As String)
            _City = value
        End Set
    End Property
#End Region

#Region "Reference"

    Private _instanceMDI As System.Windows.Forms.Form
    ''' <summary>
    ''' Obtiene o asigna la instancia del formulario principal
    ''' </summary>
    ''' <value>Instancia del formulario principal</value>
    ''' <returns>La instancia del formulario principal</returns>
    Public Property InstanceMDI As System.Windows.Forms.Form
        Get
            Return Me._instanceMDI
        End Get
        Set(value As System.Windows.Forms.Form)
            Me._instanceMDI = value
        End Set
    End Property

#End Region

#Region "Connection Properties"

    Dim _excecutionType As ExcecutionSessionType
    ''' <summary>
    ''' Esta propiedad obtiene o establece el valor del servidor SQL Server
    ''' Provisional por el funcional de usuarios y roles
    ''' </summary>

    Property ExcecutionType As ExcecutionSessionType
        Get
            Return _excecutionType
        End Get
        Set(ByVal value As ExcecutionSessionType)
            _excecutionType = value
        End Set
    End Property


    Dim _uriWebServices As String
    ''' <summary>
    ''' Esta propiedad obtiene o establece el valor de la url del servicio web 
    ''' </summary>
    <DataMember>
    Property UriWebServices As String
        Get
            Return _uriWebServices
        End Get
        Set(ByVal value As String)
            _uriWebServices = value
        End Set
    End Property

    Dim _uriWebServicesXPO As String
    ''' <summary>
    ''' Esta propiedad obtiene o establece el valor de la url del servicio web de Entidades XPO
    ''' </summary>
    <DataMember>
    Property UriWebServicesXpo As String
        Get
            Return _uriWebServicesXPO
        End Get
        Set(ByVal value As String)
            _uriWebServicesXPO = value
        End Set
    End Property

    Dim _webServiceProtocol As Protocol = Protocol.Ninguno
    ''' <summary>
    ''' Esta Propiedad Determina el protocolo a usar para conectarse a web services.
    ''' </summary>
    ''' <value>el protocolo web services.</value>
    <DataMember>
    Public Property WebServiceProtocol As Protocol
        Get
            Return _webServiceProtocol
        End Get
        Set(ByVal value As Protocol)
            _webServiceProtocol = value
        End Set
    End Property

    Dim _webServiceProtocolXpo As Protocol = Protocol.Ninguno
    ''' <summary>
    ''' Esta Propiedad Determina el protocolo a usar para conectarse a web services de XPO.
    ''' </summary>
    ''' <value>el protocolo web services XPO.</value>
    <DataMember>
    Public Property WebServiceProtocolXpo As Protocol
        Get
            Return _webServiceProtocolXpo
        End Get
        Set(ByVal value As Protocol)
            _webServiceProtocolXpo = value
        End Set
    End Property

    Private _uriNotificationWebService As String
    ''' <summary>
    ''' Obtiene o asigna la URL de la dirección de los servicios web de notificación
    ''' </summary>
    ''' <value>URL de los servicios de notificación</value>
    ''' <returns>La URL de los servicios de notificación</returns>
    <DataMember()>
    Public Property UriNotificationWebService As String
        Get
            Return Me._uriNotificationWebService
        End Get
        Set(value As String)
            Me._uriNotificationWebService = value
        End Set
    End Property

    Private _notificationWebServiceProtocol As Protocol = Protocol.Ninguno
    ''' <summary>
    ''' Obtiene o asigna el protocolo usado por los servicios de notificación
    ''' </summary>
    ''' <value>Protocolo usado por los servicios de notificación</value>
    ''' <returns>El protocolo usado por los servicios de notificación</returns>
    <DataMember()>
    Public Property NotificationWebServiceProtocol As Protocol
        Get
            Return Me._notificationWebServiceProtocol
        End Get
        Set(value As Protocol)
            Me._notificationWebServiceProtocol = value
        End Set
    End Property

    Private _uriIndexingWebService As String
    ''' <summary>
    ''' Obtiene o asigna la URL de la dirección de los servicios web de indexación
    ''' </summary>
    ''' <value>URL de los servicios de indexación</value>
    ''' <returns>La URL de los servicios de indexación</returns>
    <DataMember()>
    Public Property UriIndexingWebService As String
        Get
            Return Me._uriIndexingWebService
        End Get
        Set(value As String)
            Me._uriIndexingWebService = value
        End Set
    End Property

    Private _indexingWebServiceProtocol As Protocol = Protocol.Ninguno
    ''' <summary>
    ''' Obtiene o asigna el protocolo usado por los servicios de indexación
    ''' </summary>
    ''' <value>Protocolo usado por los servicios de indexación</value>
    ''' <returns>El protocolo usado por los servicios de indexación</returns>
    <DataMember()>
    Public Property IndexingWebServiceProtocol As Protocol
        Get
            Return Me._indexingWebServiceProtocol
        End Get
        Set(value As Protocol)
            Me._indexingWebServiceProtocol = value
        End Set
    End Property

    Dim _uriWebSecurityServices As String
    ''' <summary>
    ''' Esta propiedad obtiene o establece el valor de la url del servicio web 
    ''' </summary>
    <DataMember>
    Property UriWebSecurityServices As String
        Get
            Return _uriWebSecurityServices
        End Get
        Set(ByVal value As String)
            _uriWebSecurityServices = value
        End Set
    End Property

    Dim _IdTimeZone As Integer
    ''' <summary>
    ''' Esta propiedad obtiene o establece el id de la zona horaria
    ''' </summary>
    <DataMember>
    Property IdTimeZone As Integer
        Get
            Return _IdTimeZone
        End Get
        Set(ByVal value As Integer)
            _IdTimeZone = value
        End Set
    End Property

    Dim _TimezoneName As String
    ''' <summary>
    ''' Esta propiedad obtiene o establece el nombre de la zona horaria
    ''' </summary>
    <DataMember>
    Property TimezoneName As String
        Get
            Return _TimezoneName
        End Get
        Set(ByVal value As String)
            _TimezoneName = value
        End Set
    End Property

    Dim _LanguageCulture As String
    ''' <summary>
    ''' Esta propiedad obtiene o establece el nombre de la cultura
    ''' </summary>
    <DataMember>
    Property LanguageCulture As String
        Get
            Return _LanguageCulture
        End Get
        Set(ByVal value As String)
            _LanguageCulture = value
        End Set
    End Property

    Dim _dateFormat As Integer
    ''' <summary>
    ''' Esta propiedad obtiene o establece el formato de dia
    ''' </summary>
    <DataMember>
    Property dateFormat As Integer
        Get
            Return _dateFormat
        End Get
        Set(ByVal value As Integer)
            _dateFormat = value
        End Set
    End Property

    Dim _timeFormat As Integer
    ''' <summary>
    ''' Esta propiedad obtiene o establece el formato de fecha
    ''' </summary>
    <DataMember>
    Property timeFormat As Integer
        Get
            Return _timeFormat
        End Get
        Set(ByVal value As Integer)
            _timeFormat = value
        End Set
    End Property
#End Region

#Region "Integration HIS"

    Private _intergrationHisStatus As IntegrationStatus = IntegrationStatus.NonIntegrated
    ''' <summary>
    ''' Obtiene o asigna el estado de integración con el sistema asistencial
    ''' </summary>
    ''' <value>Estado de integración con el sistema asistencial</value>
    ''' <returns>El estado de integración con el sistema asistencial</returns>
    Public Property IntergrationHisStatus As IntegrationStatus
        Get
            Return Me._intergrationHisStatus
        End Get
        Set(value As IntegrationStatus)
            Me._intergrationHisStatus = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el objeto que contiene la información
    ''' de la sesión en el sistema asistencial
    ''' </summary>
    ''' <value>Información de sesión HIS</value>
    ''' <returns>La información de la sesión HIS</returns>
    Public Property SessionHis As HisSessionValues

#End Region

#Region "Containers"

    Private _hisContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de Indigo Vie Cloud Platform
    ''' </summary>
    ''' <returns>Contenedor Indigo Vie Cloud Platform</returns>
    <DataMember()>
    Public Property HisContainer As String
        Get
            If Me._hisContainer Is Nothing Then
                Me._hisContainer = String.Empty
            End If
            Return Me._hisContainer
        End Get
        Set(value As String)
            Me._hisContainer = value
        End Set
    End Property

    Private _interopCostContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de la Interaccion con costos
    ''' </summary>
    ''' <returns>Contenedor Indigo Vie Cloud Platform</returns>
    <DataMember()>
    Public Property InteropCostContainer As String
        Get
            If Me._interopCostContainer Is Nothing Then
                Me._interopCostContainer = String.Empty
            End If
            Return Me._interopCostContainer
        End Get
        Set(value As String)
            Me._interopCostContainer = value
        End Set
    End Property

    Private _indigoConnectionString As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de la Interaccion con costos
    ''' </summary>
    ''' <returns>Contenedor Indigo Vie Cloud Platform</returns>
    <DataMember()>
    Public Property IndigoConnectionString As String
        Get
            If Me._indigoConnectionString Is Nothing Then
                Me._indigoConnectionString = String.Empty
            End If
            Return Me._indigoConnectionString
        End Get
        Set(value As String)
            Me._indigoConnectionString = value
        End Set
    End Property

    Private _securityContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de seguridad
    ''' </summary>
    ''' <returns>Contenedor Seguridad</returns>
    <DataMember()>
    Public Property SecurityContainer As String
        Get
            If Me._securityContainer Is Nothing Then
                Me._securityContainer = String.Empty
            End If
            Return Me._securityContainer
        End Get
        Set(value As String)
            Me._securityContainer = value
        End Set
    End Property

    Private _documentalContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor documental
    ''' </summary>
    ''' <returns>Contenedor Documental</returns>
    <DataMember()>
    Public Property DocumentalContainer As String
        Get
            If Me._documentalContainer Is Nothing Then
                Me._documentalContainer = String.Empty
            End If
            Return Me._documentalContainer
        End Get
        Set(value As String)
            Me._documentalContainer = value
        End Set
    End Property

    Private _transactionalContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor transaccional
    ''' </summary>
    ''' <returns>Contenedor Transaccional</returns>
    <DataMember()>
    Public Property TransactionalContainer As String
        Get
            If Me._transactionalContainer Is Nothing Then
                Me._transactionalContainer = String.Empty
            End If
            Return Me._transactionalContainer
        End Get
        Set(value As String)
            Me._transactionalContainer = value
        End Set
    End Property

    Private _vituelContainer As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de vituel
    ''' </summary>
    ''' <returns>Contenedor Vituel</returns>
    <DataMember()>
    Public Property VituelContainer As String
        Get
            If Me._vituelContainer Is Nothing Then
                Me._vituelContainer = String.Empty
            End If
            Return Me._vituelContainer
        End Get
        Set(value As String)
            Me._vituelContainer = value
        End Set
    End Property

    Private _transactionalFoundational As String
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor fundacional
    ''' </summary>
    ''' <returns>Contenedor Fundacional</returns>
    <DataMember()>
    Public Property FoundationalContainer As String
        Get
            If Me._transactionalFoundational Is Nothing Then
                Me._transactionalFoundational = String.Empty
            End If
            Return Me._transactionalFoundational
        End Get
        Set(value As String)
            Me._transactionalFoundational = value
        End Set
    End Property

    Private _ArchitectureType As Byte?
    ''' <summary>
    ''' Propiedad para almacenar el valor del contenedor de seguridad
    ''' </summary>
    ''' <returns>Contenedor Seguridad</returns>
    <DataMember()>
    Public Property ArchitectureType As Byte?
        Get
            If Me._ArchitectureType Is Nothing Then
                Me._ArchitectureType = 1
            End If
            Return Me._ArchitectureType
        End Get
        Set(value As Byte?)
            Me._ArchitectureType = value
        End Set
    End Property

    Private _decimalSeparator As String
    ''' <summary>
    ''' Separador decimal parametrizado en el contendor
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DecimalSeparator As String
        Get
            Return _decimalSeparator
        End Get
        Set(value As String)
            _decimalSeparator = value
        End Set
    End Property

    Private _cosmosDbContainer As New ThreadLocal(Of String)
    <DataMember()>
    Public Property CosmosDbContainer As String
        Get
            Return If(Me._cosmosDbContainer.Value, String.Empty)
        End Get
        Set(value As String)

            If Me._cosmosDbContainer Is Nothing Then
                Me._cosmosDbContainer = New ThreadLocal(Of String)
            End If

            Me._cosmosDbContainer.Value = value
        End Set
    End Property

    Private _cosmosDB As New ThreadLocal(Of String)
    <DataMember()>
    Public Property CosmosDB As String
        Get
            Return If(Me._cosmosDB?.Value, String.Empty)
        End Get
        Set(value As String)

            If Me._cosmosDB Is Nothing Then
                Me._cosmosDB = New ThreadLocal(Of String)
            End If

            Me._cosmosDB.Value = value
        End Set
    End Property
#End Region

#Region "Notification's Credentials"

    Private _notificationUser As String
    ''' <summary>
    ''' Obtiene o asigna el usuario con quien se realiza la
    ''' autenticación en los servicios de notificación
    ''' </summary>
    ''' <value>Nombre de usuario</value>
    ''' <returns>El nombre de usuario</returns>
    Public Property NotificationUser As String
        Get
            Return Me._notificationUser
        End Get
        Set(value As String)
            Me._notificationUser = value
        End Set
    End Property

    Private _notificationPasswd As String
    ''' <summary>
    ''' Obtiene o asigna la contraseña del usuario con que se realiza la
    ''' autenticación en los servicios de notificación
    ''' </summary>
    ''' <value>Contraseña de usuario</value>
    ''' <returns>La contraseña de usuario</returns>
    Public Property NotificationPasswd As String
        Get
            Return Me._notificationPasswd
        End Get
        Set(value As String)
            Me._notificationPasswd = value
        End Set
    End Property

#End Region

#Region "Documental System"

    Private _uriServerDocumentalSystem As String
    ''' <summary>
    ''' Url del servidor donde se aloja la base de datos
    ''' para el sistema documental
    ''' </summary>
    ''' <value>Url Servidor Sistema Documental</value>
    ''' <returns>Url</returns>
    <DataMember>
    Public Property UriServerDocumentalSystem As String
        Get
            Return Me._uriServerDocumentalSystem
        End Get
        Set(value As String)
            Me._uriServerDocumentalSystem = value
        End Set
    End Property


    Private _documentalSystemWebServiceProtocol As Protocol = Protocol.Ninguno
    ''' <summary>
    ''' Obtiene o asigna el protocolo usado por los servicios del sistema documental
    ''' </summary>
    ''' <value>Protocolo usado por los servicios del sistema documental</value>
    ''' <returns>El protocolo usado por los servicios del sistema documental</returns>
    <DataMember()>
    Public Property DocumentalSystemWebServiceProtocol As Protocol
        Get
            Return Me._documentalSystemWebServiceProtocol
        End Get
        Set(value As Protocol)
            Me._documentalSystemWebServiceProtocol = value
        End Set
    End Property


    Private _dataBaseDocumentalSystem As String
    ''' <summary>
    ''' Nombre de la base de datos que aloja los 
    ''' datos del sistema documental
    ''' </summary>
    ''' <value>Nombre base de datos Sistema Documental</value>
    ''' <returns>Bd</returns>
    <DataMember>
    Public Property DataBaseDocumentalSystem As String
        Get
            Return Me._dataBaseDocumentalSystem
        End Get
        Set(value As String)
            Me._dataBaseDocumentalSystem = value
        End Set
    End Property

    Private _userDocumentalSystem As String
    ''' <summary>
    ''' Usuario de sql para la base de datos del sistema 
    ''' documental
    ''' </summary>
    ''' <value>Nombre Usuario SQL Sistema Documental</value>
    ''' <returns>Nombre Usuario</returns>
    <DataMember>
    Public Property UserDocumentalSystem As String
        Get
            Return Me._dataBaseDocumentalSystem
        End Get
        Set(value As String)
            Me._dataBaseDocumentalSystem = value
        End Set
    End Property

    Private _pwdDocumentalSystem As String
    ''' <summary>
    ''' Password de sql para la base de datos del sistema 
    ''' documental
    ''' </summary>
    ''' <value>Password base de datos Sistema Documental</value>
    ''' <returns>Password</returns>
    <DataMember>
    Public Property PwdDocumentalSystem As String
        Get
            Return Me._pwdDocumentalSystem
        End Get
        Set(value As String)
            Me._pwdDocumentalSystem = value
        End Set
    End Property

#End Region

#Region "Company Properties"

    Private _indigoCompany As String
    ''' <summary>
    ''' Esta propiedad contiene el Codigo de la empresa Indigo  a la cual esta conectado el usuario.
    ''' </summary>
    ''' <value></value>
    <DataMember>
    Property IndigoCompany As String
        Get
            Return _indigoCompany
        End Get
        Set(ByVal value As String)
            _indigoCompany = value
        End Set
    End Property

    Private _indigoCompanyName As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la empresa Indigo  a la cual esta conectado el usuario.
    ''' </summary>
    ''' <value></value>
    <DataMember>
    Property IndigoCompanyName As String
        Get
            Return _indigoCompanyName
        End Get
        Set(ByVal value As String)
            _indigoCompanyName = value
        End Set
    End Property

    Private _indigoCompanyNit As String
    ''' <summary>
    '''  Esta propiedad contiene el nit de la empresa a la cual esta conectado el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoCompanyNit As String
        Get
            Return _indigoCompanyNit
        End Get
        Set(ByVal value As String)
            _indigoCompanyNit = value
        End Set
    End Property

    Private _indigoVerificationDigitNit As String
    ''' <summary>
    '''  Esta propiedad contiene el digito de verificación de la empresa a la cual esta conectado el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoVerificationDigitNit As String
        Get
            Return _indigoVerificationDigitNit
        End Get
        Set(ByVal value As String)
            _indigoVerificationDigitNit = value
        End Set
    End Property

    Private _indigoCompanyAddress As String
    ''' <summary>
    '''  Esta propiedad contiene la dirección de la empresa a la cual esta conectado el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoCompanyAddress As String
        Get
            Return _indigoCompanyAddress
        End Get
        Set(ByVal value As String)
            _indigoCompanyAddress = value
        End Set
    End Property

    Private _indigoCompanyPhoneNumber As String
    ''' <summary>
    '''  Esta propiedad contiene el teléfono de la empresa a la cual esta conectado el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoCompanyPhoneNumber As String
        Get
            Return _indigoCompanyPhoneNumber
        End Get
        Set(ByVal value As String)
            _indigoCompanyPhoneNumber = value
        End Set
    End Property


    Private _indigoContainerId As Integer
    ''' <summary>
    ''' Esta propiedad contiene el id del cotenedor actual
    ''' </summary>
    ''' <value></value>
    <DataMember>
    Property IndigoContainerId As Integer
        Get
            Return _indigoContainerId
        End Get
        Set(ByVal value As Integer)
            _indigoContainerId = value
        End Set
    End Property

    Private _indigoOperatingUnitId As Integer
    ''' <summary>
    ''' Esta propiedad contiene el id de la unidad operativa por defecto
    ''' </summary>
    ''' <value></value>
    <DataMember>
    Property IndigoOperatingUnitId As Integer
        Get
            Return _indigoOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            _indigoOperatingUnitId = value
        End Set
    End Property


    Private _indigoCompanyLyncIntegration As Boolean
    ''' <summary>
    '''  Esta propiedad contiene si la empresa actual esta integrada con lync
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoCompanyLyncIntegration As Boolean
        Get
            Return _indigoCompanyLyncIntegration
        End Get
        Set(ByVal value As Boolean)
            _indigoCompanyLyncIntegration = value
        End Set
    End Property

    Private _indigoCompanyType As Integer
    ''' <summary>
    '''  Esta propiedad Especifica el tipo de compañia (1 - Privada 2 - Publica)
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoCompanyType As Integer
        Get
            Return _indigoCompanyType
        End Get
        Set(ByVal value As Integer)
            _indigoCompanyType = value
        End Set
    End Property

    Private _indigoGlossesIntegration As Integer
    ''' <summary>
    '''  Esta propiedad Especifica el tipo de integracion de glosas (1 - Integracion con Genesis 2 - Integracion con DGH)
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoGlossesIntegration As Integer
        Get
            Return _indigoGlossesIntegration
        End Get
        Set(ByVal value As Integer)
            _indigoGlossesIntegration = value
        End Set
    End Property

    Private _indigoPayrollIntegration As Integer
    ''' <summary>
    '''  Esta propiedad Especifica el tipo de integracion de nomina (1 - Integracion con Genesis 2 - Integracion con DGH)
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoPayrollIntegration As Integer
        Get
            Return _indigoPayrollIntegration
        End Get
        Set(ByVal value As Integer)
            _indigoPayrollIntegration = value
        End Set
    End Property

    Private _indigoHumanTalentIntegration As Integer
    ''' <summary>
    '''  Esta propiedad Especifica el tipo de integracion de Talento Humano (1 - Integracion con Vie 2 - No se Integra)
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoHumanTalentIntegration As Integer
        Get
            Return _indigoHumanTalentIntegration
        End Get
        Set(ByVal value As Integer)
            _indigoHumanTalentIntegration = value
        End Set
    End Property

    Private _indigoDispensingIntegration As Integer
    ''' <summary>
    '''  Esta propiedad Especifica si existe integracion de dispensación (1 - Integracion Nativa 2- Integracion con Heon)
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property IndigoDispensingIntegration As Integer
        Get
            Return _indigoDispensingIntegration
        End Get
        Set(ByVal value As Integer)
            _indigoDispensingIntegration = value
        End Set
    End Property

    Private _productionCompany As Boolean
    ''' <summary>
    '''  Esta propiedad Especifica si el contenedor es de produccion
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember>
    Property ProductionCompany As Boolean
        Get
            Return _productionCompany
        End Get
        Set(ByVal value As Boolean)
            _productionCompany = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el clientId
    ''' </summary>
    Private _clientId As String
    <DataMember>
    Property ClientId As String
        Get
            Return _clientId
        End Get
        Set(value As String)
            _clientId = value
        End Set
    End Property
#End Region

#Region "User Properties"
    Dim _AppHandle As System.IntPtr
    ''' <summary>
    ''' Esta propiedad contiene el handle de la aplicacion
    ''' </summary>
    <DataMember>
    Property AppHandle As System.IntPtr
        Get
            Return _AppHandle
        End Get
        Set(value As System.IntPtr)
            _AppHandle = value
        End Set
    End Property

    Private _LoadFormControls As Boolean
    ''' <summary>
    ''' Propiedad para obtiene o establecer si el formulario de controles ya se ha cargado
    ''' </summary>
    Public Property LoadFormControls As Boolean
        Get
            Return _LoadFormControls
        End Get
        Set(value As Boolean)
            _LoadFormControls = value
        End Set
    End Property

    Dim _UserPersonalNote As String
    ''' <summary>
    ''' Esta propiedad contiene la nota personal del usuario
    ''' </summary>
    ''' <value>La nota personal.</value>
    <DataMember>
    Property UserPersonalNote As String
        Get
            Return _UserPersonalNote
        End Get
        Set(value As String)
            _UserPersonalNote = value
        End Set
    End Property

    Dim _userRol As String
    ''' <summary>
    ''' Esta propiedad contiene el rol del usuario conectado.
    ''' </summary>
    ''' <value>Rol usuario.</value>
    <DataMember>
    Property UserRol As String
        Get
            Return IIf(_userRol Is Nothing, String.Empty, _userRol)
        End Get
        Set(ByVal value As String)
            _userRol = value
        End Set
    End Property

    Dim _userGroup As String
    ''' <summary>
    ''' Esta propiedad contiene el grupo del usuario que esta conectado.
    ''' </summary>
    <DataMember>
    Property UserGroup As String
        Get
            Return IIf(_userGroup Is Nothing, String.Empty, _userGroup)
        End Get
        Set(ByVal value As String)
            _userGroup = value
        End Set
    End Property

    Dim _userIndigo As String
    ''' <summary>
    ''' Esta propiedad contiene el usuario indigo que esta conectado.
    ''' </summary>
    <DataMember>
    Property UserIndigo As String
        Get
            Return IIf(_userIndigo Is Nothing, String.Empty, _userIndigo)
        End Get
        Set(ByVal value As String)
            _userIndigo = value
        End Set
    End Property

    Dim _AutoPersona As String
    ''' <summary>
    ''' Esta propiedad contiene el autonumerico de la persona
    ''' </summary>
    <DataMember>
    Property AutoPersona As String
        Get
            Return IIf(_AutoPersona Is Nothing, String.Empty, _AutoPersona)
        End Get
        Set(ByVal value As String)
            _AutoPersona = value
        End Set
    End Property

    Dim _userIndigoId As Integer
    ''' <summary>
    ''' Esta propiedad contiene el Id del usuario indigo que esta conectado.
    ''' </summary>
    <DataMember>
    Property UserIndigoId As Integer
        Get
            Return _userIndigoId
        End Get
        Set(ByVal value As Integer)
            _userIndigoId = value
        End Set
    End Property

    Private _userType As UserType?
    ''' <summary>
    ''' Esta propiedad contiene el tipo de administrador
    ''' </summary>
    <DataMember>
    Property UserType As UserType
        Get
            If _userType Is Nothing Then
                Return UserType.StandardUser
            Else
                Return _userType
            End If
        End Get
        Set(ByVal value As UserType)
            _userType = value
        End Set
    End Property

    Private _profileType As eProfileType?
    ''' <summary>
    ''' Esta propiedad contiene el tipo de perfil
    ''' </summary>
    <DataMember>
    Property ProfileType As eProfileType
        Get
            If _profileType Is Nothing Then
                Return eProfileType.Administrative
            Else
                Return _profileType
            End If

        End Get
        Set(ByVal value As eProfileType)
            _profileType = value
        End Set
    End Property

    Dim _userIndigoName As String
    ''' <summary>
    ''' Esta propiedad contiene el usuario indigo que esta conectado.
    ''' </summary>
    <DataMember>
    Property UserIndigoName As String
        Get
            Return IIf(_userIndigoName Is Nothing, String.Empty, _userIndigoName)
        End Get
        Set(ByVal value As String)
            _userIndigoName = value
        End Set
    End Property

    Dim _userEmail As String
    ''' <summary>
    ''' Esta propiedad la direccion de Correo Electronico del usuario
    ''' </summary>
    <DataMember>
    Property UserEmail As String
        Get
            Return IIf(_userEmail Is Nothing, String.Empty, _userEmail)
        End Get
        Set(ByVal value As String)
            _userEmail = value
        End Set
    End Property


    Dim _userCharge As String
    ''' <summary>
    ''' Esta propiedad contiene cargo del usuario
    ''' </summary>
    <DataMember>
    Property UserCharge As String
        Get
            Return IIf(_userCharge Is Nothing, String.Empty, _userCharge)
        End Get
        Set(ByVal value As String)
            _userCharge = value
        End Set
    End Property

    Dim _userImage As Byte()
    ''' <summary>
    ''' Esta propiedad contiene la foto del usuario
    ''' </summary>
    <IgnoreDataMember>
    Property UserImage As Byte()
        Get
            Return _userImage
        End Get
        Set(ByVal value As Byte())
            _userImage = value
        End Set
    End Property

    Dim _UserViewMode As Boolean
    ''' <summary>
    ''' Esta propiedad identifica si el usuario esta en modo edicion o busqueda False=Edicion True=Busqueda
    ''' </summary>
    <IgnoreDataMember>
    Property UserViewMode As Boolean
        Get
            Return _UserViewMode
        End Get
        Set(ByVal value As Boolean)
            _UserViewMode = value
        End Set
    End Property

    Dim _userInterface As String
    ''' <summary>
    ''' Esta propiedad indica el código del usuario usado para la interfaz
    ''' </summary>
    <DataMember>
    Property UserInterface As String
        Get
            Return _userInterface
        End Get
        Set(ByVal value As String)
            _userInterface = value
        End Set
    End Property


    Dim _IndigoContainer As String
    ''' <summary>
    ''' Esta propiedad indica si el contenedor de genesis
    ''' </summary>
    <DataMember>
    Property IndigoContainer As String
        Get
            Return IIf(_IndigoContainer Is Nothing, String.Empty, _IndigoContainer)
        End Get
        Set(ByVal value As String)
            _IndigoContainer = value
        End Set
    End Property

    Dim _EndPoints As List(Of Endpoints)

    <DataMember>
    Property EndPoints As List(Of Endpoints)
        Get
            Return _EndPoints
        End Get
        Set(value As List(Of Endpoints))
            _EndPoints = value
        End Set
    End Property

    Dim _HostName As String
    ''' <summary>
    ''' Esta propiedad almacena el host del usuario conectado.
    ''' </summary>
    <DataMember>
    Property HostName As String
        Get
            Return IIf(_HostName Is Nothing, String.Empty, _HostName)
        End Get
        Set(ByVal value As String)
            _HostName = value
        End Set
    End Property

    Dim _NetworkIP As String
    ''' <summary>
    ''' Esta propiedad almacena la dirección IP de la red del usuario conectado.
    ''' </summary>
    <DataMember>
    Property NetworkIP As String
        Get
            Return IIf(_NetworkIP Is Nothing, String.Empty, _NetworkIP)
        End Get
        Set(ByVal value As String)
            _NetworkIP = value
        End Set
    End Property

#End Region

#Region "Permission Properties"

    'Dim _permissions As IEnumerable(Of Object)
    ' ''' <summary>
    ' ''' Esta propiedad contiene un listado de los permisos del rol
    ' ''' </summary>
    ' ''' <value>Rol usuario.</value>
    '<DataMember>
    'Property Permissions As IEnumerable(Of Object)
    '    Get
    '        Return _permissions
    '    End Get
    '    Set(ByVal value As IEnumerable(Of Object))
    '        _permissions = value
    '    End Set
    'End Property

    Private _ListOperatingUnitPermission As IEnumerable(Of Object)
    Property ListOperatingUnitPermission As IEnumerable(Of Object)
        Get
            If _ListOperatingUnitPermission Is Nothing Then
                _ListOperatingUnitPermission = New List(Of Object)
            End If
            Return _ListOperatingUnitPermission
        End Get
        Set(value As IEnumerable(Of Object))
            _ListOperatingUnitPermission = value
        End Set
    End Property

    'Dim _activeForms As Dictionary(Of String, Tuple(Of String, String, String, List(Of String), Boolean, Boolean))
    ' ''' <summary>
    ' ''' Esta propiedad contiene un listado de los formularios activos
    ' ''' </summary>
    '<IgnoreDataMember>
    'Property ActiveForms As Dictionary(Of String, Tuple(Of String, String, String, List(Of String), Boolean, Boolean))
    '    Get
    '        Return _activeForms
    '    End Get
    '    Set(ByVal value As Dictionary(Of String, Tuple(Of String, String, String, List(Of String), Boolean, Boolean)))
    '        _activeForms = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de Ids de formularios visibles para el usuario
    ''' </summary>
    ''' <value>Ids de formularios visibles</value>
    ''' <returns>Los Ids de los formularios visibles</returns>
    <IgnoreDataMember()>
    Public Property ListProductCatalog As List(Of ProductCatalog)

    ''' <summary>
    ''' Propiedad para almacenar los formularios a los cuales tiene permiso el usuario logeado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListFormPermission As List(Of VieForm)

#End Region

#Region "CurrencyProperties"
    Dim _officialCurrencyId As Integer
    ''' <summary>
    ''' Propiedad que Guarda o establece el Id de la moneda Official
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property OfficialCurrencyId As Integer
        Get
            Return _officialCurrencyId
        End Get
        Set(value As Integer)
            _officialCurrencyId = value
        End Set
    End Property

    Dim _currencyISO4217 As String
    ''' <summary>
    ''' establece o guarda la abreviacion standart de la moneda Oficial del sistema
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CurrencyISO4217 As String
        Get
            Return _currencyISO4217
        End Get
        Set(value As String)
            _currencyISO4217 = value
        End Set
    End Property

    Dim _currencyName As String
    ''' <summary>
    ''' establece o guarda el nombre segun iso de la moneda Oficial del sistema
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CurrencyName As String
        Get
            Return _currencyName
        End Get
        Set(value As String)
            _currencyName = value
        End Set
    End Property

    ''' <summary>
    ''' Propieda que retorna el formato de la moneda de la cultura 
    ''' si la abreviacion de la moneda oficial no esta vacia retorna el formato de la moneda oficial
    ''' sino envia la que se halla establecido o la del hilo 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrencyNumbertFormat As NumberFormatInfo
        Get
            If String.IsNullOrEmpty(CurrencyISO4217) Then
                Return ConfigurationSeparatorNumberFormat(If(Culture.NumberFormat Is Nothing, CultureInfo.CurrentCulture.NumberFormat, Culture.NumberFormat))
            End If

            Return ConfigurationSeparatorNumberFormat(New CultureInfo(CurrencyISO4217.GetCultureId).NumberFormat)
        End Get
    End Property

    ''' <summary>
    ''' devuelve la configuracion de la moneda estandart (el separador decimal y separador de miles)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ConfigurationSeparatorNumberFormat(Optional numberFormat As NumberFormatInfo = Nothing) As NumberFormatInfo
        Get
            Dim configuration = New NumberFormatInfo
            If numberFormat IsNot Nothing Then
                configuration = numberFormat
            End If

            If String.IsNullOrEmpty(Me.DecimalSeparator) Then
                Return configuration
            End If

            Dim dSeparator As String = Me.DecimalSeparator
            Dim gSeparator As String = If(Me.DecimalSeparator = ",", ".", ",")

            With configuration
                .CurrencyDecimalSeparator = dSeparator
                .CurrencyGroupSeparator = gSeparator
                .NumberDecimalSeparator = dSeparator
                .NumberGroupSeparator = gSeparator
            End With
            Return configuration
        End Get
    End Property

#End Region

#Region "Cache and Paths"

    Dim _localCache As Boolean
    ''' <summary>
    ''' Esta propiedad especifica si se hace cache local o no del lado del cliente
    ''' </summary>
    <DataMember>
    Property LocalCache As Boolean
        Get
            Return _localCache
        End Get
        Set(ByVal value As Boolean)
            _localCache = value
        End Set
    End Property

    Dim _localReportsPath As String
    ''' <summary>
    ''' Esta propiedad contiene la ruta local de los reportes para el lado del cliente
    ''' </summary>
    <DataMember>
    Property LocalReportsPath As String
        Get
            Return _localReportsPath
        End Get
        Set(ByVal value As String)
            _localReportsPath = value
        End Set
    End Property

    Dim _serverReportsPath As String
    ''' <summary>
    ''' Esta propiedad contiene la ruta del servidor de los reportes.
    ''' </summary>
    <DataMember>
    Property ServerReportsPath As String
        Get
            Return _serverReportsPath
        End Get
        Set(ByVal value As String)
            _serverReportsPath = value
        End Set
    End Property

    Dim _updatePath As String
    ''' <summary>
    ''' Esta propiedad contiene la ruta del servidor de los reportes.
    ''' </summary>
    <DataMember>
    Property UpdatePath As String
        Get
            Return _updatePath
        End Get
        Set(ByVal value As String)
            _updatePath = value
        End Set
    End Property

    Dim _commonFilesPath As String
    ''' <summary>
    ''' Esta propiedad contiene la ruta de almacenamiento de archivos en la carpeta de Usuarios Comunes.
    ''' </summary>
    <DataMember>
    Property CommonFilesPath As String
        Get
            Return _commonFilesPath
        End Get
        Set(ByVal value As String)
            _commonFilesPath = value
        End Set
    End Property

    Dim _userFilesPath As String
    ''' <summary>
    ''' Esta propiedad contiene la ruta de almacenamiento de archivos en la carpeta por usuario.
    ''' </summary>
    <DataMember>
    Property UserFilesPath As String
        Get
            Return _userFilesPath
        End Get
        Set(ByVal value As String)
            _userFilesPath = value
        End Set
    End Property

    Dim _IndigoVersion As String
    ''' <summary>
    ''' Esta propiedad contiene la version de software
    ''' </summary>
    <DataMember>
    Property IndigoVersion As String
        Get
            Return _IndigoVersion
        End Get
        Set(ByVal value As String)
            _IndigoVersion = value
        End Set
    End Property

    Dim _StartSelection As String
    ''' <summary>
    ''' Esta propiedad contiene el menu seleccionado en el start
    ''' </summary>
    <DataMember>
    Property StartSelection As String
        Get
            Return _StartSelection
        End Get
        Set(ByVal value As String)
            _StartSelection = value
        End Set
    End Property

    Dim _ModuleNameGroup As String
    ''' <summary>
    ''' Esta propiedad contiene el modulo seleccionado
    ''' </summary>
    <DataMember>
    Property ModuleNameGroup As String
        Get
            Return _ModuleNameGroup
        End Get
        Set(ByVal value As String)
            _ModuleNameGroup = value
        End Set
    End Property

    Dim _ModuleValueGroup As Integer
    ''' <summary>
    ''' Esta propiedad contiene el editvalue del modulo seleccionado
    ''' </summary>
    <DataMember>
    Property ModuleValueGroup As Integer
        Get
            Return _ModuleValueGroup
        End Get
        Set(ByVal value As Integer)
            _ModuleValueGroup = value
        End Set
    End Property

    Dim _ModuleName As String
    ''' <summary>
    ''' Esta propiedad contiene el modulo seleccionado
    ''' </summary>
    <DataMember>
    Property ModuleName As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property

    Dim _ModuleTag As String
    ''' <summary>
    ''' Esta propiedad contiene el tag del modulo seleccionado
    ''' </summary>
    <DataMember>
    Property ModuleTag As String
        Get
            Return _ModuleTag
        End Get
        Set(ByVal value As String)
            _ModuleTag = value
        End Set
    End Property
#End Region

#Region "WCF Message"

    Dim _auditMessageWcf As AuditMessage
    ''' <summary>
    ''' Esta propiedad especifica el contenido del mensaje de WCF para el tema  de auditoria
    ''' </summary>
    <DataMember>
    Property AuditMessageWcf As AuditMessage
        Get
            Return _auditMessageWcf
        End Get
        Set(ByVal value As AuditMessage)
            _auditMessageWcf = value
        End Set
    End Property

#End Region

#Region "Notification Suscriptor"
    Private _ObserversList As List(Of IObservador)
    Public Property ObserversList As List(Of IObservador)
        Get
            If _ObserversList IsNot Nothing Then
                Return _ObserversList
            Else
                _ObserversList = New List(Of IObservador)
                Return _ObserversList
            End If
        End Get
        Set(value As List(Of IObservador))
            _ObserversList = value
        End Set
    End Property

    Public Property Handle As IntPtr


#End Region

#Region "Tenant"
    Private _tenantId As Short

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property TenantId As Short
        Get
            Return Me._tenantId
        End Get
        Set(value As Short)
            Me._tenantId = value
        End Set
    End Property

    Private _ServiceConfigurationId As Byte
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ServiceConfigurationId() As Byte
        Get
            Return _ServiceConfigurationId
        End Get
        Set(ByVal value As Byte)
            _ServiceConfigurationId = value
        End Set
    End Property
#End Region

    Public Function IsAllowPermissionForm(tag As String) As Boolean
        If ListProductCatalog Is Nothing OrElse Not ListProductCatalog.Any Then
            Return False
        End If
        Dim Exists = (From pc In Me.ListProductCatalog
                      From m In pc.ListModules
                      From f In m.ListForm
                      Where f.IdForm = tag Select f).Any()

        Return Exists
    End Function

    Public Function GetEndpointByCode(code As String) As Endpoints
        If Me._EndPoints Is Nothing OrElse Me._EndPoints.Count = 0 Then
            Return Nothing
        End If

        Return (From e In Me._EndPoints Where e.Code = code Select e).FirstOrDefault()
    End Function

    Public Function GetEndpointsAsDic() As Dictionary(Of String, String)
        Dim dicEndpoints = New Dictionary(Of String, String)()

        For Each endpoint In Me.EndPoints
            dicEndpoints.Add(endpoint.Code, endpoint.UrlBase)
        Next

        Return dicEndpoints
    End Function

End Class
