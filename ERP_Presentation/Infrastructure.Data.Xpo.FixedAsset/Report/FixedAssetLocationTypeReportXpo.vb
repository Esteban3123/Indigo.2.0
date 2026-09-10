Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetLocationType")> _
Public Class FixedAssetLocationTypeReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key()> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fOrderLocation As Integer
    Public Property OrderLocation() As Integer
        Get
            Return fOrderLocation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OrderLocation", fOrderLocation, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocationType", GetType(FixedAssetLocationReportXpo))> _
    Public ReadOnly Property FixedAssetLocationReportXpo() As XPCollection(Of FixedAssetLocationReportXpo)
        Get
            Return GetCollection(Of FixedAssetLocationReportXpo)("FixedAssetLocationReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
