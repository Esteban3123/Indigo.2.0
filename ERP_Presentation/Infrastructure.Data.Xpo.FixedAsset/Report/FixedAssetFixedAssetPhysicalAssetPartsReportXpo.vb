Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAssetParts")> _
Public Class FixedAssetFixedAssetPhysicalAssetPartsReportXpo
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
    Dim fPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetPartsReferencesFixedAsset_FixedAssetPhysicalAsset")> _
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property
    Dim fPartAccesoriesConsumiblesId As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetPartsReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")> _
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
    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesFixedAsset_FixedAssetPhysicalAssetParts", GetType(FixedAssetFixedAssetActiveOutputDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetActiveOutputDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)("FixedAssetFixedAssetActiveOutputDetailReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
