Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntranceItemDetailPart")> _
Public Class fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo
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
    Dim fRemissionEntranceItemDetailId As FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo
    <Association("fixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpoReferencesFixedAssetFixedAssetRemissionEntranceItemDetailReportXpo")> _
    Public Property RemissionEntranceItemDetailId() As FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo
        Get
            Return fRemissionEntranceItemDetailId
        End Get
        Set(ByVal value As FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)("RemissionEntranceItemDetailId", fRemissionEntranceItemDetailId, value)
        End Set
    End Property
    Dim fPartAccesoriesConsumiblesId As Integer
    Public Property PartAccesoriesConsumiblesId() As Integer
        Get
            Return fPartAccesoriesConsumiblesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PartAccesoriesConsumiblesId", fPartAccesoriesConsumiblesId, value)
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
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpoReferencesfixedAssetFixedAssetRemissionEntranceItemDetailPartReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpo)("FixedAssetFixedAssetRemissionEntranceItemDetailPartBookReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
