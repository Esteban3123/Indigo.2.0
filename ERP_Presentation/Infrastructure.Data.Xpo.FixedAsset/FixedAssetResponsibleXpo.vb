Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
<Persistent("FixedAsset.FixedAssetResponsible")> _
Public Class FixedAssetResponsibleXpo
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
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("ResponsibleReferencesThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fVinculationTypeId As Integer
    Public Property VinculationTypeId() As Integer
        Get
            Return fVinculationTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VinculationTypeId", fVinculationTypeId, value)
        End Set
    End Property
    Dim fReponsibleTypeId As Integer
    Public Property ReponsibleTypeId() As Integer
        Get
            Return ReponsibleTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReponsibleTypeId", fReponsibleTypeId, value)
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fCodeNitName As String
    'columna que devuelve el nit y el nombre concatenado
    '<PersistentAlias("concat(concat(Code,' - '),ThirdPartyId.NitName)")>
    <Size(50)>
    <PersistentAlias("ThirdPartyId.NitName")>
    Public ReadOnly Property CodeNitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNitName"))
        End Get
    End Property


    <PersistentAlias("Concat(Code, ' - ', ThirdPartyId.Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("FixedAssetResponsibleReferences", GetType(FixedAssetPurchaseOrderEquipmentDetailXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentDetailXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)("FixedAssetPurchaseOrderEquipmentDetailXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailReferenceResponsible", GetType(FixedAssetRemissionEntranceItemDetailXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailXpo)("FixedAssetRemissionEntranceItemDetailXpo")
        End Get
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible", GetType(FixedAssetFixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)("FixedAssetFixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("PhysicalReferenceResponsible", GetType(FixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("DepreciationDetailCostReferenceResponsible", GetType(FixedAssetDepreciationDetailCostXpo))> _
    Public ReadOnly Property FixedAssetDepreciationDetailCostXpo() As XPCollection(Of FixedAssetDepreciationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailCostXpo)("FixedAssetDepreciationDetailCostXpo")
        End Get
    End Property

    <Association("TransferReferencesSourceResponsible", GetType(FixedAssetTransferXpo))>
    Public ReadOnly Property FixedAssetTransferXpo() As XPCollection(Of FixedAssetTransferXpo)
        Get
            Return GetCollection(Of FixedAssetTransferXpo)("FixedAssetTransferXpo")
        End Get
    End Property

    <Association("TransferReferencesTargetResponsible", GetType(FixedAssetTransferXpo))>
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
