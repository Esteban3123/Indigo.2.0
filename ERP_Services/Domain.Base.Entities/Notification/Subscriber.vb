''' <summary>
''' Clase base que define las características funcamentales de un subscriptor
''' a un servicio de notificación
''' </summary>
Public MustInherit Class Subscriber

#Region "Fields"

    ''' <summary>
    ''' Código de identificación única del subscriptor
    ''' </summary>
    Private _uId As String

    ''' <summary>
    ''' Maquina desde donde se está realizando la subscripción
    ''' </summary>
    Private _appMachine As Machine

    ''' <summary>
    ''' Aplicación desde donde se está realizando la subscripción
    ''' </summary>
    Private _app As Application

    ''' <summary>
    ''' Id de la conexion del subscriptor
    ''' </summary>
    Private _idCallBack As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el código de indentificación única del subscriptor
    ''' </summary>
    ''' <value>Código de identificación</value>
    ''' <returns>El código de identificación</returns>
    Public Property UID As String
        Get
            Return Me._uId.Trim()
        End Get
        Set(value As String)
            Me._uId = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la maquina desde donde se esta realizando la subscripción al servicio de notificación
    ''' </summary>
    ''' <value>Maquina desde donde se realiza la subscripción</value>
    ''' <returns>La maquina desde donde se realiza la subscripción</returns>
    Public Property AppMachine As Machine
        Get
            Return Me._appMachine
        End Get
        Set(value As Machine)
            Me._appMachine = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la aplicación desde donde se esta realizando la sibscripción al servicio de notificación
    ''' </summary>
    ''' <value>Aplicación desde donde se realiza la subscripción</value>
    ''' <returns>La aplicación desde donde se realiza la subscripción</returns>
    Public Property App As Application
        Get
            Return Me._app
        End Get
        Set(value As Application)
            Me._app = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Id de la conexion de subscriptor
    ''' </summary>
    ''' <value>Id de la conexion del subscriptor</value>
    ''' <returns>El Id de la conexion del subscriptor</returns>
    Public Property CallBack As String
        Get
            Return Me._idCallBack 
        End Get
        Set(value As String)
            Me._idCallBack = value.Trim()
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Genera una nueva instancia de la clase
    ''' </summary>
    ''' <param name="uid">Código de identificación única del subscriptor al servicio</param>
    ''' <param name="appMahine">Maquina desde donde se realiza la subscripción al servicio</param>
    ''' <param name="app">Aplicación desde donde se realiza la subscripción al servicio</param>
    ''' <param name="idCallBack">Id de la conexion del subscriptor</param>
    Public Sub New(ByVal uid As String, ByVal appMahine As Machine, ByVal app As Application, ByVal idCallBack As String)
        Me._uId = uid
        Me._appMachine = appMahine
        Me._app = app
        Me._idCallBack = idCallBack
    End Sub

#End Region

#Region "Methods"



#End Region

End Class

''' <summary>
''' Enumera los posibles estados que puede tomar un usuario
''' en el servicio de mensajeria
''' </summary>
Public Enum AppUserStatus
    ''' <summary>
    ''' En linea
    ''' </summary>
    Online
    ''' <summary>
    ''' Ausente
    ''' </summary>
    Missing
    ''' <summary>
    ''' Ocupado
    ''' </summary>
    Busy
    ''' <summary>
    ''' Desconectado
    ''' </summary>
    Offline
End Enum

''' <summary>
''' Encapsula los datos básicos del usuario de la aplicación
''' </summary>
Public NotInheritable Class AppUser

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el código de identificación del usuario
    ''' </summary>
    ''' <value>Código de identificación</value>
    ''' <returns>El código de identificación</returns>

    Public Property ID As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del usuario
    ''' </summary>
    ''' <value>Nombre del usuario</value>
    ''' <returns>El nombre del usuario</returns>

    Public Property Name As String

    ''' <summary>
    ''' Obtiene o asigna el cargo del usuario en la empresa
    ''' </summary>
    ''' <value>Carglo del usuario</value>
    ''' <returns>El cargo del usuario</returns>

    Public Property Position As String

    ''' <summary>
    ''' Obtiene o asigna el estado del usuario
    ''' </summary>
    ''' <value>Estado del usuario</value>
    ''' <returns>El estado del usuario</returns>

    Public Property Status As AppUserStatus

#End Region

#Region "Methods"

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType() IsNot GetType(AppUser) Then
            Return False
        End If
        If Not CType(obj, AppUser).ID.Trim().Equals(Me.ID.Trim()) Then
            Return False
        End If
        Return True
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

#End Region

End Class

''' <summary>
''' Encapsula los datos de la aplicación subscriptora
''' </summary>
Public NotInheritable Class Application

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre de la aplicación
    ''' </summary>
    ''' <value>Nombre de la aplicación</value>
    ''' <returns>El nombre de la aplicación</returns>

    Public Property Name As String

    ''' <summary>
    ''' Obtiene o asigna la versión de la aplicación
    ''' </summary>
    ''' <value>Versión de la aplicación</value>
    ''' <returns>La versión de la aplicación</returns>

    Public Property Version As Version

    ''' <summary>
    ''' Obtiene o asigna el numero de identificación del proceso de la aplicación en la maquina
    ''' </summary>
    ''' <value>Numero del proceso</value>
    ''' <returns>El numero del proceso</returns>

    Public Property ProcessID As Long

    ''' <summary>
    ''' Obtiene o asigna el usuario logueado en la aplicación
    ''' </summary>
    ''' <value>Usuario logueado</value>
    ''' <returns>El usuario logueado</returns>

    Public Property AppUser As AppUser

    ''' <summary>
    ''' Obtiene o asigna la empresa a la que se encuentra conectada la aplicación
    ''' </summary>
    ''' <value>Empresa</value>
    ''' <returns>La empresa</returns>

    Public Property Company As String

#End Region

End Class

''' <summary>
''' Encapsula los datos de una maquina
''' </summary>
Public NotInheritable Class Machine

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre de la maquina
    ''' </summary>
    ''' <value>Nombre de la maquina</value>
    ''' <returns>El nombre de la maquina</returns>
    Public Property Name As String
    ''' <summary>
    ''' Obtiene o asigna el nombre del sistema operativo de la maquina
    ''' </summary>
    ''' <value>Nombre del sistema operativo</value>
    ''' <returns>El nombre del sistema operativo</returns>

    Public Property OS As String
    ''' <summary>
    ''' Obtiene o asigna la lista de direcciones MAC de la maquina
    ''' </summary>
    ''' <value>Lista de direcciones MAC</value>
    ''' <returns>La lista de direcciones MAC</returns>
    Public Property MACsAddress As List(Of String)
    ''' <summary>
    ''' Obtiene o asigna la lista de direcciones IP's asignadas a la maquina
    ''' </summary>
    ''' <value>Lista de direcciones</value>
    ''' <returns>La lista de direcciones</returns>
    Public Property IPsAddress As List(Of String)

    ''' <summary>
    ''' Obtiene o asigna el nombre del usuario logueado en la maquina
    ''' </summary>
    ''' <value>Nombre del usuario</value>
    ''' <returns>El nombre del usuario</returns>
    Public Property OSUser As String

#End Region

End Class