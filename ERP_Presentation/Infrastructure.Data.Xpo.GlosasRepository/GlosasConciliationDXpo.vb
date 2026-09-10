Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.ConciliationD")> _
Partial Public Class Glosas_ConciliationDXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdConciliationC As Glosas_ConciliationCXpo
    <Association("Glosas_ConciliationDReferencesGlosas_ConciliationC")> _
    Public Property ConciliationCId() As Glosas_ConciliationCXpo
        Get
            Return fIdConciliationC
        End Get
        Set(ByVal value As Glosas_ConciliationCXpo)
            SetPropertyValue(Of Glosas_ConciliationCXpo)("ConciliationCId", fIdConciliationC, value)
        End Set
    End Property

    Dim fGlosaPortfolioId As GlosasPortfolioGlosaXpo
    <Association("Glosas_ConciliationDReferencesGlosas_GlosaPortfolioGlosada")> _
    Public Property GlosaPortfolioId() As GlosasPortfolioGlosaXpo
        Get
            Return fGlosaPortfolioId
        End Get
        Set(ByVal value As GlosasPortfolioGlosaXpo)
            SetPropertyValue(Of GlosasPortfolioGlosaXpo)("GlosaPortfolioId", fGlosaPortfolioId, value)
        End Set
    End Property

    Dim fIdObjectionReceptionC As Integer
    Dim fInvoiceNumber As String
    <Persistent("InvoiceNumber")>
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

