Imports DevExpress.Xpo

<Persistent("Maintenance.WorkOrder")>
Public Class Maintenance_WorkOrder
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

    Dim fConsecutive As String
    Public Property Consecutive() As String
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Consecutive", fConsecutive, value)
        End Set
    End Property

    Dim fBranchOfficeId As PayrollBranchOffice
    <Association("Maintenance_WorkOrderReferencesPayroll_PayrollBranchOffice")>
    Public Property BranchOfficeId() As PayrollBranchOffice
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As PayrollBranchOffice)
            SetPropertyValue(Of PayrollBranchOffice)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Dim fProtocolId As Maintenance_MaintenanceProtocol
    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceProtocol")>
    Public Property ProtocolId() As Maintenance_MaintenanceProtocol
        Get
            Return fProtocolId
        End Get
        Set(ByVal value As Maintenance_MaintenanceProtocol)
            SetPropertyValue(Of Maintenance_MaintenanceProtocol)("ProtocolId", fProtocolId, value)
        End Set
    End Property

    Dim fPhysicalAssetId As FixedAsset_FixedAssetPhysicalAsset
    <Association("Maintenance_WorkOrderReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public Property PhysicalAssetId() As FixedAsset_FixedAssetPhysicalAsset
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAsset_FixedAssetPhysicalAsset)
            SetPropertyValue(Of FixedAsset_FixedAssetPhysicalAsset)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property

    Dim fMaintenanceResponsibleId As MaintenanceResponsibleXpo
    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceResponsible")>
    Public Property MaintenanceResponsibleId() As MaintenanceResponsibleXpo
        Get
            Return fMaintenanceResponsibleId
        End Get
        Set(ByVal value As MaintenanceResponsibleXpo)
            SetPropertyValue(Of MaintenanceResponsibleXpo)("MaintenanceResponsibleId", fMaintenanceResponsibleId, value)
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
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

    Dim fConfirmationUser As String
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnullateUser As String
    Public Property AnullateUser() As String
        Get
            Return fAnullateUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnullateUser", fAnullateUser, value)
        End Set
    End Property

    Dim fAnullateDate As DateTime
    Public Property AnullateDate() As DateTime
        Get
            Return fAnullateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnullateDate", fAnullateDate, value)
        End Set
    End Property

    Dim fEntityId As Integer?
    Public Property EntityId() As Integer?
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fReversalReasonId As Maintenance_MaintenanceAnulateReason
    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceAnulateReason")>
    Public Property ReversalReasonId() As Maintenance_MaintenanceAnulateReason
        Get
            Return fReversalReasonId
        End Get
        Set(ByVal value As Maintenance_MaintenanceAnulateReason)
            SetPropertyValue(Of Maintenance_MaintenanceAnulateReason)("ReversalReasonId", fReversalReasonId, value)
        End Set
    End Property

    Dim fDescriptionReversal As String
    Public Property DescriptionReversal() As String
        Get
            Return fDescriptionReversal
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionReversal", fDescriptionReversal, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(
State = 1, 'Registrado', 
State = 2, 'Confirmado',
State = 3, 'Anulado',
State = 4, 'Aprobado',
State = 5, 'Rechazado',
'')")>
    Public ReadOnly Property StateName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    <PersistentAlias("Iif(MaintenanceResponsibleId IS NULL, '', MaintenanceResponsibleId.CodeNitName)")>
    Public ReadOnly Property ResponsibleCodeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ResponsibleCodeName"))
        End Get
    End Property

#End Region

#Region "Association"
    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceWorkOrderActivities")>
    Public ReadOnly Property Maintenance_WorkOrderActivities() As XPCollection(Of Maintenance_WorkOrderActivities)
        Get
            Return GetCollection(Of Maintenance_WorkOrderActivities)("Maintenance_WorkOrderActivities")
        End Get
    End Property


    <Association("Maintenance_ViewWorkOrderReportReferencesMaintenance_MaintenanceWorkOrder")>
    Public ReadOnly Property ViewWorkOrderReportXpo() As XPCollection(Of ViewWorkOrderReportXpo)
        Get
            Return GetCollection(Of ViewWorkOrderReportXpo)("ViewWorkOrderReportXpo")
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