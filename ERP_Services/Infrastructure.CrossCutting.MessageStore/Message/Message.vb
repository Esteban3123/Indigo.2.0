'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Store
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Text

#End Region

''' <summary>
''' Encapsula los datos de un mensaje
''' </summary>
<Serializable()>
Public Class Message
    Implements IMessage

#Region "Fields"

    ''' <summary>
    ''' Titulo del mensaje
    ''' </summary>
    Private _title As String

    ''' <summary>
    ''' Marca de tiempo cuando el mensaje fue creado
    ''' </summary>
    Private _timeStamp As TimeSpan

    ''' <summary>
    ''' Cuerpo del mensaje
    ''' </summary>
    Private _body As DataSet

    ''' <summary>
    ''' Valor que indica si el mensaje fue leido y contiene errore
    ''' </summary>
    Private _hasError As Boolean

    ''' <summary>
    ''' Mensaje del error
    ''' </summary>
    Private _messageError As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el Id interno que identifica
    ''' el mensaje en el almacén de mensajes.
    ''' Ésta propiedad se llena cuando se lee el mensaje
    ''' del almacén y es de uso interno
    ''' </summary>
    ''' <value>Id interno del mensaje</value>
    ''' <returns>El Id interno del mensaje</returns>
    Public Property IdFile As Guid Implements IMessage.IdFile

    ''' <summary>
    ''' Obtiene o asigna el titulo del mensaje
    ''' </summary>
    ''' <value>Titulo del mensaje</value>
    ''' <returns>El titulo del mensaje</returns>
    Public Property Title As String Implements IMessage.Title
        Get
            Return Me._title
        End Get
        Set(value As String)
            Me._title = value
            Me.EnsureTitleExt()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el cuerpo del mensaje
    ''' </summary>
    ''' <value>Cuerpo del mensaje</value>
    ''' <returns>El cuerpo del mensaje</returns>
    Public Property Body As DataSet Implements IMessage.Body
        Get
            Return Me._body
        End Get
        Set(value As DataSet)
            Me._body = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene le hora y fecha en la que fue almacenado el mensaje
    ''' </summary>
    ''' <returns>Fecha y hora de almacenamiento</returns>
    Public ReadOnly Property TimeStamp As TimeSpan Implements IMessage.TimeStamp
        Get
            Return Me._timeStamp
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tamaño del mensaje
    ''' </summary>
    ''' <value>Tamaño del nmensaje</value>
    ''' <returns>El tamaño del mensaje</returns>
    Public Property Size As UInteger Implements IMessage.Size

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el mensaje
    ''' fue leido y ocurrión algún error al procesarlo
    ''' </summary>
    ''' <value>Valor que indica si el mensaje tiene errores</value>
    ''' <returns>Un valor quee indica si el mensaje tiene errores</returns>
    Public Property HasError As Boolean Implements IMessage.HasError
        Get
            Return Me._hasError
        End Get
        Set(value As Boolean)
            Me._hasError = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el mensaje del error
    ''' </summary>
    ''' <value>Mensaje del error</value>
    ''' <returns>El mensaje del error</returns>
    Public Property MessageError As String Implements IMessage.MessageError
        Get
            Return Me._messageError
        End Get
        Set(value As String)
            Me._messageError = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="title">Titulo del mensaje</param>
    ''' <param name="body">Cuerpo del mensaje</param>
    Public Sub New(ByVal title As String, ByVal body As DataSet)
        Me.IdFile = Guid.Empty
        Me._hasError = False
        Me._messageError = String.Empty
        Me._title = title
        Me._timeStamp = New TimeSpan(DateTime.Now.Ticks)
        Me._body = body
        Me.EnsureTitleExt()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(info As SerializationInfo, context As StreamingContext)
        Me.IdFile = Guid.Empty
        Me._title = info.GetString("Title")
        Me._timeStamp = New TimeSpan(Long.Parse(info.GetInt64("TimeStamp")))
        Me._body = New DataSet()
        Me._body.ReadXmlSchema(New MemoryStream(DirectCast(info.GetValue("BodySchema", GetType(Byte())), Byte())))
        Me._body.ReadXml(New MemoryStream(DirectCast(info.GetValue("BodyData", GetType(Byte())), Byte())))
        Me._hasError = info.GetBoolean("HasError")
        Me._messageError = info.GetString("MessageError")
        Me.EnsureTitleExt()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Verifica y asegura que el titulo tenga una extensión de mensaje
    ''' </summary>
    Private Sub EnsureTitleExt()
        If Me._title IsNot Nothing Then
            If Me._title.Trim().Equals(String.Empty) Then
                Me._title = "Noname"
            End If
            If Path.GetExtension(Me._title.Trim()) Is Nothing OrElse Path.GetExtension(Me._title.Trim()).Equals(String.Empty) Then
                Me._title &= ".mess"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene datos de serialización
    ''' </summary>
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("Title", Me._title)
        info.AddValue("TimeStamp", Me._timeStamp.Ticks)
        Using ms As New MemoryStream()
            Me._body.WriteXmlSchema(ms)
            ms.Position = 0
            info.AddValue("BodySchema", ms.ToArray())
        End Using
        Using ms As New MemoryStream()
            Me._body.WriteXml(ms)
            ms.Position = 0
            info.AddValue("BodyData", ms.ToArray())
        End Using
        info.AddValue("HasError", Me._hasError)
        info.AddValue("MessageError", Me._messageError)
    End Sub

#End Region

#Region "Statics"

    ''' <summary>
    ''' Obtiene una secuencia de bytes a partir de un mensaje
    ''' </summary>
    ''' <returns>Secuencia de bytes del mensaje</returns>
    Public Shared Function FromMessage(ByVal message As IMessage) As Stream
        Dim Serializer As New BinaryFormatter()
        Dim ms As New MemoryStream()
        Serializer.Serialize(ms, message)
        ms.Position = 0
        Return ms
    End Function

    ''' <summary>
    ''' Obtiene un mensaje a partir de una secuencia de bytes
    ''' </summary>
    ''' <param name="ms">Secuencia de bytes</param>
    ''' <returns>Mensaje en la secuencia de bytes</returns>
    Public Shared Function FromStream(ByVal ms As Stream) As IMessage
        ms.Position = 0
        Dim Serializer As New BinaryFormatter()
        Return CType(Serializer.Deserialize(ms), IMessage)
    End Function

#End Region

End Class
