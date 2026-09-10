Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.ResponsibleType")>
Partial Public Class FixedAsset_ResponsibleType
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
        <Indexed(Name:="IX_ResponsibleType", Unique:=True)>
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fDescription As String
        Public Property Description() As String
            Get
                Return fDescription
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Description", fDescription, value)
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
        <Association("FixedAsset_FixedAssetResponsibleReferencesFixedAsset_ResponsibleType")>
        Public ReadOnly Property FixedAsset_FixedAssetResponsibles() As XPCollection(Of FixedAsset_FixedAssetResponsible)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetResponsible)("FixedAsset_FixedAssetResponsibles")
            End Get
        End Property
        <Association("Maintenance_MaintenanceProtocolReferencesFixedAsset_ResponsibleType")>
        Public ReadOnly Property Maintenance_MaintenanceProtocols() As XPCollection(Of Maintenance_MaintenanceProtocol)
            Get
                Return GetCollection(Of Maintenance_MaintenanceProtocol)("Maintenance_MaintenanceProtocols")
            End Get
        End Property
End Class