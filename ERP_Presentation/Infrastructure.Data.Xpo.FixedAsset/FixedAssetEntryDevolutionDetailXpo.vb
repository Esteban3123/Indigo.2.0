Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryDevolutionDetail")> _
Public Class FixedAssetEntryDevolutionDetailXpo
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

    Dim fFixedAssetEntryDevolutionId As Integer
    Public Property FixedAssetEntryDevolutionId() As Integer
        Get
            Return fFixedAssetEntryDevolutionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryDevolutionId", fFixedAssetEntryDevolutionId, value)
        End Set
    End Property

    Dim fFixedAssetEntryItemId As Integer
    Public Property FixedAssetEntryItemId() As Integer
        Get
            Return fFixedAssetEntryItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryItemId", fFixedAssetEntryItemId, value)
        End Set
    End Property

    Dim fFixedAssetEntryItemDetailId As Integer
    Public Property FixedAssetEntryItemDetailId() As Integer
        Get
            Return fFixedAssetEntryItemDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryItemDetailId", fFixedAssetEntryItemDetailId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


