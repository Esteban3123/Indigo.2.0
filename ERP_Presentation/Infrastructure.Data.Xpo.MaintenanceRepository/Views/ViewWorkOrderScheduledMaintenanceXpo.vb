Imports DevExpress.Xpo

<Persistent("Maintenance.ViewWorkOrderScheduledMaintenance")>
Public Class ViewWorkOrderScheduledMaintenanceXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fProgramingId As Integer
    Public Property ProgramingId() As Integer
        Get
            Return fProgramingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProgramingId", fProgramingId, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fMaintenancePlanProgramatedId As Integer
    Public Property MaintenancePlanProgramatedId() As Integer
        Get
            Return fMaintenancePlanProgramatedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaintenancePlanProgramatedId", fMaintenancePlanProgramatedId, value)
        End Set
    End Property

    Dim fMaintenanceType As String
    Public Property MaintenanceType() As String
        Get
            Return fMaintenanceType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaintenanceType", fMaintenanceType, value)
        End Set
    End Property

    Dim fProgramState As Byte
    Public Property ProgramState() As Byte
        Get
            Return fProgramState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProgramState", fProgramState, value)
        End Set
    End Property

    Dim fProgramStateName As String
    Public Property ProgramStateName() As String
        Get
            Return fProgramStateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProgramStateName", fProgramStateName, value)
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

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
        End Set
    End Property

    Dim fItemId As Integer
    Public Property ItemId() As Integer
        Get
            Return fItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemId", fItemId, value)
        End Set
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

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fInventoryTypeId As Integer
    Public Property InventoryTypeId() As Integer
        Get
            Return fInventoryTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryTypeId", fInventoryTypeId, value)
        End Set
    End Property

    Dim fLocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return fLocationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LocationId", fLocationId, value)
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

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Dim fBranchOfficeCodeName As String
    Public Property BranchOfficeCodeName() As String
        Get
            Return fBranchOfficeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BranchOfficeCodeName", fBranchOfficeCodeName, value)
        End Set
    End Property

    Dim fTrademarkCodeName As String
    Public Property TrademarkCodeName() As String
        Get
            Return fTrademarkCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TrademarkCodeName", fTrademarkCodeName, value)
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

    Dim fRequestDate As DateTime
    Public Property RequestDate() As DateTime
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RequestDate", fRequestDate, value)
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

    Dim fWorkOrderState As Byte
    Public Property WorkOrderState() As Byte
        Get
            Return fWorkOrderState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("WorkOrderState", fWorkOrderState, value)
        End Set
    End Property

    Dim fProtocolId As Integer
    Public Property ProtocolId() As Integer
        Get
            Return fProtocolId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProtocolId", fProtocolId, value)
        End Set
    End Property

    Dim fProtocolCodeName As String
    Public Property ProtocolCodeName() As String
        Get
            Return fProtocolCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProtocolCodeName", fProtocolCodeName, value)
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

    Dim fReponsibleTypeId As Integer?
    Public Property ReponsibleTypeId() As Integer?
        Get
            Return fReponsibleTypeId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ReponsibleTypeId", fReponsibleTypeId, value)
        End Set
    End Property

    Dim fResponsibleCodeName As String
    Public Property ResponsibleCodeName() As String
        Get
            Return fResponsibleCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleCodeName", fResponsibleCodeName, value)
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

#End Region

#Region "Custom Members"

    <PersistentAlias("GetYear(ProgramDate)")>
    Public ReadOnly Property ProgramDateYear() As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ProgramDateYear"))
        End Get
    End Property

    <PersistentAlias("GetMonth(ProgramDate)")>
    Public ReadOnly Property ProgramDateMonth() As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ProgramDateMonth"))
        End Get
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
