Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.AccountReceivableDocument")> _
Public Class PortfolioAccountReceivableDocumentReportXpo
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
    <Indexed(Name:="IX_AccountReceivableDocument", Unique:=True)> _
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("Portfolio_AccountReceivableDocumentReferencesCommon_Customer")> _
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioAccountReceivableDocumentReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioAccountReceivableDocumentReportXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
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
    Dim fShare As Integer
    Public Property Share() As Integer
        Get
            Return fShare
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Share", fShare, value)
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
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property
    Dim fObservation As String
    <Size(300)> _
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
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
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPortfolio_AccountReceivableDocument", GetType(PortfolioAccountReceivableDocumentDetailReportXpo))> _
    Public ReadOnly Property Portfolio_AccountReceivableDocumentDetails() As XPCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)("Portfolio_AccountReceivableDocumentDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
