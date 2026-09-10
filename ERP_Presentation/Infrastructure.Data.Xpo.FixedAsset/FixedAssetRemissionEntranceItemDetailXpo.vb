Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntranceItemDetail")> _
Public Class FixedAssetRemissionEntranceItemDetailXpo
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

    Dim fRemissionEntranceItemId As FixedAssetRemissionEntranceItemXpo
    <Association("RemissionEntranceItemDetailReferenceRemissionEntranceItem")> _
    Public Property RemissionEntranceItemId() As FixedAssetRemissionEntranceItemXpo
        Get
            Return fRemissionEntranceItemId
        End Get
        Set(ByVal value As FixedAssetRemissionEntranceItemXpo)
            SetPropertyValue(Of FixedAssetRemissionEntranceItemXpo)("RemissionEntranceItemId", fRemissionEntranceItemId, value)
        End Set
    End Property

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fResponsibleId As FixedAssetResponsibleXpo
    <Association("RemissionEntranceItemDetailReferenceResponsible")> _
    Public Property ResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property

    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("RemissionEntranceItemDetailReferenceLocation")> _
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    Dim fAdquisitionDate As Date
    Public Property AdquisitionDate() As Date
        Get
            Return fAdquisitionDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("AdquisitionDate", fAdquisitionDate, value)
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

    Dim fWarrantyExpirationDate As Date
    Public Property WarrantyExpirationDate() As Date
        Get
            Return fWarrantyExpirationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("WarrantyExpirationDate", fWarrantyExpirationDate, value)
        End Set
    End Property

    Dim fStatusAssetId As FixedAssetStatusAssetXpo
    <Association("RemissionEntranceItemDetailReferenceStatusAsset")> _
    Public Property StatusAssetId() As FixedAssetStatusAssetXpo
        Get
            Return fStatusAssetId
        End Get
        Set(ByVal value As FixedAssetStatusAssetXpo)
            SetPropertyValue(Of FixedAssetStatusAssetXpo)("StatusAssetId", fStatusAssetId, value)
        End Set
    End Property

    <Association("RemissionEntranceItemDetailBookReferenceRemissionEntranceItemDetail", GetType(FixedAssetRemissionEntranceItemDetailBookXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailBookXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailBookXpo)("FixedAssetRemissionEntranceItemDetailBookXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailPartReferenceRemissionEntranceItemDetail", GetType(FixedAssetRemissionEntranceItemDetailPartXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailPartXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailPartXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailPartXpo)("FixedAssetRemissionEntranceItemDetailPartXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

