Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyReportXpo
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
    Dim fPersonId As Integer
    Public Property PersonId() As Integer
        Get
            Return fPersonId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
    '<Indexed("IX_ThirdParty_Nit_UNI")> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fPersonType As Byte
    Public Property PersonType() As Byte
        Get
            Return fPersonType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PersonType", fPersonType, value)
        End Set
    End Property
    Dim fRetentionType As Byte
    Public Property RetentionType() As Byte
        Get
            Return fRetentionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetentionType", fRetentionType, value)
        End Set
    End Property
    Dim fContributionType As Byte
    Public Property ContributionType() As Byte
        Get
            Return fContributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ContributionType", fContributionType, value)
        End Set
    End Property
    Dim fIca As Boolean
    Public Property Ica() As Boolean
        Get
            Return fIca
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Ica", fIca, value)
        End Set
    End Property
    Dim fIcaPercentage As Decimal
    Public Property IcaPercentage() As Decimal
        Get
            Return fIcaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaPercentage", fIcaPercentage, value)
        End Set
    End Property
    Dim fIcaTop As Boolean
    Public Property IcaTop() As Boolean
        Get
            Return fIcaTop
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IcaTop", fIcaTop, value)
        End Set
    End Property
    Dim fIcaTopValue As Decimal
    Public Property IcaTopValue() As Decimal
        Get
            Return fIcaTopValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaTopValue", fIcaTopValue, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property
    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)>
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property
    <PersistentAlias("Iif(PersonType = 1, 'Natural', PersonType = 2, 'Jurídica', '')")>
    Public ReadOnly Property PersonTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PersonTypeName"))
        End Get
    End Property

    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsXpo() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsXpo")
        End Get
    End Property
    <Association("PayrollBankXpoReferencesCommonThirdPartyXpo", GetType(PayrollBankXpo))> _
    Public ReadOnly Property PayrollBankXpo() As XPCollection(Of PayrollBankXpo)
        Get
            Return GetCollection(Of PayrollBankXpo)("PayrollBankXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptDetailsXpoReferencesCommonThirdPartyXpo", GetType(TreasuryCashReceiptDetailsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptDetailsXpo() As XPCollection(Of TreasuryCashReceiptDetailsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptDetailsXpo)("TreasuryCashReceiptDetailsXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptsXpoReferencesCommonThirdPartyXpo", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesCommonThirdPartyXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesCommonThirdPartyXpo", GetType(TreasuryVoucherTransactionDetailsXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonThirdPartyReportXpo", GetType(PortfolioAccountReceivableReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableReportXpo() As XPCollection(Of PortfolioAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableReportXpo)("PortfolioAccountReceivableReportXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableXpoReferencesCommonThirdPartyReportXpo", GetType(PaymentsAccountPayableXpo))> _
    Public ReadOnly Property PaymentsAccountPayableXpo() As XPCollection(Of PaymentsAccountPayableXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableXpo)("PaymentsAccountPayableXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesCommonThirdPartyReportXpo", GetType(PaymentsAccountPayableDetailConceptXpo))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpo() As XPCollection(Of PaymentsAccountPayableDetailConceptXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpo)("PaymentsAccountPayableDetailConceptXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesCommonThirdPartyReportXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("PaymentsAdvancePaymentsXpoReferencesCommonThirdPartyReportXpo", GetType(PaymentsAdvancePaymentsXpo))> _
    Public ReadOnly Property PaymentsAdvancePaymentsXpo() As XPCollection(Of PaymentsAdvancePaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAdvancePaymentsXpo)("PaymentsAdvancePaymentsXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountXpoReferencesCommonThirdPartyReportXpo", GetType(TreasuryCrossingAccountXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountXpo() As XPCollection(Of TreasuryCrossingAccountXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountXpo)("TreasuryCrossingAccountXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingXpoReferencesCommonThirdPartyReportXpo", GetType(PortfolioAccountReceivableAccountingXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingXpo() As XPCollection(Of PortfolioAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingXpo)("PortfolioAccountReceivableAccountingXpo")
        End Get
    End Property
    <Association("CommonSuplierReportXpoReferencesCommonThirdPartyReportXpo", GetType(CommonSuplierReportXpo))>
    Public ReadOnly Property CommonSuplierReportXpo() As XPCollection(Of CommonSuplierReportXpo)
        Get
            Return GetCollection(Of CommonSuplierReportXpo)("CommonSuplierReportXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentDetailXpoReferencesCommonThirdPartyReportXpo", GetType(TreasurySchedulePaymentDetailXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentDetailXpo() As XPCollection(Of TreasurySchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentDetailXpo)("TreasurySchedulePaymentDetailXpo")
        End Get
    End Property
    <Association("TreasuryNotesDetailXpoReferencesCommonThirdPartyReportXpo", GetType(TreasuryNotesDetailXpo))> _
    Public ReadOnly Property TreasuryNotesDetailXpo() As XPCollection(Of TreasuryNotesDetailXpo)
        Get
            Return GetCollection(Of TreasuryNotesDetailXpo)("TreasuryNotesDetailXpo")
        End Get
    End Property
    <Association("PortfolioTransferXpoReferencesCommonThirdPartyReportXpo", GetType(PortfolioTransferXpo))> _
    Public ReadOnly Property PortfolioTransferXpo() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("PortfolioTransferXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesCommonThirdPartyReportXpo", GetType(TreasuryCrossingAccountDetailOtherConceptXpo))>
    Public ReadOnly Property TreasuryCrossingAccountDetailOtherConceptXpo() As XPCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)("TreasuryCrossingAccountDetailOtherConceptXpo")
        End Get
    End Property
    <Association("TreasuryEntityBankAccountsXpoReferencesThirdPartyXpo", GetType(TreasuryEntityBankAccountsXpo))>
    Public ReadOnly Property TreasuryEntityBankAccountsXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountsXpo")
        End Get
    End Property
    <Association("CashRegistersXpoReferencesThirdPartyXpo", GetType(TreasuryCashRegistersXpo))>
    Public ReadOnly Property TreasuryCashRegistersXpo() As XPCollection(Of TreasuryCashRegistersXpo)
        Get
            Return GetCollection(Of TreasuryCashRegistersXpo)("TreasuryCashRegistersXpo")
        End Get
    End Property

    <Association("SchedulePaymentDetailXpoXpoReferencesCommonThirdPartyReportXpo", GetType(SchedulePaymentDetailXpo))>
    Public ReadOnly Property SchedulePaymentDetailXpo() As XPCollection(Of SchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of SchedulePaymentDetailXpo)("SchedulePaymentDetailXpo")
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
