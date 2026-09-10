

Module Module1

    Sub Main()
        Dim sw = New SWElectronicDocuments()
        sw.ExecuteProcess().Wait()

        'Dim Service1 As New System.ServiceProcess.ServiceBase
        'Dim serviceToRun As New SWElectronicDocuments()

        'If Environment.UserInteractive Then
        '    serviceToRun.Run(Service1)
        '    Console.WriteLine("Press any key to stop the service")
        '    Console.Read()
        '    serviceToRun.Stop()
        'Else
        '    ServiceBase.Run(serviceToRun)
        'End If
    End Sub

End Module