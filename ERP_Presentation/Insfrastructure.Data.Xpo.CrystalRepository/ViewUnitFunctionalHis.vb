
Imports DevExpress.Xpo

<Persistent("dbo.ViewUnitFunctionalHis")>
Public Class ViewUnitFunctionalHis
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

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    Dim fUnidadFuncional As String
    Public Property UnidadFuncional() As String
        Get
            Return fUnidadFuncional
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnidadFuncional", fUnidadFuncional, value)
        End Set
    End Property

    Dim fTipoUnidadFuncional As Integer
    Public Property TipoUnidadFuncional() As Integer
        Get
            Return fTipoUnidadFuncional
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CodigoDescripcion", fTipoUnidadFuncional, value)
        End Set
    End Property



    Dim fCenterCode As String
    Public Property CenterCode() As String
        Get
            Return fCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterCode", fCenterCode, value)
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
