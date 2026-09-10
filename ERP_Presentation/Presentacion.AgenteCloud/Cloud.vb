'***********************************************************************
' Assembly         : Presentation.CloudAgent.Cloud
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : WalterSierra
' Last Modified On : 12-03-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Liberias Importadas"

Imports Presentation.CloudAgent.IndigoReference
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.CloudAgent.IndigoReference.Glosas
''Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports System.Data
Imports System.IO
Imports Presentation.CloudAgent.IndigoReference.Common


#End Region

''' <summary>
''' Proxy WCF, contiene una propiedad por cada modulo
''' </summary>
Public Class Cloud

#Region "Variables"

    ''' <summary>
    ''' uri donde estan localizado los servicios wcf
    ''' </summary>
    Dim URIServices As String

    ''' <summary>
    ''' protocolo utilizado para los servicios wcf
    ''' </summary>
    Dim ProtocolServices As Protocol

    ''' <summary>
    ''' nombre del archivo de configuracion
    ''' </summary>
    Public Const FileName As String = "Configuracion.IndigoCrystal"
    ''' <summary>
    ''' esta variable es la encargada de contener los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region



#Region "Propiedades"

    Dim _SecurityDefaultServices As Presentation.CloudAgent.IndigoReference.SecurityDefault.SecurityServiceClient

    ''' <summary>
    ''' Propiedad para el manejo de los servicios de seguridad.
    ''' </summary>
    ''' <value>El servicio seguridad.</value>
    Public ReadOnly Property SecurityDefaultServices As Presentation.CloudAgent.IndigoReference.SecurityDefault.SecurityServiceClient
        Get
            Return _SecurityDefaultServices
        End Get
    End Property

    Dim _SecurityServices As SecurityServiceClient

    ''' <summary>
    ''' Propiedad para el manejo de los servicios de seguridad.
    ''' </summary>
    ''' <value>El servicio seguridad.</value>
    Public ReadOnly Property SecurityServices As SecurityServiceClient
        Get
            Return _SecurityServices
        End Get
    End Property

    Dim _SecurityCommons As CommonServiceClient
    ''' <summary>
    ''' Propiedad para el manejo de los servicios comunes
    ''' </summary>
    ''' <value>El servicio de comunes.</value>
    Public ReadOnly Property SecurityCommons As CommonServiceClient
        Get
            Return _SecurityCommons
        End Get
    End Property

    Dim _GlosasServices As GlosasServiceClient
    ''' <summary>
    ''' Propiedad para el manejo de los servicios de glosas
    ''' </summary>
    ''' <value>El servicio de comunes.</value>
    Public ReadOnly Property GlosasServices As GlosasServiceClient
        Get
            Return _GlosasServices
        End Get
    End Property


#End Region

#Region "Constructores"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="Cloud" />.
    ''' </summary>
    Public Sub New()
        'cargo el archivo de configuracion
        'inicializo instancias de cada uno de los servicios
        Indigo.UriWebServices = "http://localhost:7055/"
        _SecurityCommons = New CommonServiceClient(GetEndPoint(ProtocolServices, eServicios.Common), GetRemoteAddress(ProtocolServices, eServicios.Common))
        _SecurityServices = New SecurityServiceClient(GetEndPoint(ProtocolServices, eServicios.Security), GetRemoteAddress(ProtocolServices, eServicios.Security))
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="Cloud" />.
    ''' </summary>
    ''' <param name="Protocol">el protocolo, se deja esta sobrecarga para los modulos de pruebas unitarias</param>
    Public Sub New(Protocol As Protocol)
        If Protocol = Protocol.Ninguno Then
            Throw New Exception("Protocolo de Comunicaciones No Valido!")
        End If
        'cargo el archivo de configuracion
        'inicializo instancias de cada uno de los servicios
        _SecurityCommons = New CommonServiceClient(GetEndPoint(Protocol, eServicios.Common), GetRemoteAddress(Protocol, eServicios.Common))
        _SecurityServices = New SecurityServiceClient(GetEndPoint(Protocol, eServicios.Security), GetRemoteAddress(Protocol, eServicios.Security))
    End Sub
#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocol">el protocolo.</param>
    ''' <param name="Service">El servicio a utilizar</param>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint(Protocol As Protocol, Service As eServicios) As String
        Return String.Format("{0}_Endpoint_{1}", [Enum].GetName(GetType(Protocol), Protocol), [Enum].GetName(GetType(eServicios), Service))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocol">el protocolo.</param>
    ''' <param name="Service">El servicio a utilizar</param>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress(Protocol As Protocol, Service As eServicios) As String
        Return String.Format("{0}{1}.svc/{2}{3}", Indigo.UriWebServices, [Enum].GetName(GetType(eServicios), Service), [Enum].GetName(GetType(Protocol), Protocol), [Enum].GetName(GetType(eServicios), Service))
    End Function

#End Region


End Class
