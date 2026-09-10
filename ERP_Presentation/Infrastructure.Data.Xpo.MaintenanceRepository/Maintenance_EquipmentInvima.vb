Imports DevExpress.Xpo

<Persistent("Maintenance.EquipmentInvima")>
Public Class MaintenanceEquipmentInvimaXpo
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
    Dim fDateInit As DateTime
    Public Property DateInit() As DateTime
        Get
            Return fDateInit
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateInit", fDateInit, value)
        End Set
    End Property
    Dim fNumberRegister As String
    Public Property NumberRegister() As String
        Get
            Return fNumberRegister
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberRegister", fNumberRegister, value)
        End Set
    End Property
    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
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
#End Region

#Region "CustomMembers"

    Dim fEquipmentRegistrationId As Maintenance_EquipmentRegistration
    <Association("Maintenance_EquipmentInvimaReferencesMaintenance_EquipmentRegistration")>
    Public Property EquipmentRegistrationId() As Maintenance_EquipmentRegistration
        Get
            Return fEquipmentRegistrationId
        End Get
        Set(ByVal value As Maintenance_EquipmentRegistration)
            SetPropertyValue(Of Maintenance_EquipmentRegistration)("EquipmentRegistrationId", fEquipmentRegistrationId, value)
        End Set
    End Property

#End Region

#Region "Builder"
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

