Imports DistributedServices.ElectronicDocuments
Imports Infrastructure.CrossCutting.Base

Public Class SWSendElectronicDocuments

    Public timer As New Timers.Timer
    Private containers() As String
    Private containerSecurity As String
    ''' <summary>
    ''' nombre del blob de azure
    ''' </summary>
    Private electronicBlobContainerName As String

    ''' <summary>
    ''' cadena de conexion del blob de azure
    ''' </summary>
    Private currentBlobConnectionString As String

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            Dim appSettings = Configuration.ConfigurationManager.AppSettings

            If appSettings.Count = 0 Then
                EventLog.WriteEntry("El archivo de configuración esta vacio", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_Intervals_")) Then
                EventLog.WriteEntry("No se ha configurado un intervalo de tiempo para ejecutarse el servicio.", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_Containers_")) Then
                EventLog.WriteEntry("No se ha configurado los contenedores a consultar.", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_ContainerSecurity_")) Then
                EventLog.WriteEntry("No se ha configurado el contenedor de seguridad.", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("FxGetUrlNotification")) Then
                EventLog.WriteEntry("No se ha configurado la ruta del servicio de Azure.", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("FxEmailNotification")) Then
                EventLog.WriteEntry("No se ha configurado la URL de la API.", EventLogEntryType.Warning)
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("FromEmailNotification")) Then
                EventLog.WriteEntry("No se ha configurado el remitente del correo electronico.", EventLogEntryType.Warning)
                Exit Sub
            End If

            containers = Split(appSettings("_Containers_"), ",")
            containerSecurity = appSettings("_ContainerSecurity_")

            currentBlobConnectionString = appSettings("AzureBlobConnectionString")
            electronicBlobContainerName = appSettings("BlobContainerName")

            timer = New Timers.Timer()
            AddHandler timer.Elapsed, AddressOf OnElapsedTime
            timer.Interval = Convert.ToDouble(appSettings("_Intervals_"))
            timer.Enabled = True
            timer.Start()
        Catch ex As Exception
            EventLog.WriteEntry("Error iniciando el Servicio para la generación y envío de Documentos Electrónicos.", EventLogEntryType.Error)
        End Try
    End Sub

    Private Async Sub OnElapsedTime(sender As Object, e As Timers.ElapsedEventArgs)
        Try
            timer.Enabled = False

            Await ExecuteProcess()

            timer.Enabled = True
        Catch ex As Exception
            timer.Enabled = True
        End Try
    End Sub

    Private Async Function ExecuteProcess() As Task
        If containers IsNot Nothing AndAlso containers.Count > 0 Then
            For Each container As String In containers
                If Not String.IsNullOrEmpty(container) Then
                    ServerSessionValues.Current.CurrentContainer = container.Trim()
                    SessionValues.Instance.TransactionalContainer = container.Trim()
                    SessionValues.Instance.SecurityContainer = containerSecurity.Trim()
                    ServerSessionValues.Current.CurrentBlobConnectionString = currentBlobConnectionString
                    ServerSessionValues.Current.BlobContainerName = electronicBlobContainerName
                    Using service As IElectronicDocumentsService = New ElectronicDocumentsService
                        Dim response = Await service.ExecuteSendMailProcess()
                        If response.StateResult = False Then
                            EventLog.WriteEntry(response.Message, EventLogEntryType.Error)
                        End If
                    End Using
                End If
            Next
        End If
    End Function

    Protected Overrides Sub OnStop()
        timer.Stop()
    End Sub

End Class
