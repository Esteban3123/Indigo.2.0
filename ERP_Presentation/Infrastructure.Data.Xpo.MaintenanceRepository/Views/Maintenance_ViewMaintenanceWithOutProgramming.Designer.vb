Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ViewMaintenanceWithOutProgramming")>
Partial Public Class Maintenance_ViewMaintenanceWithOutProgramming
        Inherits XPLiteObject
        Dim fId As Integer
        <Key()>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property
        Dim fPhysetAssetId As Integer
        Public Property PhysetAssetId() As Integer
            Get
                Return fPhysetAssetId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("PhysetAssetId", fPhysetAssetId, value)
            End Set
        End Property
        Dim fLocationId As Integer
        Public Property LocationId() As Integer
            Get
                Return fLocationId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("LocationId", fLocationId, value)
            End Set
        End Property
        Dim fItemId As Integer
        Public Property ItemId() As Integer
            Get
                Return fItemId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("ItemId", fItemId, value)
            End Set
        End Property
        Dim fInventoryTypeId As Integer
        Public Property InventoryTypeId() As Integer
            Get
                Return fInventoryTypeId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("InventoryTypeId", fInventoryTypeId, value)
            End Set
        End Property
        Dim fItemTypeId As Integer
        Public Property ItemTypeId() As Integer
            Get
                Return fItemTypeId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("ItemTypeId", fItemTypeId, value)
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
        Dim fArticleCode As String
        <Size(20)>
        Public Property ArticleCode() As String
            Get
                Return fArticleCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ArticleCode", fArticleCode, value)
            End Set
        End Property
        Dim fArticleName As String
        <Size(300)>
        Public Property ArticleName() As String
            Get
                Return fArticleName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ArticleName", fArticleName, value)
            End Set
        End Property
        Dim fTrademarkCodeName As String
        <Size(73)>
        Public Property TrademarkCodeName() As String
            Get
                Return fTrademarkCodeName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("TrademarkCodeName", fTrademarkCodeName, value)
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
        Dim fModel As String
        Public Property Model() As String
            Get
                Return fModel
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Model", fModel, value)
            End Set
        End Property
        Dim fLocationCode As String
        <Size(20)>
        Public Property LocationCode() As String
            Get
                Return fLocationCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("LocationCode", fLocationCode, value)
            End Set
        End Property
        Dim fLocationName As String
        <Size(50)>
        Public Property LocationName() As String
            Get
                Return fLocationName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("LocationName", fLocationName, value)
            End Set
        End Property
        Dim fLocationCodeName As String
        <Size(73)>
        Public Property LocationCodeName() As String
            Get
                Return fLocationCodeName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("LocationCodeName", fLocationCodeName, value)
            End Set
        End Property
End Class