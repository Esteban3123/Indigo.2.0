Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.PortfolioProvisionDetail")>
Public Class PortfolioProvisionDetailXpo
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

    Dim fPortfolioProvisionId As Integer
    Public Property PortfolioProvisionId() As Integer
        Get
            Return fPortfolioProvisionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioProvisionId", fPortfolioProvisionId, value)
        End Set
    End Property

    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
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

    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
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

    Dim fBalanceAccountReceivable As Decimal
    Public Property BalanceAccountReceivable() As Decimal
        Get
            Return fBalanceAccountReceivable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceAccountReceivable", fBalanceAccountReceivable, value)
        End Set
    End Property

    Dim fConfirmDateAccountReceivable As DateTime
    Public Property ConfirmDateAccountReceivable() As DateTime
        Get
            Return fConfirmDateAccountReceivable
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDateAccountReceivable", fConfirmDateAccountReceivable, value)
        End Set
    End Property

    Dim fAgesId As AgesPortfolioXpo
    <Association("PortfolioProvisionDetailReferencesAgesPortfolio")>
    Public Property AgesId() As AgesPortfolioXpo
        Get
            Return fAgesId
        End Get
        Set(ByVal value As AgesPortfolioXpo)
            SetPropertyValue(Of AgesPortfolioXpo)("AgesId", fAgesId, value)
        End Set
    End Property

    Dim fFacturerValue As Decimal
    Public Property FacturerValue() As Decimal
        Get
            Return fFacturerValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("FacturerValue", fFacturerValue, value)
        End Set
    End Property
    Dim fValueGlosado As Decimal
    Public Property ValueGlosado() As Decimal
        Get
            Return fValueGlosado
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosado", fValueGlosado, value)
        End Set
    End Property
    Dim fExpectative As Integer
    Public Property Expectative() As Integer
        Get
            Return fExpectative
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Expectative", fExpectative, value)
        End Set
    End Property
    Dim fNetPresentValue As Decimal
    Public Property NetPresentValue() As Decimal
        Get
            Return fNetPresentValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NetPresentValue", fNetPresentValue, value)
        End Set
    End Property


#Region "Properties View"
    Dim fRegimen As Integer
    Public Property Regimen() As Integer
        Get
            Return fRegimen
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Regimen", fRegimen, value)
        End Set
    End Property

    Dim fRegimenName As String
    Public Property RegimenName() As String
        Get
            Return fRegimenName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RegimenName ", fRegimenName, value)
        End Set
    End Property

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName ", fThirdPartyNitName, value)
        End Set
    End Property
#End Region


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
