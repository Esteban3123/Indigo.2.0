Public Class MyProcess
    Property DescriptionProcess As String
    Property NameProcess As String
    Property IconProcess As String
    ReadOnly Property Icono As Byte()
        Get
            Return Convert.FromBase64String(IconProcess)
        End Get
    End Property
End Class
