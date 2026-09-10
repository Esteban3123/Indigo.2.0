Imports DistributedServices.ElectronicDocuments
Imports Infrastructure.CrossCutting.Base

Public Class SWElectronicDocuments

    Public Shared containers() As String

    Public Shared containerSecurity As String

    ''' <summary>
    ''' nombre del blob de azure
    ''' </summary>
    Public Shared ElectronicBlobContainerName As String

    ''' <summary>
    ''' cadena de conexion del blob de azure
    ''' </summary>
    Public Shared CurrentBlobConnectionString As string

    Public Async Sub OnElapsedTime(sender As Object, e As Timers.ElapsedEventArgs)
        Try
            timer.Enabled = False

            Await ExecuteProcess()

            timer.Enabled = True
        Catch ex As Exception
            timer.Enabled = True
        End Try
    End Sub

    Public Async Function ExecuteProcess() As Task
        If containers IsNot Nothing AndAlso containers.Count > 0 Then
            For Each container As String In containers
                If Not String.IsNullOrEmpty(container) Then
                    ServerSessionValues.Current.CurrentContainer = container.Trim()
                    ServerSessionValues.Current.CurrentBlobConnectionString = CurrentBlobConnectionString
                    ServerSessionValues.Current.BlobContainerName = ElectronicBlobContainerName
                    SessionValues.Instance.TransactionalContainer = container.Trim()
                    SessionValues.Instance.SecurityContainer = containerSecurity.Trim()
                    Using service As IElectronicDocumentsService = New ElectronicDocumentsService
                        Dim response = Await service.ExecuteProcess()
                        If response.StateResult = False Then
                            Console.WriteLine(response.Message)
                            Console.ReadKey()
                        End If
                    End Using
                End If
            Next
        End If
    End Function

End Class
