#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyXpo
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
    Dim fNit As String
    <Size(15)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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
    Dim fPersonType As Byte
    Public Property PersonType() As Byte
        Get
            Return fPersonType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PersonType", fPersonType, value)
        End Set
    End Property
    Dim fRetentionType As Byte
    Public Property RetentionType() As Byte
        Get
            Return fRetentionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetentionType", fRetentionType, value)
        End Set
    End Property
    Dim fContributionType As Byte
    Public Property ContributionType() As Byte
        Get
            Return fContributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ContributionType", fContributionType, value)
        End Set
    End Property
    Dim fIca As Boolean
    Public Property Ica() As Boolean
        Get
            Return fIca
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Ica", fIca, value)
        End Set
    End Property
    Dim fIcaPercentage As Decimal
    Public Property IcaPercentage() As Decimal
        Get
            Return fIcaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaPercentage", fIcaPercentage, value)
        End Set
    End Property
    Dim fIcaTop As Boolean
    Public Property IcaTop() As Boolean
        Get
            Return fIcaTop
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IcaTop", fIcaTop, value)
        End Set
    End Property
    Dim fIcaTopValue As Decimal
    Public Property IcaTopValue() As Decimal
        Get
            Return fIcaTopValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaTopValue", fIcaTopValue, value)
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
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

#End Region

#Region "CustomMembers"

    Dim fNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

#End Region

#Region "Association"

    <Association("MaintenanceSupplierReferencesCommonThirdParty", GetType(Maintenance_Supplier))>
    Public ReadOnly Property Maintenance_Supplier() As XPCollection(Of Maintenance_Supplier)
        Get
            Return GetCollection(Of Maintenance_Supplier)("Maintenance_Supplier")
        End Get
    End Property

    <Association("MaintenanceResponsibleReferencesThirdParty", GetType(MaintenanceResponsibleXpo))>
    Public ReadOnly Property MaintenanceResponsibleXpo() As XPCollection(Of MaintenanceResponsibleXpo)
        Get
            Return GetCollection(Of MaintenanceResponsibleXpo)("MaintenanceResponsibleXpo")
        End Get
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
