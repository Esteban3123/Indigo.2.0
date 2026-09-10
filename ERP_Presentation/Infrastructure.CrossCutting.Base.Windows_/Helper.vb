'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-12-26
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-26
' Description      : Provee servicios de ayuda
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration

#End Region

''' <summary>
''' Provee servicios de ayuda en la capa trasversal
''' </summary>
Public NotInheritable Class Helper

#Region "Properties"

    ''' <summary>
    ''' Nombre de la funte
    ''' </summary>
    Private Const SOURCE As String = "Indigo Vie - XPO"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el nombre de la aplicacion
    ''' </summary>
    ''' <returns>Nombre de la aplicacion</returns>
    Public Shared Function GetAppName() As String
        Return My.Application.Info.ProductName
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la compañia quien desarrollo la aplicación
    ''' </summary>
    ''' <returns>Nombre de la compañia</returns>
    Public Shared Function GetCompanyName() As String
        Return My.Application.Info.CompanyName
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos comunes
    ''' </summary>
    ''' <returns>Ruta de archivos comunes</returns>
    Public Shared Function GetPathApplicationFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(Window.Utils.LocalFolder(), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(Utils.UserFolder(), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

#End Region

#Region "Get Config XPO"

    Public Shared Function GetXpoConnectionString() As String
        Try
            Dim conn = ConfigurationManager.ConnectionStrings("XpoConnection")
            If conn IsNot Nothing Then
                Return conn.ToString().Trim()
            End If
            Return String.Empty
        Catch ex As Exception
            HandledException(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetXpoConnectionStringByConnectionKey() As String
        Try
            Dim conn = ConfigurationManager.ConnectionStrings(
                ConfigurationManager.AppSettings.Get("XpoConnectionName"))
            If conn IsNot Nothing Then
                Return conn.ToString().Trim()
            End If
            Return String.Empty
        Catch ex As Exception
            HandledException(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function IsCacheEnabled() As Boolean
        Dim cached As String = ConfigurationManager.AppSettings.Get("CachedEnable")
        Return Not String.IsNullOrEmpty(cached) AndAlso cached.ToLower().Equals("true")
    End Function

#End Region

#Region "Error Logging"

    ''' <summary>
    ''' Da manejo a las excepciones ocurridas en el servicio XPO
    ''' </summary>
    ''' <param name="ex">Excepción a manejar</param>
    ''' <returns>Valor que indica si se le dio manejo a la excepción</returns>
    Public Shared Function HandledException(ByVal ex As System.Exception, Optional level As EventLogEntryType = EventLogEntryType.Error) As Boolean
        Try
            If Not EventLog.SourceExists(SOURCE) Then
                EventLog.CreateEventSource(SOURCE, "Application")
            End If

            Dim CustomEventLog As New EventLog()
            CustomEventLog.Source = SOURCE
            CustomEventLog.WriteEntry(GetExceptionDetails(ex), level)
            Return True
        Catch e As Exception
            Using eventLog As EventLog = New EventLog("Application")
                eventLog.Source = "Application"
                eventLog.WriteEntry(e.Message, EventLogEntryType.Warning)
                eventLog.WriteEntry(GetExceptionDetails(ex), level)
                Return False
            End Using
        End Try
    End Function

    Private Shared Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

End Class
