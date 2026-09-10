#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#End Region

<Persistent("Maintenance.MaintenanceResponsible")>
Public Class MaintenanceResponsibleXpo
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

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("MaintenanceResponsibleReferencesThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fVinculationTypeId As Integer
    Public Property VinculationTypeId() As Integer
        Get
            Return fVinculationTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VinculationTypeId", fVinculationTypeId, value)
        End Set
    End Property
    Dim fReponsibleTypeId As Integer
    Public Property ReponsibleTypeId() As Integer
        Get
            Return ReponsibleTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReponsibleTypeId", fReponsibleTypeId, value)
        End Set
    End Property

    Dim fResponsibleRole As String
    Public Property ResponsibleRole() As String
        Get
            Return fResponsibleRole
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleRole", fResponsibleRole, value)
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

#End Region

#Region "CustomMembers"

    <PersistentAlias("Iif(ResponsibleRole =1, 'Administrador',Iif(ResponsibleRole =2,'Encargado',Iif(ResponsibleRole =3,'Coordinador',Iif(ResponsibleRole =4,'Operario-Tecnico','Externo'))))")>
    Public ReadOnly Property ResponsibleRoleName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ResponsibleRoleName"))
        End Get
    End Property

    Dim fCodeNitName As String
    <PersistentAlias("ThirdPartyId.NitName")>
    Public ReadOnly Property CodeNitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNitName"))
        End Get
    End Property

    <PersistentAlias("Concat(Code, ' - ', ThirdPartyId.Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("IIF(Status, 'Activo','Inactivo' )")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Association Members"


    <Association("Maintenance_WorkOrderReferencesMaintenance_MaintenanceResponsible")>
    Public ReadOnly Property Maintenance_WorkOrders() As XPCollection(Of Maintenance_WorkOrder)
        Get
            Return GetCollection(Of Maintenance_WorkOrder)("Maintenance_WorkOrders")
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
