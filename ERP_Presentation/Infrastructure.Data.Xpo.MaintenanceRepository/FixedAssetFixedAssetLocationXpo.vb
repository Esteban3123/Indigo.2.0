Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetLocation")> _
Public Class FixedAssetFixedAssetLocationXpo
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

    Dim fParentId As FixedAssetFixedAssetLocationXpo
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation")>
    Public Property ParentId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("ParentId", fParentId, value)
        End Set
    End Property

    <PersistentAlias("ParentId")>
    Public ReadOnly Property PadreId As Integer
        Get
            If ParentId Is Nothing Then
                Return 0
            Else
                Return ParentId.Id
            End If
        End Get
    End Property

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(300)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre de una Ubicación
    <Size(320)>
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fRiskLevel As Byte
    Public Property RiskLevel() As Byte
        Get
            Return fRiskLevel
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RiskLevel", fRiskLevel, value)
        End Set
    End Property
#End Region

#Region "CustomMembers"

    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetFixedAssetLocationXpo))>
    Public ReadOnly Property FixedAsset_FixedAssetLocationCollection() As XPCollection(Of FixedAssetFixedAssetLocationXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetLocationXpo)("FixedAsset_FixedAssetLocationCollection")
        End Get
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetPhysicalAssetXpo))>
    Public ReadOnly Property FixedAssetFixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetFixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("PhysicalReferenceLocation", GetType(FixedAssetPhysicalAssetXpo))>
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocationXpo", GetType(FixedAsset_FixedAssetPhysicalAsset))>
    Public ReadOnly Property FixedAsset_FixedAssetPhysicalAsset() As XPCollection(Of FixedAsset_FixedAssetPhysicalAsset)
        Get
            Return GetCollection(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAsset_FixedAssetPhysicalAsset")
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
