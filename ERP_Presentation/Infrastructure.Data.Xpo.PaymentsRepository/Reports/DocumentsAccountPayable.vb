Public Class DocumentsAccountPayable

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            fCode = value
        End Set
    End Property

End Class
