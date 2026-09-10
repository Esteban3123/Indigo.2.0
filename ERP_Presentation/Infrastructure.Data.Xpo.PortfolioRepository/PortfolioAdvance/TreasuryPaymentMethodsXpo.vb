Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.PaymentMethods")> _
Public Class TreasuryPaymentMethodsXpo
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
    Dim fIdCashReceipt As Treasury_CashReceipts
    <Association("Portfolio_TreasuryPaymentMethodsXpo_CashReceipts")>
    Public Property IdCashReceipt() As Treasury_CashReceipts
        Get
            Return fIdCashReceipt
        End Get
        Set(ByVal value As Treasury_CashReceipts)
            SetPropertyValue(Of Treasury_CashReceipts)("IdCashReceipt", fIdCashReceipt, value)
        End Set
    End Property
    Dim fPaymentMethodTypes As Byte
    Public Property PaymentMethodTypes() As Byte
        Get
            Return fPaymentMethodTypes
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethodTypes", fPaymentMethodTypes, value)
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
    Dim fIdBank As Integer
    Public Property IdBank() As Integer
        Get
            Return fIdBank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdBank", fIdBank, value)
        End Set
    End Property
    Dim fCheckNumber As String
    <Size(30)> _
    Public Property CheckNumber() As String
        Get
            Return fCheckNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckNumber", fCheckNumber, value)
        End Set
    End Property
    Dim fDepositDate As DateTime
    Public Property DepositDate() As DateTime
        Get
            Return fDepositDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DepositDate", fDepositDate, value)
        End Set
    End Property
    Dim fIdEntityBankAccount As Integer
    Public Property IdEntityBankAccount() As Integer
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property
    Dim fDepositNumber As String
    <Size(30)> _
    Public Property DepositNumber() As String
        Get
            Return fDepositNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DepositNumber", fDepositNumber, value)
        End Set
    End Property
    Dim fDepositType As Byte
    Public Property DepositType() As Byte
        Get
            Return fDepositType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DepositType", fDepositType, value)
        End Set
    End Property
    Dim fIdCard As Integer
    Public Property IdCard() As Integer
        Get
            Return fIdCard
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCard", fIdCard, value)
        End Set
    End Property
    Dim fCardNumber As String
    <Size(30)> _
    Public Property CardNumber() As String
        Get
            Return fCardNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CardNumber", fCardNumber, value)
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
    Dim fCommissionValue As Decimal
    Public Property CommissionValue() As Decimal
        Get
            Return fCommissionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CommissionValue", fCommissionValue, value)
        End Set
    End Property
    Dim fPercentageCommission As Decimal
    Public Property PercentageCommission() As Decimal
        Get
            Return fPercentageCommission
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageCommission", fPercentageCommission, value)
        End Set
    End Property
    Dim fRTFValue As Decimal
    Public Property RTFValue() As Decimal
        Get
            Return fRTFValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFValue", fRTFValue, value)
        End Set
    End Property
    Dim fPercentageRTF As Decimal
    Public Property PercentageRTF() As Decimal
        Get
            Return fPercentageRTF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRTF", fPercentageRTF, value)
        End Set
    End Property
    Dim fICAValue As Decimal
    Public Property ICAValue() As Decimal
        Get
            Return fICAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ICAValue", fICAValue, value)
        End Set
    End Property
    Dim fPercentageICA As Decimal
    Public Property PercentageICA() As Decimal
        Get
            Return fPercentageICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageICA", fPercentageICA, value)
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

    Dim fTRM As Decimal
    Public Property TRM As Decimal
        Get
            Return fTRM
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("TRM", fTRM, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
