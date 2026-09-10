Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.CostCenter")> _
Public Class PayrollCostCenterXpo
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
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(220)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("TreasuryCashReceiptDetailsXpoReferencesPayrollCostCenterXpo", GetType(TreasuryCashReceiptDetailsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptDetailsXpo() As XPCollection(Of TreasuryCashReceiptDetailsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptDetailsXpo)("TreasuryCashReceiptDetailsXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptsXpoReferencesPayrollCostCenterXpo", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsXpo")
        End Get
    End Property
    <Association("TreasuryCashRegistersXpoReferencesPayrollCostCenterXpo", GetType(TreasuryCashRegistersXpo))> _
    Public ReadOnly Property TreasuryCashRegistersXpo() As XPCollection(Of TreasuryCashRegistersXpo)
        Get
            Return GetCollection(Of TreasuryCashRegistersXpo)("TreasuryCashRegistersXpo")
        End Get
    End Property
    <Association("TreasuryEntityBankAccountsXpoReferencesPayrollCostCenterXpo", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountsXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesPayrollCostCenterXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesPayrollCostCenterXpo", GetType(TreasuryVoucherTransactionDetailsXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesPayrollCostCenterXpo", GetType(PaymentsAccountPayableDetailConceptXpo))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpo() As XPCollection(Of PaymentsAccountPayableDetailConceptXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpo)("PaymentsAccountPayableDetailConceptXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("PaymentsAdvancePaymentsXpoReferencesPayrollCostCenterXpo", GetType(PaymentsAdvancePaymentsXpo))> _
    Public ReadOnly Property PaymentsAdvancePaymentsXpo() As XPCollection(Of PaymentsAdvancePaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAdvancePaymentsXpo)("PaymentsAdvancePaymentsXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingXpoReferencesPayrollCostCenterXpo", GetType(PortfolioAccountReceivableAccountingXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingXpo() As XPCollection(Of PortfolioAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingXpo)("PortfolioAccountReceivableAccountingXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentXpoReferencesPayrollCostCenterXpo", GetType(TreasuryConsignmentXpo))> _
    Public ReadOnly Property TreasuryConsignmentXpo() As XPCollection(Of TreasuryConsignmentXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentXpo)("TreasuryConsignmentXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentDetailXpoReferencesPayrollCostCenterXpo", GetType(TreasuryConsignmentDetailXpo))> _
    Public ReadOnly Property TreasuryConsignmentDetailXpo() As XPCollection(Of TreasuryConsignmentDetailXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentDetailXpo)("TreasuryConsignmentDetailXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesPayrollCostCenterXpo", GetType(TreasuryNotesXpo))> _
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
        End Get
    End Property
    <Association("TreasuryNotesDetailXpoReferencesPayrollCostCenterXpo", GetType(TreasuryNotesDetailXpo))> _
    Public ReadOnly Property TreasuryNotesDetailXpo() As XPCollection(Of TreasuryNotesDetailXpo)
        Get
            Return GetCollection(Of TreasuryNotesDetailXpo)("TreasuryNotesDetailXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentXpoReferencesPayrollCostCenterXpo", GetType(TreasurySchedulePaymentXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentXpo() As XPCollection(Of TreasurySchedulePaymentXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentXpo)("TreasurySchedulePaymentXpo")
        End Get
    End Property
    <Association("PortfolioTransferXpoReferencesPayrollCostCenterXpo", GetType(PortfolioTransferXpo))> _
    Public ReadOnly Property PortfolioTransferXpo() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("PortfolioTransferXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailXpoReferencesPayrollCostCenterXpo", GetType(PortfolioTransferDetailXpo))> _
    Public ReadOnly Property PortfolioTransferDetailXpo() As XPCollection(Of PortfolioTransferDetailXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesPayrollCostCenterXpo", GetType(TreasuryCrossingAccountDetailOtherConceptXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailOtherConceptXpo() As XPCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)("TreasuryCrossingAccountDetailOtherConceptXpo")
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
