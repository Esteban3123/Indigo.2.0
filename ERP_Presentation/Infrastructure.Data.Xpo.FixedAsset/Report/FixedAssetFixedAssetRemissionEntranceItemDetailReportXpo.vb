Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntranceItemDetail")> _
Public Class FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo
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
    Dim fRemissionEntranceItemId As FixedAssetFixedAssetRemissionEntranceItemReportXpo
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpoReferencesFixedAssetFixedAssetRemissionEntranceItemReportXpo")> _
    Public Property RemissionEntranceItemId() As FixedAssetFixedAssetRemissionEntranceItemReportXpo
        Get
            Return fRemissionEntranceItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetRemissionEntranceItemReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)("RemissionEntranceItemId", fRemissionEntranceItemId, value)
        End Set
    End Property
    Dim fPlate As String
    <Size(50)> _
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fSerie As String
    <Size(50)> _
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpoReferencesFixedAssetResponsibleReportXpo")> _
    Public Property ResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetLocationReportXpo
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpoReferencesFixedAssetLocationReportXpo")> _
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
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpoReferencesFixedAssetFixedAssetRemissionEntranceItemDetailReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpo)("FixedAssetFixedAssetRemissionEntranceItemDetailBookReportXpo")
        End Get
    End Property
    <Association("fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpoReferencesFixedAssetFixedAssetRemissionEntranceItemDetailReportXpo", GetType(fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo))> _
    Public ReadOnly Property fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo() As XPCollection(Of fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo)
        Get
            Return GetCollection(Of fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo)("fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
