Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemDetail")> _
Public Class FixedAssetItemDetailXpo
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

    Dim fFixedAssetItemId As FixedAssetEquipmentXpo
    <Association("FixedAssetItemDetailReferenceFixedAssetItem")> _
    Public Property FixedAssetItemId() As FixedAssetEquipmentXpo
        Get
            Return fFixedAssetItemId
        End Get
        Set(ByVal value As FixedAssetEquipmentXpo)
            SetPropertyValue(Of FixedAssetEquipmentXpo)("FixedAssetItemId", fFixedAssetItemId, value)
        End Set
    End Property

    Dim fLegalBookId As BookXpo
    <Association("FixedAssetItemDetailReferenceLegalBook")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fLifeTime As Integer
    Public Property LifeTime() As Integer
        Get
            Return fLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LifeTime", fLifeTime, value)
        End Set
    End Property

    Dim fUnitLifeTime As Integer
    Public Property UnitLifeTime() As Integer
        Get
            Return fUnitLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitLifeTime", fUnitLifeTime, value)
        End Set
    End Property

    Dim fDepreciationType As Integer
    Public Property DepreciationType() As Integer
        Get
            Return fDepreciationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciationType", fDepreciationType, value)
        End Set
    End Property

    Dim fTotalProductionUnit As Decimal
    Public Property TotalProductionUnit() As Decimal
        Get
            Return fTotalProductionUnit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalProductionUnit", fTotalProductionUnit, value)
        End Set
    End Property

    Dim fPercentageRescue As Decimal
    Public Property PercentageRescue() As Decimal
        Get
            Return fPercentageRescue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRescue", fPercentageRescue, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class


