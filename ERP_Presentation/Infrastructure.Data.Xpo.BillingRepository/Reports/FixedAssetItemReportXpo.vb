Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItem")> _
Public Class FixedAssetItemReportXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

#End Region

#Region "Navigation"

    <Association("FixedAsset_FixedAssetPhysicalAsset_References_FixedAsset_FixedAssetItem", GetType(FixedAssetPhysicalAssetReportXpo))>
    Public ReadOnly Property FixedAssetPhysicalAssetReportXpo() As XPCollection(Of FixedAssetPhysicalAssetReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetReportXpo)("FixedAssetPhysicalAssetReportXpo")
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
