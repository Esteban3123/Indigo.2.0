Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.SuppliersDistributionLines")> _
Public Class CommonSupplierDistributionLinesReporXpo
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
    Dim fIdSupplier As CommonSupplierReportXpo
    <Association("CommonSupplierReportXpoReferencesCommonSupplierReportXpo")> _
    Public Property IdSupplier() As CommonSupplierReportXpo
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As CommonSupplierReportXpo)
            SetPropertyValue(Of CommonSupplierReportXpo)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdDistributionLine As Integer
    Public Property IdDistributionLine() As Integer
        Get
            Return fIdDistributionLine
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdDistributionLine", fIdDistributionLine, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesCommonSupplierDistributionLinesReporXpo", GetType(FixedAssetFixedAssetRemissionEntranceReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)("FixedAssetFixedAssetRemissionEntranceReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
