Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.GeneralLedgerIVA")> _
Public Class GeneralLedgerIVAXpo
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
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
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

    <Association("FixedAssetItemReferenceGeneralLegerIVA", GetType(FixedAssetEquipmentXpo))> _
    Public ReadOnly Property FixedAssetEquipmentXpo() As XPCollection(Of FixedAssetEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetEquipmentXpo)("FixedAssetEquipmentXpo")
        End Get
    End Property

    <Association("FixedAssetPurchaseOrderEquipmentReferencesIVACode", GetType(FixedAssetPurchaseOrderEquipmentXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentXpo)("FixedAssetPurchaseOrderEquipmentXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemReferenceIVA", GetType(FixedAssetRemissionEntranceItemXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemXpo() As XPCollection(Of FixedAssetRemissionEntranceItemXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemXpo)("FixedAssetRemissionEntranceItemXpo")
        End Get
    End Property

    <Association("PurchaseOrderItemReferenceIVA", GetType(FixedAssetPurchaseOrderItemXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderItemXpo() As XPCollection(Of FixedAssetPurchaseOrderItemXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderItemXpo)("FixedAssetPurchaseOrderItemXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

