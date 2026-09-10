Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.AccountReceivable")> _
Public Class PortfolioAccountReceivableXpo
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
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
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
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fAccountWithoutRadicateId As Integer
    Public Property AccountWithoutRadicateId() As Integer
        Get
            Return fAccountWithoutRadicateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountWithoutRadicateId", fAccountWithoutRadicateId, value)
        End Set
    End Property
    Dim fAccountRadicateId As Integer
    Public Property AccountRadicateId() As Integer
        Get
            Return fAccountRadicateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountRadicateId", fAccountRadicateId, value)
        End Set
    End Property
    Dim fAccountObjectionRemediedId As Integer
    Public Property AccountObjectionRemediedId() As Integer
        Get
            Return fAccountObjectionRemediedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountObjectionRemediedId", fAccountObjectionRemediedId, value)
        End Set
    End Property
    Dim fAccountConciliationId As Integer
    Public Property AccountConciliationId() As Integer
        Get
            Return fAccountConciliationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountConciliationId", fAccountConciliationId, value)
        End Set
    End Property
    Dim fAccountLegalCollectionId As Integer
    Public Property AccountLegalCollectionId() As Integer
        Get
            Return fAccountLegalCollectionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountLegalCollectionId", fAccountLegalCollectionId, value)
        End Set
    End Property
    Dim fAccountDebtorOrder As Integer
    Public Property AccountDebtorOrder() As Integer
        Get
            Return fAccountDebtorOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountDebtorOrder", fAccountDebtorOrder, value)
        End Set
    End Property
    Dim fAccountCreditorOrder As Integer
    Public Property AccountCreditorOrder() As Integer
        Get
            Return fAccountCreditorOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountCreditorOrder", fAccountCreditorOrder, value)
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
    <Association("GlosasTransferJuridicalDebtCollectionDXpoReferencesPortfolioAccountReceivableXpo", GetType(GlosasTransferJuridicalDebtCollectionDXpo))>
    Public ReadOnly Property GlosasTransferJuridicalDebtCollectionDXpo() As XPCollection(Of GlosasTransferJuridicalDebtCollectionDXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionDXpo)("GlosasTransferJuridicalDebtCollectionDXpo")
        End Get
    End Property

    <Association("PortfolioViewAccountReceivableRadication_References_PortfolioAccountReceivable", GetType(ViewAccountReceivableRadicationXpo))>
    Public ReadOnly Property ViewAccountReceivableRadication() As XPCollection(Of ViewAccountReceivableRadicationXpo)
        Get
            Return GetCollection(Of ViewAccountReceivableRadicationXpo)("ViewAccountReceivableRadication")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
