Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("FixedAsset.FixedAssetAmortizationDetailCost")>
Partial Public Class FixedAssetAmortizationDetailCostXpo
    Inherits XPLiteObject

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

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

    Dim fFixedAssetAmortizationDetailId As FixedAssetAmortizationDetailXpo
    <Association("FixedAssetAmortizationDetailCostReferenceFixedAssetAmortizationDetail")>
    Public Property FixedAssetAmortizationDetailId() As FixedAssetAmortizationDetailXpo
        Get
            Return fFixedAssetAmortizationDetailId
        End Get
        Set(ByVal value As FixedAssetAmortizationDetailXpo)
            SetPropertyValue(Of FixedAssetAmortizationDetailXpo)("FixedAssetAmortizationDetailId", fFixedAssetAmortizationDetailId, value)
        End Set
    End Property

    Dim fMainAccountId As PUCServiceXpo
    <Association("FixedAssetAmortizationDetailCostReferenceMainAccount")>
    Public Property MainAccountId() As PUCServiceXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("FixedAssetAmortizationDetailCostReferenceThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("FixedAssetAmortizationDetailCostReferenceLocation")>
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("FixedAssetAmortizationDetailCostReferenceCostCenter")>
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fAmortizedDays As Integer
    Public Property AmortizedDays() As Integer
        Get
            Return fAmortizedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AmortizedDays", fAmortizedDays, value)
        End Set
    End Property

    Dim fAmortizedValue As Decimal
    Public Property AmortizedValue() As Decimal
        Get
            Return fAmortizedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmortizedValue", fAmortizedValue, value)
        End Set
    End Property

#End Region

End Class