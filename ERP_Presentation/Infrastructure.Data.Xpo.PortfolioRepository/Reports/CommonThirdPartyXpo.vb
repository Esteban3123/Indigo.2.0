Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyXpo
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
    Dim fPersonId As CommonPersonXpo
    <Association("CommonThirdPartyXpoReferencesCommonPersonXpo")> _
    Public Property PersonId() As CommonPersonXpo
        Get
            Return fPersonId
        End Get
        Set(ByVal value As CommonPersonXpo)
            SetPropertyValue(Of CommonPersonXpo)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
    '<Indexed(Name:="IX_ThirdParty_Nit_UNI", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fDigitVerification As String
    <Size(1)> _
    Public Property DigitVerification() As String
        Get
            Return fDigitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DigitVerification", fDigitVerification, value)
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
    Dim fNitName As String
    'columna que devuelve el Número y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
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
    Dim fEntityCode As String
    <Size(15)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property
    Dim fEconomicActivityId As Integer
    Public Property EconomicActivityId() As Integer
        Get
            Return fEconomicActivityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EconomicActivityId", fEconomicActivityId, value)
        End Set
    End Property
    Dim fClass1 As Byte
    <Persistent("Class")> _
    Public Property Class1() As Byte
        Get
            Return fClass1
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Class1", fClass1, value)
        End Set
    End Property
    Dim fDigitalSignature() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property DigitalSignature() As Byte()
        Get
            Return fDigitalSignature
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("DigitalSignature", fDigitalSignature, value)
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
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
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

    <Association("CommonSupplierXpoReferencesCommonThirdPartyXpo", GetType(CommonSupplierXpo))> _
    Public ReadOnly Property CommonSupplierXpo() As XPCollection(Of CommonSupplierXpo)
        Get
            Return GetCollection(Of CommonSupplierXpo)("CommonSupplierXpo")
        End Get
    End Property
    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsXpo() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioAccountReceivableReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableReportXpo() As XPCollection(Of PortfolioAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableReportXpo)("PortfolioAccountReceivableReportXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingReportXpo)("PortfolioAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioTransferReportXpo))> _
    Public ReadOnly Property PortfolioTransferReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferReportXpo")
        End Get
    End Property

    <Association("Portfolio_PortfolioTransferReferencesCommon_Thirdparty", GetType(PortfolioTransferXpo))> _
    Public ReadOnly Property PortfolioTransferXpo() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("PortfolioTransferXpo")
        End Get
    End Property

    <Association("PortfolioAdvanceReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("CommonCustomerReportXpoReferencesCommonThirdPartyXpo", GetType(CommonCustomerReportXpo))> _
    Public ReadOnly Property CommonCustomerReportXpo() As XPCollection(Of CommonCustomerReportXpo)
        Get
            Return GetCollection(Of CommonCustomerReportXpo)("CommonCustomerReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteDetailReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioNoteDetailReportXpo))> _
    Public ReadOnly Property PortfolioNoteDetailReportXpo() As XPCollection(Of PortfolioNoteDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteDetailReportXpo)("PortfolioNoteDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAccountReceivableReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioInitialBalanceAccountReceivableReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableReportXpo)("PortfolioInitialBalanceAccountReceivableReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioInitialBalanceAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)("PortfolioInitialBalanceAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAdvanceReportXpoReferencesCommonThirdPartyXpo", GetType(PortfolioInitialBalanceAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAdvanceReportXpo() As XPCollection(Of PortfolioInitialBalanceAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAdvanceReportXpo)("PortfolioInitialBalanceAdvanceReportXpo")
        End Get
    End Property
    <Association("CommonSellerReportXpoReferencesCommonThirdPartyXpo", GetType(CommonSellerReportXpo))> _
    Public ReadOnly Property CommonSellerReportXpo() As XPCollection(Of CommonSellerReportXpo)
        Get
            Return GetCollection(Of CommonSellerReportXpo)("CommonSellerReportXpo")
        End Get
    End Property
    <Association("Treasury_EntityBankAccountsReferencesCommon_ThirdParty", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountReportXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountReportXpo")
        End Get
    End Property
    <Association("Payroll_BankReferencesCommon_ThirdParty", GetType(PayrollBankXpo))> _
    Public ReadOnly Property PayrollBankReportXpo() As XPCollection(Of PayrollBankXpo)
        Get
            Return GetCollection(Of PayrollBankXpo)("PayrollBankReportXpo")
        End Get
    End Property
    <Association("Treasury_CashReceiptsReferencesCommon_ThirdParty", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property CashReceiptsReportXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("CashReceiptsReportXpo")
        End Get
    End Property
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesCommon_ThirdParty")>
    Public ReadOnly Property PortfolioAccountReceivableDocumentDetail() As XPCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)("PortfolioAccountReceivableDocumentDetail")
        End Get
    End Property

    <Association("PortfolioRevaluationDetailReferencesThirdParty")>
    Public ReadOnly Property PortfolioRevaluationDetails() As XPCollection(Of PortfolioRevaluationDetailXpo)
        Get
            Return GetCollection(Of PortfolioRevaluationDetailXpo)("PortfolioRevaluationDetails")
        End Get
    End Property

    <Association("LawyerReferencesThirdParty", GetType(LawyerXpo))>
    Public ReadOnly Property LawyerXpo() As XPCollection(Of LawyerXpo)
        Get
            Return GetCollection(Of LawyerXpo)("LawyerXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
