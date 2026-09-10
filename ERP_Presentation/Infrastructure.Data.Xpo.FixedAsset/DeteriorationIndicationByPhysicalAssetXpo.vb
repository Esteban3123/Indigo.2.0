Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.DeteriorationIndicationByPhysicalAsset")>
Public Class DeteriorationIndicationByPhysicalAssetXpo
    Inherits XPLiteObject
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
    Dim fDeteriorationIndicationId As DeteriorationIndicationsXpo
    <Association("DeteriorationIndicationsReferenceDeteriorationIndicationByPhysicalAsset")>
    Public Property DeteriorationIndicationId() As DeteriorationIndicationsXpo
        Get
            Return fDeteriorationIndicationId
        End Get
        Set(ByVal value As DeteriorationIndicationsXpo)
            SetPropertyValue(Of DeteriorationIndicationsXpo)("DeteriorationIndicationId", fDeteriorationIndicationId, value)
        End Set
    End Property
    Dim fPhysicalAssetId As FixedAssetPhysicalAssetXpo
    <Association("FixedAssetPhysicalAssetReferenceDeteriorationIndicationByPhysicalAsset")>
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
