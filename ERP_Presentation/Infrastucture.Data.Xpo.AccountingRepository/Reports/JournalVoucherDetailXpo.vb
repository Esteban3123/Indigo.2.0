Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.JournalVoucherDetails")> _
Public Class JournalVoucherDetailXpo
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
    Dim fIdAccounting As JournalVoucherXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_JournalVouchers")> _
    Public Property IdAccounting() As JournalVoucherXpo
        Get
            Return fIdAccounting
        End Get
        Set(ByVal value As JournalVoucherXpo)
            SetPropertyValue(Of JournalVoucherXpo)("IdAccounting", fIdAccounting, value)
        End Set
    End Property
    Dim fIdMainAccount As Integer
    Public Property IdMainAccount() As Integer
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
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
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
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
    Dim fIdRetention As Integer
    Public Property IdRetention() As Integer
        Get
            Return fIdRetention
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetention", fIdRetention, value)
        End Set
    End Property
    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
