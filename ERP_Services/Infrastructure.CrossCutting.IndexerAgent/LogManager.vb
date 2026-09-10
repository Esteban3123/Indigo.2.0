Imports System.Text

Public Class LogManager

    ''' <summary>
    ''' Nombre de la funte
    ''' </summary>
    Private Const SOURCE As String = "IndexerAgent"

    ''' <summary>
    ''' Escribe una entrada en el log de eventos
    ''' para una excepción ocurrida
    ''' </summary>
    ''' <param name="ex">Excepción a registrar</param>
    Public Shared Sub WriteEntry(ex As Exception)
        Dim message As String = GetInnerExceptionMessages(ex)
        WriteEntry(message, EventLogEntryType.[Error])
    End Sub

    ''' <summary>
    ''' Escribe una entrada en el log de eventos
    ''' </summary>
    ''' <param name="message">Mensaje a escribir</param>
    ''' <param name="level">Nivel del evento</param>
    Public Shared Sub WriteEntry(message As String, level As EventLogEntryType)
        If Not EventLog.SourceExists(SOURCE) Then
            CreateLog()
        End If

        Dim CustomEventLog As New EventLog()
        CustomEventLog.Source = SOURCE
        CustomEventLog.Log = SOURCE
        CustomEventLog.WriteEntry(message, level)
    End Sub

    ''' <summary>
    ''' Crea el log para el registro de eventos del servicio
    ''' </summary>
    Private Shared Sub CreateLog()
        EventLog.CreateEventSource(SOURCE, SOURCE)
        Dim CustomEventLog As New EventLog()

        CustomEventLog.Source = SOURCE
        CustomEventLog.Log = SOURCE

        CustomEventLog.WriteEntry("The " + SOURCE + " was successfully initialize component.", EventLogEntryType.Information)
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

End Class
