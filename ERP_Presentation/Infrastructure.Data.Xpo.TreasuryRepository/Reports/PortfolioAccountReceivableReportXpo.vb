Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.AccountReceivable")> _
Public Class PortfolioAccountReceivableReportXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonThirdPartyReportXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fSellerId As Integer
    Public Property SellerId() As Integer
        Get
            Return fSellerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SellerId", fSellerId, value)
        End Set
    End Property
    Dim fInvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceId", fInvoiceId, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(20)> _
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
    Dim fTerm As Integer
    Public Property Term() As Integer
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Term", fTerm, value)
        End Set
    End Property
    Dim fExpiredDate As DateTime
    Public Property ExpiredDate() As DateTime
        Get
            Return fExpiredDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpiredDate", fExpiredDate, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fPortfolioStatus As Byte
    Public Property PortfolioStatus() As Byte
        Get
            Return fPortfolioStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PortfolioStatus", fPortfolioStatus, value)
        End Set
    End Property
    Dim fOpeningBalance As Boolean
    Public Property OpeningBalance() As Boolean
        Get
            Return fOpeningBalance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OpeningBalance", fOpeningBalance, value)
        End Set
    End Property
    Dim fRecognitionId As Integer
    Public Property RecognitionId() As Integer
        Get
            Return fRecognitionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecognitionId", fRecognitionId, value)
        End Set
    End Property
    Dim fPaymentAgreement As Boolean
    Public Property PaymentAgreement() As Boolean
        Get
            Return fPaymentAgreement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PaymentAgreement", fPaymentAgreement, value)
        End Set
    End Property
    Dim fRegistrationAdjusted As Boolean
    Public Property RegistrationAdjusted() As Boolean
        Get
            Return fRegistrationAdjusted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RegistrationAdjusted", fRegistrationAdjusted, value)
        End Set
    End Property
    Dim fMainAccountWithoutFilingId As Integer
    Public Property MainAccountWithoutFilingId() As Integer
        Get
            Return fMainAccountWithoutFilingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountWithoutFilingId", fMainAccountWithoutFilingId, value)
        End Set
    End Property
    Dim fNumberShares As Integer
    Public Property NumberShares() As Integer
        Get
            Return fNumberShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberShares", fNumberShares, value)
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
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    <Association("TreasuryCashReceiptAccountReceivableXpoReferencesPortfolioAccountReceivableReportXpo", GetType(TreasuryCashReceiptAccountReceivableXpo))> _
    Public ReadOnly Property TreasuryCashReceiptAccountReceivableXpo() As XPCollection(Of TreasuryCashReceiptAccountReceivableXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptAccountReceivableXpo)("TreasuryCashReceiptAccountReceivableXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesPortfolioAccountReceivableReportXpo", GetType(TreasuryCrossingAccountDetailCxCXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxCXpo() As XPCollection(Of TreasuryCrossingAccountDetailCxCXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxCXpo)("TreasuryCrossingAccountDetailCxCXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioAccountReceivableAccountingXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingXpo() As XPCollection(Of PortfolioAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingXpo)("PortfolioAccountReceivableAccountingXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioTransferDetailXpo))> _
    Public ReadOnly Property PortfolioTransferDetailXpo() As XPCollection(Of PortfolioTransferDetailXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioTransferDetailAccountShareXpo))> _
    Public ReadOnly Property PortfolioTransferDetailAccountShareXpo() As XPCollection(Of PortfolioTransferDetailAccountShareXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailAccountShareXpo)("PortfolioTransferDetailAccountShareXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableShareXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioAccountReceivableShareXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableShareXpo() As XPCollection(Of PortfolioAccountReceivableShareXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableShareXpo)("PortfolioAccountReceivableShareXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
