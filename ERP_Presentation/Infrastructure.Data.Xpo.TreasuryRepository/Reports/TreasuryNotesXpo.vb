Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base

<Persistent("Treasury.TreasuryNote")> _
Public Class TreasuryNotesXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fNoteType As Byte
    Public Property NoteType() As Byte
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("NoteType", fNoteType, value)
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

    Dim fCashRegisterId As TreasuryCashRegistersXpo
    <Association("TreasuryNotesXpoReferencesTreasuryCashRegistersXpo")> _
    Public Property CashRegisterId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property

    Dim fEntityBankAccountId As TreasuryEntityBankAccountsXpo
    <Association("TreasuryNotesXpoReferencesTreasuryEntityBankAccountsXpo")> _
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountsXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fVoucherTransactionId As TreasuryVoucherTransactionXpo
    <Association("TreasuryNotesXpoReferencesTreasuryVoucherTransactionXpo")> _
    Public Property VoucherTransactionId() As TreasuryVoucherTransactionXpo
        Get
            Return fVoucherTransactionId
        End Get
        Set(ByVal value As TreasuryVoucherTransactionXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionXpo)("VoucherTransactionId", fVoucherTransactionId, value)
        End Set
    End Property

    Dim fCashReceiptId As TreasuryCashReceiptsXpo
    <Association("TreasuryNotesXpoReferencesTreasuryCashReceiptsXpo")> _
    Public Property CashReceiptId() As TreasuryCashReceiptsXpo
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As TreasuryCashReceiptsXpo)
            SetPropertyValue(Of TreasuryCashReceiptsXpo)("CashReceiptId", fCashReceiptId, value)
        End Set
    End Property

    Dim fConsignmentId As TreasuryConsignmentXpo
    <Association("TreasuryNotesXpo_References_TreasuryConsignmentXpo")> _
    Public Property ConsignmentId() As TreasuryConsignmentXpo
        Get
            Return fConsignmentId
        End Get
        Set(ByVal value As TreasuryConsignmentXpo)
            SetPropertyValue(Of TreasuryConsignmentXpo)("ConsignmentId", fConsignmentId, value)
        End Set
    End Property

    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryNotesXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("TreasuryNotesXpoReferencesPayrollCostCenterXpo")>
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("TreasuryNotesXpoReferencesCommonCurrencyXpo")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    <PersistentAlias("CurrencyId.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
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

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
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

#End Region

#Region "Custom Members"

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

#End Region

#Region "Navigations"

    <Association("Treasury_TreasuryNoteDetailReferencesTreasury_TreasuryNote", GetType(TreasuryNotesDetailXpo))> _
    Public ReadOnly Property Treasury_TreasuryNoteDetails() As XPCollection(Of TreasuryNotesDetailXpo)
        Get
            Return GetCollection(Of TreasuryNotesDetailXpo)("Treasury_TreasuryNoteDetails")
        End Get
    End Property

#End Region

#Region "Builders"

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
