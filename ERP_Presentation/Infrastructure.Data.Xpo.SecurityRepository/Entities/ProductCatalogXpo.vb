Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Base

<Persistent("Security.ProductCatalog")>
Public Class ProductCatalogXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fProductName As String
    <Size(100)>
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fPlatformName As String
    <Size(100)>
    Public Property PlatformName() As String
        Get
            Return fPlatformName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlatformName", fPlatformName, value)
        End Set
    End Property

    Dim fSuiteName As String
    <Size(100)>
    Public Property SuiteName() As String
        Get
            Return fSuiteName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SuiteName", fSuiteName, value)
        End Set
    End Property

    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property

    Dim fVisible As Byte
    Public Property Visible() As Byte
        Get
            Return fVisible
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Visible", fVisible, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
