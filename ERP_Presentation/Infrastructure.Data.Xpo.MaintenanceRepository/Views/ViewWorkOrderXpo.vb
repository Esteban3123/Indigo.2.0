Imports DevExpress.Xpo

<Persistent("Maintenance.ViewWorkOrder")>
Public Class ViewWorkOrderXpo
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

    Dim fWorkOrderStateName As String
    Public Property WorkOrderStateName() As String
        Get
            Return fWorkOrderStateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkOrderStateName", fWorkOrderStateName, value)
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

    Dim fMaintenanceResponsibleId As Integer
    Public Property MaintenanceResponsibleId() As Integer
        Get
            Return fMaintenanceResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaintenanceResponsibleId", fMaintenanceResponsibleId, value)
        End Set
    End Property

    Dim fReponsibleTypeId As Integer
    Public Property ReponsibleTypeId() As Integer
        Get
            Return fReponsibleTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReponsibleTypeId", fReponsibleTypeId, value)
        End Set
    End Property

    Dim fMaintenanceResponsibleCodeName As String
    Public Property MaintenanceResponsibleCodeName() As String
        Get
            Return fMaintenanceResponsibleCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaintenanceResponsibleCodeName", fMaintenanceResponsibleCodeName, value)
        End Set
    End Property

    Dim fResponsibleRole As Byte
    Public Property ResponsibleRole() As Byte
        Get
            Return fResponsibleRole
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ResponsibleRole", fResponsibleRole, value)
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

    Dim fPartCodeDescription As String
    Public Property PartCodeDescription() As String
        Get
            Return fPartCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PartCodeDescription", fPartCodeDescription, value)
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

    Dim fFixedAssetResponsibleId As Integer
    Public Property FixedAssetResponsibleId() As Integer
        Get
            Return fFixedAssetResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetResponsibleId", fFixedAssetResponsibleId, value)
        End Set
    End Property

    Dim fFixedAssetResponsibleCodeName As String
    Public Property FixedAssetResponsibleCodeName() As String
        Get
            Return fFixedAssetResponsibleCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FixedAssetResponsibleCodeName", fFixedAssetResponsibleCodeName, value)
        End Set
    End Property

    Dim fFixedAssetUserCode As String
    Public Property FixedAssetUserCode() As String
        Get
            Return fFixedAssetUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FixedAssetUserCode", fFixedAssetUserCode, value)
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
