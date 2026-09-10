Imports DevExpress.Xpo

<Persistent("Maintenance.MaintenanceProtocol")>
Partial Public Class Maintenance_MaintenanceProtocol
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

    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property

    Dim fFixedAssetItemId As FixedAsset_FixedAssetItem
    <Association("Maintenance_MaintenanceProtocolReferencesFixedAsset_FixedAssetItem")>
    Public Property FixedAssetItemId() As FixedAsset_FixedAssetItem
        Get
            Return fFixedAssetItemId
        End Get
        Set(ByVal value As FixedAsset_FixedAssetItem)
            SetPropertyValue(Of FixedAsset_FixedAssetItem)("FixedAssetItemId", fFixedAssetItemId, value)
        End Set
    End Property

    Dim fResponsibleTypeId As FixedAsset_ResponsibleType
    <Association("Maintenance_MaintenanceProtocolReferencesFixedAsset_ResponsibleType")>
    Public Property ResponsibleTypeId() As FixedAsset_ResponsibleType
        Get
            Return fResponsibleTypeId
        End Get
        Set(ByVal value As FixedAsset_ResponsibleType)
            SetPropertyValue(Of FixedAsset_ResponsibleType)("ResponsibleTypeId", fResponsibleTypeId, value)
        End Set
    End Property

    Dim fColor As Integer
    Public Property Color() As Integer
        Get
            Return fColor
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Color", fColor, value)
        End Set
    End Property

    Dim fConsumabeDescription As String
    Public Property ConsumabeDescription() As String
        Get
            Return fConsumabeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConsumabeDescription", fConsumabeDescription, value)
        End Set
    End Property

    Dim fSupplyDescription As String
    Public Property SupplyDescription() As String
        Get
            Return fSupplyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SupplyDescription", fSupplyDescription, value)
        End Set
    End Property

    Dim fToolsDescription As String
    Public Property ToolsDescription() As String
        Get
            Return fToolsDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ToolsDescription", fToolsDescription, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    Dim fCreationUser As String
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

#End Region

#Region "Navigation Members"

    <Association("Maintenance_ProtocolConsumablesReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_ProtocolConsumabless() As XPCollection(Of Maintenance_ProtocolConsumables)
        Get
            Return GetCollection(Of Maintenance_ProtocolConsumables)("Maintenance_ProtocolConsumabless")
        End Get
    End Property

    <Association("Maintenance_ProtocolSupplierReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_ProtocolSuppliers() As XPCollection(Of Maintenance_ProtocolSupplier)
        Get
            Return GetCollection(Of Maintenance_ProtocolSupplier)("Maintenance_ProtocolSuppliers")
        End Get
    End Property

    <Association("Maintenance_ProtocolToolsReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_ProtocolToolss() As XPCollection(Of Maintenance_ProtocolTools)
        Get
            Return GetCollection(Of Maintenance_ProtocolTools)("Maintenance_ProtocolToolss")
        End Get
    End Property

    <Association("Maintenance_ProtocolActivitiesReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_ProtocolActivitiess() As XPCollection(Of Maintenance_ProtocolActivities)
        Get
            Return GetCollection(Of Maintenance_ProtocolActivities)("Maintenance_ProtocolActivitiess")
        End Get
    End Property

    <Association("FixedAsset_FixedAssetItemProtocolReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property FixedAsset_FixedAssetItemProtocols() As XPCollection(Of FixedAsset_FixedAssetItemProtocol)
        Get
            Return GetCollection(Of FixedAsset_FixedAssetItemProtocol)("FixedAsset_FixedAssetItemProtocols")
        End Get
    End Property

    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_WorkOrders() As XPCollection(Of Maintenance_WorkOrder)
        Get
            Return GetCollection(Of Maintenance_WorkOrder)("Maintenance_WorkOrders")
        End Get
    End Property
    <Association("Maintenance_MaintenancePlanAndMetrologyReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property Maintenance_MaintenancePlanAndMetrologys() As XPCollection(Of Maintenance_MaintenancePlanAndMetrology)
        Get
            Return GetCollection(Of Maintenance_MaintenancePlanAndMetrology)("Maintenance_MaintenancePlanAndMetrologys")
        End Get
    End Property

    <Association("Maintenance_MaintenancePlanAndMetrologyReferencesMaintenance_MaintenanceProtocol1")>
    Public ReadOnly Property Maintenance_MaintenancePlanAndMetrologys1() As XPCollection(Of Maintenance_MaintenancePlanAndMetrology)
        Get
            Return GetCollection(Of Maintenance_MaintenancePlanAndMetrology)("Maintenance_MaintenancePlanAndMetrologys1")
        End Get
    End Property

    <Association("Maintenance_ViewWorkOrderReferencesMaintenance_MaintenanceProtocol")>
    Public ReadOnly Property ViewWorkOrderReportXpo() As XPCollection(Of ViewWorkOrderReportXpo)
        Get
            Return GetCollection(Of ViewWorkOrderReportXpo)("ViewWorkOrderReportXpo")
        End Get
    End Property

#End Region

End Class