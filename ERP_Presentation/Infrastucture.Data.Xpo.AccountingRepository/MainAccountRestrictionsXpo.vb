Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.MainAccountRestrictions")>
Public Class MainAccountRestrictionsXpo
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

    Dim fMainAccountId As MainAccountsXpo
    <Association("GeneralLedger_MainAccountRestrictionsReferencesGeneralLedger_MainAccounts")>
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fItemType As Byte
    Public Property ItemType() As Byte
        Get
            Return fItemType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fCostCenterId As CostCenterXpo
    <Association("GeneralLedger_MainAccountRestrictionsReferencesPayroll_CostCenter")>
    Public Property CostCenterId() As CostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As CostCenterXpo)
            SetPropertyValue(Of CostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("GeneralLedger_MainAccountRestrictionsReferencesCommon_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fRestrictionType As Byte
    Public Property RestrictionType() As Byte
        Get
            Return fRestrictionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RestrictionType", fRestrictionType, value)
        End Set
    End Property

    Dim fAllItems As Boolean
    Public Property AllItems() As Boolean
        Get
            Return fAllItems
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllItems", fAllItems, value)
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
