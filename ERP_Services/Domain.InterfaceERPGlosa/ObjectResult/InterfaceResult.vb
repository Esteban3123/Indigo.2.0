Public Class InterfaceResult

    Dim _Message As String
    Dim _Result As Boolean
    Dim _consecutive As String
    Dim _invoice As String
    Dim _Account As String
    Public Sub New(message As String, Result As Boolean, Optional ByVal Consecutive As String = "", Optional ByVal invoice As String = "", Optional ByVal Account As String = "")
        _Message = message
        _Result = Result
        _consecutive = Consecutive
        _invoice = invoice
        _Account = Account
    End Sub

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property Message As String
        Get
            Return _Message
        End Get
        Set(value As String)
            _Message = value
        End Set
    End Property

    Public Property Result As Boolean
        Get
            Return _Result
        End Get
        Set(value As Boolean)
            _Result = value
        End Set
    End Property

    Public Property Consecutive As String
        Get
            Return _consecutive
        End Get
        Set(value As String)
            _consecutive = value
        End Set
    End Property

    Public Property Invoice As String
        Get
            Return _invoice
        End Get
        Set(value As String)
            _invoice = value
        End Set
    End Property


    Public Property Account As String
        Get
            Return _Account
        End Get
        Set(value As String)
            _Account = value
        End Set
    End Property
 
End Class
