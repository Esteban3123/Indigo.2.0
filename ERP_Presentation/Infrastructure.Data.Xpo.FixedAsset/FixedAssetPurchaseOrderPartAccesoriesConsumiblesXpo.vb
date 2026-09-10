Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPurchaseOrderPartAccesoriesConsumibles")> _
Public Class FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo
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
    Dim fIdFixedAssetPurchaseOrderEquipmentDetail As FixedAssetPurchaseOrderEquipmentDetailXpo
    <Association("FixedAssetPurchaseOrderPartsAccesoriesConsumiblesReferences")> _
    Public Property IdFixedAssetPurchaseOrderEquipmentDetail() As FixedAssetPurchaseOrderEquipmentDetailXpo
        Get
            Return fIdFixedAssetPurchaseOrderEquipmentDetail
        End Get
        Set(ByVal value As FixedAssetPurchaseOrderEquipmentDetailXpo)
            SetPropertyValue(Of FixedAssetPurchaseOrderEquipmentDetailXpo)("IdFixedAssetPurchaseOrderEquipmentDetail", fIdFixedAssetPurchaseOrderEquipmentDetail, value)
        End Set
    End Property
    Dim fIdEquipment As Integer
    Public Property IdEquipment() As Integer
        Get
            Return fIdEquipment
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEquipment", fIdEquipment, value)
        End Set
    End Property
    Dim fIdPartAccesoriesConsumibles As FixedAssetPartsAccesoriesConsumablesXpo
    <Association("FixedAssetPurchaseOrderPartsReferences")> _
    Public Property IdPartAccesoriesConsumibles() As FixedAssetPartsAccesoriesConsumablesXpo
        Get
            Return fIdPartAccesoriesConsumibles
        End Get
        Set(ByVal value As FixedAssetPartsAccesoriesConsumablesXpo)
            SetPropertyValue(Of FixedAssetPartsAccesoriesConsumablesXpo)("IdPartAccesoriesConsumibles", fIdPartAccesoriesConsumibles, value)
        End Set
    End Property
    Dim fDepreciatePart As Boolean
    Public Property DepreciatePart() As Boolean
        Get
            Return fDepreciatePart
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DepreciatePart", fDepreciatePart, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

