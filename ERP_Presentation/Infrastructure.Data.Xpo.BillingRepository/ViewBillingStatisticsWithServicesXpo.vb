'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewBillingStatisticsWithServices")>
Public Class ViewBillingStatisticsWithServicesXpo
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

    Dim fCareCenterDescription As String
    Public Property CareCenterDescription() As String
        Get
            Return fCareCenterDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterDescription", fCareCenterDescription, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fThirdPartyDescription As String
    Public Property ThirdPartyDescription() As String
        Get
            Return fThirdPartyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyDescription", fThirdPartyDescription, value)
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

    Dim fHealthAdministratorDescription As String
    Public Property HealthAdministratorDescription() As String
        Get
            Return fHealthAdministratorDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorDescription", fHealthAdministratorDescription, value)
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

    Dim fCareGroupDescription As String
    Public Property CareGroupDescription() As String
        Get
            Return fCareGroupDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupDescription", fCareGroupDescription, value)
        End Set
    End Property

    Dim fStatusInvoice As Integer
    Public Property StatusInvoice() As Integer
        Get
            Return fStatusInvoice
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StatusInvoice", fStatusInvoice, value)
        End Set
    End Property

    Dim fStatusDescription As String
    Public Property StatusDescription() As String
        Get
            Return fStatusDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusDescription", fStatusDescription, value)
        End Set
    End Property

    Dim fDocumentType As Integer
    Public Property DocumentType() As Integer
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fDocumentTypeDescription As String
    Public Property DocumentTypeDescription() As String
        Get
            Return fDocumentTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentTypeDescription", fDocumentTypeDescription, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property

    Dim fCauseIncomeDescription As String
    Public Property CauseIncomeDescription() As String
        Get
            Return fCauseIncomeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CauseIncomeDescription", fCauseIncomeDescription, value)
        End Set
    End Property

    Dim fAdmissionTypeDescription As String
    Public Property AdmissionTypeDescription() As String
        Get
            Return fAdmissionTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionTypeDescription", fAdmissionTypeDescription, value)
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

    Dim fSexDescription As String
    Public Property SexDescription() As String
        Get
            Return fSexDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SexDescription", fSexDescription, value)
        End Set
    End Property

    Dim fTotalInvoice As Decimal
    Public Property TotalInvoice() As Decimal
        Get
            Return fTotalInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalInvoice", fTotalInvoice, value)
        End Set
    End Property

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
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

    Dim fDiagnosticCode As String
    Public Property DiagnosticCode() As String
        Get
            Return fDiagnosticCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiagnosticCode", fDiagnosticCode, value)
        End Set
    End Property

    Dim fDiagnosticName As String
    Public Property DiagnosticName() As String
        Get
            Return fDiagnosticName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiagnosticName", fDiagnosticName, value)
        End Set
    End Property

    Dim fIsCutAccountDescription As String
    Public Property IsCutAccountDescription() As String
        Get
            Return fIsCutAccountDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IsCutAccountDescription", fIsCutAccountDescription, value)
        End Set
    End Property

    Dim fValueCopay As Decimal
    Public Property ValueCopay() As Decimal
        Get
            Return fValueCopay
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCopay", fValueCopay, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fUserName As String
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserName", fUserName, value)
        End Set
    End Property

    Dim fUserDescription As String
    Public Property UserDescription() As String
        Get
            Return fUserDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserDescription", fUserDescription, value)
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

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
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

    Dim fEntityValue As Decimal
    Public Property EntityValue() As Decimal
        Get
            Return fEntityValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("EntityValue", fEntityValue, value)
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

    Dim fPatientDescription As String
    Public Property PatientDescription() As String
        Get
            Return fPatientDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientDescription", fPatientDescription, value)
        End Set
    End Property

    Dim fPerformsHealthProfessionalCode As String
    Public Property PerformsHealthProfessionalCode() As String
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
        End Set
    End Property

    Dim fProfessionalDescription As String
    Public Property ProfessionalDescription() As String
        Get
            Return fProfessionalDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalDescription", fProfessionalDescription, value)
        End Set
    End Property

    Dim fIPSServiceId As Integer
    Public Property IPSServiceId() As Integer
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fIPSServiceCode As String
    Public Property IPSServiceCode() As String
        Get
            Return fIPSServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceCode", fIPSServiceCode, value)
        End Set
    End Property

    Dim fIPSServiceName As String
    Public Property IPSServiceName() As String
        Get
            Return fIPSServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceName", fIPSServiceName, value)
        End Set
    End Property

    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fDescriptionIPSServiceOrProduct As String
    Public Property DescriptionIPSServiceOrProduct() As String
        Get
            Return fDescriptionIPSServiceOrProduct
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionIPSServiceOrProduct", fDescriptionIPSServiceOrProduct, value)
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

    Dim fServiceOrderCode As String
    Public Property ServiceOrderCode() As String
        Get
            Return fServiceOrderCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceOrderCode", fServiceOrderCode, value)
        End Set
    End Property

    Dim fIdentificationTypeDescription As String
    Public Property IdentificationTypeDescription() As String
        Get
            Return fIdentificationTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationTypeDescription", fIdentificationTypeDescription, value)
        End Set
    End Property

    Dim fPatientIdentification As String
    Public Property PatientIdentification() As String
        Get
            Return fPatientIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientIdentification", fPatientIdentification, value)
        End Set
    End Property

    Dim fPatientBirth As DateTime
    Public Property PatientBirth() As DateTime
        Get
            Return fPatientBirth
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PatientBirth", fPatientBirth, value)
        End Set
    End Property

    Dim fPatientAdress As String
    Public Property PatientAdress() As String
        Get
            Return fPatientAdress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAdress", fPatientAdress, value)
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

    Dim fCupsEntityId As Integer
    Public Property CupsEntityId() As Integer
        Get
            Return fCupsEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CupsEntityId", fCupsEntityId, value)
        End Set
    End Property

    Dim fCupsEntityCode As String
    Public Property CupsEntityCode() As String
        Get
            Return fCupsEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityCode", fCupsEntityCode, value)
        End Set
    End Property

    Dim fCupsEntityName As String
    Public Property CupsEntityName() As String
        Get
            Return fCupsEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityName", fCupsEntityName, value)
        End Set
    End Property

    Dim fPresentationDescription As String
    Public Property PresentationDescription() As String
        Get
            Return fPresentationDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PresentationDescription", fPresentationDescription, value)
        End Set
    End Property

    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
        End Set
    End Property

    Dim fProfessionalName As String
    Public Property ProfessionalName() As String
        Get
            Return fProfessionalName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalName", fProfessionalName, value)
        End Set
    End Property

    Dim fOrderDate As DateTime
    Public Property OrderDate() As DateTime
        Get
            Return fOrderDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("OrderDate", fOrderDate, value)
        End Set
    End Property

    Dim fApplyProcedure As String
    Public Property ApplyProcedure() As String
        Get
            Return fApplyProcedure
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApplyProcedure", fApplyProcedure, value)
        End Set
    End Property

    Dim fBillingGroupCode As String
    Public Property BillingGroupCode() As String
        Get
            Return fBillingGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroupCode", fBillingGroupCode, value)
        End Set
    End Property

    Dim fBillingGroupName As String
    Public Property BillingGroupName() As String
        Get
            Return fBillingGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroupName", fBillingGroupName, value)
        End Set
    End Property

    Dim fApplyRIASDescription As String
    Public Property ApplyRIASDescription() As String
        Get
            Return fApplyRIASDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApplyRIASDescription", fApplyRIASDescription, value)
        End Set
    End Property

    Dim fRIASCode As String
    Public Property RIASCode() As String
        Get
            Return fRIASCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIASCode", fRIASCode, value)
        End Set
    End Property

    Dim fRIASName As String
    Public Property RIASName() As String
        Get
            Return fRIASName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIASName", fRIASName, value)
        End Set
    End Property

    Dim fServiceOrProduct As String
    Public Property ServiceOrProduct() As String
        Get
            Return fServiceOrProduct
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceOrProduct", fServiceOrProduct, value)
        End Set
    End Property

    Dim fCIE10 As String
    Public Property CIE10() As String
        Get
            Return fCIE10
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CIE10", fCIE10, value)
        End Set
    End Property

    Dim fPatientAge As Integer
    Public Property PatientAge() As Integer
        Get
            Return fPatientAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientAge", fPatientAge, value)
        End Set
    End Property

    Dim fEntityTypeDescription As String
    Public Property EntityTypeDescription() As String
        Get
            Return fEntityTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityTypeDescription", fEntityTypeDescription, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property
    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property
    Dim fInvoiceCategory As String
    Public Property InvoiceCategory() As String
        Get
            Return fInvoiceCategory
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceCategory", fInvoiceCategory, value)
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
