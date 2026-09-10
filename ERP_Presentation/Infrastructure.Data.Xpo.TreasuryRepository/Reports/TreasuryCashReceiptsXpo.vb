Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Treasury.CashReceipts")> _
Public Class TreasuryCashReceiptsXpo
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
    '<Indexed("IX_CashReceipt")> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("TreasuryCashReceiptsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fCollectType As Byte
    Public Property CollectType() As Byte
        Get
            Return fCollectType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CollectType", fCollectType, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCashReceiptsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryCashReceiptsXpoReferencesPayrollCostCenterXpo")> _
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(2000)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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
    Dim fIdCashRegister As TreasuryCashRegistersXpo
    <Association("TreasuryCashReceiptsXpoReferencesTreasuryCashRegistersXpo")> _
    Public Property IdCashRegister() As TreasuryCashRegistersXpo
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property
    Dim fIdBankAccount As TreasuryEntityBankAccountsXpo
    <Association("TreasuryCashReceiptsXpoReferencesTreasuryEntityBankAccountsXpo")> _
    Public Property IdBankAccount() As TreasuryEntityBankAccountsXpo
        Get
            Return fIdBankAccount
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("IdBankAccount", fIdBankAccount, value)
        End Set
    End Property
    Dim fPaymentResponsibles As String
    Public Property PaymentResponsibles() As String
        Get
            Return fPaymentResponsibles
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PaymentResponsibles", fPaymentResponsibles, value)
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
    Dim fIdRefund As Integer
    Public Property IdRefund() As Integer
        Get
            Return fIdRefund
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRefund", fIdRefund, value)
        End Set
    End Property
    Dim fStatus As String
    <Persistent("Status")>
    Public Property Status() As String
        Get
            Select Case fStatus
                Case 1
                    fStatus = ResourceManager.GetString("StateRegistered")
                Case 2
                    fStatus = ResourceManager.GetString("StateConfirmed")
                Case 3
                    fStatus = ResourceManager.GetString("StatusCanceled")
                Case 4
                    fStatus = ResourceManager.GetString("StatusReverse")
            End Select
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    <PersistentAlias("Iif(CollectType = 1, IdCashRegister.CurrencyAbbreviation, IdBankAccount.CurrencyAbbreviation)")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("Iif(CollectType = 1, IdCashRegister.CurrencyNameISO, IdBankAccount.CurrencyNameISO)")>
    Public ReadOnly Property CurrencyNameISO As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyNameISO"))
        End Get
    End Property

    <PersistentAlias("Iif(CollectType = 1, IdCashRegister.CurrencyId, IdBankAccount.CurrencyId)")>
    Public ReadOnly Property CurrencyId As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Iif(CollectType = 1, IdCashRegister.CommonCurrency.ISO4217Xpo.CodeAbbreviation, IdBankAccount.CommonCurrency.ISO4217Xpo.CodeAbbreviation)")>
    Public ReadOnly Property CurrencyAbbreviationISO As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviationISO"))
        End Get
    End Property

    'Propiedad Añadida
    Dim fReverseValue As Decimal
    <NonPersistent()>
    Public Property ReverseValue() As Decimal
        Get
            fReverseValue = IIf(Me.Status = ResourceManager.GetString("StatusReverse"), Me.Value, 0)
            Return fReverseValue
        End Get
        Set(ByVal value As Decimal)
            Me.fReverseValue = value
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

    'Propiedad Añadida
    Dim fGroupByCashEntityBank As String
    <NonPersistent()> _
    Public Property GroupByCashEntityBank() As String
        Get
            Return fGroupByCashEntityBank
        End Get
        Set(ByVal value As String)
            Me.fGroupByCashEntityBank = value
        End Set
    End Property

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

    'Propiedad Añadida codigo y nombre de usuario
    Dim fCodeNameUser As String
    <NonPersistent()>
    Public Property CodeNameUser() As String
        Get
            Return fCodeNameUser
        End Get
        Set(ByVal value As String)
            Me.fCodeNameUser = value
        End Set
    End Property

    'Propiedad Añadida Valor debito y credito
    Dim fValueDebit As Decimal
    <NonPersistent()>
    Public Property ValueDebit() As Decimal
        Get
            Return fValueDebit
        End Get
        Set(ByVal value As Decimal)
            Me.fValueDebit = value
        End Set
    End Property

    'Propiedad Añadida Valor debito y credito
    Dim fValueCredit As Decimal
    <NonPersistent()>
    Public Property ValueCredit() As Decimal
        Get
            Return fValueCredit
        End Get
        Set(ByVal value As Decimal)
            Me.fValueCredit = value
        End Set
    End Property

    <Association("TreasuryCashReceiptDetailsXpoReferencesTreasuryCashReceiptsXpo", GetType(TreasuryCashReceiptDetailsXpo))>
    Public ReadOnly Property TreasuryCashReceiptDetailsXpo() As XPCollection(Of TreasuryCashReceiptDetailsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptDetailsXpo)("TreasuryCashReceiptDetailsXpo")
        End Get
    End Property
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryCashReceiptsXpo", GetType(TreasuryPaymentMethodsXpo))>
    Public ReadOnly Property TreasuryPaymentMethodsXpo() As XPCollection(Of TreasuryPaymentMethodsXpo)
        Get
            Return GetCollection(Of TreasuryPaymentMethodsXpo)("TreasuryPaymentMethodsXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesTreasuryCashReceiptsXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesTreasuryCashReceiptsXpo", GetType(TreasuryNotesXpo))> _
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
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
