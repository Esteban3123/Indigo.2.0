Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.Consumable")>
Partial Public Class Maintenance_Consumable
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
        <Indexed(Name:="IX_Consumable", Unique:=True)>
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
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
            End Set
        End Property
        Dim fState As Boolean
        <Indexed(Name:="IX_Consumable_State")>
        Public Property State() As Boolean
            Get
                Return fState
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("State", fState, value)
            End Set
        End Property
        <Association("Maintenance_ConsumableDetailReferencesMaintenance_Consumable")>
        Public ReadOnly Property Maintenance_ConsumableDetails() As XPCollection(Of Maintenance_ConsumableDetail)
            Get
                Return GetCollection(Of Maintenance_ConsumableDetail)("Maintenance_ConsumableDetails")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemConsumibleReferencesMaintenance_Consumable")>
        Public ReadOnly Property FixedAsset_FixedAssetItemConsumibles() As XPCollection(Of FixedAsset_FixedAssetItemConsumible)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemConsumible)("FixedAsset_FixedAssetItemConsumibles")
            End Get
        End Property
        <Association("Maintenance_ProtocolConsumablesReferencesMaintenance_Consumable")>
        Public ReadOnly Property Maintenance_ProtocolConsumabless() As XPCollection(Of Maintenance_ProtocolConsumables)
            Get
                Return GetCollection(Of Maintenance_ProtocolConsumables)("Maintenance_ProtocolConsumabless")
            End Get
        End Property
End Class