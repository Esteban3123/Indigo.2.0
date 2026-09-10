Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#Region "Structure"

Public Structure KeyValueRefund

    <Persistent("Row")>
    Public Property Row As Integer

    <Persistent("RefundId")>
    Public Property RefundId As Integer

    <Persistent("RefundCode")>
    Public Property RefundCode As String

End Structure

#End Region

<Persistent("Payments.VTrazabilityRefunds")>
Public Class VTrazabilityRefundsXpo
    Inherits XPLiteObject

    <Key(), Persistent()>
    Public Property Key As KeyValueRefund

    Dim fRow As Integer
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property

    Dim fRefundId As Integer
    Public Property RefundId() As Integer
        Get
            Return fRefundId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RefundId", fRefundId, value)
        End Set
    End Property

    Dim fRefundCode As String
    <Size(223)>
    Public Property RefundCode() As String
        Get
            Return fRefundCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RefundCode", fRefundCode, value)
        End Set
    End Property

    Dim fFilingUnit As String
    <Size(223)>
    Public Property FilingUnit() As String
        Get
            Return fFilingUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FilingUnit", fFilingUnit, value)
        End Set
    End Property

    Dim fCodeTransfer As String
    <Size(20)>
    Public Property CodeTransfer() As String
        Get
            Return fCodeTransfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeTransfer", fCodeTransfer, value)
        End Set
    End Property

    Dim fFilingSource As String
    <Size(223)>
    Public Property FilingSource() As String
        Get
            Return fFilingSource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FilingSource", fFilingSource, value)
        End Set
    End Property

    Dim fFilingTarget As String
    <Size(223)>
    Public Property FilingTarget() As String
        Get
            Return fFilingTarget
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FilingTarget", fFilingTarget, value)
        End Set
    End Property

    Dim fStateTraslate As String
    <Size(10)>
    Public Property StateTraslate() As String
        Get
            Return fStateTraslate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StateTraslate", fStateTraslate, value)
        End Set
    End Property

    Dim fCreateUserOfficeTranfer As String
    <Size(20)>
    Public Property CreateUserOfficeTranfer() As String
        Get
            Return fCreateUserOfficeTranfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreateUserOfficeTranfer", fCreateUserOfficeTranfer, value)
        End Set
    End Property

    Dim fConfirmationDateOfficeTranfer As DateTime
    Public Property ConfirmationDateOfficeTranfer() As DateTime
        Get
            Return fConfirmationDateOfficeTranfer
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDateOfficeTranfer", fConfirmationDateOfficeTranfer, value)
        End Set
    End Property

    Dim fConfirmationUserOfficeTranfer As String
    <Size(20)>
    Public Property ConfirmationUserOfficeTranfer() As String
        Get
            Return fConfirmationUserOfficeTranfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUserOfficeTranfer", fConfirmationUserOfficeTranfer, value)
        End Set
    End Property

    Dim fAnnulmentDateOfficeTranfer As DateTime
    Public Property AnnulmentDateOfficeTranfer() As DateTime
        Get
            Return fAnnulmentDateOfficeTranfer
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDateOfficeTranfer", fAnnulmentDateOfficeTranfer, value)
        End Set
    End Property

    Dim fAnnulmentUserOfficeTranfer As String
    <Size(20)>
    Public Property AnnulmentUserOfficeTranfer() As String
        Get
            Return fAnnulmentUserOfficeTranfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUserOfficeTranfer", fAnnulmentUserOfficeTranfer, value)
        End Set
    End Property

    Dim fStateDetailTraslate As String
    <Size(24)>
    Public Property StateDetailTraslate() As String
        Get
            Return fStateDetailTraslate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StateDetailTraslate", fStateDetailTraslate, value)
        End Set
    End Property

    Dim fAcceptanceDateDetailTranfer As DateTime
    Public Property AcceptanceDateDetailTranfer() As DateTime
        Get
            Return fAcceptanceDateDetailTranfer
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AcceptanceDateDetailTranfer", fAcceptanceDateDetailTranfer, value)
        End Set
    End Property

    Dim fAcceptanceUserDetailTranfer As String
    <Size(20)>
    Public Property AcceptanceUserDetailTranfer() As String
        Get
            Return fAcceptanceUserDetailTranfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AcceptanceUserDetailTranfer", fAcceptanceUserDetailTranfer, value)
        End Set
    End Property

    Dim fRejectionDateDetailTranfer As DateTime
    Public Property RejectionDateDetailTranfer() As DateTime
        Get
            Return fRejectionDateDetailTranfer
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RejectionDateDetailTranfer", fRejectionDateDetailTranfer, value)
        End Set
    End Property

    Dim fRejectionUserDetailTranfer As String
    <Size(20)>
    Public Property RejectionUserDetailTranfer() As String
        Get
            Return fRejectionUserDetailTranfer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RejectionUserDetailTranfer", fRejectionUserDetailTranfer, value)
        End Set
    End Property

    Dim fRejectionReason As String
    <Size(73)>
    Public Property RejectionReason() As String
        Get
            Return fRejectionReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RejectionReason", fRejectionReason, value)
        End Set
    End Property

    Dim fRejectionDescription As String
    <Size(500)>
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
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class


