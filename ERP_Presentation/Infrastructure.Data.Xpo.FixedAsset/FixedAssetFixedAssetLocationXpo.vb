Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetLocation")> _
Public Class FixedAssetFixedAssetLocationXpo
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

    Dim fParentId As FixedAssetFixedAssetLocationXpo
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation")> _
    Public Property ParentId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("ParentId", fParentId, value)
        End Set
    End Property

    <PersistentAlias("ParentId")> _
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre de una Ubicación
    <Size(320)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fLocationTypeId As FixedAssetFixedAssetLocationTypeXpo
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocationType")> _
    Public Property LocationTypeId() As FixedAssetFixedAssetLocationTypeXpo
        Get
            Return fLocationTypeId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationTypeXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationTypeXpo)("LocationTypeId", fLocationTypeId, value)
        End Set
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
    
    
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetFixedAssetLocationXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetLocationCollection() As XPCollection(Of FixedAssetFixedAssetLocationXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetLocationXpo)("FixedAsset_FixedAssetLocationCollection")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailReferenceLocation", GetType(FixedAssetRemissionEntranceItemDetailXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailXpo)("FixedAssetRemissionEntranceItemDetailXpo")
        End Get
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetFixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)("FixedAssetFixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("PhysicalReferenceLocation", GetType(FixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("DepreciationDetailCostReferenceLocation", GetType(FixedAssetDepreciationDetailCostXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailCostXpo() As XPCollection(Of FixedAssetDepreciationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailCostXpo)("FixedAssetDepreciationDetailCostXpo")
        End Get
    End Property

    <Association("FixedAssetAmortizationDetailCostReferenceLocation", GetType(FixedAssetAmortizationDetailCostXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailCostXpo() As XPCollection(Of FixedAssetAmortizationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailCostXpo)("FixedAssetAmortizationDetailCostXpo")
        End Get
    End Property

    <Association("TransferReferencesSourceLocation", GetType(FixedAssetTransferXpo))>
    Public ReadOnly Property FixedAssetTransferXpo() As XPCollection(Of FixedAssetTransferXpo)
        Get
            Return GetCollection(Of FixedAssetTransferXpo)("FixedAssetTransferXpo")
        End Get
    End Property

    <Association("TransferReferencesTargetLocation", GetType(FixedAssetTransferXpo))>
    Public ReadOnly Property FixedAssetTransferXpo2() As XPCollection(Of FixedAssetTransferXpo)
        Get
            Return GetCollection(Of FixedAssetTransferXpo)("FixedAssetTransferXpo2")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
