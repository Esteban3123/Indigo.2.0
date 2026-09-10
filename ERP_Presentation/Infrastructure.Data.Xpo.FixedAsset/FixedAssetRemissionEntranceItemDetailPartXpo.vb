Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntranceItemDetailPart")> _
Public Class FixedAssetRemissionEntranceItemDetailPartXpo
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

    Dim fRemissionEntranceItemDetailId As FixedAssetRemissionEntranceItemDetailXpo
    <Association("RemissionEntranceItemDetailPartReferenceRemissionEntranceItemDetail")> _
    Public Property RemissionEntranceItemDetailId() As FixedAssetRemissionEntranceItemDetailXpo
        Get
            Return fRemissionEntranceItemDetailId
        End Get
        Set(ByVal value As FixedAssetRemissionEntranceItemDetailXpo)
            SetPropertyValue(Of FixedAssetRemissionEntranceItemDetailXpo)("RemissionEntranceItemDetailId", fRemissionEntranceItemDetailId, value)
        End Set
    End Property

    Dim fPartAccesoriesConsumiblesId As FixedAssetPartsAccesoriesConsumablesXpo
    <Association("RemissionEntranceItemDetailPartReferencePartAccesories")> _
    Public Property PartAccesoriesConsumiblesId() As FixedAssetPartsAccesoriesConsumablesXpo
        Get
            Return fPartAccesoriesConsumiblesId
        End Get
        Set(ByVal value As FixedAssetPartsAccesoriesConsumablesXpo)
            SetPropertyValue(Of FixedAssetPartsAccesoriesConsumablesXpo)("PartAccesoriesConsumiblesId", fPartAccesoriesConsumiblesId, value)
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

    <Association("RemissionEntranceItemDetailPartBookReferenceRemissionEntranceItemDetailPart", GetType(FixedAssetRemissionEntranceItemDetailPartBookXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailPartBookXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailPartBookXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailPartBookXpo)("FixedAssetRemissionEntranceItemDetailPartBookXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

