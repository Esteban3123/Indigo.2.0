Imports DevExpress.Xpo

<Persistent("Payments.PaymentsNoteDetails")> _
Public Class PaymentsPaymentsNoteDetailsXpo
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
    Dim fIdPaymentsNote As PaymentsPaymentNotes
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesPaymentsPaymentNotes")> _
    Public Property IdPaymentsNote() As PaymentsPaymentNotes
        Get
            Return fIdPaymentsNote
        End Get
        Set(ByVal value As PaymentsPaymentNotes)
            SetPropertyValue(Of PaymentsPaymentNotes)("IdPaymentsNote", fIdPaymentsNote, value)
        End Set
    End Property
    Dim fIdAccountPayableConceptNotes As PaymentsAccountPayableConceptNotesXpo
    <Association("Payments_PaymentsNoteDetailsReferencesPayments_AccountPayableConceptNotes")> _
    Public Property IdAccountPayableConceptNotes() As PaymentsAccountPayableConceptNotesXpo
        Get
            Return fIdAccountPayableConceptNotes
        End Get
        Set(ByVal value As PaymentsAccountPayableConceptNotesXpo)
            SetPropertyValue(Of PaymentsAccountPayableConceptNotesXpo)("IdAccountPayableConceptNotes", fIdAccountPayableConceptNotes, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpoP
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesPayrollCostCenterXpoP")> _
    Public Property IdCostCenter() As PayrollCostCenterXpoP
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("IdCostCenter", fIdCostCenter, value)
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
    Dim fBillingValue As Decimal
    Public Property BillingValue() As Decimal
        Get
            Return fBillingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillingValue", fBillingValue, value)
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
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fComments As String
    <Size(500)> _
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comments", fComments, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property

    Dim fDiscountableIVA As Boolean?
    Public Property DiscountableIVA() As Boolean?
        Get
            Return fDiscountableIVA
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("DiscountableIVA", fDiscountableIVA, value)
        End Set
    End Property

    Dim fIVAValue As Decimal?
    Public Property IVAValue() As Decimal?
        Get
            Return fIVAValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("IVAValue", fIVAValue, value)
        End Set
    End Property

    Dim fTotalConceptValue As Decimal?
    Public Property TotalConceptValue() As Decimal?
        Get
            Return fTotalConceptValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalConceptValue", fTotalConceptValue, value)
        End Set
    End Property

    Dim fIdGeneralLedgerIVA As GeneralLedgerIVAXpo
    <Association("GeneralLedger_References_PaymentsPaymentsNoteDetailsXpo")>
    Public Property IdGeneralLedgerIVA() As GeneralLedgerIVAXpo
        Get
            Return fIdGeneralLedgerIVA
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("IdGeneralLedgerIVA", fIdGeneralLedgerIVA, value)
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
    Public Property TaxRegistration() As Byte
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TaxRegistration", fTaxRegistration, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
