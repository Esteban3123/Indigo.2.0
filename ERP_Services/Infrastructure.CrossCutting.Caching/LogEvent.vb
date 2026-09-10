'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Registrar evento
''' </summary>
Public NotInheritable Class LogEvent

    ''' <summary>
    ''' Logs the event.	
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub LogEvent()
        Dim eventViewer As New EventLog
        eventViewer.Log = "Nombre de la solucion"
        eventViewer.MachineName = "."
        eventViewer.Source = "Nombre de la solucion"
        Diagnostics.EventLog.WriteEntry(eventViewer.Source, "Se ha Presentado un Problema al intentar Establecer comunicacion con el Servidor de Cache", EventLogEntryType.Error)
    End Sub

End Class