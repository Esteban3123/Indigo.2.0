Imports DevExpress.Xpo

<Persistent("Portfolio.ViewAccountReceivableByPortfolioProvision")>
Public Class ViewAccountReceivableByPortfolioProvisionXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fAccountReceivableType As Byte
    Public Property AccountReceivableType() As Byte
        Get
            Return fAccountReceivableType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountReceivableType", fAccountReceivableType, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fAccountReceivableDate As DateTime
    Public Property AccountReceivableDate() As DateTime
        Get
            Return fAccountReceivableDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AccountReceivableDate", fAccountReceivableDate, value)
        End Set
    End Property

    Dim fRadicatedDate As DateTime
    Public Property RadicatedDate() As DateTime
        Get
            Return fRadicatedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedDate", fRadicatedDate, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fRegimenName As String
    Public Property RegimenName() As String
        Get
            Return fRegimenName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RegimenName", fRegimenName, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fGlosaPortfolioGlosadaId As Integer?
    Public Property GlosaPortfolioGlosadaId() As Integer?
        Get
            Return fGlosaPortfolioGlosadaId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("GlosaPortfolioGlosadaId", fGlosaPortfolioGlosadaId, value)
        End Set
    End Property

    Dim fValueGlosado As Decimal
    Public Property ValueGlosado() As Decimal
        Get
            Return fValueGlosado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosado", fValueGlosado, value)
        End Set
    End Property

    Dim fBalanceGlosa As Decimal
    Public Property BalanceGlosa() As Decimal
        Get
            Return fBalanceGlosa
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceGlosa", fBalanceGlosa, value)
        End Set
    End Property

    Dim fDeteriorationBalance As Decimal
    Public Property DeteriorationBalance() As Decimal
        Get
            Return fDeteriorationBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeteriorationBalance", fDeteriorationBalance, value)
        End Set
    End Property

    Dim fExpectative As Integer
    Public Property Expectative() As Integer
        Get
            Return fExpectative
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Expectative", fExpectative, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(ThirdPartyNit,' - ',ThirdPartyName)")>
    Public ReadOnly Property ThirdPartyDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ThirdPartyDescription"))
        End Get
    End Property

    <PersistentAlias("concat(InvoiceNumber,' - ',ThirdPartyName)")>
    Public ReadOnly Property InvoiceNumberThirdParty() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("InvoiceNumberThirdParty"))
        End Get
    End Property

    Dim fAgesId As Integer?
    <NonPersistent()>
    Public Property AgesId() As Integer?
        Get
            Return fAgesId
        End Get
        Set(ByVal value As Integer?)
            Me.fAgesId = value
        End Set
    End Property

    Dim fAgesDescription As String
    <NonPersistent()>
    Public Property AgesDescription() As String
        Get
            Return fAgesDescription
        End Get
        Set(ByVal value As String)
            Me.fAgesDescription = value
        End Set
    End Property

    Dim fPercentageProvision As Decimal
    <NonPersistent()>
    Public Property PercentageProvision() As Decimal
        Get
            Return fPercentageProvision
        End Get
        Set(ByVal value As Decimal)
            Me.fPercentageProvision = value
        End Set
    End Property

    Dim fPercentageDeterioration As Decimal
    <NonPersistent()>
    Public Property PercentageDeterioration() As Decimal
        Get
            Return fPercentageDeterioration
        End Get
        Set(ByVal value As Decimal)
            Me.fPercentageDeterioration = value
        End Set
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
