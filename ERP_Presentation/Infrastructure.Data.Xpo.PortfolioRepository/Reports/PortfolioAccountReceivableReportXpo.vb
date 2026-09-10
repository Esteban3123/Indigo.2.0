Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.AccountReceivable")> _
Public Class PortfolioAccountReceivableReportXpo
    Inherits XPLiteObject

#Region "Properties"

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
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonCustomerReportXpo")> _
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fSellerId As CommonSellerReportXpo
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonSellerReportXpo")>
    Public Property SellerId() As CommonSellerReportXpo
        Get
            Return fSellerId
        End Get
        Set(ByVal value As CommonSellerReportXpo)
            SetPropertyValue(Of CommonSellerReportXpo)("SellerId", fSellerId, value)
        End Set
    End Property

    Dim fInvoiceId As PortfolioInvoiceXpo
    Public Property InvoiceId() As PortfolioInvoiceXpo
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As PortfolioInvoiceXpo)
            SetPropertyValue(Of PortfolioInvoiceXpo)("InvoiceId", fInvoiceId, value)
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
    Dim fMainAccountWithoutFilingId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioAccountReceivableReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountWithoutFilingId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountWithoutFilingId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountWithoutFilingId", fMainAccountWithoutFilingId, value)
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

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("Currency_PortfolioAccountReceivableReport")>
    Public Property CurrencyId As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(value As CommonCurrencyXpo)
            SetPropertyValue("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    <PersistentAlias("CurrencyId.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property



#End Region

#Region "Custom Properties"

    'Propiedad Añadida total de diferencia de dias de la fecha de vencimiento con la fecha del flitro
    Dim fDifferenceDays As Integer
    <NonPersistent()>
    Public Property DifferenceDays() As Integer
        Get
            Return fDifferenceDays
        End Get
        Set(ByVal value As Integer)
            Me.fDifferenceDays = value
        End Set
    End Property



    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()>
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    <PersistentAlias("Iif(
PortfolioStatus = 1, 'Sin Radicar', 
PortfolioStatus = 2, 'Radicada',
PortfolioStatus = 3, 'Radicada Entidad',
PortfolioStatus = 4, 'Objetada',
PortfolioStatus = 5, 'Contestada Radicada',
PortfolioStatus = 6, 'Aceptada',
PortfolioStatus = 7, 'Certificada Parcial',
PortfolioStatus = 8, 'Certificada Total',
PortfolioStatus = 9, 'No Subsanable',
PortfolioStatus = 10, 'Dificil Recaudo',
PortfolioStatus = 11, 'Factura Devuelta',
PortfolioStatus = 12, 'Glosa Ratificada',
PortfolioStatus = 13, 'Radicacion Tramite Objecion',
PortfolioStatus = 14, 'Devolucion Factura',
PortfolioStatus = 15, 'Cuenta Dificil Recaudo',
PortfolioStatus = 16, 'Cobro Juridico',
'')")>
    Public ReadOnly Property PortfolioStatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PortfolioStatusName"))
        End Get
    End Property

#End Region

#Region "Navigation Properties"

    <Association("PortfolioAccountReceivableAccountingReportXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingReportXpo)("PortfolioAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableShareReportXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioAccountReceivableShareReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableShareReportXpo() As XPCollection(Of PortfolioAccountReceivableShareReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableShareReportXpo)("PortfolioAccountReceivableShareReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailReportXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioTransferDetailReportXpo))> _
    Public ReadOnly Property PortfolioTransferDetailReportXpo() As XPCollection(Of PortfolioTransferDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailReportXpo)("PortfolioTransferDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailAccountShareReportXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioTransferDetailAccountShareReportXpo))> _
    Public ReadOnly Property PortfolioTransferDetailAccountShareReportXpo() As XPCollection(Of PortfolioTransferDetailAccountShareReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailAccountShareReportXpo)("PortfolioTransferDetailAccountShareReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesPortfolioAccountReceivableReportXpo", GetType(PortfolioNoteAccountReceivableAdvanceReportXpo))>
    Public ReadOnly Property PortfolioNoteAccountReceivableAdvanceReportXpo() As XPCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)("PortfolioNoteAccountReceivableAdvanceReportXpo")
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
