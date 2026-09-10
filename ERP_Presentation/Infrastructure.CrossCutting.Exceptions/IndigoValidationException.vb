Public Class IndigoValidationException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class


Public Class IndigoTokenException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class
