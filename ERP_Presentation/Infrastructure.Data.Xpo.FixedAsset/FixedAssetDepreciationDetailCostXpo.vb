Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetDepreciationDetailCost")> _
Public Class FixedAssetDepreciationDetailCostXpo
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

    Dim fFixedAssetDepreciationDetailId As FixedAssetDepreciationDetailXpo
    <Association("DepreciationDetailCostReferenceDepreciationDetail")> _
    Public Property FixedAssetDepreciationDetailId() As FixedAssetDepreciationDetailXpo
        Get
            Return fFixedAssetDepreciationDetailId
        End Get
        Set(ByVal value As FixedAssetDepreciationDetailXpo)
            SetPropertyValue(Of FixedAssetDepreciationDetailXpo)("FixedAssetDepreciationDetailId", fFixedAssetDepreciationDetailId, value)
        End Set
    End Property

    Dim fMainAccountId As PUCServiceXpo
    <Association("DepreciationDetailCostReferenceMainAccount")> _
    Public Property MainAccountId() As PUCServiceXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fResponsibleId As FixedAssetResponsibleXpo
    <Association("DepreciationDetailCostReferenceResponsible")> _
    Public Property ResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property

    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("DepreciationDetailCostReferenceLocation")> _
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("DepreciationDetailCostReferenceCostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fDepreciatedDays As Integer
    Public Property DepreciatedDays() As Integer
        Get
            Return fDepreciatedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciatedDays", fDepreciatedDays, value)
        End Set
    End Property

    Dim fDepreciationValue As Decimal
    Public Property DepreciationValue() As Decimal
        Get
            Return fDepreciationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciationValue", fDepreciationValue, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

