Imports System.Collections.Concurrent
Imports Domain.Entities

Public Class HomologationCupsEventArgs
    Inherits EventArgs

    Property ListHomologations As List(Of List(Of CupsHomologation))
    Property IsProcedureQx As Boolean
    Property arguments As Object
    Property NewAdmission As Object
    Property CareCenterCode As String
    Property listDetail As ConcurrentBag(Of Object)
    Property CareGroupId As Integer
    Property AuthorizationNumber As String

End Class
