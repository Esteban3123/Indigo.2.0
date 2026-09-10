Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Threading
Imports Azure.Messaging.ServiceBus
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Newtonsoft.Json

Public Class AzureServiceBusIndigoQueue
    Implements IIndigoQueue

    ''' <summary>
    ''' El cliente propietario de la conexión y que se puede utilizar para crear remitentes y receptores.
    ''' </summary>
    Private client As ServiceBusClient

    '/// <summary>
    '/// El remitente encargado de publicar mensajes en la cola.
    '/// </summary>
    Private sender As ServiceBusSender

    ''' <summary>
    ''' Modelo que contiene la estructura del mensaje que se envía a la cola
    ''' </summary>
    Private _eventData As EventData

    ''' <summary>
    ''' Hilo encargado de la ejecución del metodo SenderMessage
    ''' </summary>
    Private mThreadFic As New Thread(AddressOf SenderMessage)

    ''' <summary>
    ''' Metodo que recibe la data del evento para posteriormente realizar el envio del mensaje a la Queue
    ''' </summary>
    ''' <param name="eventData"></param>
    Public Sub Publish(eventData As EventData) Implements IIndigoQueue.Publish
        _eventData = eventData
        Dim CurrentContainer = ServerSessionValues.Current.CurrentContainer.Clone()
        If IsDBNull(_eventData) Then
            Throw New ArgumentNullException(NameOf(_eventData))
        End If

        If IsDBNull(_eventData.data) Then
            Throw New ArgumentNullException(NameOf(_eventData.data))
        End If

        If String.IsNullOrEmpty(CurrentContainer) Then
            Throw New ArgumentNullException(NameOf(ServerSessionValues.Current.CurrentContainer))
        End If

        Task.Run(Sub() SenderMessage(CurrentContainer))

    End Sub

    ''' <summary>
    ''' Metodo encargado de publicar el mensaje en la Queue
    ''' </summary>
    Public Async Sub SenderMessage(ByVal container As String)
        Try
            Dim configurationQueue = GetQueueConfiguration(container)
            Dim urlQueue = configurationQueue?.UrlQueue
            Dim topic = configurationQueue?.Topic

            If configurationQueue Is Nothing OrElse IsDBNull(configurationQueue) Then
                Throw New Exception("Event has not configurated")
            End If

            If IsDBNull(configurationQueue.UrlQueue) Or IsDBNull(configurationQueue.Topic) Then
                Throw New Exception("UrlQueue or Topic has not configurated")
            End If

            client = New ServiceBusClient(urlQueue)
            sender = client.CreateSender(topic)

            Using messageBatch As ServiceBusMessageBatch = Await sender.CreateMessageBatchAsync()
                Dim serializedServiceBusMessage As New ServiceBusMessage(JsonConvert.SerializeObject(_eventData))
                serializedServiceBusMessage.ApplicationProperties("transmitter") = "Vie_Cloud_Platform"
                serializedServiceBusMessage.ApplicationProperties.Add("ContainerDB", _eventData.db)
                serializedServiceBusMessage.ApplicationProperties.Add("SourceEvent", _eventData.source)

                If Not messageBatch.TryAddMessage(serializedServiceBusMessage) Then
                    Throw New Exception($"The message {serializedServiceBusMessage.MessageId} is too large to fit in the batch.")
                End If

                Await sender.SendMessagesAsync(messageBatch)

                Await sender.DisposeAsync()
                Await client.DisposeAsync()

            End Using

            If _eventData.OutboxId IsNot Nothing Then
                Me.UpdateOutBoxEntity(_eventData.OutboxId, _eventData.db)
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Funcion para consultar los datos de configuracion de la Queue en la tabla Security.EventsConfiguration
    ''' </summary>
    ''' <returns></returns>
    Private Function GetQueueConfiguration(container As String) As EventsConfigurationEntities
        Dim securityContainer = ConfigurationManager.AppSettings.Get("containerSecurity")
        Dim moduleDctionary As New ModuleDictionary()
        Dim code As String = moduleDctionary.GetModule(_eventData.source)
        Using context = New SecurityDBContext(
                Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, securityContainer, True)
            )

            Dim eventConfiguration = (From c In context.Containers
                                      Join eventConfig In context.EventsConfiguration On c.Id Equals eventConfig.ContainerId
                                      Where c.TransactionalContainer.Equals(container) And eventConfig.Code.Equals(code)
                                      Select eventConfig).FirstOrDefault

            Return eventConfiguration
        End Using

    End Function

    ''' <summary>
    ''' Marca como publicado el registro en Outbox de forma parametrizada.
    ''' </summary>
    ''' <param name="id">Identificador del registro Outbox.</param>
    ''' <param name="dataBase">Clave de la cadena de conexión / contenedor.</param>
    Private Sub UpdateOutBoxEntity(id As Integer, dataBase As String)
        Dim sql As String =
        "UPDATE [Integrations].[Outbox] " &
        "SET [IsPublished] = 1 " &
        "WHERE [Id] = @Id;"

        ' Preparamos el parámetro con tipo fuerte
        Dim parameters As SqlParameter() = {
        New SqlParameter("@Id", SqlDbType.Int) With {.Value = id}
    }

        ExecuteNonQuery(sql, parameters, dataBase)
    End Sub

    ''' <summary>
    ''' Ejecuta un comando INSERT/UPDATE/DELETE de forma segura con parámetros.
    ''' </summary>
    ''' <param name="commandText">SQL con placeholders (@param).</param>
    ''' <param name="parameters">Array de SqlParameter ya configurados.</param>
    ''' <param name="dataBase">Clave de la cadena de conexión / contenedor.</param>
    ''' <returns>Número de filas afectadas, o -1 en caso de error.</returns>
    Private Function ExecuteNonQuery(
        ByVal commandText As String,
        ByVal parameters As SqlParameter(),
        ByVal dataBase As String
    ) As Integer

        Dim connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(
        Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS,
        String.Empty,
        dataBase,
        False)

        Using conexion As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(commandText, conexion)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30000

                ' Agregamos de forma segura todos los parámetros
                cmd.Parameters.AddRange(parameters)

                Try
                    If conexion.State = ConnectionState.Closed Then
                        conexion.Open()
                    End If

                    Return cmd.ExecuteNonQuery()

                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return -1

                Finally
                    If conexion.State <> ConnectionState.Closed Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Using
    End Function

End Class
