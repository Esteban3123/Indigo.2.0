Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableTransfer")> _
Public Class PaymentsAccountPayableTransferReportXpo
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fFilingUnitSourceId As PaymentsFillingUnitReportXpo
    <Association("PaymentsAccountPayableTransferReportXpoReferencesPaymentsFillingUnitReportXpo")> _
    Public Property FilingUnitSourceId() As PaymentsFillingUnitReportXpo
        Get
            Return fFilingUnitSourceId
        End Get
        Set(ByVal value As PaymentsFillingUnitReportXpo)
            SetPropertyValue(Of PaymentsFillingUnitReportXpo)("FilingUnitSourceId", fFilingUnitSourceId, value)
        End Set
    End Property
    Dim fFilingUnitTargetId As PaymentsFillingUnitReportXpo
    <Association("PaymentsAccountPayableTransferReportXpoReferencesPaymentsFillingUnitReportXpo1")> _
    Public Property FilingUnitTargetId() As PaymentsFillingUnitReportXpo
        Get
            Return fFilingUnitTargetId
        End Get
        Set(ByVal value As PaymentsFillingUnitReportXpo)
            SetPropertyValue(Of PaymentsFillingUnitReportXpo)("FilingUnitTargetId", fFilingUnitTargetId, value)
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
    <Association("PaymentsAccountPayableTransferDetailReportXpoReferencesPaymentsAccountPayableTransferReportXpo", GetType(PaymentsAccountPayableTransferDetailReportXpo))> _
    Public ReadOnly Property PaymentsAccountPayableTransferDetailReportXpo() As XPCollection(Of PaymentsAccountPayableTransferDetailReportXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableTransferDetailReportXpo)("PaymentsAccountPayableTransferDetailReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
