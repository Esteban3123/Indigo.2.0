Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaObjectionsReceptionD")> _
Public Class GlosasObjectionDXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fGlosaObjectionsReceptionCId As GlosasObjectionCXpo
    <Indexed("InvoiceNumber", Name:="IX_GlosaObjectionsReceptionD", Unique:=True)>
    <Association("Glosas_GlosaObjectionsReceptionDReferencesGlosas_GlosaObjectionsReceptionC")>
    Public Property GlosaObjectionsReceptionCId() As GlosasObjectionCXpo
        Get
            Return fGlosaObjectionsReceptionCId
        End Get
        Set(ByVal value As GlosasObjectionCXpo)
            SetPropertyValue(Of GlosasObjectionCXpo)("GlosaObjectionsReceptionCId", fGlosaObjectionsReceptionCId, value)
        End Set
    End Property

    Dim fPortfolioGlosaId As GlosasPortfolioGlosaXpo
    <Association("Glosas_GlosaObjectionsReceptionDReferencesGlosas_GlosaPortfolioGlosada")>
    Public Property PortfolioGlosaId() As GlosasPortfolioGlosaXpo
        Get
            Return fPortfolioGlosaId
        End Get
        Set(ByVal value As GlosasPortfolioGlosaXpo)
            SetPropertyValue(Of GlosasPortfolioGlosaXpo)("PortfolioGlosaId", fPortfolioGlosaId, value)
        End Set
    End Property

    Dim fObservationInvoiceCode As String
    <Size(5)>
    Public Property ObservationInvoiceCode() As String
        Get
            Return fObservationInvoiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObservationInvoiceCode", fObservationInvoiceCode, value)
        End Set
    End Property

    Dim fDocumentType As Char
    Public Property DocumentType() As Char
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    <Size(50)>
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fComment As String
    <Size(250)>
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property

    Dim fState As Char
    Public Property State() As Char
        Get
            Return fState
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("State", fState, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <NonPersistent()>
    ReadOnly Property RadicatedConsecutive As String
        Get
            Return fGlosaObjectionsReceptionCId.RadicatedConsecutive
        End Get
    End Property

#End Region

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
