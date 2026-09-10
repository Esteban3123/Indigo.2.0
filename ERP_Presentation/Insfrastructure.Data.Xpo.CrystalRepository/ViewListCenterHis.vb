
Imports DevExpress.Xpo

<Persistent("dbo.ViewListCenterHis")>
Public Class ViewListCenterHis
    Inherits XPLiteObject

    Dim fCodigo As String
    <Key()>
    Public Property Codigo() As String
        Get
            Return fCodigo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Codigo", fCodigo, value)
        End Set
    End Property

    Dim fCentroAtencion As String
    Public Property CentroAtencion() As String
        Get
            Return fCentroAtencion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CentroAtencion", fCentroAtencion, value)
        End Set
    End Property

    Dim fCodigoDescripcion As String
    Public Property CodigoDescripcion() As String
        Get
            Return fCodigoDescripcion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoDescripcion", fCodigoDescripcion, value)
        End Set
    End Property

    Dim fCodigoUsuario As String
    Public Property CodigoUsuario() As String
        Get
            Return fCodigoUsuario
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoUsuario", fCodigoUsuario, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
