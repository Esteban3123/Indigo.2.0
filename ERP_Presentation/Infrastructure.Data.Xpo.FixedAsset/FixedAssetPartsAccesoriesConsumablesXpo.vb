Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPartsAccesoriesConsumables")> _
Public Class FixedAssetPartsAccesoriesConsumablesXpo
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

    <Association("FixedAssetPurchaseOrderPartsReferences", GetType(FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo() As XPCollection(Of FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo)("FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailPartReferencePartAccesories", GetType(FixedAssetRemissionEntranceItemDetailPartXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailPartXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailPartXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailPartXpo)("FixedAssetRemissionEntranceItemDetailPartXpo")
        End Get
    End Property

    <Association("PhysicalAssetPartsReferencePartAccesories", GetType(FixedAssetPhysicalAssetPartsXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetPartsXpo() As XPCollection(Of FixedAssetPhysicalAssetPartsXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetPartsXpo)("FixedAssetPhysicalAssetPartsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

