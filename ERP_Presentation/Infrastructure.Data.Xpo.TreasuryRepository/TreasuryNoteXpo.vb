Imports DevExpress.Xpo

<Persistent("Treasury.TreasuryNote")> _
Public Class TreasuryNoteXpo
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

    Dim fNoteDate As Date
    Public Property NoteDate() As Date
        Get
            Return fNoteDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("NoteDate", fNoteDate, value)
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

    Dim fCashRegisterId As Integer
    Public Property CashRegisterId() As Integer
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property

    Dim fEntityBankAccountId As Integer
    Public Property EntityBankAccountId() As Integer
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fVoucherTransactionId As VoucherTransactionXpo
    <Association("Treasury_TreasuryNoteReferencesTreasury_VoucherTransaction")> _
    Public Property VoucherTransactionId() As VoucherTransactionXpo
        Get
            Return fVoucherTransactionId
        End Get
        Set(ByVal value As VoucherTransactionXpo)
            SetPropertyValue(Of VoucherTransactionXpo)("VoucherTransactionId", fVoucherTransactionId, value)
        End Set
    End Property

    Dim fCashReceiptId As CashReceiptsXpo
    <Association("Treasury_TreasuryNoteReferencesTreasury_CashReceipts")> _
    Public Property CashReceiptId() As CashReceiptsXpo
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As CashReceiptsXpo)
            SetPropertyValue(Of CashReceiptsXpo)("CashReceiptId", fCashReceiptId, value)
        End Set
    End Property

    Dim fConsignmentId As ConsignmentXpo
    <Association("Treasury_TreasuryNoteReferencesTreasury_Consignment")> _
    Public Property ConsignmentId() As ConsignmentXpo
        Get
            Return fConsignmentId
        End Get
        Set(ByVal value As ConsignmentXpo)
            SetPropertyValue(Of ConsignmentXpo)("ConsignmentId", fConsignmentId, value)
        End Set
    End Property

    Dim fCrossingAccountId As CrossingAccountXpo
    <Association("Treasury_TreasuryNoteReferencesTreasury_CrossingAccount")> _
    Public Property CrossingAccountId() As CrossingAccountXpo
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As CrossingAccountXpo)
            SetPropertyValue(Of CrossingAccountXpo)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
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

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceTreasuryNoteXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(NoteType = 1, 'Cuenta Bancaria', NoteType = 2, 'Caja', NoteType = 3, 'Reversión de Comprobante de Egreso', NoteType = 4, 'Reversión de Recibo de Caja', NoteType = 5, 'Reversión de Consignación', NoteType = 6, 'Reversión cruce CxC vs CxP', NoteType = 7, 'Devolución de Recibo de Caja', NoteType = 8, 'Gastos Conciliación Tarjetas' ,'')")>
    Public ReadOnly Property NoteTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NoteTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("iif(CashReceiptId.Id > 0, CashReceiptId.Value, ConsignmentId.Id > 0, ConsignmentId.Value, CrossingAccountId.Id > 0, Value, VoucherTransactionId.Id > 0, VoucherTransactionId.Value, Treasury_TreasuryNoteDetails.count > 0, Treasury_TreasuryNoteDetails.Sum(Value), 0)")>
    Public ReadOnly Property TotalValueDetail() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("TotalValueDetail"))
        End Get
    End Property
#End Region

#Region "Navigations"

    <Association("Treasury_TreasuryNoteDetailReferencesTreasury_TreasuryNote", GetType(TreasuryNoteDetailXpo))> _
    Public ReadOnly Property Treasury_TreasuryNoteDetails() As XPCollection(Of TreasuryNoteDetailXpo)
        Get
            Return GetCollection(Of TreasuryNoteDetailXpo)("Treasury_TreasuryNoteDetails")
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
