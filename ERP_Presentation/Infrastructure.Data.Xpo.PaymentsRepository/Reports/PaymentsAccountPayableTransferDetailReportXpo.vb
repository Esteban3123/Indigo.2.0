Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableTransferDetail")> _
Public Class PaymentsAccountPayableTransferDetailReportXpo
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
    Dim fAccountPayableTransferId As PaymentsAccountPayableTransferReportXpo
    <Association("PaymentsAccountPayableTransferDetailReportXpoReferencesPaymentsAccountPayableTransferReportXpo")> _
    Public Property AccountPayableTransferId() As PaymentsAccountPayableTransferReportXpo
        Get
            Return fAccountPayableTransferId
        End Get
        Set(ByVal value As PaymentsAccountPayableTransferReportXpo)
            SetPropertyValue(Of PaymentsAccountPayableTransferReportXpo)("AccountPayableTransferId", fAccountPayableTransferId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsAccountPayableTransferDetailReportXpoReferencesPaymentsAccountPayable")> _
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fRefundId As TreasuryRefundsXpo
    <Association("Payments_AccountPayableTransferDetailReferencesTreasury_Refunds")> _
    Public Property RefundId() As TreasuryRefundsXpo
        Get
            Return fRefundId
        End Get
        Set(ByVal value As TreasuryRefundsXpo)
            SetPropertyValue(Of TreasuryRefundsXpo)("RefundId", fRefundId, value)
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
    Dim fAcceptanceUser As String
    <Size(20)> _
    Public Property AcceptanceUser() As String
        Get
            Return fAcceptanceUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AcceptanceUser", fAcceptanceUser, value)
        End Set
    End Property
    Dim fAcceptanceDate As DateTime
    Public Property AcceptanceDate() As DateTime
        Get
            Return fAcceptanceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AcceptanceDate", fAcceptanceDate, value)
        End Set
    End Property
    Dim fRejectionUser As String
    <Size(20)> _
    Public Property RejectionUser() As String
        Get
            Return fRejectionUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RejectionUser", fRejectionUser, value)
        End Set
    End Property
    Dim fRejectionDate As DateTime
    Public Property RejectionDate() As DateTime
        Get
            Return fRejectionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RejectionDate", fRejectionDate, value)
        End Set
    End Property
    Dim fRejectionReasonId As Integer
    Public Property RejectionReasonId() As Integer
        Get
            Return fRejectionReasonId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RejectionReasonId", fRejectionReasonId, value)
        End Set
    End Property
    Dim fRejectionDescription As String
    <Size(500)> _
    Public Property RejectionDescription() As String
        Get
            Return fRejectionDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RejectionDescription", fRejectionDescription, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
