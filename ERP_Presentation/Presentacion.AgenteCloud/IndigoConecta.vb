
#Region "Librerias Importadas"

Imports System.IO
Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports IndigoSingleton

#End Region

Public NotInheritable Class IndigoConecta

#Region "Variables"

    ''' <summary>
    ''' uri donde estan localizado los servicios wcf
    ''' </summary>
    Dim URIServicios As String

    ''' <summary>
    ''' protocolo utilizado para los servicios wcf
    ''' </summary>
    Dim ProtocolServices As Protocol

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session de Historias Clinicas
    ''' </summary>
    Dim IndigoHis As IndigoValoresSesion = IndigoValoresSesion.Instancia
#End Region

#Region "Singleton"
    Shared m_instance As IndigoConecta

    ''' <summary>
    ''' Obtiene la Instancia singleton de CloudFactory
    ''' </summary>
    Public Shared ReadOnly Property Instancia() As IndigoConecta
        Get
            If m_instance Is Nothing Then
                m_instance = New IndigoConecta
            End If
            Return m_instance
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la Instancia singleton de CloudFactory
    ''' </summary>
    Public Shared ReadOnly Property InstanciaDefault() As IndigoConecta
        Get
            If m_instance Is Nothing OrElse (m_instance.CurrentCloud IsNot Nothing AndAlso m_instance.CurrentCloud.IndigoSeguridadDefault Is Nothing) Then
                m_instance = New IndigoConecta(True)
            End If
            Return m_instance
        End Get
    End Property

#End Region

#Region "Propiedades"

    Private _CurrentCloud As ICloud

    ''' <summary>
    ''' Obtiene el ICloud actual de <see cref="ICloud" />
    ''' </summary>
    Public ReadOnly Property CurrentCloud() As ICloud
        Get
            Return _CurrentCloud
        End Get
    End Property

#End Region

#Region "Constructor"

    Private Sub New()
        LoadConfiguration()
        If Indigo.UriWebServices.Length = 0 Then
            Throw New ArgumentNullException("Indigo.UriWebServices", "No Puede ser Vacio")
        End If
        If Indigo.WebServiceProtocol = Protocol.Ninguno Then
            Throw New InvalidOperationException("Indigo.WebServiceProtocol No Valido!")
        End If
        _CurrentCloud = New IndigoConect
    End Sub
    Private Sub New(ByVal _default As Boolean)
        Indigo.UriWebSecurityServices = ConfigurationFile.Instance.UrlWebSecurityServer
        _CurrentCloud = New IndigoConect(True)
    End Sub
    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub LoadConfiguration()

        'cargo la URL EntityFramework
        Indigo.UriWebServices = ConfigurationFile.Instance.UrlWebServer
        'cargo el protocolo Entity
        Indigo.WebServiceProtocol = ConfigurationFile.Instance.ProtocolUrlWebServer

        'cargo la URL XPO
        Indigo.UriWebServicesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo XPO
        Indigo.WebServiceProtocolXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer

        'cargo la URL de los servicios del sistema documental
        Indigo.UriServerDocumentalSystem = ConfigurationFile.Instance.UrlDocumentalSystemWebServer
        'cargo el protocolo del sistema documental
        Indigo.DocumentalSystemWebServiceProtocol = ConfigurationFile.Instance.ProtocolUrlDocumentalSystemWebServer

        'Cargo la URL Indexación
        Indigo.UriIndexingWebService = ConfigurationFile.Instance.UrlIndexingWebServer
        'Cargo el protocolo del servicio de indexación
        Indigo.IndexingWebServiceProtocol = ConfigurationFile.Instance.ProtocolUrlIndexingWebServer

        'Cargamos la uri de los servicios de notificación -- Obsoleto
        Indigo.UriNotificationWebService = ConfigurationFile.Instance.UrlNotificationWebServer

        'Cargo la empresa que tiene por defecto el archivo de configuracion
        Indigo.IndigoContainer = ConfigurationFile.Instance.DefaultCompanyContainerCode

        Indigo.UriWebSecurityServices = ConfigurationFile.Instance.UrlWebSecurityServer
    End Sub

    ''' <summary>
    ''' Elimina la instancia de la sigleton
    ''' </summary>
    Public Shared Sub Reset()
        m_instance = Nothing
    End Sub

#End Region

End Class
