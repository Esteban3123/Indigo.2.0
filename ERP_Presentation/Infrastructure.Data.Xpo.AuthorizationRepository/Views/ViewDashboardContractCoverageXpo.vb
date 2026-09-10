'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2020
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
<Persistent("Authorization.ViewDashboardContractCoverage")>
Public Class ViewDashboardContractCoverageXpo
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

    Dim fServiceOrderDetailId As Integer
    Public Property ServiceOrderDetailId() As Integer
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property

    Dim fServiceOrderCode As String
    Public Property ServiceOrderCode() As String
        Get
            Return fServiceOrderCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceOrderCode", fServiceOrderCode, value)
        End Set
    End Property

    Dim fServiceOrderDate As DateTime
    Public Property ServiceOrderDate() As DateTime
        Get
            Return fServiceOrderDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceOrderDate", fServiceOrderDate, value)
        End Set
    End Property

    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
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

    Dim fServiceOrderStatus As Integer
    Public Property ServiceOrderStatus() As Integer
        Get
            Return fServiceOrderStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderStatus", fServiceOrderStatus, value)
        End Set
    End Property

    Dim fServiceOrderStatusName As String
    Public Property ServiceOrderStatusName() As String
        Get
            Return fServiceOrderStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceOrderStatusName", fServiceOrderStatusName, value)
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

    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
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
