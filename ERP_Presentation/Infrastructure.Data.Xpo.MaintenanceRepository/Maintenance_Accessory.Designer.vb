Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.Accessory")>
Partial Public Class Maintenance_Accessory
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
        <Indexed(Name:="IX_Accessory", Unique:=True)>
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
        <Indexed(Name:="IX_Accessory_State")>
        Public Property State() As Boolean
            Get
                Return fState
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("State", fState, value)
            End Set
        End Property
        <Association("Maintenance_AccesoryDetailReferencesMaintenance_Accessory")>
        Public ReadOnly Property Maintenance_AccesoryDetails() As XPCollection(Of Maintenance_AccesoryDetail)
            Get
                Return GetCollection(Of Maintenance_AccesoryDetail)("Maintenance_AccesoryDetails")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemAccesoryReferencesMaintenance_Accessory")>
        Public ReadOnly Property FixedAsset_FixedAssetItemAccesorys() As XPCollection(Of FixedAsset_FixedAssetItemAccesory)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemAccesory)("FixedAsset_FixedAssetItemAccesorys")
            End Get
        End Property
End Class