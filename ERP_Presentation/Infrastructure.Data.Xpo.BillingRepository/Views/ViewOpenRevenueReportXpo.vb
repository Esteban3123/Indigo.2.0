Imports DevExpress.Xpo

<Persistent("Billing.ViewOpenRevenueReport")>
Public Class ViewOpenRevenueReportXpo
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

    Dim fIncome As String
    Public Property Income() As String
        Get
            Return fIncome
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Income", fIncome, value)
        End Set
    End Property

    Dim fFolioOrder As Byte
    Public Property FolioOrder() As Byte
        Get
            Return fFolioOrder
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioOrder", fFolioOrder, value)
        End Set
    End Property

    Dim fFolioType As String
    Public Property FolioType() As String
        Get
            Return fFolioType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioType", fFolioType, value)
        End Set
    End Property

    Dim fFolioStatusName As String
    Public Property FolioStatusName() As String
        Get
            Return fFolioStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioStatusName", fFolioStatusName, value)
        End Set
    End Property

    Dim fResponsibleRecoveryFeeName As String
    Public Property ResponsibleRecoveryFeeName() As String
        Get
            Return fResponsibleRecoveryFeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleRecoveryFeeName", fResponsibleRecoveryFeeName, value)
        End Set
    End Property

    Dim fFolioObservations As String
    Public Property FolioObservations() As String
        Get
            Return fFolioObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioObservations", fFolioObservations, value)
        End Set
    End Property

    Dim fTotalFolio As Decimal
    Public Property TotalFolio() As Decimal
        Get
            Return fTotalFolio
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalFolio", fTotalFolio, value)
        End Set
    End Property

    Dim fConceptStatusFolio As String
    Public Property ConceptStatusFolio() As String
        Get
            Return fConceptStatusFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptStatusFolio", fConceptStatusFolio, value)
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

    Dim fCodCareGroup As String
    Public Property CodCareGroup() As String
        Get
            Return fCodCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodCareGroup", fCodCareGroup, value)
        End Set
    End Property

    Dim fCareGroup As String
    Public Property CareGroup() As String
        Get
            Return fCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroup", fCareGroup, value)
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

    Dim fEntity As String
    Public Property Entity() As String
        Get
            Return fEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entity", fEntity, value)
        End Set
    End Property

    Dim fCodCareCenter As String
    Public Property CodCareCenter() As String
        Get
            Return fCodCareCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodCareCenter", fCodCareCenter, value)
        End Set
    End Property

    Dim fCareCenter As String
    Public Property CareCenter() As String
        Get
            Return fCareCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenter", fCareCenter, value)
        End Set
    End Property

    Dim fIdentification As String
    Public Property Identification() As String
        Get
            Return fIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Identification", fIdentification, value)
        End Set
    End Property

    Dim fPlaceExpedition As String
    Public Property PlaceExpedition() As String
        Get
            Return fPlaceExpedition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlaceExpedition", fPlaceExpedition, value)
        End Set
    End Property

    Dim fPatient As String
    Public Property Patient() As String
        Get
            Return fPatient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Patient", fPatient, value)
        End Set
    End Property

    Dim fAge As String
    Public Property Age() As Integer
        Get
            Return fAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Age", fAge, value)
        End Set
    End Property

    Dim fLifeCycleEtareoGroup As String
    Public Property LifeCycleEtareoGroup() As String
        Get
            Return fLifeCycleEtareoGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LifeCycleEtareoGroup", fLifeCycleEtareoGroup, value)
        End Set
    End Property

    Dim fGroupEtareo_RES5268 As String
    Public Property GroupEtareo_RES5268() As String
        Get
            Return fGroupEtareo_RES5268
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupEtareo_RES5268", fGroupEtareo_RES5268, value)
        End Set
    End Property

    Dim fGroupEtareoUPC As String
    Public Property GroupEtareoUPC() As String
        Get
            Return fGroupEtareoUPC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupEtareoUPC", fGroupEtareoUPC, value)
        End Set
    End Property

    Dim fDateIncome As DateTime
    Public Property DateIncome() As DateTime
        Get
            Return fDateIncome
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateIncome", fDateIncome, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fFunctionalUnit As String
    Public Property FunctionalUnit() As String
        Get
            Return fFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnit", fFunctionalUnit, value)
        End Set
    End Property

    Dim fMedicalDischargeDate As DateTime
    Public Property MedicalDischargeDate() As DateTime
        Get
            Return fMedicalDischargeDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("MedicalDischargeDate", fMedicalDischargeDate, value)
        End Set
    End Property

    Dim fCIE10Income As String
    Public Property CIE10Income() As String
        Get
            Return fCIE10Income
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CIE10Income", fCIE10Income, value)
        End Set
    End Property

    Dim fAdmissionDiagnosis As String
    Public Property AdmissionDiagnosis() As String
        Get
            Return fAdmissionDiagnosis
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionDiagnosis", fAdmissionDiagnosis, value)
        End Set
    End Property

    Dim fCIE10Egress As String
    Public Property CIE10Egress() As String
        Get
            Return fCIE10Egress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CIE10Egress", fCIE10Egress, value)
        End Set
    End Property

    Dim fEgressDiagnosis As String
    Public Property EgressDiagnosis() As String
        Get
            Return fEgressDiagnosis
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EgressDiagnosis", fEgressDiagnosis, value)
        End Set
    End Property

    Dim fCodCreationUser As String
    Public Property CodCreationUser() As String
        Get
            Return fCodCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodCreationUser", fCodCreationUser, value)
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

    Dim fCodMedificationUser As String
    Public Property CodMedificationUser() As String
        Get
            Return fCodMedificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodMedificationUser", fCodMedificationUser, value)
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

    Dim fCurrentUnit As String
    Public Property CurrentUnit() As String
        Get
            Return fCurrentUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrentUnit", fCurrentUnit, value)
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

    Dim fTypeIncome As String
    Public Property TypeIncome() As String
        Get
            Return fTypeIncome
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeIncome", fTypeIncome, value)
        End Set
    End Property

    Dim fCurrentIllness As String
    Public Property CurrentIllness() As String
        Get
            Return fCurrentIllness
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrentIllness", fCurrentIllness, value)
        End Set
    End Property

    Dim fLocation As String
    Public Property Location() As String
        Get
            Return fLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Location", fLocation, value)
        End Set
    End Property

    Dim fMunicipality As String
    Public Property Municipality() As String
        Get
            Return fMunicipality
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Municipality", fMunicipality, value)
        End Set
    End Property

    Dim fLandline As String
    Public Property Landline() As String
        Get
            Return fLandline
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Landline", fLandline, value)
        End Set
    End Property

    Dim fMobilePhone As String
    Public Property MobilePhone() As String
        Get
            Return fMobilePhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MobilePhone", fMobilePhone, value)
        End Set
    End Property

    Dim fCauseIncome As String
    Public Property CauseIncome() As String
        Get
            Return fCauseIncome
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CauseIncome", fCauseIncome, value)
        End Set
    End Property

    Dim fRiskType As String
    Public Property RiskType() As String
        Get
            Return fRiskType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiskType", fRiskType, value)
        End Set
    End Property

    Dim fMonthAdmissionDate As String
    Public Property MonthAdmissionDate() As String
        Get
            Return fMonthAdmissionDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MonthAdmissionDate", fMonthAdmissionDate, value)
        End Set
    End Property

    Dim fMonthYearEntryDate As Integer
    Public Property MonthYearEntryDate() As Integer
        Get
            Return fMonthYearEntryDate
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MonthYearEntryDate", fMonthYearEntryDate, value)
        End Set
    End Property

    Dim fIncomeMedicalDischarge As String
    Public Property IncomeMedicalDischarge() As String
        Get
            Return fIncomeMedicalDischarge
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IncomeMedicalDischarge", fIncomeMedicalDischarge, value)
        End Set
    End Property

    Dim fDaysMedicalDischarge As Integer
    Public Property DaysMedicalDischarge() As Integer
        Get
            Return fDaysMedicalDischarge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DaysMedicalDischarge", fDaysMedicalDischarge, value)
        End Set
    End Property

    Dim fStateLoads As String
    Public Property StateLoads() As String
        Get
            Return fStateLoads
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StateLoads", fStateLoads, value)
        End Set
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