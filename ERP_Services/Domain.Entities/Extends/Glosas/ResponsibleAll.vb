Public Class ResponsibleAll

    Private _Id As Integer
    Property Id As Integer
        Get
            Return _Id
        End Get
        Set(value As Integer)
            _Id = value
        End Set
    End Property

    Private _responsibleCode As String
    Property responsibleCode As String
        Get
            Return _responsibleCode
        End Get
        Set(value As String)
            _responsibleCode = value
        End Set
    End Property



    Private _responsibleName As String
    Property responsibleName As String
        Get
            Return _responsibleName
        End Get
        Set(value As String)
            _responsibleName = value
        End Set
    End Property


    Private _responsibleCodeName As String
    Property responsibleCodeName As String
        Get
            Return _responsibleCodeName
        End Get
        Set(value As String)
            _responsibleCodeName = value
        End Set
    End Property

    Private _responsible As Responsible
    Property responsible As Responsible
        Get
            Return _responsible
        End Get
        Set(value As Responsible)
            Me._responsible = value
        End Set
    End Property

End Class
