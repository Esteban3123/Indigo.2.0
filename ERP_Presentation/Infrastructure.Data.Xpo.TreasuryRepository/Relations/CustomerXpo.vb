
Imports DevExpress.Xpo

<Persistent("Common.Customer")>
Public Class CustomerXpo
    Inherits XPLiteObject
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
    Dim fNit As String
    <Indexed(Name:="IX_Customer", Unique:=True)>
    <Size(15)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fEPSCode As String
    <Size(20)>
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Customer_References_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fMainAccountReceivableId As GeneralLedgerMainAccountsXpo
    <Association("Customer_References_MainAccounts")>
    Public Property MainAccountReceivableId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountReceivableId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountReceivableId", fMainAccountReceivableId, value)
        End Set
    End Property

    <Association("Portfolio_PortfolioTransferReferencesCommon_Customer", GetType(PortfolioTransferXpo))>
    Public ReadOnly Property Portfolio_PortfolioTransfers() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("Portfolio_PortfolioTransfers")
        End Get
    End Property
    <Size(50)>
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

    <Association("AgreementsRedemptionPoints_Reference_Customer", GetType(AgreementsRedemptionPointsXpo))>
    Public ReadOnly Property AgreementsRedemptionPointsXpo() As XPCollection(Of AgreementsRedemptionPointsXpo)
        Get
            Return GetCollection(Of AgreementsRedemptionPointsXpo)("AgreementsRedemptionPointsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
