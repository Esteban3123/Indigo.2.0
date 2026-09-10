Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fPersonId As CommonPersonXpo
    <Association("CommonThirdPartyReferencesCommonPerson")> _
    Public Property PersonId() As CommonPersonXpo
        Get
            Return fPersonId
        End Get
        Set(ByVal value As CommonPersonXpo)
            SetPropertyValue(Of CommonPersonXpo)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
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
    <PersistentAlias("Iif(PersonType = 1, 'Natural', PersonType = 2, 'Jurídica', '')")>
    Public ReadOnly Property PersonTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PersonTypeName"))
        End Get
    End Property
    Dim fRetentionType As String
    Public Property RetentionType() As Integer
        Get
            Return fRetentionType
        End Get

        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionType", fRetentionType, value)
        End Set
    End Property
    <PersistentAlias("IiF(RetentionType = 0, 'Ninguna', IiF(RetentionType = 1, 'Exento de retención', IiF(RetentionType = 2, 'Hace Retención', IiF(RetentionType = 3, 'Autoretenedor', ''))))")>
    Public ReadOnly Property RetentionTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RetentionTypeName"))
        End Get
    End Property
    Dim fContributionType As String
    Public Property ContributionType() As Integer
        Get
            Return fContributionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContributionType", fContributionType, value)
        End Set
    End Property
    <PersistentAlias("Iif(ContributionType = 0, 'No responsable de Iva',
                    ContributionType = 1, ' Responsables de Iva', 
                    ContributionType = 2, 'Empresa estatal',
                    ContributionType = 3, 'Gran Contribuyente',
                    ContributionType = 4, 'Régimen simple', '')")>
    Public ReadOnly Property ContributionTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContributionTypeName"))
        End Get
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
    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
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

    Dim fState As Boolean
    <Persistent("State")>
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    Dim fElectronicBiller As Boolean
    Public Property ElectronicBiller() As Boolean
        Get
            Return fElectronicBiller
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ElectronicBiller", fElectronicBiller, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property StateName As String
        Get
            If fState = False Then
                Return "Inactivo"
            Else
                Return "Activo"
            End If
        End Get
    End Property

    <Association("MaintenanceSupplierReferencesThirdParty", GetType(Maintenance_Supplier))> _
    Public ReadOnly Property Maintenance_Supplier() As XPCollection(Of Maintenance_Supplier)
        Get
            Return GetCollection(Of Maintenance_Supplier)("Maintenance_Supplier")
        End Get
    End Property

    <Association("Customer_ThirdParty", GetType(CommonCustomerXpo))>
    Public ReadOnly Property Customer_ThirdParty() As XPCollection(Of CommonCustomerXpo)
        Get
            Return GetCollection(Of CommonCustomerXpo)("Customer_ThirdParty")
        End Get
    End Property

    Dim fNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(315)>
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

    <Association("CommonThirdPartyReferencesCommonThirdPartyEconomicActivitiesXpo", GetType(CommonThirdPartyEconomicActivitiesXpo))>
    Public ReadOnly Property CommonThirdPartyEconomicActivitiesXpo() As XPCollection(Of CommonThirdPartyEconomicActivitiesXpo)
        Get
            Return GetCollection(Of CommonThirdPartyEconomicActivitiesXpo)("CommonThirdPartyEconomicActivitiesXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
