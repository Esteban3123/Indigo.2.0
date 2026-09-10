Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPartsAccesoriesConsumables")>
Partial Public Class FixedAsset_FixedAssetPartsAccesoriesConsumables
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
        <Size(50)>
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
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
        Dim fCreationUser As String
        <Size(20)>
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
        <Size(20)>
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
        <Association("FixedAsset_FixedAssetItemTypePartsAccesoriesReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")>
        Public ReadOnly Property FixedAsset_FixedAssetItemTypePartsAccesoriess() As XPCollection(Of FixedAsset_FixedAssetItemTypePartsAccesories)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemTypePartsAccesories)("FixedAsset_FixedAssetItemTypePartsAccesoriess")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemPartReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")>
        Public ReadOnly Property FixedAsset_FixedAssetItemParts() As XPCollection(Of FixedAsset_FixedAssetItemPart)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemPart)("FixedAsset_FixedAssetItemParts")
            End Get
        End Property
End Class