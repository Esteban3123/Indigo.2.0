Imports Microsoft.Azure.Amqp
Imports Newtonsoft.Json
Imports RabbitMQ.Client
Imports System.Text
Imports System.Threading

Public Class RabbitMQIndigoQueue
    Implements IIndigoQueue

    ''' <summary>
    ''' Modelo que contiene la estructura del mensaje que se envía a la cola
    ''' </summary>
    Private _eventData As EventData

    ''' <summary>
    ''' Hilo encargado de la ejecución del metodo SenderMessage
    ''' </summary>
    Private mThreadFic As New Thread(AddressOf SenderMessage)

    ''' <summary>
    ''' URL para conectarse a la cola
    ''' </summary>
    Private ReadOnly _urlQueue As String

    ''' <summary>
    ''' Nombre de la cola creada
    ''' </summary>
    Private ReadOnly _queueName As String

    Public Sub New(UrlQueue As String, queueName As String)
        _urlQueue = UrlQueue
        _queueName = queueName
    End Sub

    ''' <summary>
    ''' Metodo que recibe la data del evento para posteriormente realizar el envio del mensaje a la Queue
    ''' </summary>
    ''' <param name="eventData"></param>
    Public Sub Publish(eventData As EventData) Implements IIndigoQueue.Publish
        If Not IsDBNull(_urlQueue) And Not IsDBNull(eventData.data) And eventData.data IsNot Nothing Then
            _eventData = eventData

            mThreadFic = New Thread(AddressOf SenderMessage)
            mThreadFic.Start()

        End If

    End Sub

    ''' <summary>
    ''' Metodo encargado de publicar el mensaje en la Queue
    ''' </summary>
    Public Sub SenderMessage()
        Try

            Dim factory = New ConnectionFactory With {
                .Uri = New Uri(_urlQueue)
            }
            factory.RequestedHeartbeat = TimeSpan.FromTicks(60)

            Using connection = factory.CreateConnection()
                Using channel = connection.CreateModel()
                    channel.QueueDeclare(_queueName,
                                         durable:=True,
                                         exclusive:=False,
                                         autoDelete:=False,
                                         arguments:=Nothing)

                    Dim message = JsonConvert.SerializeObject(_eventData)
                    Dim messageUTF8 = Encoding.UTF8.GetBytes(message)
                    channel.BasicPublish("", _queueName, Nothing, messageUTF8)

                End Using

            End Using

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

End Class
