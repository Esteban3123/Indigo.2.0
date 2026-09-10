Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashReceiptDetails")> _
Public Class TreasuryCashReceiptDetailsXpo
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
    Dim fIdCashReceipt As TreasuryCashReceiptsXpo
    <Association("TreasuryCashReceiptDetailsXpoReferencesTreasuryCashReceiptsXpo")> _
    Public Property IdCashReceipt() As TreasuryCashReceiptsXpo
        Get
            Return fIdCashReceipt
        End Get
        Set(ByVal value As TreasuryCashReceiptsXpo)
            SetPropertyValue(Of TreasuryCashReceiptsXpo)("IdCashReceipt", fIdCashReceipt, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("TreasuryCashReceiptDetailsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCashReceiptDetailsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryCashReceiptDetailsXpoReferencesPayrollCostCenterXpo")> _
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fIdCashReceiptConcept As TreasuryCashReceiptConceptsXpo
    <Association("TreasuryCashReceiptDetailsXpoReferencesTreasuryCashReceiptConceptsXpo")> _
    Public Property IdCashReceiptConcept() As TreasuryCashReceiptConceptsXpo
        Get
            Return fIdCashReceiptConcept
        End Get
        Set(ByVal value As TreasuryCashReceiptConceptsXpo)
            SetPropertyValue(Of TreasuryCashReceiptConceptsXpo)("IdCashReceiptConcept", fIdCashReceiptConcept, value)
        End Set
    End Property
    Dim fCashReceiptConceptAffectation As Byte
    Public Property CashReceiptConceptAffectation() As Byte
        Get
            Return fCashReceiptConceptAffectation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CashReceiptConceptAffectation", fCashReceiptConceptAffectation, value)
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
    Dim fIdRetentionConcept As Integer
    Public Property IdRetentionConcept() As Integer
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property
    Dim fPercentageRetention As Decimal
    Public Property PercentageRetention() As Decimal
        Get
            Return fPercentageRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRetention", fPercentageRetention, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fCardNumber As String
    <Size(30)> _
    Public Property CardNumber() As String
        Get
            Return fCardNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CardNumber", fCardNumber, value)
        End Set
    End Property
    <Association("TreasuryCashReceiptAccountReceivableXpoReferencesTreasuryCashReceiptDetailsXpo", GetType(TreasuryCashReceiptAccountReceivableXpo))> _
    Public ReadOnly Property TreasuryCashReceiptAccountReceivableXpo() As XPCollection(Of TreasuryCashReceiptAccountReceivableXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptAccountReceivableXpo)("TreasuryCashReceiptAccountReceivableXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptAdvancePaymentXpoReferencesTreasuryCashReceiptDetailsXpo", GetType(TreasuryCashReceiptAdvancePaymentXpo))> _
    Public ReadOnly Property TreasuryCashReceiptAdvancePaymentXpo() As XPCollection(Of TreasuryCashReceiptAdvancePaymentXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptAdvancePaymentXpo)("TreasuryCashReceiptAdvancePaymentXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesTreasuryCashReceiptDetailsXpo", GetType(PortfolioAdvanceReportXpo))>
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptAccountPayableXpoReferencesTreasuryCashReceiptDetailsXpo", GetType(TreasuryCashReceiptDetailAccountPayableXpo))>
    Public ReadOnly Property TreasuryCashReceiptDetailAccountPayableXpo() As XPCollection(Of TreasuryCashReceiptDetailAccountPayableXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptDetailAccountPayableXpo)("TreasuryCashReceiptDetailAccountPayableXpo")
        End Get
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
