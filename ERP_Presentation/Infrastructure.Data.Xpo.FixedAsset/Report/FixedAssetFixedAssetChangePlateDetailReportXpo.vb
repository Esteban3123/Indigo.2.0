Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetChangePlateDetail")> _
Public Class FixedAssetFixedAssetChangePlateDetailReportXpo
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
    Dim fFixedAssetChangePlateId As FixedAssetFixedAssetChangePlateReportXpo
    <Association("FixedAsset_FixedAssetChangePlateDetailReferencesFixedAsset_FixedAssetChangePlate")> _
    Public Property FixedAssetChangePlateId() As FixedAssetFixedAssetChangePlateReportXpo
        Get
            Return fFixedAssetChangePlateId
        End Get
        Set(ByVal value As FixedAssetFixedAssetChangePlateReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetChangePlateReportXpo)("FixedAssetChangePlateId", fFixedAssetChangePlateId, value)
        End Set
    End Property
    Dim fFixedAssetPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("FixedAsset_FixedAssetChangePlateDetailReferencesFixedAsset_FixedAssetPhysicalAsset")> _
    Public Property FixedAssetPhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fFixedAssetPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
        End Set
    End Property
    Dim fOldPlate As String
    <Size(50)> _
    Public Property OldPlate() As String
        Get
            Return fOldPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OldPlate", fOldPlate, value)
        End Set
    End Property
    Dim fNewPlate As String
    <Size(50)> _
    Public Property NewPlate() As String
        Get
            Return fNewPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NewPlate", fNewPlate, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
