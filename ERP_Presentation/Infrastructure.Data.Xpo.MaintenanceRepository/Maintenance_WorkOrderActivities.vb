Imports DevExpress.Xpo

<Persistent("Maintenance.WorkOrderActivities")>
Public Class Maintenance_WorkOrderActivities
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

    Dim fWorkOrderId As Maintenance_WorkOrder
    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceWorkOrderActivities")>
    Public Property WorkOrderId() As Maintenance_WorkOrder
        Get
            Return fWorkOrderId
        End Get
        Set(ByVal value As Maintenance_WorkOrder)
            SetPropertyValue(Of Maintenance_WorkOrder)("WorkOrderId", fWorkOrderId, value)
        End Set
    End Property

    Dim fProtocolActivityId As Maintenance_ProtocolActivities
    Public Property ProtocolActivityId() As Maintenance_ProtocolActivities
        Get
            Return fProtocolActivityId
        End Get
        Set(ByVal value As Maintenance_ProtocolActivities)
            SetPropertyValue(Of Maintenance_ProtocolActivities)("ProtocolActivityId", fProtocolActivityId, value)
        End Set
    End Property
#End Region

#Region "Custom Members"


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