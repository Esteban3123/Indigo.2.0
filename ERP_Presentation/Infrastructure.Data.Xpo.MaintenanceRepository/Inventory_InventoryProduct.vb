Imports DevExpress.Xpo

<Persistent("Inventory.InventoryProduct")>
Public Class Inventory_InventoryProduct
    Inherits XPLiteObject

#Region "Members"

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

#End Region

#Region "Association Members"

    <Association("Maintenance_ProtocolSupplierReferencesInventory_InventoryProduct")>
    Public ReadOnly Property Maintenance_ProtocolSupplies() As XPCollection(Of Maintenance_ProtocolSupplier)
        Get
            Return GetCollection(Of Maintenance_ProtocolSupplier)("Maintenance_ProtocolSupplies")
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
