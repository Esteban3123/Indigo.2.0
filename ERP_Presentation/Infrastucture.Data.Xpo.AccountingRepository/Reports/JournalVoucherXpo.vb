Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.JournalVouchers")> _
Public Class JournalVoucherXpo
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
    Dim fAccountingMovementId As Integer
    <Persistent("AccountingMovementId")> _
    Public Property AccountingMovementId() As Integer
        Get
            Return fAccountingMovementId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountingMovementId", fAccountingMovementId, value)
        End Set
    End Property

    Dim fConsecutive As Long
    Public Property Consecutive() As Long
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Consecutive", fConsecutive, value)
        End Set
    End Property
    Dim fIdJournalVoucher As VoucherTypeXpo
    <Association("GeneralLedger_JournalVouchersReferencesGeneralLedger_JournalVoucherTypes")> _
    Public Property IdJournalVoucher() As VoucherTypeXpo
        Get
            Return fIdJournalVoucher
        End Get
        Set(ByVal value As VoucherTypeXpo)
            SetPropertyValue(Of VoucherTypeXpo)("IdJournalVoucher", fIdJournalVoucher, value)
        End Set
    End Property
    Dim fVoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return fVoucherDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VoucherDate", fVoucherDate, value)
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
    Dim fDetail As String
    <Size(500)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fEntityCode As String
    <Size(20)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
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
    Dim fEntityName As String
    <Size(250)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property
    Dim fIsClosedYear As Boolean
    Public Property IsClosedYear() As Boolean
        Get
            Return fIsClosedYear
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsClosedYear", fIsClosedYear, value)
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

    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_JournalVouchers", GetType(JournalVoucherDetailXpo))> _
    Public ReadOnly Property GeneralLedger_JournalVoucherDetailss() As XPCollection(Of JournalVoucherDetailXpo)
        Get
            Return GetCollection(Of JournalVoucherDetailXpo)("GeneralLedger_JournalVoucherDetailss")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
