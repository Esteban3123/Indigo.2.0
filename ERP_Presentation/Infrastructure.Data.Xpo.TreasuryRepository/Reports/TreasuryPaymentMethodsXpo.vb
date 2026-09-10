Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

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
    Dim fIdCashReceipt As TreasuryCashReceiptsXpo
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryCashReceiptsXpo")>
    Public Property IdCashReceipt() As TreasuryCashReceiptsXpo
        Get
            Return fIdCashReceipt
        End Get
        Set(ByVal value As TreasuryCashReceiptsXpo)
            SetPropertyValue(Of TreasuryCashReceiptsXpo)("IdCashReceipt", fIdCashReceipt, value)
        End Set
    End Property
    Dim fIdAgreementsRedemptionPoints As AgreementsRedemptionPointsXpo
    <Association("TreasuryPayment_References_AgreementsRedemptionPoints")>
    Public Property IdAgreementsRedemptionPoints() As AgreementsRedemptionPointsXpo
        Get
            Return fIdAgreementsRedemptionPoints
        End Get
        Set(ByVal value As AgreementsRedemptionPointsXpo)
            SetPropertyValue(Of AgreementsRedemptionPointsXpo)("IdAgreementsRedemptionPoints", fIdAgreementsRedemptionPoints, value)
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
    Dim fValueInCurrencyHeader As Decimal
    Public Property ValueInCurrencyHeader() As Decimal
        Get
            Return fValueInCurrencyHeader
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueInCurrencyHeader", fValueInCurrencyHeader, value)
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
    Dim fIdBank As PayrollBankXpo
    <Association("TreasuryPaymentMethodsXpoReferencesPayrollBankXpo")> _
    Public Property IdBank() As PayrollBankXpo
        Get
            Return fIdBank
        End Get
        Set(ByVal value As PayrollBankXpo)
            SetPropertyValue(Of PayrollBankXpo)("IdBank", fIdBank, value)
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

    Dim fTransactionDate As DateTime
    Public Property TransactionDate() As DateTime
        Get
            Return fTransactionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransactionDate", fTransactionDate, value)
        End Set
    End Property

    Dim fIdEntityBankAccount As TreasuryEntityBankAccountsXpo
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryEntityBankAccountsXpo")>
    Public Property IdEntityBankAccount() As TreasuryEntityBankAccountsXpo
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("IdEntityBankAccount", fIdEntityBankAccount, value)
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
    Dim fIdCard As TreasuryCardsXpo
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryCardsXpo")> _
    Public Property IdCard() As TreasuryCardsXpo
        Get
            Return fIdCard
        End Get
        Set(ByVal value As TreasuryCardsXpo)
            SetPropertyValue(Of TreasuryCardsXpo)("IdCard", fIdCard, value)
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

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_PaymentMethods")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    'Propiedades Añadida
    Dim fGroupByCashAndCurrency As String
    <NonPersistent()>
    Public Property GroupByCashAndCurrency() As String
        Get
            Return fGroupByCashAndCurrency
        End Get
        Set(ByVal value As String)
            Me.fGroupByCashAndCurrency = value
        End Set
    End Property

    Dim fGeneralTotalGroup As Decimal
    <NonPersistent()>
    Public Property GeneralTotalGroup() As Decimal
        Get
            Return fGeneralTotalGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fGeneralTotalGroup = value
        End Set
    End Property

    Dim fCheckValueGroup As Decimal
    <NonPersistent()>
    Public Property CheckValueGroup() As Decimal
        Get
            Return fCheckValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fCheckValueGroup = value
        End Set
    End Property

    Dim fCardValueGroup As Decimal
    <NonPersistent()>
    Public Property CardValueGroup() As Decimal
        Get
            Return fCardValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fCardValueGroup = value
        End Set
    End Property

    Dim fConsigmentValueGroup As Decimal
    <NonPersistent()>
    Public Property ConsigmentValueGroup() As Decimal
        Get
            Return fConsigmentValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fConsigmentValueGroup = value
        End Set
    End Property

    Dim fConfirmedValueGroup As Decimal
    <NonPersistent()>
    Public Property ConfirmedValueGroup() As Decimal
        Get
            Return fConfirmedValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fConfirmedValueGroup = value
        End Set
    End Property

    Dim fRegistersValueGroup As Decimal
    <NonPersistent()>
    Public Property RegistersValueGroup() As Decimal
        Get
            Return fRegistersValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fRegistersValueGroup = value
        End Set
    End Property

    Dim fConfirmedStatusValueGroup As Decimal
    <NonPersistent()>
    Public Property ConfirmedStatusValueGroup() As Decimal
        Get
            Return fConfirmedStatusValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fConfirmedStatusValueGroup = value
        End Set
    End Property

    Dim fCancelStatusValueGroup As Decimal
    <NonPersistent()>
    Public Property CancelStatusValueGroup() As Decimal
        Get
            Return fCancelStatusValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fCancelStatusValueGroup = value
        End Set
    End Property

    Dim fReversedStatusValueGroup As Decimal
    <NonPersistent()>
    Public Property ReversedStatusValueGroup() As Decimal
        Get
            Return fReversedStatusValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fReversedStatusValueGroup = value
        End Set
    End Property

    Dim fTotalStatusValueGroup As Decimal
    <NonPersistent()>
    Public Property TotalStatusValueGroup() As Decimal
        Get
            Return fTotalStatusValueGroup
        End Get
        Set(ByVal value As Decimal)
            Me.fTotalStatusValueGroup = value
        End Set
    End Property


    ''' <summary>
    ''' Valor de la tarjeta, si el estado de la cabecera no es reversado
    ''' </summary>
    <NonPersistent()>
    Public ReadOnly Property CardValue() As Decimal
        Get
            Return IIf(Me.IdCashReceipt.Status <> ResourceManager.GetString("StatusReverse"), Me.Value, 0)
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
