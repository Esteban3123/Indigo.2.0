'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.IndexerService
' Author           : Juan F. Tamyo 
' Created          : 2013-10-08
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-08
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Messaging

#End Region

''' <summary>
''' Clase encargada del manejo de mensajes MSMQ
''' </summary>
Public NotInheritable Class MSMQManagement

#Region "Fields"

    ''' <summary>
    ''' variable que toma el label del ultimo mensaje leido, o recibido
    ''' </summary>
    Public Shared LabelLastMessage As String = ""

#End Region

#Region "Methods"

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
        Catch ex As InvalidOperationException
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
                message = queue.Peek(New TimeSpan(0, 0, 10))
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
                message = queue.Receive(New TimeSpan(0, 0, 10))
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
                queue.Receive(New TimeSpan(0, 0, 10))
            End If
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

End Class
