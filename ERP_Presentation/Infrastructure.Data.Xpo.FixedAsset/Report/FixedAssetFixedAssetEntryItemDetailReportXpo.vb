Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryItemDetail")> _
 Public Class FixedAssetFixedAssetEntryItemDetailReportXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fFixedAssetEntryItemId As FixedAssetFixedAssetEntryItemReportXpo
    <Association("FixedAsset_FixedAssetEntryItemDetailReferencesFixedAsset_FixedAssetEntryItem")>
    Public Property FixedAssetEntryItemId() As FixedAssetFixedAssetEntryItemReportXpo
        Get
            Return fFixedAssetEntryItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryItemReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryItemReportXpo)("FixedAssetEntryItemId", fFixedAssetEntryItemId, value)
        End Set
    End Property

    Dim fPlate As String
    <Size(50)>
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property

    Dim fSerie As String
    <Size(50)>
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fReponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAssetFixedAssetEntryItemDetailReportXpoReferencesFixedAssetResponsibleReportXpo")>
    Public Property ReponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fReponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("ReponsibleId", fReponsibleId, value)
        End Set
    End Property

    Dim fLocationId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetEntryItemDetailReferencesFixedAsset_FixedAssetLocation")>
    Public Property LocationId() As FixedAssetLocationReportXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    Dim fAdquisitionDate As DateTime
    Public Property AdquisitionDate() As DateTime
        Get
            Return fAdquisitionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdquisitionDate", fAdquisitionDate, value)
        End Set
    End Property

    Dim fDepreciate As Boolean
    Public Property Depreciate() As Boolean
        Get
            Return fDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Depreciate", fDepreciate, value)
        End Set
    End Property

    Dim fHandlesWarranty As Boolean
    Public Property HandlesWarranty() As Boolean
        Get
            Return fHandlesWarranty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesWarranty", fHandlesWarranty, value)
        End Set
    End Property

    Dim fWarrantyExpirationDate As DateTime
    Public Property WarrantyExpirationDate() As DateTime
        Get
            Return fWarrantyExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("WarrantyExpirationDate", fWarrantyExpirationDate, value)
        End Set
    End Property

    Dim fStatusAssetId As Integer
    Public Property StatusAssetId() As Integer
        Get
            Return fStatusAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StatusAssetId", fStatusAssetId, value)
        End Set
    End Property

#End Region

#Region "Navigations"

    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryItemDetail", GetType(FixedAssetFixedAssetEntryDevolutionDetailReportXpo))>
    Public ReadOnly Property FixedAsset_FixedAssetEntryDevolutionDetails() As XPCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)("FixedAsset_FixedAssetEntryDevolutionDetails")
        End Get
    End Property

    <Association("FixedAssetFixedAssetEntryItemDetailPartReportXpoReferencesFixedAssetFixedAssetEntryItemDetailReportXpo", GetType(FixedAssetFixedAssetEntryItemDetailPartReportXpo))>
    Public ReadOnly Property FixedAsset_FixedAssetFixedAssetEntryItemDetailParts() As XPCollection(Of FixedAssetFixedAssetEntryItemDetailPartReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemDetailPartReportXpo)("FixedAsset_FixedAssetFixedAssetEntryItemDetailParts")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
