Public Class Container

    Private _name As String
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _company As String
    Property Company As String
        Get
            Return _company
        End Get
        Set(value As String)
            _company = value
        End Set
    End Property

    Private _code As String
    Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
        End Set
    End Property

End Class
