Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemType")> _
Public Class FixedAssetEquipmentTypeXpo
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

    Dim fParentId As FixedAssetEquipmentTypeXpo
    <Association("FixedAsset_FixedAssetEquipmentTypeReferencesFixedAsset_FixedAssetEquipmentType")> _
    Public Property ParentId() As FixedAssetEquipmentTypeXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As FixedAssetEquipmentTypeXpo)
            SetPropertyValue(Of FixedAssetEquipmentTypeXpo)("ParentId", fParentId, value)
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
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fInventoryTypeId As FixedAssetInventoryTypeXpo
    <Association("FixedAsset_FixedAssetEquipmentTypeReferencesFixedAsset_FixedAssetInventoryType")>
    Public Property InventoryTypeId() As FixedAssetInventoryTypeXpo
        Get
            Return fInventoryTypeId
        End Get
        Set(ByVal value As FixedAssetInventoryTypeXpo)
            SetPropertyValue(Of FixedAssetInventoryTypeXpo)("InventoryTypeId", fInventoryTypeId, value)
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

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("FixedAsset_FixedAssetEquipmentTypeReferencesFixedAsset_FixedAssetEquipmentType", GetType(FixedAssetEquipmentTypeXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetEquipmentTypeCollection() As XPCollection(Of FixedAssetEquipmentTypeXpo)
        Get
            Return GetCollection(Of FixedAssetEquipmentTypeXpo)("FixedAsset_FixedAssetEquipmentTypeCollection")
        End Get
    End Property

    <Association("FixedAssetPurchaseOrderEquipmentEquipmentType", GetType(FixedAssetEquipmentTypeXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentEquipmentType() As XPCollection(Of FixedAssetEquipmentTypeXpo)
        Get
            Return GetCollection(Of FixedAssetEquipmentTypeXpo)("FixedAssetPurchaseOrderEquipmentEquipmentType")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class