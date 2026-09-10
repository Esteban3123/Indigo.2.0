Public Class MiMenuIndigo
    Private _TagFormulario As String
    Private _NombreFormulario As String
    Private _Tipo As String
    Private _isLarge As Boolean
    Private _Grupo As String
    Private _TilePosicion As Integer
    Private _ModuleGroup As Integer

    Public Property TagFormulario As String
        Get
            Return _TagFormulario
        End Get
        Set(value As String)
            _TagFormulario = value
        End Set
    End Property

    Public Property NombreFormulario As String
        Get
            Return _NombreFormulario
        End Get
        Set(value As String)
            _NombreFormulario = value
        End Set
    End Property

    Public Property Tipo As String
        Get
            Return _Tipo
        End Get
        Set(value As String)
            _Tipo = value
        End Set
    End Property

    Public Property IsLarge As Boolean
        Get
            Return _isLarge
        End Get
        Set(value As Boolean)
            _isLarge = value
        End Set
    End Property

    Public Property Grupo As String
        Get
            Return _Grupo
        End Get
        Set(value As String)
            _Grupo = value
        End Set
    End Property

    Public Property TilePosicion As Integer
        Get
            Return _TilePosicion
        End Get
        Set(value As Integer)
            _TilePosicion = value
        End Set
    End Property

    Public Property ModuleGroup As Integer
        Get
            Return _ModuleGroup
        End Get
        Set(value As Integer)
            _ModuleGroup = value
        End Set
    End Property
End Class
