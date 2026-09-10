'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-12-26
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-26
' Description      : Encapsula y administra los datos de configuración 
'                    almacenados en el archivo de configuración
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

#End Region

''' <summary>
''' Provee servicios para interactuar con el archivo de configuración de la consola
''' </summary>
Public NotInheritable Class ConfigurationHelper

#Region "Singleton"

    ''' <summary>
    ''' Encapsula la unica instancia de la clase
    ''' </summary>
    Private Shared _instance As ConfigurationHelper

    ''' <summary>
    ''' Obtiene la unica instancia del archivo de configuración
    ''' </summary>
    ''' <returns>Una instancia del archivo de configuración</returns>
    Public Shared ReadOnly Property Instance As ConfigurationHelper
        Get
            If _instance Is Nothing Then
                _instance = New ConfigurationHelper()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Shared"

    ''' <summary>
    ''' Nombre del archivo de configuración
    ''' </summary>
    Public Const FILE_NAME As String = "Indigo.Config"

    ''' <summary>
    ''' Obtiene la ruta completa del archivo de configuración
    ''' </summary>
    ''' <returns>Rura del archivo de configuración</returns>
    Public Shared Function GetPathFile() As String
        Return Path.Combine(Helper.GetPathApplicationFiles(), ConfigurationHelper.FILE_NAME)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si existe un archivo de configuración
    ''' </summary>
    ''' <returns>Valor que indica si existe un archivo de configuración</returns>
    Public Shared Function ConfigurationFileExists() As Boolean
        Return File.Exists(GetPathFile())
    End Function

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula los datos del archivo de configuración
    ''' </summary>
    Private _confg As Configuration

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el idioma usado en la consola
    ''' </summary>
    ''' <value>Código del idioma</value>
    ''' <returns>El código del idioma</returns>
    Public Property Language As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.Language)
        End Get
        Set(value As String)
            Me._confg.Language = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre base de los contenedores
    ''' </summary>
    ''' <value>Nombre base de los contenedores</value>
    ''' <returns>El nombre base de los contenedores</returns>
    Public Property BaseNameDbContainer As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.BaseNameDbContainer)
        End Get
        Set(value As String)
            Me._confg.BaseNameDbContainer = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre o dirección IP del motor de
    ''' bases de datos principal
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de bases de datos principal</value>
    ''' <returns>El nombre o dirección IP del motor de bases de datos principal</returns>
    Public Property HostDbContainer As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.HostDbContainer)
        End Get
        Set(value As String)
            Me._confg.HostDbContainer = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de bases de datos principal
    ''' </summary>
    ''' <value>Usuario del motor de bases de datos principal</value>
    ''' <returns>El usuario del motor de bases de datos principal</returns>
    Public Property UserDbContainer As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UserDbContainer)
        End Get
        Set(value As String)
            Me._confg.UserDbContainer = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de bases de datos principal
    ''' </summary>
    ''' <value>Contraseña del motor de bases de datos principal</value>
    ''' <returns>La contraseña del motor de bases de datos principal</returns>
    Public Property PswdDbContainer As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.PswdDbContainer)
        End Get
        Set(value As String)
            Me._confg.PswdDbContainer = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre o la dirección IP del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>El nombre o dirección IP del motor de bases de datos de gestion documental</returns>
    Public Property HostDbDocumental As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.HostDbDocumental)
        End Get
        Set(value As String)
            Me._confg.HostDbDocumental = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Usuario del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>El usuario del motor de bases de datos de gestion documental</returns>
    Public Property UserDbDocumental As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UserDbDocumental)
        End Get
        Set(value As String)
            Me._confg.UserDbDocumental = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Contraseña del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>La contraseña del motor de bases de datos de gestion documental</returns>
    Public Property PswdDbDocumental As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.PswdDbDocumental)
        End Get
        Set(value As String)
            Me._confg.PswdDbDocumental = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre o la dirección IP del motor de indexación V12
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de indexación V12</value>
    ''' <returns>El nombre o dirección IP del motor de indexación V12</returns>
    Public Property HostV12 As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.HostV12)
        End Get
        Set(value As String)
            Me._confg.HostV12 = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de indexación V12
    ''' </summary>
    ''' <value>Usuario del motor de indexación V12</value>
    ''' <returns>El usuario del motor de indexación V12</returns>
    Public Property UserV12 As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UserV12)
        End Get
        Set(value As String)
            Me._confg.UserV12 = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de indexación V12
    ''' </summary>
    ''' <value>Contraseña del motor de indexación V12</value>
    ''' <returns>La contraseña del motor de indexación V12</returns>
    Public Property PswdV12 As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.PswdV12)
        End Get
        Set(value As String)
            Me._confg.PswdV12 = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio transaccional
    ''' </summary>
    ''' <value>URL del servicio transaccional</value>
    ''' <returns>La URL del servicio transaccional</returns>
    Public Property UrlEntityService As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UrlEntityService)
        End Get
        Set(value As String)
            Me._confg.UrlEntityService = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio XPO
    ''' </summary>
    ''' <value>URL del servicio XPO</value>
    ''' <returns>La URL del servicio XPO</returns>
    Public Property UrlXpoService As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UrlXpoService)
        End Get
        Set(value As String)
            Me._confg.UrlXpoService = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de notificación
    ''' </summary>
    ''' <value>URL del servicio de notificación</value>
    ''' <returns>La URL del servicio de notificación</returns>
    Public Property UrlNotificationService As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UrlNotificationService)
        End Get
        Set(value As String)
            Me._confg.UrlNotificationService = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de gestion documental
    ''' </summary>
    ''' <value>URL del servicio de gestion documental</value>
    ''' <returns>La URL del servicio de gestion documental</returns>
    Public Property UrlDocumentalService As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UrlDocumentalService)
        End Get
        Set(value As String)
            Me._confg.UrlDocumentalService = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de indexación
    ''' </summary>
    ''' <value>URL del servicio de indexación</value>
    ''' <returns>La URL del servicio de indexación</returns>
    Public Property UrlIndexingService As String
        Get
            Return CryptoHelper.Decrypt(Me._confg.UrlIndexingService)
        End Get
        Set(value As String)
            Me._confg.UrlIndexingService = CryptoHelper.Encrypt(value.Trim())
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Private Sub New()
        Me._confg = New Configuration()
        If ConfigurationFileExists() Then
            Dim frm As New BinaryFormatter()
            Using strm As Stream = File.Open(GetPathFile(), FileMode.Open)
                Me._confg = DirectCast(frm.Deserialize(strm), Configuration)
            End Using
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Persiste los cambios realizados al archivo
    ''' de configuración en disco
    ''' </summary>
    Public Sub Flush()
        If Not Directory.Exists(Path.GetDirectoryName(GetPathFile())) Then
            Directory.CreateDirectory(Path.GetDirectoryName(GetPathFile()))
        End If
        If ConfigurationFileExists() Then
            File.Delete(GetPathFile())
        End If
        Dim frm As New BinaryFormatter()
        Using strm As Stream = File.Open(GetPathFile(), FileMode.Create)
            frm.Serialize(strm, Me._confg)
            strm.Flush()
        End Using
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula la estructura usada por el archivo de configuración de la consola de administración
''' </summary>
<Serializable()>
Friend NotInheritable Class Configuration
    Implements ISerializable

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el idioma usado en la consola
    ''' </summary>
    ''' <value>Código del idioma</value>
    ''' <returns>El código del idioma</returns>
    Public Property Language As String

    ''' <summary>
    ''' Obtiene o asigna el nombre base de los contenedores
    ''' </summary>
    ''' <value>Nombre base de los contenedores</value>
    ''' <returns>El nombre base de los contenedores</returns>
    Public Property BaseNameDbContainer As String

    ''' <summary>
    ''' Obtiene o asigna el nombre o dirección IP del motor de
    ''' bases de datos principal
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de bases de datos principal</value>
    ''' <returns>El nombre o dirección IP del motor de bases de datos principal</returns>
    Public Property HostDbContainer As String

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de bases de datos principal
    ''' </summary>
    ''' <value>Usuario del motor de bases de datos principal</value>
    ''' <returns>El usuario del motor de bases de datos principal</returns>
    Public Property UserDbContainer As String

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de bases de datos principal
    ''' </summary>
    ''' <value>Contraseña del motor de bases de datos principal</value>
    ''' <returns>La contraseña del motor de bases de datos principal</returns>
    Public Property PswdDbContainer As String

    ''' <summary>
    ''' Obtiene o asigna el nombre o la dirección IP del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>El nombre o dirección IP del motor de bases de datos de gestion documental</returns>
    Public Property HostDbDocumental As String

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Usuario del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>El usuario del motor de bases de datos de gestion documental</returns>
    Public Property UserDbDocumental As String

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de
    ''' bases de datos de gestion documental
    ''' </summary>
    ''' <value>Contraseña del motor de bases de datos de 
    ''' gestion documental</value>
    ''' <returns>La contraseña del motor de bases de datos de gestion documental</returns>
    Public Property PswdDbDocumental As String

    ''' <summary>
    ''' Obtiene o asigna el nombre o la dirección IP del motor de indexación V12
    ''' </summary>
    ''' <value>Nombre o dirección IP del motor de indexación V12</value>
    ''' <returns>El nombre o dirección IP del motor de indexación V12</returns>
    Public Property HostV12 As String

    ''' <summary>
    ''' Obtiene o asigna el usuario del motor de indexación V12
    ''' </summary>
    ''' <value>Usuario del motor de indexación V12</value>
    ''' <returns>El usuario del motor de indexación V12</returns>
    Public Property UserV12 As String

    ''' <summary>
    ''' Obtiene o asigna la contraseña del motor de indexación V12
    ''' </summary>
    ''' <value>Contraseña del motor de indexación V12</value>
    ''' <returns>La contraseña del motor de indexación V12</returns>
    Public Property PswdV12 As String

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio transaccional
    ''' </summary>
    ''' <value>URL del servicio transaccional</value>
    ''' <returns>La URL del servicio transaccional</returns>
    Public Property UrlEntityService As String

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio XPO
    ''' </summary>
    ''' <value>URL del servicio XPO</value>
    ''' <returns>La URL del servicio XPO</returns>
    Public Property UrlXpoService As String

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de notificación
    ''' </summary>
    ''' <value>URL del servicio de notificación</value>
    ''' <returns>La URL del servicio de notificación</returns>
    Public Property UrlNotificationService As String

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de gestion documental
    ''' </summary>
    ''' <value>URL del servicio de gestion documental</value>
    ''' <returns>La URL del servicio de gestion documental</returns>
    Public Property UrlDocumentalService As String

    ''' <summary>
    ''' Obtiene o asigna la dirección URL del servicio de indexación
    ''' </summary>
    ''' <value>URL del servicio de indexación</value>
    ''' <returns>La URL del servicio de indexación</returns>
    Public Property UrlIndexingService As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.Language = ""
        Me.BaseNameDbContainer = ""
        Me.HostDbContainer = ""
        Me.UserDbContainer = ""
        Me.PswdDbContainer = ""
        Me.HostDbDocumental = ""
        Me.UserDbDocumental = ""
        Me.PswdDbDocumental = ""
        Me.HostV12 = ""
        Me.UserV12 = ""
        Me.PswdV12 = ""
        Me.UrlEntityService = ""
        Me.UrlXpoService = ""
        Me.UrlNotificationService = ""
        Me.UrlDocumentalService = ""
        Me.UrlIndexingService = ""
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(info As SerializationInfo, context As StreamingContext)
        Me.Language = info.GetString("Language")
        Me.BaseNameDbContainer = info.GetString("BaseNameDbContainer")
        Me.HostDbContainer = info.GetString("HostDbContainer")
        Me.UserDbContainer = info.GetString("UserDbContainer")
        Me.PswdDbContainer = info.GetString("PswdDbContainer")
        Me.HostDbDocumental = info.GetString("HostDbDocumental")
        Me.UserDbDocumental = info.GetString("UserDbDocumental")
        Me.PswdDbDocumental = info.GetString("PswdDbDocumental")
        Me.HostV12 = info.GetString("HostV12")
        Me.UserV12 = info.GetString("UserV12")
        Me.PswdV12 = info.GetString("PswdV12")
        Me.UrlEntityService = info.GetString("UrlEntityService")
        Me.UrlXpoService = info.GetString("UrlXpoService")
        Me.UrlNotificationService = info.GetString("UrlNotificationService")
        Me.UrlDocumentalService = info.GetString("UrlDocumentalService")
        Me.UrlIndexingService = info.GetString("UrlIndexingService")
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo usado para la serialización del objeto
    ''' </summary>
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("Language", Me.Language)
        info.AddValue("BaseNameDbContainer", Me.BaseNameDbContainer)
        info.AddValue("HostDbContainer", Me.HostDbContainer)
        info.AddValue("UserDbContainer", Me.UserDbContainer)
        info.AddValue("PswdDbContainer", Me.PswdDbContainer)
        info.AddValue("HostDbDocumental", Me.HostDbDocumental)
        info.AddValue("UserDbDocumental", Me.UserDbDocumental)
        info.AddValue("PswdDbDocumental", Me.PswdDbDocumental)
        info.AddValue("HostV12", Me.HostV12)
        info.AddValue("UserV12", Me.UserV12)
        info.AddValue("PswdV12", Me.PswdV12)
        info.AddValue("UrlEntityService", Me.UrlEntityService)
        info.AddValue("UrlXpoService", Me.UrlXpoService)
        info.AddValue("UrlNotificationService", Me.UrlNotificationService)
        info.AddValue("UrlDocumentalService", Me.UrlDocumentalService)
        info.AddValue("UrlIndexingService", Me.UrlIndexingService)
    End Sub

#End Region

End Class