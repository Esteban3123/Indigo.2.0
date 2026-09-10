Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

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
    Dim fPersonId As CommonPersonXpoP
    <Association("CommonThirdPartyXpoReferencesCommonPersonXpoP")>
    Public Property PersonId() As CommonPersonXpoP
        Get
            Return fPersonId
        End Get
        Set(ByVal value As CommonPersonXpoP)
            SetPropertyValue(Of CommonPersonXpoP)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
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

    Dim fElectronicBiller As Boolean
    Public Property ElectronicBiller As Boolean
        Get
            Return fElectronicBiller
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ElectronicBiller", fElectronicBiller, value)
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
    Dim fNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
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

    <Association("MaintenanceThidrPartyReferencesAccountPayable", GetType(AccountPayableXpo))> _
    Public ReadOnly Property MaintenanceAccountPayableCollection() As XPCollection(Of AccountPayableXpo)
        Get
            Return GetCollection(Of AccountPayableXpo)("MaintenanceAccountPayableCollection")
        End Get
    End Property

    <Association("MaintenanceSupplierReferencesThirdParty", GetType(Maintenance_Supplier))>
    Public ReadOnly Property MaintenanceSupplierCollection() As XPCollection(Of Maintenance_Supplier)
        Get
            Return GetCollection(Of Maintenance_Supplier)("MaintenanceSupplierCollection")
        End Get
    End Property

    <Association("FiscalResponsibilityReferencesThirdParty", GetType(CommonThirdPartyFiscalResponsibilityXpo))>
    Public ReadOnly Property CommonThirdPartyFiscalResponsibilityXpoCollection() As XPCollection(Of CommonThirdPartyFiscalResponsibilityXpo)
        Get
            Return GetCollection(Of CommonThirdPartyFiscalResponsibilityXpo)("CommonThirdPartyFiscalResponsibilityXpoCollection")
        End Get
    End Property

    <Association("PaymentsAccountPayableReferencesCommonThirdPartyXpo", GetType(PaymentsAccountPayable))> _
    Public ReadOnly Property PaymentsAccountPayableCollection() As XPCollection(Of PaymentsAccountPayable)
        Get
            Return GetCollection(Of PaymentsAccountPayable)("PaymentsAccountPayableCollection")
        End Get
    End Property
    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsXpoCollection() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsXpoCollection")
        End Get
    End Property
    <Association("PaymentsPaymentTransferReferencesCommonThirdPartyXpo", GetType(PaymentsPaymentTransfer))> _
    Public ReadOnly Property PaymentsPaymentTransfer() As XPCollection(Of PaymentsPaymentTransfer)
        Get
            Return GetCollection(Of PaymentsPaymentTransfer)("PaymentsPaymentTransfer")
        End Get
    End Property
    <Association("TreasuryCrossingAccountReferencesCommonThirdPartyXpo", GetType(TreasuryCrossingAccount))> _
    Public ReadOnly Property TreasuryCrossingAccount() As XPCollection(Of TreasuryCrossingAccount)
        Get
            Return GetCollection(Of TreasuryCrossingAccount)("TreasuryCrossingAccount")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsReferencesCommonThirdPartyXpo", GetType(TreasuryVoucherTransactionDetails))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetails() As XPCollection(Of TreasuryVoucherTransactionDetails)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetails)("TreasuryVoucherTransactionDetails")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionReferencesCommonThirdPartyXpo", GetType(TreasuryVoucherTransaction))> _
    Public ReadOnly Property TreasuryVoucherTransaction() As XPCollection(Of TreasuryVoucherTransaction)
        Get
            Return GetCollection(Of TreasuryVoucherTransaction)("TreasuryVoucherTransaction")
        End Get
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesCommonThirdPartyXpo", GetType(PaymentsAccountPayableDetailConceptXpoP))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpoP() As XPCollection(Of PaymentsAccountPayableDetailConceptXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpoP)("PaymentsAccountPayableDetailConceptXpoP")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesCommonThirdPartyXpo", GetType(PaymentsInitialBalanceAdvanceXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAdvanceXpo() As XPCollection(Of PaymentsInitialBalanceAdvanceXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAdvanceXpo)("PaymentsInitialBalanceAdvanceXpo")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesCommonThirdPartyXpo", GetType(PaymentsInitialBalanceAccountPayableXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAccountPayableXpo() As XPCollection(Of PaymentsInitialBalanceAccountPayableXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAccountPayableXpo)("PaymentsInitialBalanceAccountPayableXpo")
        End Get
    End Property
    <Association("PaymentsDeferredCausationXpoReferencesCommonThirdPartyXpo", GetType(PaymentsDeferredCausationXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationXpo() As XPCollection(Of PaymentsDeferredCausationXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationXpo)("PaymentsDeferredCausationXpo")
        End Get
    End Property

    <Association("PaymentsPaymentsNoteDetailsXpoReferencesCommonThirdPartyXpo", GetType(PaymentsPaymentsNoteDetailsXpo))>
    Public ReadOnly Property PaymentsPaymentsNoteDetailsXpo() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("PaymentsPaymentsNoteDetailsXpo")
        End Get
    End Property

    <Association("PaymentsRevaluationDetailReferencesThirdParty", GetType(PaymentsRevaluationDetailXpo))>
    Public ReadOnly Property PaymentsRevaluationDetailId() As XPCollection(Of PaymentsRevaluationDetailXpo)
        Get
            Return GetCollection(Of PaymentsRevaluationDetailXpo)("PaymentsRevaluationDetailId")
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
