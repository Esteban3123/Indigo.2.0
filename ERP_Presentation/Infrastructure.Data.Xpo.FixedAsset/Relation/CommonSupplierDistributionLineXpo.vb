Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.SuppliersDistributionLines")>
Public Class CommonSupplierDistributionLineXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fIdSupplier As CommonSuppliertXpo
    <Association("Common_SupplierDistributionLinesXpo_References_Common_SupplierXpo")>
    Public Property IdSupplier() As CommonSuppliertXpo
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As CommonSuppliertXpo)
            SetPropertyValue(Of CommonSuppliertXpo)("IdSupplier", fIdSupplier, value)
        End Set
    End Property

#End Region

#Region "Associations"

    <Association("FixedAsset_FixedAssetPurchaseOrderXpo_References_Common_SupplierDistributionLineXpo", GetType(FixedAssetPurchaseOrderXpo))>
    Public ReadOnly Property FixedAssetPurchaseOrdersXpo() As XPCollection(Of FixedAssetPurchaseOrderXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderXpo)("FixedAssetPurchaseOrdersXpo")
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
