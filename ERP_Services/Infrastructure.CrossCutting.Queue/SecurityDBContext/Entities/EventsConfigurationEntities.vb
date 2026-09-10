Public Class EventsConfigurationEntities

    Public Property Id() As Integer
    Public Property Code() As String
    Public Property ContainerId() As Integer
    Public Property UrlQueue() As String
    Public Property Topic() As String
    Public Property Status() As Boolean

    Public Overridable Property Containers() As ContainersEntities
End Class
