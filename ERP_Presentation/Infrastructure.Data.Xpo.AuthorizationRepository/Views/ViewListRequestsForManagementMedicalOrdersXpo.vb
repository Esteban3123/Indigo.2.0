Imports DevExpress.Xpo

<Persistent("Authorization.ViewListRequestsForManagementMedicalOrders")>
Public Class ViewListRequestsForManagementMedicalOrderXpo
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

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fCareCenterName As String
    Public Property CareCenterName() As String
        Get
            Return fCareCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterName", fCareCenterName, value)
        End Set
    End Property

    Dim fFunctionalUnitCode As String
    Public Property FunctionalUnitCode() As String
        Get
            Return fFunctionalUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCode", fFunctionalUnitCode, value)
        End Set
    End Property

    Dim fFunctionalUnitName As String
    Public Property FunctionalUnitName() As String
        Get
            Return fFunctionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fFolio As String
    Public Property Folio() As String
        Get
            Return fFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Folio", fFolio, value)
        End Set
    End Property

    Dim fTypeClinicalHistory As Integer
    Public Property TypeClinicalHistory() As Integer
        Get
            Return fTypeClinicalHistory
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TypeClinicalHistory", fTypeClinicalHistory, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fCareGroupCode As String
    Public Property CareGroupCode() As String
        Get
            Return fCareGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCode", fCareGroupCode, value)
        End Set
    End Property

    Dim fCareGroupName As String
    Public Property CareGroupName() As String
        Get
            Return fCareGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupName", fCareGroupName, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fHealthAdministratorCode As String
    Public Property HealthAdministratorCode() As String
        Get
            Return fHealthAdministratorCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCode", fHealthAdministratorCode, value)
        End Set
    End Property

    Dim fHealthAdministratorName As String
    Public Property HealthAdministratorName() As String
        Get
            Return fHealthAdministratorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorName", fHealthAdministratorName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientAddress As String
    Public Property PatientAddress() As String
        Get
            Return fPatientAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAddress", fPatientAddress, value)
        End Set
    End Property

    Dim fPatientPhone As String
    Public Property PatientPhone() As String
        Get
            Return fPatientPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientPhone", fPatientPhone, value)
        End Set
    End Property

    Dim fPatientAge As String
    Public Property PatientAge() As String
        Get
            Return fPatientAge
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAge", fPatientAge, value)
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

    Dim fProfessionalCode As String
    Public Property ProfessionalCode() As String
        Get
            Return fProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalCode", fProfessionalCode, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
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

    Dim fItemCode As String
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCode", fItemCode, value)
        End Set
    End Property

    Dim fItemCodeOriginal As String
    Public Property ItemCodeOriginal() As String
        Get
            Return fItemCodeOriginal
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeOriginal", fItemCodeOriginal, value)
        End Set
    End Property

    Dim fItemName As String
    Public Property ItemName() As String
        Get
            Return fItemName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemName", fItemName, value)
        End Set
    End Property

    Dim fDescriptionCodeName As String
    Public Property DescriptionCodeName() As String
        Get
            Return fDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionCodeName", fDescriptionCodeName, value)
        End Set
    End Property

    Dim fCovered As Boolean
    Public Property Covered() As Boolean
        Get
            Return fCovered
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Covered", fCovered, value)
        End Set
    End Property

    Dim fContracted As Boolean
    Public Property Contracted() As Boolean
        Get
            Return fContracted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Contracted", fContracted, value)
        End Set
    End Property

    Dim fQuoted As Boolean
    Public Property Quoted() As Boolean
        Get
            Return fQuoted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Quoted", fQuoted, value)
        End Set
    End Property

    Dim fAuthorized As Boolean
    Public Property Authorized() As Boolean
        Get
            Return fAuthorized
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Authorized", fAuthorized, value)
        End Set
    End Property

    Dim fManagementMedicalOrderId As Integer?
    Public Property ManagementMedicalOrderId() As Integer?
        Get
            Return fManagementMedicalOrderId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ManagementMedicalOrderId", fManagementMedicalOrderId, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fAuthorizationGroupId As Integer
    Public Property AuthorizationGroupId() As Integer
        Get
            Return fAuthorizationGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationGroupId", fAuthorizationGroupId, value)
        End Set
    End Property

    Dim fAuthorizationGroupCodeName As String
    Public Property AuthorizationGroupCodeName() As String
        Get
            Return fAuthorizationGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationGroupCodeName", fAuthorizationGroupCodeName, value)
        End Set
    End Property

    Dim fProfessionalCodeName As String
    Public Property ProfessionalCodeName() As String
        Get
            Return fProfessionalCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalCodeName", fProfessionalCodeName, value)
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

    <PersistentAlias("CONCAT(PatientName, ' - ', PatientAge)")>
    Public ReadOnly Property PatientNameAge() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PatientNameAge"))
        End Get
    End Property

    <PersistentAlias("IIF(Type = 1, 'Servicio', Type = 2, 'Producto', 'N/A')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(FunctionalUnitCode, ' - ', FunctionalUnitName)")>
    Public ReadOnly Property FunctionalUnitCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FunctionalUnitCodeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(CareGroupCode, ' - ', CareGroupName)")>
    Public ReadOnly Property CareGroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CareGroupCodeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(HealthAdministratorCode, ' - ', HealthAdministratorName)")>
    Public ReadOnly Property HealthAdministratorCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("HealthAdministratorCodeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(ItemCode, ' - ', ItemName)")>
    Public ReadOnly Property ItemCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ItemCodeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(CareCenterCode, ' - ', CareCenterName)")>
    Public ReadOnly Property CareCenterCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CareCenterCodeName"))
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
