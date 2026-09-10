'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/06/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Authorization.ViewListRequestsTraceability")>
Public Class ViewListRequestsTraceabilityXpo
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

    Dim fCareCenterCodeName As String
    Public Property CareCenterCodeName() As String
        Get
            Return fCareCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCodeName", fCareCenterCodeName, value)
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

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
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

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
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

    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
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

    Dim fServiceId As Integer
    Public Property ServiceId() As Integer
        Get
            Return fServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceId", fServiceId, value)
        End Set
    End Property

    Dim fServiceCode As String
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
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

    Dim fServiceDescription As String
    Public Property ServiceDescription() As String
        Get
            Return fServiceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceDescription", fServiceDescription, value)
        End Set
    End Property

    Dim fContractDescriptionId As Integer?
    Public Property ContractDescriptionId() As Integer?
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionCodeName As String
    Public Property ContractDescriptionCodeName() As String
        Get
            Return fContractDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCodeName", fContractDescriptionCodeName, value)
        End Set
    End Property

    Dim fIsCovered As Boolean
    Public Property IsCovered() As Boolean
        Get
            Return fIsCovered
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsCovered", fIsCovered, value)
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

    Dim fAssignUserCode As String
    Public Property AssignUserCode() As String
        Get
            Return fAssignUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssignUserCode", fAssignUserCode, value)
        End Set
    End Property

    Dim fAssignUser As String
    Public Property AssignUser() As String
        Get
            Return fAssignUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssignUser", fAssignUser, value)
        End Set
    End Property

    Dim fRequestTime As Integer
    Public Property RequestTime() As Integer
        Get
            Return fRequestTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestTime", fRequestTime, value)
        End Set
    End Property

    Dim fRequestUnitTime As Integer
    Public Property RequestUnitTime() As Integer
        Get
            Return fRequestUnitTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestUnitTime", fRequestUnitTime, value)
        End Set
    End Property

    Dim fRequestElapsedTime As Integer
    Public Property RequestElapsedTime() As Integer
        Get
            Return fRequestElapsedTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestElapsedTime", fRequestElapsedTime, value)
        End Set
    End Property

    Dim fColorRequest As Integer
    Public Property ColorRequest() As Integer
        Get
            Return fColorRequest
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ColorRequest", fColorRequest, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkId As Integer
    Public Property TraceabilityPaperworkId() As Integer
        Get
            Return fTraceabilityPaperworkId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkId", fTraceabilityPaperworkId, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkStatus As Integer
    Public Property TraceabilityPaperworkStatus() As Integer
        Get
            Return fTraceabilityPaperworkStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkStatus", fTraceabilityPaperworkStatus, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkEventsId As Integer
    Public Property TraceabilityPaperworkEventsId() As Integer
        Get
            Return fTraceabilityPaperworkEventsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkEventsId", fTraceabilityPaperworkEventsId, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkEventsStatus As Integer
    Public Property TraceabilityPaperworkEventsStatus() As Integer
        Get
            Return fTraceabilityPaperworkEventsStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkEventsStatus", fTraceabilityPaperworkEventsStatus, value)
        End Set
    End Property

    Dim fAuthorizationSourceId As Integer
    Public Property AuthorizationSourceId() As Integer
        Get
            Return fAuthorizationSourceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationSourceId", fAuthorizationSourceId, value)
        End Set
    End Property

    Dim fIsManual As Boolean
    Public Property IsManual() As Boolean
        Get
            Return fIsManual
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsManual", fIsManual, value)
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

    Dim fAlert As Boolean
    Public Property Alert() As Boolean
        Get
            Return fAlert
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Alert", fAlert, value)
        End Set
    End Property

    Dim fPatientThirdPartyId As Integer
    Public Property PatientThirdPartyId() As Integer
        Get
            Return fPatientThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientThirdPartyId", fPatientThirdPartyId, value)
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

    Dim fCareCenterTargetCodeName As String
    Public Property CareCenterTargetCodeName() As String
        Get
            Return fCareCenterTargetCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterTargetCodeName", fCareCenterTargetCodeName, value)
        End Set
    End Property

    Dim fFunctionalUnitTargetCodeName As String
    Public Property FunctionalUnitTargetCodeName() As String
        Get
            Return fFunctionalUnitTargetCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitTargetCodeName", fFunctionalUnitTargetCodeName, value)
        End Set
    End Property

    Dim fDiagnosticCode As String
    Public Property DiagnosticCode() As String
        Get
            Return fDiagnosticCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiagnosticCode", fDiagnosticCode, value)
        End Set
    End Property

    Dim fDiagnosticDescription As String
    Public Property DiagnosticDescription() As String
        Get
            Return fDiagnosticDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiagnosticDescription", fDiagnosticDescription, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkPostponementReasonsId As Integer
    Public Property TraceabilityPaperworkPostponementReasonsId() As Integer
        Get
            Return fTraceabilityPaperworkPostponementReasonsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkPostponementReasonsId", fTraceabilityPaperworkPostponementReasonsId, value)
        End Set
    End Property

    Dim fPostponementReasonsId As Integer
    Public Property PostponementReasonsId() As Integer
        Get
            Return fPostponementReasonsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PostponementReasonsId", fPostponementReasonsId, value)
        End Set
    End Property

    Dim fPostponementDate As DateTime?
    Public Property PostponementDate() As DateTime?
        Get
            Return fPostponementDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("PostponementDate", fPostponementDate, value)
        End Set
    End Property

    Dim fPostponementCreationDate As DateTime
    Public Property PostponementCreationDate() As DateTime
        Get
            Return fPostponementCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PostponementCreationDate", fPostponementCreationDate, value)
        End Set
    End Property

    Dim fPostponementCreationUser As String
    Public Property PostponementCreationUser() As String
        Get
            Return fPostponementCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PostponementCreationUser", fPostponementCreationUser, value)
        End Set
    End Property

    Dim fPostponementCodeName As String
    Public Property PostponementCodeName() As String
        Get
            Return fPostponementCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PostponementCodeName", fPostponementCodeName, value)
        End Set
    End Property

    Dim fPreviousStatus As Integer?
    Public Property PreviousStatus() As Integer?
        Get
            Return fPreviousStatus
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PreviousStatus", fPreviousStatus, value)
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

    <PersistentAlias("Iif(RequestUnitTime = 1, 'Minutos', RequestUnitTime = 2, 'Horas', RequestUnitTime = 3, 'Días', '')")>
    Public ReadOnly Property RequestUnitTimeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RequestUnitTimeName"))
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
