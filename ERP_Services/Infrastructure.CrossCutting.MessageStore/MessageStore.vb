'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Store.IO
Imports System.IO

#End Region

''' <summary>
''' Encapsula la logica necesaria para la manipulación
''' de un almacén de mensajes
''' </summary>
Public Class MessageStore
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Encapsula la configuración del almacén de mensajes
    ''' </summary>
    Private _configStore As IConfigStore

    ''' <summary>
    ''' Base de datos de mensajes
    ''' </summary>
    Private _fileDb As FileDB

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la configuración del almacén de mensajes
    ''' </summary>
    ''' <returns>Configuración del almacén de mensajes</returns>
    Public ReadOnly Property ConfigStore As IConfigStore
        Get
            Return Me._configStore
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="configStore">Configuración del almacén de mensajes</param>
    Public Sub New(ByVal configStore As IConfigStore)
        Me._configStore = configStore
        Me._fileDb = New FileDB(Me._configStore.PathFileStore, FileAccess.ReadWrite)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Actualiza un mensaje
    ''' </summary>
    ''' <param name="mess">Mensaje a actualizar</param>
    ''' <returns>Nuevo IdFile del mensaje actualizado</returns>
    Public Function UpdateMessageAsync(ByVal mess As IMessage) As Task(Of Guid)
        Return Task.Factory.StartNew(Of Guid)(Function()
                                                  Return Me.UpdateMessage(mess)
                                              End Function)
    End Function

    ''' <summary>
    ''' Actualiza un mensaje
    ''' </summary>
    ''' <param name="mess">Mensaje a actualizar</param>
    ''' <returns>Nuevo IdFile del mensaje actualizado</returns>
    Public Function UpdateMessage(ByVal mess As IMessage) As Guid
        If Not mess.IdFile.Equals(Guid.Empty) Then
            If GetMessage(mess.IdFile) IsNot Nothing Then
                Return AddMessage(mess)
            End If
        End If
        Return Guid.Empty
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de mensajes en el almacén de forma asíncrona
    ''' </summary>
    ''' <returns>Cantidad de mensajes en el almacén</returns>
    Public Function GetCountMessageAsync() As Task(Of Integer)
        Return Task.Factory.StartNew(AddressOf GetCountMessage)
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de mensajes en el almacén
    ''' </summary>
    ''' <returns>Cantidad de mensajes en el almacén</returns>
    Public Function GetCountMessage() As Integer
        Return Me._fileDb.ListFiles().Length
    End Function

    ''' <summary>
    ''' Lista todos los mensajes del almacén de forma asíncrona
    ''' </summary>
    ''' <returns>Lista de mensajes</returns>
    Public Function ListMessagesAsync() As Task(Of IEnumerable(Of IMessage))
        Return Task.Factory.StartNew(AddressOf ListMessages)
    End Function

    ''' <summary>
    ''' Lista todos los mensajes del almacén
    ''' </summary>
    ''' <returns>Lista de mensajes</returns>
    Public Function ListMessages() As IEnumerable(Of IMessage)
        Dim files As New List(Of IMessage)()
        Try
            For Each entry As EntryInfo In Me._fileDb.ListFiles()
                Using ms As New MemoryStream()
                    Me._fileDb.Read(entry.ID, ms)
                    Dim mess As IMessage = Message.FromStream(ms)
                    mess.IdFile = entry.ID
                    mess.Size = entry.FileLength
                    files.Add(mess)
                End Using
            Next
            Return files
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return files
        End Try
    End Function

    ''' <summary>
    ''' Exporta los archivos a un directorio
    ''' </summary>
    ''' <param name="directory">Ruta del directorio donde exportar</param>
    Public Sub Export(ByVal directory As String)
        Me._fileDb.Export(directory)
    End Sub

    ''' <summary>
    ''' Lista todos los mensajes vacios en el almacén
    ''' </summary>
    ''' <returns>Lista de mensajes vacios</returns>
    Public Function ListEmptyMessages() As IEnumerable(Of Guid)
        Dim files As New List(Of Guid)()
        For Each entry As EntryInfo In Me._fileDb.ListFiles()
            Using ms As New MemoryStream()
                Me._fileDb.Read(entry.ID, ms)
                ms.Flush()
                Dim b = ms.GetBuffer()
                If b(0) = 0 Then
                    files.Add(entry.ID)
                End If
            End Using
        Next
        Return files
    End Function

    ''' <summary>
    ''' Agrega un mensaje al almacén de forma asíncrona
    ''' </summary>
    ''' <param name="mess">Mensaje a agregar</param>
    ''' <returns>Id interno del mensaje</returns>
    Public Function AddMessageAsync(ByVal mess As IMessage) As Task(Of Guid)
        Return Task.Factory.StartNew(Of Guid)(Function()
                                                  Return AddMessage(mess)
                                              End Function)
    End Function

    ''' <summary>
    ''' Agrega un mensaje al almacén
    ''' </summary>
    ''' <param name="mess">Mensaje a agregar</param>
    ''' <returns>Id interno del mensaje</returns>
    Public Function AddMessage(ByVal mess As IMessage) As Guid
        Dim ms = Message.FromMessage(mess)
        Dim entry As EntryInfo = Me._fileDb.Store(mess.Title, ms)
        Return entry.ID
    End Function

    ''' <summary>
    ''' Lee el primer mensaje sin eliminarlo del almacén de forma asíncrona
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function ReadMessageAsync() As Task(Of IMessage)
        Return Task.Factory.StartNew(AddressOf ReadMessage)
    End Function

    ''' <summary>
    ''' Lee el primer mensaje sin eliminarlo del almacén
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function ReadMessage() As IMessage
        Dim entry As EntryInfo = Me._fileDb.ListFiles().FirstOrDefault()
        If entry IsNot Nothing Then
            Using ms As New MemoryStream()
                Me._fileDb.Read(entry.ID, ms)
                Dim mess As IMessage = Message.FromStream(ms)
                mess.IdFile = entry.ID
                mess.Size = entry.FileLength
                Return mess
            End Using
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Lee el primer mensaje eliminandolo del almacén de forma asíncrona
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function GetMessageAsync() As Task(Of IMessage)
        Return Task.Factory.StartNew(AddressOf GetMessage)
    End Function

    ''' <summary>
    ''' Lee el primer mensaje eliminandolo del almacén
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function GetMessage() As IMessage
        Dim entry As EntryInfo = Me._fileDb.ListFiles().FirstOrDefault()
        Dim mess As IMessage = Nothing
        If entry IsNot Nothing Then
            Using ms As New MemoryStream()
                Me._fileDb.Read(entry.ID, ms)
                mess = Message.FromStream(ms)
                mess.IdFile = entry.ID
                mess.Size = entry.FileLength
            End Using
            Me._fileDb.Delete(entry.ID)
            Me._fileDb.Shrink()
        End If
        Return mess
    End Function

    ''' <summary>
    ''' Lee el un mensaje eliminandolo del almacén de forma asíncrona
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function GetMessageAsync(ByVal idFile As Guid) As Task(Of IMessage)
        Return Task.Factory.StartNew(Of IMessage)(Function()
                                                      Return GetMessage(idFile)
                                                  End Function)
    End Function

    ''' <summary>
    ''' Lee el un mensaje eliminandolo del almacén
    ''' </summary>
    ''' <returns>Mensaje leido</returns>
    Public Function GetMessage(ByVal idFile As Guid) As IMessage
        Dim entry As EntryInfo = Me._fileDb.ListFiles().Where(Function(m) m.ID.Equals(idFile)).FirstOrDefault()
        Dim mess As IMessage = Nothing
        If entry IsNot Nothing Then
            Using ms As New MemoryStream()
                Me._fileDb.Read(entry.ID, ms)
                mess = Message.FromStream(ms)
                mess.IdFile = entry.ID
                mess.Size = entry.FileLength
            End Using
            Me._fileDb.Delete(entry.ID)
            Me._fileDb.Shrink()
        End If
        Return mess
    End Function

    ''' <summary>
    ''' Reorganiza y comprime los espacios en blanco cuando
    ''' se ha eliminado muchos mensajes
    ''' </summary>
    Public Sub ShrinkDb()
        Me._fileDb.Shrink()
    End Sub

    ''' <summary>
    ''' Reorganiza y comprime asíncronamente los espacios en blanco cuando
    ''' se ha eliminado muchos mensajes
    ''' </summary>
    Public Function ShrinkDbAsync() As Task
        Return Task.Factory.StartNew(AddressOf ShrinkDb)
    End Function

    ''' <summary>
    ''' Elimina un mensaje por su Id
    ''' </summary>
    ''' <param name="idFile">Id interno del mensaje en el almacén</param>
    ''' <returns>Valor que indica si el mensaje se eliminó</returns>
    Public Function DeleteMessage(ByVal idFile As Guid) As Boolean
        Dim result = Me._fileDb.Delete(idFile)
        Return result
    End Function

    ''' <summary>
    ''' Elimina un mensaje de forma asíncrona
    ''' </summary>
    ''' <param name="mess">Mensaje a eliminar</param>
    ''' <returns>Valor que indica si el mensaje se eliminó</returns>
    Public Function DeleteMessageAsync(ByVal mess As IMessage) As Task(Of Boolean)
        Return Task.Factory.StartNew(Of Boolean)(Function()
                                                     Return DeleteMessage(mess.IdFile)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Elimina un mensaje
    ''' </summary>
    ''' <param name="mess">Mensaje a eliminar</param>
    ''' <returns>Valor que indica si el mensaje se eliminó</returns>
    Public Function DeleteMessage(ByVal mess As IMessage) As Boolean
        Return DeleteMessage(mess.IdFile)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
            Me._fileDb.Dispose()
            Me._fileDb = Nothing
            Me._configStore = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class