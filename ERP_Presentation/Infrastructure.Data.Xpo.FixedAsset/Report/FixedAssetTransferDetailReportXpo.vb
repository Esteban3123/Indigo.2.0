Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetTransferDetail")> _
Public Class FixedAssetTransferDetailReportXpo
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
    Dim fFixedAssetTransferId As FixedAssetTransferReportXpo
    <Association("FixedAsset_FixedAssetTransferDetailReferencesFixedAsset_FixedAssetTransfer")> _
    Public Property FixedAssetTransferId() As FixedAssetTransferReportXpo
        Get
            Return fFixedAssetTransferId
        End Get
        Set(ByVal value As FixedAssetTransferReportXpo)
            SetPropertyValue(Of FixedAssetTransferReportXpo)("FixedAssetTransferId", fFixedAssetTransferId, value)
        End Set
    End Property
    Dim fPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("FixedAsset_FixedAssetTransferDetailReferencesFixedAsset_FixedAssetPhysicalAsset")> _
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
