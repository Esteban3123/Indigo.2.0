Module Module1

    Sub Main()
        Dim service As New Infrastructure.CrossCutting.AuditServices.IndigoAuditService
        service.Start()
        Console.ReadKey()
        Console.ReadKey()
    End Sub

End Module
