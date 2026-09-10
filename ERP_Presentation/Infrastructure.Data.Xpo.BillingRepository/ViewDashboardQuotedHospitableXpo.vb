'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/07/2020
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
<Persistent("Billing.ViewDashboardQuotedHospitable")>
Public Class ViewDashboardQuotedHospitableXpo
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

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
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

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
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

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
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

    Dim fServiceName As String
    Public Property ServiceName() As String
        Get
            Return fServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceName", fServiceName, value)
        End Set
    End Property

    Dim fServiceCodeName As String
    Public Property ServiceCodeName() As String
        Get
            Return fServiceCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCodeName", fServiceCodeName, value)
        End Set
    End Property

    Dim fServiceType As Integer
    Public Property ServiceType() As Integer
        Get
            Return fServiceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceType", fServiceType, value)
        End Set
    End Property

    Dim fContractDescriptionId As Integer
    Public Property ContractDescriptionId() As Integer
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionCode As String
    Public Property ContractDescriptionCode() As String
        Get
            Return fContractDescriptionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCode", fContractDescriptionCode, value)
        End Set
    End Property

    Dim fContractDescriptionName As String
    Public Property ContractDescriptionName() As String
        Get
            Return fContractDescriptionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionName", fContractDescriptionName, value)
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

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
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

    Dim fProfessionalCode As String
    Public Property ProfessionalCode() As String
        Get
            Return fProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalCode", fProfessionalCode, value)
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

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
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

    Dim fExtramural As Boolean
    Public Property Extramural() As Boolean
        Get
            Return fExtramural
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Extramural", fExtramural, value)
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

    Dim fItemType As String
    Public Property ItemType() As String
        Get
            Return fItemType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fDashboardQuotedId As Integer
    Public Property DashboardQuotedId() As Integer
        Get
            Return fDashboardQuotedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DashboardQuotedId", fDashboardQuotedId, value)
        End Set
    End Property

    Dim fDashboardQuotedStatus As Integer
    Public Property DashboardQuotedStatus() As Integer
        Get
            Return fDashboardQuotedStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DashboardQuotedStatus", fDashboardQuotedStatus, value)
        End Set
    End Property

    Dim fDashboardQuotedStatusName As String
    Public Property DashboardQuotedStatusName() As String
        Get
            Return fDashboardQuotedStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DashboardQuotedStatusName", fDashboardQuotedStatusName, value)
        End Set
    End Property

    Dim fQuotationId As Integer
    Public Property QuotationId() As Integer
        Get
            Return fQuotationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuotationId", fQuotationId, value)
        End Set
    End Property

    Dim fQuotationStatus As Integer
    Public Property QuotationStatus() As Integer
        Get
            Return fQuotationStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuotationStatus", fQuotationStatus, value)
        End Set
    End Property

    Dim fQuotationStatusName As String
    Public Property QuotationStatusName() As String
        Get
            Return fQuotationStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuotationStatusName", fQuotationStatusName, value)
        End Set
    End Property

    Dim fQuotationCode As String
    Public Property QuotationCode() As String
        Get
            Return fQuotationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuotationCode", fQuotationCode, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
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
