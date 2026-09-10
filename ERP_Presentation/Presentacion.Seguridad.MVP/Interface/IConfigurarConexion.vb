'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : AndresBonilla
' Created          : 04-02-2011
'
' Last Modified By : AndresBonilla
' Last Modified On : 04-04-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Presentation.Base
Imports Presentation.Controls

''' <summary>
''' Interface para Configurar la Conexion del Software
''' </summary>
Public Interface IConfigurarConexion

#Region "Propiedades"
    ''' <summary>
    ''' Nombre Zona horaria
    ''' </summary>
    ''' <returns></returns>
    Property TimezonegleName As String

    ''' <summary>
    ''' Zona horaria
    ''' </summary>
    ''' <returns></returns>
    Property TimezoneValue As Integer
    ''' <summary>
    ''' Formato fecha
    ''' </summary>
    ''' <returns></returns>
    Property DateFormat As Integer
    ''' <summary>
    ''' Formato hora
    ''' </summary>
    ''' <returns></returns>
    Property Timeformat As Integer

    Property Ciudad As String
    ''' <summary>
    ''' esta propiedad sirve para escribir la empresa indigo del ERP
    ''' ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmpresaIndigoERP As String
    ' ''' <summary>
    ' ''' esta propiedad sirve para escribir la empresa indigo de historias clinicas
    ' ''' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Property EmpresaIndigoHis As String

    ''' <summary>
    ''' esta propiedad sirve para obtener el tiempo que puede estar Inactivo el sistema
    ''' ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TiempoInactividad As Integer

    ''' <summary>
    ''' Esta propiedad obtiene o establece la URL del Servidor
    ''' </summary>
    ''' <value></value>
    Property UrlServidor As String

    ' ''' <summary>
    ' ''' Esta propiedad obtiene o establece la URL del Servidor Historias Clinicas
    ' ''' </summary>
    ' ''' <value></value>
    'Property UrlServidorHis As String

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor de entidades.
    ''' </summary>
    Property UrlServidorEntidades As String

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor de notificaciones.
    ''' </summary>
    Property UrlServidorNotificacion As String

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor del sistema documental.
    ''' </summary>
    Property UrlServidorSistemaDocumental As String

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor de indexación.
    ''' </summary>
    Property UrlServidorIndexacion As String

    ''' <summary>
    ''' Esta propiedad obtiene o establece el protocolo URL del servidor.
    ''' </summary>
    Property ProtocoloUrlServidor As String

    ''' <summary>
    ''' Esta propiedad obtiene o establece el protocolo URL del servidor del Sistema Documental.
    ''' </summary>
    Property ProtocoloUrlServidorSistemaDocumental As String

    ''' <summary>
    ''' Esta propiedad obtiene o establece el protocolo URL del servidor de indexación.
    ''' </summary>
    Property ProtocoloUrlServidorIndexacion As String

    ''' <summary>
    ''' Esta Propiedad obtiene o establece el Protocolo URL del Servidor de entidades.
    ''' </summary>
    Property ProtocoloUrlServidorEntidades As String
    ''' <summary>
    ''' esta propiedad contiene un mensaje para cualquier evento proporcionado
    ''' </summary>
    WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se usa la versión liviana
    ''' </summary>
    Property LightweightVersion As Boolean

    ''' <summary>
    ''' Propiedad que obtiene o establece el idioma de la aplicacion.
    ''' </summary>
    Property idioma As String

    ''' <summary>
    ''' Esta propiedad establece un valor para mostrar en el xtramessage.
    ''' </summary>
    WriteOnly Property Mensajes As String

    ''' <summary>
    ''' Esta propiedad sirve para escribir la ruta de la carpeta en la cual vamos a guardar las definiciones de reportes personalizados
    ''' </summary>
    Property RutaReportesPersonalizados As String

#End Region

#Region "Metodos"
    ''' <summary>
    ''' este metodo sirve para guardar la configuracion de los datos de conexion
    ''' </summary>
    ''' <remarks></remarks>
    Sub Aceptar()
    ''' <summary>
    ''' esta metodo sirve para cancelar el proceso de configuracion de los datos de conexion
    ''' </summary>
    ''' <remarks></remarks>
    Sub Cancelar()


#End Region


End Interface
