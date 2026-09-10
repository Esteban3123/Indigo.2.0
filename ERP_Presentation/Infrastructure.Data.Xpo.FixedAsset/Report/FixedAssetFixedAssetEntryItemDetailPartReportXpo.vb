Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryItemDetailPart")> _
Public Class FixedAssetFixedAssetEntryItemDetailPartReportXpo
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
    Dim fFixedAssetEntryItemDetailId As FixedAssetFixedAssetEntryItemDetailReportXpo
    <Association("FixedAssetFixedAssetEntryItemDetailPartReportXpoReferencesFixedAssetFixedAssetEntryItemDetailReportXpo")> _
    Public Property FixedAssetEntryItemDetailId() As FixedAssetFixedAssetEntryItemDetailReportXpo
        Get
            Return fFixedAssetEntryItemDetailId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryItemDetailReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryItemDetailReportXpo)("FixedAssetEntryItemDetailId", fFixedAssetEntryItemDetailId, value)
        End Set
    End Property
    Dim fPartAccesoriesConsumiblesId As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo
    <Association("FixedAssetFixedAssetEntryItemDetailPartReportXpoReferencesFixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo")>
    Public Property PartAccesoriesConsumiblesId() As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo
        Get
            Return fPartAccesoriesConsumiblesId
        End Get
        Set(ByVal value As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo)("PartAccesoriesConsumiblesId", fPartAccesoriesConsumiblesId, value)
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
    <Association("FixedAssetFixedAssetEntryItemDetailPartBookReportXpoReferencesFixedAssetFixedAssetEntryItemDetailPartReportXpo", GetType(FixedAssetFixedAssetEntryItemDetailPartBookReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryItemDetailPartBookReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemDetailPartBookReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemDetailPartBookReportXpo)("FixedAssetFixedAssetEntryItemDetailPartBookReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
