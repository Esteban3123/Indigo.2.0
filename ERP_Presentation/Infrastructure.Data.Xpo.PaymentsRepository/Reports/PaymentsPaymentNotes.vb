Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.PaymentNotes")> _
Public Class PaymentsPaymentNotes
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
    '<Indexed("IX_Notes")> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fNoteDate As DateTime
    Public Property NoteDate() As DateTime
        Get
            Return fNoteDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("NoteDate", fNoteDate, value)
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
    Dim fIdSupplier As Maintenance_Supplier
    <Association("PaymentsPaymentNotesReferencesMaintenance_Supplier")> _
    Public Property IdSupplier() As Maintenance_Supplier
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdSupplierDistributionLines As CommonSuppliersDistributionLinesXpo
    <Association("PaymentsPaymentNotesReferencesCommonSuppliersDistributionLinesXpo")> _
    Public Property IdSupplierDistributionLines() As CommonSuppliersDistributionLinesXpo
        Get
            Return fIdSupplierDistributionLines
        End Get
        Set(ByVal value As CommonSuppliersDistributionLinesXpo)
            SetPropertyValue(Of CommonSuppliersDistributionLinesXpo)("IdSupplierDistributionLines", fIdSupplierDistributionLines, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fComment As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
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
    Dim fReinstatement As Boolean
    Public Property Reinstatement() As Boolean
        Get
            Return fReinstatement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Reinstatement", fReinstatement, value)
        End Set
    End Property
    Dim fIdVoucherTransaction As TreasuryVoucherTransaction
    <Association("PaymentsPaymentNotesReferencesTreasuryVoucherTransaction")> _
    Public Property IdVoucherTransaction() As TreasuryVoucherTransaction
        Get
            Return fIdVoucherTransaction
        End Get
        Set(ByVal value As TreasuryVoucherTransaction)
            SetPropertyValue(Of TreasuryVoucherTransaction)("IdVoucherTransaction", fIdVoucherTransaction, value)
        End Set
    End Property
    Dim fIndicatesBillAdvance As Byte
    Public Property IndicatesBillAdvance() As Byte
        Get
            Return fIndicatesBillAdvance
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("IndicatesBillAdvance", fIndicatesBillAdvance, value)
        End Set
    End Property
    Dim fCancelCheck As Boolean
    Public Property CancelCheck() As Boolean
        Get
            Return fCancelCheck
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CancelCheck", fCancelCheck, value)
        End Set
    End Property
    Dim fBudgetInterface As Boolean
    Public Property BudgetInterface() As Boolean
        Get
            Return fBudgetInterface
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("BudgetInterface", fBudgetInterface, value)
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

    'Propiedad Añadida valor total en letras
    Dim fValueTotal As Decimal
    <NonPersistent()> _
    Public Property ValueTotal() As Decimal
        Get
            Return fValueTotal
        End Get
        Set(ByVal value As Decimal)
            Me.fValueTotal = value
        End Set
    End Property

    'Propiedad Añadida valor total en letras
    Dim fValueLetters As String
    <NonPersistent()>
    Public Property ValueLetters() As String
        Get
            Return fValueLetters
        End Get
        Set(ByVal value As String)
            Me.fValueLetters = value
        End Set
    End Property

    Dim fTaxRegistration As Byte
    <NonPersistent()>
    Public Property TaxRegistration() As Byte
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Byte)
            Me.fTaxRegistration = value
        End Set
    End Property

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_PaymentsPaymentNotes")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
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

    Dim fIdAccountPayable As AccountPayableXpo
    <Association("PaymentsPaymentNotesReferencesAccountPayableXpo")>
    Public Property IdAccountPayable() As AccountPayableXpo
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue(Of AccountPayableXpo)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsPaymentNotes", GetType(PaymentsPaymentNotesAccountPayableAdvance))> _
    Public ReadOnly Property Payments_PaymentNotesAccountPayableAdvances() As XPCollection(Of PaymentsPaymentNotesAccountPayableAdvance)
        Get
            Return GetCollection(Of PaymentsPaymentNotesAccountPayableAdvance)("Payments_PaymentNotesAccountPayableAdvances")
        End Get
    End Property
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesPaymentsPaymentNotes", GetType(PaymentsPaymentsNoteDetailsXpo))> _
    Public ReadOnly Property PaymentsPaymentsNoteDetailsXpo() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("PaymentsPaymentsNoteDetailsXpo")
        End Get
    End Property
    <Association("PaymentsPaymentsNoteSharesXpoReferencesPaymentsPaymentNotes", GetType(PaymentsPaymentsNoteSharesXpo))> _
    Public ReadOnly Property PaymentsPaymentsNoteSharesXpo() As XPCollection(Of PaymentsPaymentsNoteSharesXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteSharesXpo)("PaymentsPaymentsNoteSharesXpo")
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
