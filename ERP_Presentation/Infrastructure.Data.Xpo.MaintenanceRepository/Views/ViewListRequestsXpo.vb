Imports DevExpress.Xpo

<Persistent("Maintenance.ViewListRequests")>
Public Class ViewListRequestsXpo
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

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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

    Dim fDateFailure As DateTime
    Public Property DateFailure() As DateTime
        Get
            Return fDateFailure
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateFailure", fDateFailure, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer?
    Public Property BranchOfficeId() As Integer?
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Dim fTypeDescription As String
    Public Property TypeDescription() As String
        Get
            Return fTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeDescription", fTypeDescription, value)
        End Set
    End Property

    Dim fPhysicalAssetId As Integer
    Public Property PhysicalAssetId() As Integer
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property

    Dim fItemCatalogId As Integer
    Public Property ItemCatalogId() As Integer
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property

    Dim fItemCatalogCodeDescription As String
    Public Property ItemCatalogCodeDescription() As String
        Get
            Return fItemCatalogCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCatalogCodeDescription", fItemCatalogCodeDescription, value)
        End Set
    End Property

    Dim fItemCodeDescription As String
    Public Property ItemCodeDescription() As String
        Get
            Return fItemCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeDescription", fItemCodeDescription, value)
        End Set
    End Property

    Dim fLocationCodeName As String
    Public Property LocationCodeName() As String
        Get
            Return fLocationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LocationCodeName", fLocationCodeName, value)
        End Set
    End Property

    Dim fPartCodeDescription As String
    Public Property PartCodeDescription() As String
        Get
            Return fPartCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PartCodeDescription", fPartCodeDescription, value)
        End Set
    End Property

    Dim fRequestUser As String
    Public Property RequestUser() As String
        Get
            Return fRequestUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestUser", fRequestUser, value)
        End Set
    End Property

    Dim fMaintenanceResponsibleId As Integer?
    Public Property MaintenanceResponsibleId() As Integer?
        Get
            Return fMaintenanceResponsibleId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MaintenanceResponsibleId", fMaintenanceResponsibleId, value)
        End Set
    End Property

    Dim fResponsibleRole As Byte?
    Public Property ResponsibleRole() As Byte?
        Get
            Return fResponsibleRole
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("ResponsibleRole", fResponsibleRole, value)
        End Set
    End Property

    Dim fAssignUser As String
    Public Property AssignUser() As String
        Get
            Return fAssignUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssignUser", fAssignUser, value)
        End Set
    End Property

    Dim fWorkOrderId As Integer?
    Public Property WorkOrderId() As Integer?
        Get
            Return fWorkOrderId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("WorkOrderId", fWorkOrderId, value)
        End Set
    End Property

    Dim fWorkOrderCode As String
    Public Property WorkOrderCode() As String
        Get
            Return fWorkOrderCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkOrderCode", fWorkOrderCode, value)
        End Set
    End Property

    Dim fProgramDate As DateTime
    Public Property ProgramDate() As DateTime
        Get
            Return fProgramDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ProgramDate", fProgramDate, value)
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

    Dim fWorkOrderStatus As Byte?
    Public Property WorkOrderStatus() As Byte?
        Get
            Return fWorkOrderStatus
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("WorkOrderStatus", fWorkOrderStatus, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    Dim fSelectOption As Boolean
    <NonPersistent>
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            fSelectOption = value
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
