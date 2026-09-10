''' <summary>
''' clase para listar el detalle del equipo cuando se hace su respectivo registro
''' </summary>
''' <remarks></remarks>
Public Class DetailEquipment


    Private _Id As Integer
    Property Id As Integer
        Get
            Return _Id
        End Get
        Set(value As Integer)
            _Id = value
        End Set
    End Property


    Private _Code As String
    Property Code As String
        Get
            Return _Code
        End Get
        Set(value As String)
            _Code = value
        End Set
    End Property


    Private _Type As String
    Property Type As String
        Get
            Return _Type
        End Get
        Set(value As String)
            _Type = value
        End Set
    End Property



    Private _Comment As String
    Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            _Comment = value
        End Set
    End Property





    Private _Name As String
    Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property
End Class
