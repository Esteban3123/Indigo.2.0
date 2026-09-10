Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemTechnicalLog")>
Partial Public Class FixedAsset_FixedAssetItemTechnicalLog
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
        Dim fFixedAssetItemId As FixedAsset_FixedAssetItem
        <Indexed("TechnicalLogId", Name:="IX_FixedAssetItemTechnicalLog")>
        <Association("FixedAsset_FixedAssetItemTechnicalLogReferencesFixedAsset_FixedAssetItem")>
        Public Property FixedAssetItemId() As FixedAsset_FixedAssetItem
            Get
                Return fFixedAssetItemId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItem)
                SetPropertyValue(Of FixedAsset_FixedAssetItem)("FixedAssetItemId", fFixedAssetItemId, value)
            End Set
        End Property
        Dim fTechnicalLogId As Maintenance_TechnicalLog
        <Association("FixedAsset_FixedAssetItemTechnicalLogReferencesMaintenance_TechnicalLog")>
        Public Property TechnicalLogId() As Maintenance_TechnicalLog
            Get
                Return fTechnicalLogId
            End Get
            Set(ByVal value As Maintenance_TechnicalLog)
                SetPropertyValue(Of Maintenance_TechnicalLog)("TechnicalLogId", fTechnicalLogId, value)
            End Set
        End Property
End Class