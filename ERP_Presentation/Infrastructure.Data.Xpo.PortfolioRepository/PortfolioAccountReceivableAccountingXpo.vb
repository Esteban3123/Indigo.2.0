Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.AccountReceivableAccounting")> _
Public Class PortfolioAccountReceivableAccountingXpo
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
    Dim fAccountReceivableId As PortfolioAccountReceivableXpo
    <Association("AccountReceivableAccounting_AccountReceivable")>
    Public Property AccountReceivableId() As PortfolioAccountReceivableXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableXpo)
            SetPropertyValue(Of PortfolioAccountReceivableXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fMainAccountId As MainAccountsXpo
    <Association("cuentaContable")>
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fThirdPartyId As Common_ThirdParty
    <Association("Portfolio_AccountReceivableAccountingReferencesCommon_ThirdPartyId")>
    Public Property ThirdPartyId() As Common_ThirdParty
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Common_ThirdParty)
            SetPropertyValue(Of Common_ThirdParty)("ThirdPartyId", fThirdPartyId, value)
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
    <Size(100)>
    <PersistentAlias("concat([AccountReceivableId.InvoiceNumber],' - ', [CurrencyAbbreviation], ' - ', [Balance])")>
    Public ReadOnly Property CodeBillNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeBillNumber"))
        End Get
    End Property
#End Region

#Region "PersistentAlias"
    <PersistentAlias("AccountReceivableId.CurrencyId")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return If(Convert.ToInt32(EvaluateAlias("CurrencyId")) = 0 _
                        , CrossCutting.Base.SessionValues.Instance.OfficialCurrencyId,
                        Convert.ToInt32(EvaluateAlias("CurrencyId")))

        End Get
    End Property

    <PersistentAlias("AccountReceivableId.CurrencyAbbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))) _
                        , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                        Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))

        End Get
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
