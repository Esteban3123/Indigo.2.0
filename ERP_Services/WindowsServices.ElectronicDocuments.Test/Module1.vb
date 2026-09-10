Module Module1

    Public timer As New Timers.Timer

    Sub Main()
        Try
            Dim appSettings = Configuration.ConfigurationManager.AppSettings

            If appSettings.Count = 0 Then
                Console.WriteLine("El archivo de configuración esta vacio")
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_Intervals_")) Then
                Console.WriteLine("No se ha configurado un intervalo de tiempo para ejecutarse el servicio.")
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_Containers_")) Then
                Console.WriteLine("No se ha configurado los contenedores a consultar.")
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("_PathElectronicDocuments_")) Then
                Console.WriteLine("No se ha configurado la ruta donde se almacenaran los documentos electronicos.")
            End If

            If String.IsNullOrEmpty(appSettings("_ContainerSecurity_")) Then
                Console.WriteLine("No se ha configurado el contenedor de seguridad.")
                Exit Sub
            End If

            If String.IsNullOrEmpty(appSettings("FxGetUrlNotification")) Then
                Console.WriteLine("No se ha configurado la ruta del servicio de Azure.")
            End If

            If String.IsNullOrEmpty(appSettings("FxEmailNotification")) Then
                Console.WriteLine("No se ha configurado la URL de la API.")
            End If

            If String.IsNullOrEmpty(appSettings("FromEmailNotification")) Then
                Console.WriteLine("No se ha configurado el remitente del correo electronico.")
            End If

            If String.IsNullOrEmpty(appSettings("AzureBlobConnectionString")) Then
                Console.WriteLine("No se ha configurado la conexion de blobstorage.")
            End If

            If String.IsNullOrEmpty(appSettings("BlobContainerName")) Then
                Console.WriteLine("No se ha configurado el contenedor de blob.")
            End If

            SWElectronicDocuments.CurrentBlobConnectionString = appSettings("AzureBlobConnectionString")
            SWElectronicDocuments.ElectronicBlobContainerName = appSettings("BlobContainerName")
            SWElectronicDocuments.containers = Split(appSettings("_Containers_"), ",")
            SWElectronicDocuments.containerSecurity = appSettings("_ContainerSecurity_")

            Dim sw As New SWElectronicDocuments
            sw.ExecuteProcess().Wait()
        Catch ex As Exception
            timer.Stop()
            Console.WriteLine("Error iniciando el Servicio para la generación y envío de Documentos Electrónicos.")
        End Try
    End Sub

End Module
