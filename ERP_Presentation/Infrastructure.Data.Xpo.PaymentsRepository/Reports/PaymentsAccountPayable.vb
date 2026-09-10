Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.AccountPayable")> _
Public Class PaymentsAccountPayable
    Inherits XPLiteObject

#Region "Members"

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

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fNumberFiling As Long
    Public Property NumberFiling() As Long
        Get
            Return fNumberFiling
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("NumberFiling", fNumberFiling, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityCode As String
    <Size(20)>
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    <Size(250)>
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fIdSupplier As Maintenance_Supplier
    <Association("PaymentsAccountPayableReferencesMaintenance_Supplier")>
    Public Property IdSupplier() As Maintenance_Supplier
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("IdSupplier", fIdSupplier, value)
        End Set
    End Property

    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("PaymentsAccountPayableReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsAccountPayableReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpoP
    <Association("PaymentsAccountPayableReferencesPayrollCostCenterXpoP")> _
    Public Property IdCostCenter() As PayrollCostCenterXpoP
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fBillNumber As String
    <Size(20)> _
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
        End Set
    End Property
    Dim fBillDate As DateTime
    Public Property BillDate() As DateTime
        Get
            Return fBillDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BillDate", fBillDate, value)
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
    Dim fServicePeriodDate As DateTime
    Public Property ServicePeriodDate() As DateTime
        Get
            Return fServicePeriodDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServicePeriodDate", fServicePeriodDate, value)
        End Set
    End Property
    Dim fFilingUnitId As Integer
    Public Property FilingUnitId() As Integer
        Get
            Return fFilingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FilingUnitId", fFilingUnitId, value)
        End Set
    End Property
    Dim fSupplierTypeId As Integer
    Public Property SupplierTypeId() As Integer
        Get
            Return fSupplierTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierTypeId", fSupplierTypeId, value)
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
    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property
    Dim fComents As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Coments() As String
        Get
            Return fComents
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Coments", fComents, value)
        End Set
    End Property
    Dim fStatus As Byte
    '<Persistent("Status")> _
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fInitialBalance As Boolean
    Public Property InitialBalance() As Boolean
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fIdInitialBalance As Integer
    Public Property IdInitialBalance() As Integer
        Get
            Return fIdInitialBalance
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdInitialBalance", fIdInitialBalance, value)
        End Set
    End Property
    Dim fPreviousBudget As Boolean
    Public Property PreviousBudget() As Boolean
        Get
            Return fPreviousBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PreviousBudget", fPreviousBudget, value)
        End Set
    End Property
    Dim fShares As Integer
    Public Property Shares() As Integer
        Get
            Return fShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Shares", fShares, value)
        End Set
    End Property
    Dim fInvoiceValue As Decimal
    Public Property InvoiceValue() As Decimal
        Get
            Return fInvoiceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValue", fInvoiceValue, value)
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
    Dim fIdOperatingUnit As Integer
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
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

    Dim fDeductibleIva As Boolean?
    Public Property DeductibleIva() As Boolean?
        Get
            Return fDeductibleIva
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("DeductibleIva", fDeductibleIva, value)
        End Set
    End Property

    Dim fIdSuppliersDistributionLines As CommonSuppliersDistributionLinesXpo
    <Association("PaymentsAccountPayableReferencesCommonSuppliersDistributionLinesXpo")>
    Public Property IdSuppliersDistributionLines() As CommonSuppliersDistributionLinesXpo
        Get
            Return fIdSuppliersDistributionLines
        End Get
        Set(ByVal value As CommonSuppliersDistributionLinesXpo)
            SetPropertyValue(Of CommonSuppliersDistributionLinesXpo)("IdSuppliersDistributionLines", fIdSuppliersDistributionLines, value)
        End Set
    End Property
    Dim fDocumentSupportId As Integer
    Public Property DocumentSupportId() As Integer
        Get
            Return fDocumentSupportId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentSupportId", fDocumentSupportId, value)
        End Set
    End Property
    Dim fTaxRegistration As Integer?
    Public Property TaxRegistration() As Integer?
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("TaxRegistration", fTaxRegistration, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property
#End Region

#Region "Navigations Properties"

    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsAccountPayable", GetType(PaymentsPaymentNotesAccountPayableAdvance))>
    Public ReadOnly Property PaymentsPaymentNotesAccountPayableAdvance() As XPCollection(Of PaymentsPaymentNotesAccountPayableAdvance)
        Get
            Return GetCollection(Of PaymentsPaymentNotesAccountPayableAdvance)("PaymentsPaymentNotesAccountPayableAdvance")
        End Get
    End Property

    <Association("PaymentsPaymentTransferDetailReferencesPaymentsAccountPayable", GetType(PaymentsPaymentTransferDetail))>
    Public ReadOnly Property PaymentsPaymentTransferDetail() As XPCollection(Of PaymentsPaymentTransferDetail)
        Get
            Return GetCollection(Of PaymentsPaymentTransferDetail)("PaymentsPaymentTransferDetail")
        End Get
    End Property

    <Association("TreasuryCrossingAccountDetailCxPReferencesPaymentsAccountPayable", GetType(TreasuryCrossingAccountDetailCxP))>
    Public ReadOnly Property TreasuryCrossingAccountDetailCxP() As XPCollection(Of TreasuryCrossingAccountDetailCxP)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxP)("TreasuryCrossingAccountDetailCxP")
        End Get
    End Property

    <Association("TreasuryDischargeBillReferencesPaymentsAccountPayable", GetType(TreasuryDischargeBill))>
    Public ReadOnly Property TreasuryDischargeBill() As XPCollection(Of TreasuryDischargeBill)
        Get
            Return GetCollection(Of TreasuryDischargeBill)("TreasuryDischargeBill")
        End Get
    End Property

    <Association("PaymentsAccountPayableSharesXpoPReferencesPaymentsAccountPayable", GetType(PaymentsAccountPayableSharesXpoP))>
    Public ReadOnly Property PaymentsAccountPayableSharesXpoP() As XPCollection(Of PaymentsAccountPayableSharesXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableSharesXpoP)("PaymentsAccountPayableSharesXpoP")
        End Get
    End Property

    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesPaymentsAccountPayable", GetType(PaymentsAccountPayableDetailConceptXpoP))>
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpoP() As XPCollection(Of PaymentsAccountPayableDetailConceptXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpoP)("PaymentsAccountPayableDetailConceptXpoP")
        End Get
    End Property

    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesPaymentsAccountPayable", GetType(PaymentsInitialBalanceAccountPayableXpo))>
    Public ReadOnly Property PaymentsInitialBalanceAccountPayableXpo() As XPCollection(Of PaymentsInitialBalanceAccountPayableXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAccountPayableXpo)("PaymentsInitialBalanceAccountPayableXpo")
        End Get
    End Property

    <Association("PaymentsDeferredCausationXpoReferencesPaymentsAccountPayable", GetType(PaymentsDeferredCausationXpo))>
    Public ReadOnly Property PaymentsDeferredCausationXpo() As XPCollection(Of PaymentsDeferredCausationXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationXpo)("PaymentsDeferredCausationXpo")
        End Get
    End Property

    <Association("PaymentsAccountPayableTransferDetailReportXpoReferencesPaymentsAccountPayable", GetType(PaymentsAccountPayableTransferDetailReportXpo))>
    Public ReadOnly Property PaymentsAccountPayableTransferDetailReportXpo() As XPCollection(Of PaymentsAccountPayableTransferDetailReportXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableTransferDetailReportXpo)("PaymentsAccountPayableTransferDetailReportXpo")
        End Get
    End Property

    <Association("PaymentsViewAccountObligation_References_PaymentsAccountPayable", GetType(ViewAccountObligationXpo))>
    Public ReadOnly Property ViewAccountObligation() As XPCollection(Of ViewAccountObligationXpo)
        Get
            Return GetCollection(Of ViewAccountObligationXpo)("ViewAccountObligation")
        End Get
    End Property

    <Association("PaymentsViewAccountPayableCommitment_References_PaymentsAccountPayable", GetType(ViewAccountPayableCommitmentXpo))>
    Public ReadOnly Property ViewAccountPayableCommitment() As XPCollection(Of ViewAccountPayableCommitmentXpo)
        Get
            Return GetCollection(Of ViewAccountPayableCommitmentXpo)("ViewAccountPayableCommitment")
        End Get
    End Property

    <Association("PaymentsAccountPayableDocumentSupport_References_PaymentsAccountPayable", GetType(AccountPayableDocumentSupportXpo))>
    Public ReadOnly Property AccountPayableDocumentSupport() As XPCollection(Of AccountPayableDocumentSupportXpo)
        Get
            Return GetCollection(Of AccountPayableDocumentSupportXpo)("AccountPayableDocumentSupport")
        End Get
    End Property

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_PaymentsAccountPayable")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

#End Region

#Region "Non Persistens Properties"

    'Propiedad Añadida valor total en letras
    Dim fValueLetters As String
    <NonPersistent()> _
    Public Property ValueLetters() As String
        Get
            Return fValueLetters
        End Get
        Set(ByVal value As String)
            Me.fValueLetters = value
        End Set
    End Property

    'Propiedad Añadida saldo total en letras
    Dim fBalanceLetters As String
    <NonPersistent()> _
    Public Property BalanceLetters() As String
        Get
            Return fBalanceLetters
        End Get
        Set(ByVal value As String)
            Me.fBalanceLetters = value
        End Set
    End Property

    'Propiedad Añadida total de diferencia de dias de la fecha de vencimiento con la fecha del flitro
    Dim fDifferenceDays As Integer
    <NonPersistent()> _
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
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    Dim fEntityCodeNameText As String
    <NonPersistent()> _
    Public Property EntityCodeNameText() As String
        Get
            Return Me.fEntityCodeNameText
        End Get
        Set(value As String)
            Me.fEntityCodeNameText = value
        End Set
    End Property

    'Propiedad Añadida de la lista del objeto
    Dim fViewExtractAccountPayable As List(Of VReportExtractAccountPayable)
    <NonPersistent()>
    Public Property ViewExtractAccountPayable() As List(Of VReportExtractAccountPayable)
        Get
            Return fViewExtractAccountPayable
        End Get
        Set(ByVal value As List(Of VReportExtractAccountPayable))
            Me.fViewExtractAccountPayable = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene los ingresos acumulados previos de retención
    ''' </summary>
    Dim fTotalIncomeSum As Decimal
    <NonPersistent>
    Public ReadOnly Property TotalIncomeSum As Decimal
        Get
            For Each detailConcept In Me.PaymentsAccountPayableDetailConceptXpoP
                If detailConcept.PaymentsAccountPayableConceptLiquidationXpo IsNot Nothing Then
                    fTotalIncomeSum = detailConcept.PaymentsAccountPayableConceptLiquidationXpo.Sum(Function(s) s.TotalIncome)
                End If
            Next
            Return fTotalIncomeSum
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
