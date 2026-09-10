' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.IndexerAgent
' Author           : Juan F. Tamayo
' Created          : 2013-10-07
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-07
' ***********************************************************************

#Region "Imports"

Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports Domain.Base.Entities
Imports System.Data
Imports System.Configuration
Imports Futesh.Documents
Imports Futesh.Enterprise
Imports System.Text
Imports Infrastructure.CrossCutting.MessageStore

#End Region

Public Class IndexerAgent

#Region "Consts"

    ''' <summary>
    ''' Expresión regular que valida la parte del Server en la cadena de conexión
    ''' </summary>
    Private Const RegEx_Server As String = "^Server(\s)*?=(\s)*?((([a-zA-Z][^\`\~\!\@\#\$\%\&\*\(\)\=\+\[\]\{\}\\\|\;\:\'\""\,\<\>\/\?\¡\¿\¬\°\¨\´]{0,14})|((1[0-9]?[0-9]?|2[0-5]?[0-5]?|[1-9][0-9]?|[0-9])\.(1[0-9]?[0-9]?|2[0-5]?[0-5]?|[1-9][0-9]?|[0-9])\.(1[0-9]?[0-9]?|2[0-5]?[0-5]?|[1-9][0-9]?|[0-9])\.(1[0-9]?[0-9]?|2[0-5]?[0-5]?|[1-9][0-9]?|[0-9])))(:([1-9]+))?(\\(\w)+)?)(\s)*?;(\s)*?User Id(\s)*?=(\s)*?(\w)+(\s)*?;(\s)*?Password(\s)*?=(\s)*?([^\=\;])+$"

    ''' <summary>
    ''' Cantidad maxima de mensajes eliminados
    ''' antes de comprimir la base de datos de mensajes
    ''' </summary>
    Private Const MAX_COUNTDELETEDMESSAGES As Int32 = 255

#End Region

#Region "Fields"

    ''' <summary>
    ''' Cadena de conexion al servidor Futesh
    ''' </summary>
    Private _connectionString As String
    ''' <summary>
    ''' Intervalo de tiempo antes de revisar la cola de mensajes
    ''' </summary>
    Private _interval As Int32
    ''' <summary>
    ''' Bandera que controla la parada del proceso de indexación
    ''' </summary>
    Private _stopping As Boolean
    ''' <summary>
    ''' Cliente quien realiza la conexión al motor de indexación
    ''' </summary>
    Private _client As Client.Client
    ''' <summary>
    ''' Cantidad de mensajes eliminados
    ''' </summary>
    Private _countDeletedMessages As Int32 = 0
    ''' <summary>
    ''' Valor que indica si de debe eliminar los mensajes con error
    ''' </summary>
    Private _deleteErrorMessage As Boolean = True

#End Region

#Region "Methods"

    Public Sub Start()
        Me.OnStart(Nothing)
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        If Me.LoadConfig() Then
            Me._stopping = False
            Me.TmrRelay.Start()
        Else 'No se pudo cargar la configuración y por tanto el servicio no inicia
            EventLog.WriteEntry("No se pudo cargar la configuración, por tanto el servicio no se ejecutará.", EventLogEntryType.Information)
        End If
        MyBase.OnStart(args)
    End Sub

    Protected Overrides Sub OnPause()
        Me.TmrRelay.Stop()
        Me._stopping = True
        MyBase.OnPause()
    End Sub

    Protected Overrides Sub OnContinue()
        Me._stopping = False
        Me.TmrRelay.Start()
        MyBase.OnContinue()
    End Sub

    Protected Overrides Sub OnStop()
        Me.TmrRelay.Stop()
        Me._stopping = True
        MyBase.OnStop()
    End Sub

    ''' <summary>
    ''' Ejecuta la rutina de indexación
    ''' </summary>
    Private Sub RunIndexer()
        Try
            Dim sb As New StringBuilder()
            sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Ejecutando el indexador")

            Dim messages As New List(Of IMessage)()
            Dim messagesReaded As New List(Of IMessage)()

            Using store As New MessageStore.MessageStore(New IndexingConfigStore())
                If Me._countDeletedMessages >= MAX_COUNTDELETEDMESSAGES Then
                    store.ShrinkDb()
                    Me._countDeletedMessages = 0
                End If
                messages = store.ListMessages().Where(Function(mm) Not mm.HasError).ToList()
                EventLog.WriteEntry(messages.Count & " Mensajes a indexar", EventLogEntryType.Information)
            End Using

            For Each m As IMessage In messages
                If Me._stopping Then 'Si el servicio se detiene, debemos actualizar y descartar los mensajes ya procesados
                    For Each mm As IMessage In messagesReaded
                        Using store As New MessageStore.MessageStore(New IndexingConfigStore())
                            If mm.HasError Then
                                Dim newId = store.UpdateMessage(mm)
                                sb.AppendLine(newId.ToString() & " - ERROR: " & mm.MessageError)
                            Else
                                store.DeleteMessage(mm.IdFile)
                                sb.AppendLine(mm.IdFile.ToString() & " - INDEXED")
                            End If
                        End Using
                    Next
                    Exit Sub
                End If
                If m.Body IsNot Nothing Then
                    Dim result = IndexingDocument(m.Body)
                    If Me._deleteErrorMessage Then
                        m.HasError = (Not result.Item1)
                        m.MessageError = result.Item2
                    End If
                Else 'Error de cuerpo vacio
                    If Me._deleteErrorMessage Then
                        m.HasError = True
                        m.MessageError = "El cuerpo del mensaje se encuentra nulo o vacio"
                    End If
                End If
                messagesReaded.Add(m)
            Next

            'Debemos actualizar y descartar los mensajes ya procesados
            For Each mm As IMessage In messagesReaded
                Using store As New MessageStore.MessageStore(New IndexingConfigStore())
                    If mm.HasError Then
                        Dim newId = store.UpdateMessage(mm)
                        sb.AppendLine(newId.ToString() & " - ERROR: " & mm.MessageError)
                    Else
                        store.DeleteMessage(mm.IdFile)
                        Me._countDeletedMessages += 1
                        sb.AppendLine(mm.IdFile.ToString() & " - INDEXED")
                    End If
                End Using
            Next
            sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Termina la ejecución del indexador")
            If messagesReaded.Count > 0 Then
                EventLog.WriteEntry(sb.ToString(), EventLogEntryType.Information)
            End If
        Catch ex As Exception
            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error indexando documento." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la lista de mensajes de excepcion
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <returns>Lista de mensajes en la excepción</returns>
    Private Shared Function GetInnerExceptionMessages(ex As Exception) As String
        Dim list As New StringBuilder()
        GetInnerExceptionMessage(ex, list)
        Return list.ToString()
    End Function

    ''' <summary>
    ''' Obtiene la recursivamente la secuencia de excepciones heredadas
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <param name="list">Lista de mensajes</param>
    Private Shared Sub GetInnerExceptionMessage(ex As Exception, ByRef list As StringBuilder)
        list.AppendLine("Name: " + ex.[GetType]().Name + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace + Environment.NewLine + "<------------------------------------------------------------------>" + Environment.NewLine)
        If ex.InnerException IsNot Nothing AndAlso list.Length < 300 Then
            GetInnerExceptionMessage(ex, list)
        End If
    End Sub

    ''' <summary>
    ''' Registra una excepción en el archivo de debug si se encuentra habilitado
    ''' </summary>
    ''' <param name="message">Mensaje a registrar junto a la excepción</param>
    ''' <param name="ex">Excepción a registrar</param>
    Private Sub ExceptionToDebug(ByVal message As String, ByVal ex As Exception)
        Dim fw As New System.IO.StreamWriter(Me.Debugging(message.Trim()))
        fw.WriteLine(ex.Message & vbCrLf & ex.StackTrace & vbCrLf)
        fw.WriteLine("=======================================================" & vbCrLf)
        Dim aux As Exception = ex.InnerException
        While aux IsNot Nothing
            fw.WriteLine(aux.Message & vbCrLf & aux.StackTrace & vbCrLf)
            fw.WriteLine("=======================================================" & vbCrLf)
            aux = aux.InnerException
        End While
        fw.Flush()
    End Sub

    ''' <summary>
    ''' Escribe un mensaje en el archivo de debug
    ''' </summary>
    ''' <param name="message">Mensaje a escribir</param>
    Private Function Debugging(ByVal message As String) As System.IO.Stream
        Dim pathDebug As String = System.IO.Path.Combine(Environment.CurrentDirectory, "Debug")
        If Not System.IO.Directory.Exists(pathDebug) Then
            System.IO.Directory.CreateDirectory(pathDebug)
        End If
        Dim fs As New System.IO.FileStream(System.IO.Path.Combine(pathDebug, "Agent.debugging"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
        Dim fw As New System.IO.StreamWriter(fs)
        fw.WriteLine(message.Trim() & vbCrLf)
        fw.Flush()
        Return fs
    End Function

    ''' <summary>
    ''' Realiza la deserialización del mensaje y posteriormente la indexación
    ''' </summary>
    ''' <param name="dt">Dataset del mesaje serializado</param>
    ''' <returns></returns>
    Private Function IndexingDocument(ByVal dt As DataSet) As Tuple(Of Boolean, String)
        'Try
        '    Dim dbContainer As String = dt.Tables("Header").Rows(0).Item("DatabaseContainer").ToString()
        '    Dim idxDoc As New IndexedDocument()
        '    With dt.Tables("IndexedDocument")
        '        If .Rows(0).Item("Id") IsNot Nothing Then
        '            idxDoc.Id = Int64.Parse(.Rows(0).Item("Id").ToString())
        '        Else
        '            idxDoc.Id = 0
        '        End If
        '        If .Rows(0).Item("IdDb") IsNot Nothing Then
        '            idxDoc.IdDb = Int64.Parse(.Rows(0).Item("IdDb").ToString())
        '        Else
        '            idxDoc.IdDb = -1
        '        End If
        '        If .Rows(0).Item("Indexes") IsNot Nothing Then
        '            idxDoc.Indexes = Convert.ChangeType(.Rows(0).Item("Indexes"), GetType(String()))
        '        Else
        '            idxDoc.Indexes = New String() {}
        '        End If
        '        If .Rows(0).Item("IdEntity") IsNot Nothing Then
        '            idxDoc.IdEntity = .Rows(0).Item("IdEntity").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad IdEntity en el documeto")
        '        End If
        '        If .Rows(0).Item("IdForm") IsNot Nothing Then
        '            idxDoc.IdForm = .Rows(0).Item("IdForm").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad IdForm en el documeto")
        '        End If
        '        If .Rows(0).Item("IndexingDate") IsNot Nothing Then
        '            idxDoc.IndexingDate = Convert.ToDateTime(.Rows(0).Item("IndexingDate"))
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad IndexingDate en el documeto")
        '        End If
        '        If .Rows(0).Item("DocumentType") IsNot Nothing Then
        '            idxDoc.DocumentType = Int32.Parse(.Rows(0).Item("DocumentType").ToString())
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad DocumentType en el documeto")
        '        End If
        '        If .Rows(0).Item("Extension") IsNot Nothing Then
        '            idxDoc.Extension = .Rows(0).Item("Extension").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad Extension en el documeto")
        '        End If
        '        If .Rows(0).Item("CreationDate") IsNot Nothing Then
        '            idxDoc.CreationDate = Convert.ToDateTime(.Rows(0).Item("CreationDate").ToString())
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad CreationDate en el documeto")
        '        End If
        '        If .Rows(0).Item("Update") IsNot Nothing Then
        '            idxDoc.Update = Convert.ToDateTime(.Rows(0).Item("Update").ToString())
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad Update en el documeto")
        '        End If
        '        If .Rows(0).Item("CreationUser") IsNot Nothing Then
        '            idxDoc.CreationUser = .Rows(0).Item("CreationUser").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad CreationUser en el documeto")
        '        End If
        '        If .Rows(0).Item("UpdateUser") IsNot Nothing Then
        '            idxDoc.UpdateUser = .Rows(0).Item("UpdateUser").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad UpdateUser en el documeto")
        '        End If
        '        If .Rows(0).Item("Title") IsNot Nothing Then
        '            idxDoc.Title = .Rows(0).Item("Title").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad Title en el documeto")
        '        End If
        '        If .Rows(0).Item("Content") IsNot Nothing Then
        '            idxDoc.Content = .Rows(0).Item("Content").ToString()
        '        Else
        '            Return New Tuple(Of Boolean, String)(False, "No se ha especificado la propiedad Content en el documeto")
        '        End If
        '    End With

        '    If Me._client.IsConnected Then
        '        Me._client.Disconnect()
        '    End If
        '    Me._client.Connect()
        '    Me._client.UseDatabase(dbContainer.Trim())

        '    Dim doc As New Futesh.Documents.Document()
        '    If idxDoc.Id > 0 AndAlso idxDoc.IdDb > -1 Then
        '        Dim res = Me._client.Search(idxDoc.IdEntity.Trim(), Nothing, Nothing, Nothing, 0, 1, True)
        '        If res IsNot Nothing AndAlso res.RawCount > 0 Then
        '            doc = res(0)
        '            doc.SetFieldValue("Title", idxDoc.Title)
        '            doc.SetFieldValue("Content", idxDoc.Content)
        '            doc.SetFieldValue("UpdateUser", idxDoc.UpdateUser)
        '            doc.SetFieldValue("Update", idxDoc.Update.Ticks.ToString())
        '        Else
        '            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error consultando el documento que va a ser actualizado en el mensaje (IdEntity: " & idxDoc.IdEntity & ").", EventLogEntryType.Error)
        '            Return New Tuple(Of Boolean, String)(False, "")
        '        End If
        '    Else
        '        doc = Futesh.Documents.Document.Create(Of IndexedDocument)(idxDoc)
        '    End If

        '    If doc.State = Document.DocumentState.Unchanged OrElse doc.State = Document.DocumentState.Modified Then
        '        If doc.State = Document.DocumentState.Unchanged Then
        '            doc.SetFieldValue("Content", doc.GetFieldValue("Content") & ".")
        '        End If
        '        Dim tmp As Futesh.Documents.Document = Me._client.UpdateDocument(doc)
        '        If tmp.State = Document.DocumentState.Unchanged Then
        '            Return New Tuple(Of Boolean, String)(True, String.Empty)
        '        Else
        '            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error actualizando el documento en el mensaje (" & MSMQManagement.LabelLastMessage & ").", EventLogEntryType.Error)
        '            Return New Tuple(Of Boolean, String)(False, "")
        '        End If
        '    Else
        '        Dim tmp As Futesh.Documents.Document = Me._client.AddDocument(doc)
        '        If tmp.State = Document.DocumentState.Unchanged Then
        '            Return New Tuple(Of Boolean, String)(True, String.Empty)
        '        Else
        '            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - (IndexingDocument) Error indexando el documento en el mensaje (" & MSMQManagement.LabelLastMessage & "). Estado del documento (" & tmp.State.ToString() & ")", EventLogEntryType.Error)
        '            Return New Tuple(Of Boolean, String)(False, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - (IndexingDocument) Error indexando el documento en el mensaje (" & MSMQManagement.LabelLastMessage & "). Estado del documento (" & tmp.State.ToString() & ")")
        '        End If
        '    End If
        '    Return New Tuple(Of Boolean, String)(True, String.Empty)
        'Catch ex As Exception
        '    EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error deserializando el mensaje y enviando el documento." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, EventLogEntryType.Error)
        '    Return New Tuple(Of Boolean, String)(False, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error deserializando el mensaje y enviando el documento." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace)
        'End Try
        Return New Tuple(Of Boolean, String)(False, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error deserializando el mensaje y enviando el documento.")
    End Function

    ''' <summary>
    ''' Carga la configuración desde el archivo
    ''' </summary>
    ''' <returns>Un valor que indica si se cargo correctamente</returns>
    Private Function LoadConfig() As Boolean
        Try
            EventLog.WriteEntry("Se va a leer la cadena de conexión", EventLogEntryType.Warning)
            Dim conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            If conf.ConnectionStrings.ConnectionStrings("Futesh") IsNot Nothing Then
                If Regex.IsMatch(conf.ConnectionStrings.ConnectionStrings("Futesh").ConnectionString.Trim(), IndexerAgent.RegEx_Server) Then
                    Me._connectionString = conf.ConnectionStrings.ConnectionStrings("Futesh").ConnectionString.Trim()
                    EventLog.WriteEntry("Cadena de conexión: " & Me._connectionString, EventLogEntryType.Information)
                    If conf.AppSettings.Settings("Interval") IsNot Nothing Then
                        Int32.TryParse(conf.AppSettings.Settings("Interval").Value.Trim(), Me._interval)
                        If Me._interval = 0 Then
                            Me._interval = 5000
                            EventLog.WriteEntry("No se ha configurado un intervalo válido, por tanto se usara el valor por defecto (5 segundos).", EventLogEntryType.Error)
                            Return True
                        End If
                        Me._client = Client.Client.Create(Futesh.Configuration.Version.FuteshV200, "Futesh")
                        EventLog.WriteEntry("Se realizó la conexión al servidor Futesh.", EventLogEntryType.Information)
                        Me.TmrRelay.Interval = Double.Parse(Me._interval * 1000)
                        Return True
                    Else
                        Me._interval = 5000
                        EventLog.WriteEntry("No se ha configurado un intervalo, por tanto se usara el valor por defecto (5 segundos).", EventLogEntryType.Error)
                        Return True
                    End If
                    If conf.AppSettings.Settings("DeleteErrorMessage") Is Nothing Then
                        Me._deleteErrorMessage = True
                    Else
                        Boolean.TryParse(conf.AppSettings.Settings("DeleteErrorMessage").Value, Me._deleteErrorMessage)
                    End If
                Else
                    EventLog.WriteEntry("Cadena de conexión mal formada.", EventLogEntryType.Error)
                    Return False
                End If
            Else
                EventLog.WriteEntry("No existe la cadena de conexión al motor de indexación.", EventLogEntryType.Error)
                Return False
            End If
        Catch ex As Exception
            EventLog.WriteEntry("Error al cargar la configuración." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, EventLogEntryType.Error)
            Return False
        End Try
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se ejecuta la logica del agente
    ''' </summary>
    Private Sub TmrRelay_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles TmrRelay.Elapsed
        Me.TmrRelay.Stop()
        Me.RunIndexer()
        Me.TmrRelay.Start()
    End Sub

#End Region

End Class
