'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Logging
' Author           : WalterSierra
' Created          : 15-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-26
' Description      :  Clase encargada de grabar las trazas de la aplicacion
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
#End Region

''' <summary>
''' Clase encargada de grabar las trazas de la aplicacion
''' </summary>
Public NotInheritable Class IndigoLogging

    ''' <summary>
    ''' Escribe en el repositorio de log un mensaje de tipo informacion
    ''' </summary>
    ''' <param name="message">el message.</param>
    ''' <param name="prioriry">la prioridad. (enumeracion)</param>
    ''' <remarks></remarks>
    Public Shared Sub LogInformationMessage(ByVal message As String, Optional ByVal prioriry As Priority = Priority.Normal)
        If String.IsNullOrEmpty(message) = True Then
            Throw New ArgumentNullException("Mensaje vacio")
        End If
        Dim writer As LogWriter = EnterpriseLibraryContainer.Current.GetInstance(Of LogWriter)()
        Dim log As LogEntry = New LogEntry
        With log
            .Severity = TraceEventType.Information
            .Title = "Mensage Informativo"
        End With
        log.Message = message
        log.Categories.Add(Categories.General)
        log.Priority = prioriry
        writer.Write(log)
    End Sub

    ''' <summary>
    ''' Logs the warning message.	
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="priority">The prioridad.</param>
    ''' <remarks></remarks>
    Public Shared Sub LogWarningMessage(ByVal message As String, Optional ByVal priority As Priority = Priority.High)
        If String.IsNullOrEmpty(message) = True Then
            Throw New ArgumentNullException("Mensaje vacio")
        End If
        Dim writer As LogWriter = EnterpriseLibraryContainer.Current.GetInstance(Of LogWriter)()
        Dim log As LogEntry = New LogEntry
        With log
            .Severity = TraceEventType.Warning
            .Title = "Mensage de Advertencia"
        End With
        log.Message = message
        log.Categories.Add(Categories.General)
        log.Priority = priority
        writer.Write(log)
    End Sub

    ''' <summary>
    ''' Logs the critical message.	
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="priority">The prioridad.</param>
    ''' <remarks></remarks>
    Public Shared Sub LogCriticalMessage(ByVal message As String, Optional ByVal priority As Priority = Priority.VeryHigh)
        If String.IsNullOrEmpty(message) = True Then
            Throw New ArgumentNullException("Mensaje vacio")
        End If
        Dim writer As LogWriter = EnterpriseLibraryContainer.Current.GetInstance(Of LogWriter)()
        Dim log As LogEntry = New LogEntry
        With log
            .Severity = TraceEventType.Critical
            .Title = "Mensage Critico"
        End With
        log.Message = message
        log.Categories.Add(Categories.General)
        log.Priority = priority
        writer.Write(log)
    End Sub

End Class
