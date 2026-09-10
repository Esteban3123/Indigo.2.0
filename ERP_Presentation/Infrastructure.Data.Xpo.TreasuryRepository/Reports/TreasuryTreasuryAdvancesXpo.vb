Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.TreasuryAdvances")> _
Public Class TreasuryTreasuryAdvancesXpo
    Inherits XPLiteObject

#Region "Members"
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdVoucherTransactionDetail As TreasuryVoucherTransactionDetailsXpo
    <Association("TreasuryTreasuryAdvancesXpoReferencesTreasuryVoucherTransactionDetailsXpo")>
    Public Property IdVoucherTransactionDetail() As TreasuryVoucherTransactionDetailsXpo
        Get
            Return fIdVoucherTransactionDetail
        End Get
        Set(ByVal value As TreasuryVoucherTransactionDetailsXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionDetailsXpo)("IdVoucherTransactionDetail", fIdVoucherTransactionDetail, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(500)>
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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

    Dim fValueInCurrencyHeader As Decimal
    <Persistent("ValueInCurrencyHeader")>
    Public Property ValueInCurrencyHeader() As Decimal
        Get
            Return fValueInCurrencyHeader
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueInCurrencyHeader", fValueInCurrencyHeader, value)
        End Set
    End Property
#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region
End Class
