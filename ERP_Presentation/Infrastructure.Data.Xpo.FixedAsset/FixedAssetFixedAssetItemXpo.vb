Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItem")> _
Public Class FixedAssetFixedAssetItemXpo
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
    <Indexed(Name:="IX_FixedAssetItem", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    Dim fItemTypeId As Integer
    Public Property ItemTypeId() As Integer
        Get
            Return fItemTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemTypeId", fItemTypeId, value)
        End Set
    End Property

    Dim fItemCatalogId As FixedAssetItemCatalogXpo
    <Association("FixedAssetFixedAssetItemReferences")>
    Public Property ItemCatalogId() As FixedAssetItemCatalogXpo
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As FixedAssetItemCatalogXpo)
            SetPropertyValue(Of FixedAssetItemCatalogXpo)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property
    Dim fIVAId As Integer
    Public Property IVAId() As Integer
        Get
            Return fIVAId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVAId", fIVAId, value)
        End Set
    End Property
    Dim fLastCostItem As Decimal
    Public Property LastCostItem() As Decimal
        Get
            Return fLastCostItem
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LastCostItem", fLastCostItem, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fFairValue As Decimal
    Public Property FairValue() As Decimal
        Get
            Return fFairValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FairValue", fFairValue, value)
        End Set
    End Property
    Dim fAllowDepreciate As Boolean
    Public Property AllowDepreciate() As Boolean
        Get
            Return fAllowDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowDepreciate", fAllowDepreciate, value)
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
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem", GetType(FixedAssetFixedAssetPhysicalAssetXpo))>
    Public ReadOnly Property FixedAssetFixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)("FixedAssetFixedAssetPhysicalAssetXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
