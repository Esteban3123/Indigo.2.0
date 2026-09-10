Imports DevExpress.Xpo
Imports Domain.Entities

<Persistent("dbo.ViewLiquidationGetAdmissionAll")>
Partial Public Class ViewLiquidationGetAdmissionAll
    Inherits XPLiteObject

    Dim fAdmissionCode As String
    <Key(True)>
    Public Property AdmissionCode() As String
        Get
            Return fAdmissionCode
        End Get
        Set(value As String)
            SetPropertyValue("AdmissionCode", fAdmissionCode, value)
        End Set
    End Property

    Dim fAdmissionCodeWithOutTrim As String
    Public Property AdmissionCodeWithOutTrim() As String
        Get
            Return fAdmissionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCodeWithOutTrim", fAdmissionCodeWithOutTrim, value)
        End Set
    End Property

    Dim fAdmissionDate As Date
    Public Property AdmissionDate() As Date
        Get
            Return fAdmissionDate
        End Get
        Set(value As Date)
            SetPropertyValue("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property

    Dim fAdmissionType As Integer
    Public Property AdmissionType() As Integer
        Get
            Return fAdmissionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("AdmissionType", fAdmissionType, value)
        End Set
    End Property

    Dim fAdmissionCaregroupId As Integer?
    Public Property AdmissionCaregroupId() As Integer?
        Get
            Return fAdmissionCaregroupId
        End Get
        Set(value As Integer?)
            SetPropertyValue("AdmissionCaregroupId", fAdmissionCaregroupId, value)
        End Set
    End Property

    Dim fAdmissionReason As Integer
    Public Property AdmissionReason() As Integer
        Get
            Return fAdmissionReason
        End Get
        Set(value As Integer)
            SetPropertyValue("AdmissionReason", fAdmissionReason, value)
        End Set
    End Property

    Dim fAdmissionRiskType As Integer
    Public Property AdmissionRiskType() As Integer
        Get
            Return fAdmissionRiskType
        End Get
        Set(value As Integer)
            SetPropertyValue("AdmissionRiskType", fAdmissionRiskType, value)
        End Set
    End Property

    Dim fAdmissionCentAtencCodeName As String
    Public Property AdmissionCentAtencCodeName() As String
        Get
            Return fAdmissionCentAtencCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("AdmissionCentAtencCodeName", fAdmissionCentAtencCodeName, value)
        End Set
    End Property

    Dim fAdmissionUniFuncCodeName As String
    Public Property AdmissionUniFuncCodeName() As String
        Get
            Return fAdmissionUniFuncCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("AdmissionUniFuncCodeName", fAdmissionUniFuncCodeName, value)
        End Set
    End Property

    Dim fPlaceEntry As Integer
    Public Property PlaceEntry() As Integer
        Get
            Return fPlaceEntry
        End Get
        Set(value As Integer)
            SetPropertyValue("PlaceEntry", fPlaceEntry, value)
        End Set
    End Property

    Dim fBenefitPlan As String
    Public Property BenefitPlan() As String
        Get
            Return fBenefitPlan
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("BenefitPlan", fBenefitPlan, value)
        End Set
    End Property

    Dim fAuthorizationNumber As String
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property

    Dim fResponsiblePhone As String
    Public Property ResponsiblePhone() As String
        Get
            Return fResponsiblePhone
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ResponsiblePhone", fResponsiblePhone, value)
        End Set
    End Property

    Dim fResponsibleName As String
    Public Property ResponsibleName() As String
        Get
            Return fResponsibleName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleName", fResponsibleName, value)
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

    Dim fPatientBirth As DateTime
    Public Property PatientBirth() As DateTime
        Get
            Return fPatientBirth
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("PatientBirth", fPatientBirth, value)
        End Set
    End Property

    Dim fPatientGenus As Integer
    Public Property PatientGenus() As Integer
        Get
            Return fPatientGenus
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientGenus", fPatientGenus, value)
        End Set
    End Property

    Dim fPatientEstrato As Integer
    Public Property PatientEstrato() As Integer
        Get
            Return fPatientEstrato
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientEstrato", fPatientEstrato, value)
        End Set
    End Property

    Dim fPatientType As Integer
    Public Property PatientType() As Integer
        Get
            Return fPatientType
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientType", fPatientType, value)
        End Set
    End Property

    Dim fPatientAfiliation As Integer
    Public Property PatientAfiliation() As Integer
        Get
            Return fPatientAfiliation
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientAfiliation", fPatientAfiliation, value)
        End Set
    End Property

    Dim fPatientDocumentType As Integer
    Public Property PatientDocumentType() As Integer
        Get
            Return fPatientDocumentType
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientDocumentType", fPatientDocumentType, value)
        End Set
    End Property

    Dim fNivelCode As String
    Public Property NivelCode() As String
        Get
            Return fNivelCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("NivelCode", fNivelCode, value)
        End Set
    End Property

    Dim fNivelName As String
    Public Property NivelName() As String
        Get
            Return fNivelName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("NivelName", fNivelName, value)
        End Set
    End Property

    Dim fNivelModeratorSharePercentage As Decimal
    Public Property NivelModeratorSharePercentage() As Decimal
        Get
            Return fNivelModeratorSharePercentage
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorSharePercentage", fNivelModeratorSharePercentage, value)
        End Set
    End Property

    Dim fNivelCoPayContribPercentage As Decimal
    Public Property NivelCoPayContribPercentage() As Decimal
        Get
            Return fNivelCoPayContribPercentage
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribPercentage", fNivelCoPayContribPercentage, value)
        End Set
    End Property

    Dim fNivelCoPaySubsiPercentage As Decimal
    Public Property NivelCoPaySubsiPercentage() As Decimal
        Get
            Return fNivelCoPaySubsiPercentage
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsiPercentage", fNivelCoPaySubsiPercentage, value)
        End Set
    End Property

    Dim fNivelCoPayVincuPercentage As Decimal
    Public Property NivelCoPayVincuPercentage() As Decimal
        Get
            Return fNivelCoPayVincuPercentage
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuPercentage", fNivelCoPayVincuPercentage, value)
        End Set
    End Property

    Dim fNivelSisben As Integer
    Public Property NivelSisben() As Integer
        Get
            Return fNivelSisben
        End Get
        Set(value As Integer)
            SetPropertyValue("NivelSisben", fNivelSisben, value)
        End Set
    End Property

    Dim fNivelModeratorShareTop As Decimal
    Public Property NivelModeratorShareTop() As Decimal
        Get
            Return fNivelModeratorShareTop
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorShareTop", fNivelModeratorShareTop, value)
        End Set
    End Property

    Dim fNivelCoPayContribTop As Decimal
    Public Property NivelCoPayContribTop() As Decimal
        Get
            Return fNivelCoPayContribTop
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribTop", fNivelCoPayContribTop, value)
        End Set
    End Property

    Dim fNivelCoPaySubsibTop As Decimal
    Public Property NivelCoPaySubsibTop() As Decimal
        Get
            Return fNivelCoPaySubsibTop
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsibTop", fNivelCoPaySubsibTop, value)
        End Set
    End Property

    Dim fNivelCoPayVincuTop As Decimal
    Public Property NivelCoPayVincuTop() As Decimal
        Get
            Return fNivelCoPayVincuTop
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuTop", fNivelCoPayVincuTop, value)
        End Set
    End Property

    Dim fNivelModeratorShareTopYear As Decimal
    Public Property NivelModeratorShareTopYear() As Decimal
        Get
            Return fNivelModeratorShareTopYear
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorShareTopYear", fNivelModeratorShareTopYear, value)
        End Set
    End Property

    Dim fNivelCoPayContribTopYear As Decimal
    Public Property NivelCoPayContribTopYear() As Decimal
        Get
            Return fNivelCoPayContribTopYear
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribTopYear", fNivelCoPayContribTopYear, value)
        End Set
    End Property

    Dim fNivelCoPaySubsibTopYear As Decimal
    Public Property NivelCoPaySubsibTopYear() As Decimal
        Get
            Return fNivelCoPaySubsibTopYear
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsibTopYear", fNivelCoPaySubsibTopYear, value)
        End Set
    End Property

    Dim fNivelCoPayVincuTopYear As Decimal
    Public Property NivelCoPayVincuTopYear() As Decimal
        Get
            Return fNivelCoPayVincuTopYear
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuTopYear", fNivelCoPayVincuTopYear, value)
        End Set
    End Property

    Dim fStatus As String
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer?
    Public Property HealthAdministratorId() As Integer?
        Get
            Return fHealthAdministratorId
        End Get
        Set(value As Integer?)
            SetPropertyValue("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fPatientEntityCode As String
    Public Property PatientEntityCode() As String
        Get
            Return fPatientEntityCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("PatientEntityCode", fPatientEntityCode, value)
        End Set
    End Property

    Dim fPatientEntityName As String
    Public Property PatientEntityName() As String
        Get
            Return fPatientEntityName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("PatientEntityName", fPatientEntityName, value)
        End Set
    End Property

    Dim fPatientEntityId As Integer
    Public Property PatientEntityId() As Integer
        Get
            Return fPatientEntityId
        End Get
        Set(value As Integer)
            SetPropertyValue("PatientEntityId", fPatientEntityId, value)
        End Set
    End Property

    Dim fPatientPhone As String
    Public Property PatientPhone() As String
        Get
            Return fPatientPhone
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("PatientPhone", fPatientPhone, value)
        End Set
    End Property

    Dim fPatientCareGroupId As Integer?
    Public Property PatientCareGroupId() As Integer?
        Get
            Return fPatientCareGroupId
        End Get
        Set(value As Integer?)
            SetPropertyValue("PatientCareGroupId", fPatientCareGroupId, value)
        End Set
    End Property

    Dim fPatientName As String
    <Indexed(Name:="IDX_V3", Unique:=False)>
    <Size(250)>
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    <PersistentAlias("concat('No. INGRESO: ',Trim(AdmissionCode),' PACIENTE: ',Trim(PatientCode),' - ',Trim(PatientName))")>
    Public ReadOnly Property FullNameAdmission As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FullNameAdmission"))
        End Get
    End Property

    Dim fBedStay As String
    <Size(10)>
    Public Property BedStay() As String
        Get
            Return fBedStay
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BedStay", fBedStay, value)
        End Set
    End Property
    Dim fLiquidationType As Integer
    <Size(16)>
    Public Property LiquidationType() As Integer
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fStatusName As String
    <Size(14)>
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property

    Dim fAdmissionCareGroupCodeName As String
    Public Property AdmissionCareGroupCodeName() As String
        Get
            Return fAdmissionCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCareGroupCodeName", fAdmissionCareGroupCodeName, value)
        End Set
    End Property

    Dim fFECALTPAC As DateTime
    Public Property FECALTPAC() As DateTime
        Get
            Return fFECALTPAC
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECALTPAC", fFECALTPAC, value)
        End Set
    End Property

    Dim fUniFuncEgre As String
    Public Property UniFuncEgre() As String
        Get
            Return fUniFuncEgre
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UniFuncEgre", fUniFuncEgre, value)
        End Set
    End Property

    Dim fTratamientoEspecial As Integer?
    Public Property TratamientoEspecial() As Integer?
        Get
            Return fTratamientoEspecial
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("TratamientoEspecial", fTratamientoEspecial, value)
        End Set
    End Property

    Dim fRevenueControlId As Integer?
    Public Property RevenueControlId() As Integer?
        Get
            Return fRevenueControlId
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("RevenueControlId", fRevenueControlId, value)
        End Set
    End Property

    Dim fThirdPartyPatientId As Integer?
    Public Property ThirdPartyPatientId() As Integer?
        Get
            Return fThirdPartyPatientId
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("ThirdPartyPatientId", fThirdPartyPatientId, value)
        End Set
    End Property

    Dim fCareGroupTypePatient As Integer?
    Public Property CareGroupTypePatient() As Integer?
        Get
            Return fCareGroupTypePatient
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("CareGroupTypePatient", fCareGroupTypePatient, value)
        End Set
    End Property

    Dim fAdmissionTypeLiquidationEmergencyStays As Byte
    Public Property AdmissionTypeLiquidationEmergencyStays() As Byte
        Get
            Return fAdmissionTypeLiquidationEmergencyStays
        End Get
        Set(value As Byte)
            SetPropertyValue("AdmissionTypeLiquidationEmergencyStays", fAdmissionTypeLiquidationEmergencyStays, value)
        End Set
    End Property

    Dim fPatientEntity As String
    Public Property PatientEntity As String
        Get
            Return fPatientEntity
        End Get
        Set(value As String)
            SetPropertyValue("PatientEntity", fPatientEntity, value)
        End Set
    End Property

    Dim fSMLV As Decimal
    Public Property SMLV As Decimal
        Get
            Return fSMLV
        End Get
        Set(value As Decimal)
            SetPropertyValue("SMLV", fSMLV, value)
        End Set
    End Property

    Dim fAdmissionHealthAdministratorCodeName As String
    Public Property AdmissionHealthAdministratorCodeName() As String
        Get
            Return fAdmissionHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionHealthAdministratorCodeName", fAdmissionHealthAdministratorCodeName, value)
        End Set
    End Property

    Dim fAdmissionCareGroupApplyRIAS As Boolean
    Public Property AdmissionCareGroupApplyRIAS() As Boolean
        Get
            Return fAdmissionCareGroupApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdmissionCareGroupApplyRIAS", fAdmissionCareGroupApplyRIAS, value)
        End Set
    End Property

    Dim fHealthAdministratorThirdPartyId As Integer?
    Public Property HealthAdministratorThirdPartyId() As Integer?
        Get
            Return fHealthAdministratorThirdPartyId
        End Get
        Set(value As Integer?)
            SetPropertyValue("HealthAdministratorThirdPartyId", fHealthAdministratorThirdPartyId, value)
        End Set
    End Property

    Dim fContractId As Integer?
    Public Property ContractId() As Integer?
        Get
            Return fContractId
        End Get
        Set(value As Integer?)
            SetPropertyValue("ContractId", fContractId, value)
        End Set
    End Property

    Dim fCareGroupContractCodeName As String
    Public Property CareGroupContractCodeName() As String
        Get
            Return fCareGroupContractCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupContractCodeName", fCareGroupContractCodeName, value)
        End Set
    End Property

    Dim fIsMasterAccount As Boolean
    Public Property IsMasterAccount() As Boolean
        Get
            Return fIsMasterAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMasterAccount", fIsMasterAccount, value)
        End Set
    End Property

    Dim fMasterAccountId As Integer?
    Public Property MasterAccountId() As Integer?
        Get
            Return fMasterAccountId
        End Get
        Set(value As Integer?)
            SetPropertyValue("MasterAccountId", fMasterAccountId, value)
        End Set
    End Property

    Dim fDateTRM As DateTime?
    Public Property DateTRM() As DateTime?
        Get
            Return fDateTRM
        End Get
        Set(value As DateTime?)
            SetPropertyValue("DateTRM", fDateTRM, value)
        End Set
    End Property

    <NonPersistent()>
    Public Property Id As Integer
    <NonPersistent()>
    Public Property FolioQuantity As Integer
    <NonPersistent()>
    Public Property LiquidationTypeName As String
    <NonPersistent()>
    Public Property ContractCodeName As String
    <NonPersistent()>
    Public Property ListRevenueControlDetails As List(Of FolioDataDetail)

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class