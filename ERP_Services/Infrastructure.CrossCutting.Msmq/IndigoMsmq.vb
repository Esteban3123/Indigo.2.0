'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Msmq
' Author           : WalterSierra
' Created          : 09-04-2011
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 10-11-2013
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Messaging
Imports System.Configuration
#End Region

''' <summary>
''' clase que administra la integracion con Microsoft Message Queue 
''' http://msdn.microsoft.com/es-es/library/system.messaging.messagequeue(v=VS.80).aspx 
''' </summary>
Public Class IndigoMsmq

#Region "Consts"

    ''' <summary>
    ''' constante con la ruta del contenedor publico de MSMQ
    ''' </summary>
    Const PublicContainerPath As String = ".\IndigoAuditoria"
    ''' <summary>
    ''' constante con la ruta del contenedor pirvado de MSQM
    ''' </summary>
    Public Shared PrivateContainerPath As String = ".\Private$\IndigoAuditoria"
    ''' <summary>
    ''' el tiempo que tiene el mensaje para ser leido desde la cola - a la aplicacion
    ''' </summary>
    Private Shared ReadMessageTimeout As TimeSpan = New TimeSpan(0, 0, 10)
    ''' <summary>
    ''' parametros de envio de mensajes a la cola por defecto
    ''' </summary>
    Private Shared sendParams As DefaultPropertiesToSend
    ''' <summary>
    ''' variable que toma el label del ultimo mensaje leido, o recibido
    ''' </summary>
    Public Shared LabelLastMessage As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' inicializa una instacia de la clase <see cref="IndigoMsmq" />.
    ''' </summary>
    Shared Sub New()
        'establezco las propiedaes por defecto para el envio de los mensajes a la cola de MSMQ
        sendParams = New DefaultPropertiesToSend
        With sendParams
            .TimeToReachQueue = New TimeSpan(0, 0, 10) 'tiempo que tiene el mensaje para llegar a la cola
            'obtengo el parametro de persistencia del config
            Dim persistence As String = ConfigurationManager.AppSettings("PersistenciaMSMQ")
            If String.IsNullOrEmpty(persistence) = True Then
                Throw New InvalidOperationException("PersistenciaMSMQ no existe")
            End If
            .Recoverable = Boolean.Parse(persistence) 'true=>persitido a disco false=>persistido a memoria
            .AttachSenderId = False 'false=>no envia informacion del usuario
        End With
    End Sub

#End Region

#Region "Send Messages"

    ''' <summary>
    ''' Enviar un mensaje a MSMQ al contenedor Publico Predeterminado en la constante 
    ''' </summary>
    ''' <param name="label">el label - Etiqueta del mensaje.</param>
    ''' <param name="message">el mensaje (Objeto) que se va a enviar a la cola.</param>
    Public Shared Sub SendPublicMessage(ByVal label As String, ByVal message As Object)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            'envio el mensaje
            Dim messageObject As Message = New Message(message)
            messageObject.Label = label
            messageObject.Recoverable = sendParams.Recoverable
            messageObject.TimeToReachQueue = sendParams.TimeToReachQueue
            messageObject.AttachSenderId = sendParams.AttachSenderId
            Dim queue As New MessageQueue(PublicContainerPath)
            queue.Send(messageObject)
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Enviar un mensaje a MSMQ al contenedor Privado Predeterminado en la constante 
    ''' </summary>
    ''' <param name="label">el label - Etiqueta del mensaje.</param>
    ''' <param name="message">el mensaje (Objeto) que se va a enviar a la cola.</param>
    Public Shared Sub SendPrivateMessage(ByVal label As String, ByVal message As Object)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            'envio el mensaje
            Dim messageObject As Message = New Message(message)
            messageObject.Label = label
            messageObject.Recoverable = sendParams.Recoverable
            messageObject.TimeToReachQueue = sendParams.TimeToReachQueue
            messageObject.AttachSenderId = sendParams.AttachSenderId
            Dim queue As New MessageQueue(PrivateContainerPath)
            queue.Send(messageObject)
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Enviar un mensaje a MSMQ al contenedor especificado
    ''' </summary>
    ''' <param name="label">el label - Etiqueta del mensaje.</param>
    ''' <param name="message">el mensaje (Objeto) que se va a enviar a la cola.</param>
    ''' <param name="container">Ruta del Contenedor - (cola)</param>
    Public Shared Sub SendMessage(ByVal label As String, ByVal message As Object, ByVal container As String)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            'envio el mensaje
            Dim messageObject As Message = New Message(message)
            messageObject.Label = label
            messageObject.Recoverable = sendParams.Recoverable
            messageObject.TimeToReachQueue = sendParams.TimeToReachQueue
            messageObject.AttachSenderId = sendParams.AttachSenderId
            Dim queue As New MessageQueue(container)
            queue.Send(messageObject)
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Enviar un mensaje a MSMQ al contenedor especificado
    ''' </summary>
    ''' <param name="label">el label - Etiqueta del mensaje.</param>
    ''' <param name="message">el mensaje (Objeto) que se va a enviar a la cola.</param>
    ''' <param name="container">Ruta del Contenedor - (cola)</param>
    ''' <param name="sendParams">Parametros de envio del mensaje</param>
    Public Shared Sub SendMessage(ByVal label As String, ByVal message As Object, ByVal container As String, ByVal sendParams As DefaultPropertiesToSend)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            'envio el mensaje
            Dim messageObject As Message = New Message(message)
            messageObject.Label = label
            messageObject.Recoverable = sendParams.Recoverable
            messageObject.TimeToReachQueue = sendParams.TimeToReachQueue
            messageObject.AttachSenderId = sendParams.AttachSenderId
            Dim queue As New MessageQueue(container)
            queue.Send(messageObject)
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Get Messages"

    ''' <summary>
    ''' Recibe el mensaje del contenedor publico predeterminado. el mensaje es eliminado del contenedor una ves es recibido
    ''' </summary>
    ''' <typeparam name="T">El Tipo de Dato esperado.</typeparam>
    ''' <returns></returns>
    Public Shared Function GetPublicMessage(Of T)() As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            Dim queue As New MessageQueue(PublicContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibo el mensaje
                Dim message As Message
                message = queue.Receive(ReadMessageTimeout)
                'converto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Return CType(message.Body, T)
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Recibe el mensaje del contenedor privado predeterminado. el mensaje es eliminado del contenedor una ves es recibido
    ''' </summary>
    ''' <typeparam name="T">El Tipo de Dato esperado.</typeparam>
    ''' <returns></returns>
    Public Shared Function GetPrivateMessage(Of T)() As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            Dim queue As New MessageQueue(PrivateContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibo el mensaje
                Dim message As Message
                message = queue.Receive(ReadMessageTimeout)
                'converto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Return CType(message.Body, T)
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Recibe el mensaje del contenedor especificado. el mensaje es eliminado del contenedor una ves es leido
    ''' </summary>
    ''' <typeparam name="T">El Tipo de Dato esperado.</typeparam>
    ''' <param name="container">Ruta del Contenedor - (cola)</param>
    ''' <returns></returns>
    Public Shared Function GetMessage(Of T)(ByVal container As String) As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            Dim queue As New MessageQueue(container)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibo el mensaje
                Dim message As Message
                message = queue.Receive(ReadMessageTimeout)
                'converto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Return CType(message.Body, T)
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

#End Region

#Region "Read Messages"

    ''' <summary>
    ''' Lee el primer mensaje del contenedor publico predeterminado, el mensaje no se elimina de la cola
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato esperado para recibier el mensaje.</typeparam>
    ''' <returns></returns>
    Public Shared Function ReadPublicMessage(Of T)() As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            Dim queue As New MessageQueue(PublicContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'leo el mensaje
                Dim message As Message
                message = queue.Peek(ReadMessageTimeout)
                'converto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Return CType(message.Body, T)
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Lee el primer mensaje del contenedor privado predeterminado, el mensaje no se elimina de la cola
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato esperado para recibier el mensaje.</typeparam>
    ''' <returns></returns>
    Public Shared Function ReadPrivateMessage(Of T)() As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            Dim queue As New MessageQueue(PrivateContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'leo el mensaje
                Dim message As Message
                message = queue.Peek(ReadMessageTimeout)
                'convierto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Dim result = DirectCast(message.Body, T)
                If result IsNot Nothing Then
                    Return result
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Lee el primer mensaje del contenedor privado predeterminado, el mensaje no se elimina de la cola
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato esperado para recibir el mensaje.</typeparam>
    ''' <param name="container">la Ruta del contenedor - (cola)</param>
    ''' <returns></returns>
    Public Shared Function ReadMessage(Of T)(ByVal container As String) As T
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            Dim queue As New MessageQueue(container)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'leo el mensaje
                Dim message As Message
                message = queue.Peek(ReadMessageTimeout)
                'converto el cuerpo del mensaje en el tipo especificado
                message.Formatter = New XmlMessageFormatter(New Type() {GetType(T)})
                LabelLastMessage = message.Label
                Return CType(message.Body, T)
            Else
                Return Nothing
            End If
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

#End Region

#Region "Delete Messages"

    ''' <summary>
    ''' Elimina el primer mensaje de la cola, en el contenedor publico predeterminado
    ''' </summary>
    Public Shared Sub DeletePublicMessage()
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            Dim queue As New MessageQueue(PublicContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibe el primer mensaje y lo elimina de la col
                queue.Receive(ReadMessageTimeout)
            End If
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina el primer mensaje de la cola, en el contenedor privado predeterminado
    ''' </summary>
    Public Shared Sub DeletePrivateMessage()
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            Dim queue As New MessageQueue(PrivateContainerPath)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibe el primer mensaje y lo elimina de la col
                queue.Receive(ReadMessageTimeout)
            End If
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina el primer mensaje de la cola, en el contenedor especificado
    ''' </summary>
    ''' <param name="container">la ruta del contenedor -(cola)</param>
    Public Shared Sub DeleteMessage(ByVal container As String)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            Dim queue As New MessageQueue(container)
            'verifico si existen mensajes
            If queue.GetAllMessages.Count > 0 Then
                'recibe el primer mensaje y lo elimina de la cola
                queue.Receive(ReadMessageTimeout)
            End If
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina todos los mensajes del contenedor publico predeterminado.
    ''' </summary>
    Public Shared Sub DeleteAllPublicMessages()
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            Dim queue As New MessageQueue(PublicContainerPath)
            'elimino todos los mensajes
            queue.Purge()
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina todos los mensajes del contenedor privado predeterminado.
    ''' </summary>
    Public Shared Sub DeleteAllPrivateMessages()
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            Dim queue As New MessageQueue(PrivateContainerPath)
            'elimino todos los mensajes
            queue.Purge()
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina todos los mensajes del contenedor especificado.
    ''' </summary>
    ''' <param name="container">Ruta del Contenedor  - (cola)</param>
    Public Shared Sub DeleteAllMessages(ByVal container As String)
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            Dim queue As New MessageQueue(container)
            'elimino todos los mensajes
            queue.Purge()
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Count Message Queue"

    ''' <summary>
    ''' Regresa el total de mensajes del Contenedor Publico Predeterminado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function MessagesPrivateContainer() As Integer
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PrivateContainerPath) = False Then
                MessageQueue.Create(PrivateContainerPath)
            End If
            Dim queue As New MessageQueue(PrivateContainerPath)
            'cuento los mensajes
            Return queue.GetAllMessages.Count
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Regresa el total de mensajes del Contenedor Privado Predeterminado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function MessagesPublicContainer() As Integer
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(PublicContainerPath) = False Then
                MessageQueue.Create(PublicContainerPath)
            End If
            Dim queue As New MessageQueue(PublicContainerPath)
            'cuento los mensajes
            Return queue.GetAllMessages.Count
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Regresa el total de mensajes del Contenedor Especificado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CountMessages(ByVal container As String) As Integer
        Try
            'verifico si existe el contenedor
            If MessageQueue.Exists(container) = False Then
                MessageQueue.Create(container)
            End If
            Dim queue As New MessageQueue(container)
            'cuento los mensajes
            Return queue.GetAllMessages.Count
        Catch ex1 As InvalidOperationException
            Throw
        Catch ex As MessageQueueException
            Throw New InvalidOperationException(ex.ToString)
        End Try
    End Function

#End Region

End Class
