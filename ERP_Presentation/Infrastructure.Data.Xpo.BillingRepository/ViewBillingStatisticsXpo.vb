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

<Persistent("Billing.ViewBillingStatistics")>
Public Class ViewBillingStatisticsXpo
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

    Dim fHighMedicalDate As DateTime
    Public Property HighMedicalDate() As DateTime
        Get
            Return fHighMedicalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HighMedicalDate", fHighMedicalDate, value)
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

    Dim fBirthDate As DateTime
    Public Property BirthDate() As DateTime
        Get
            Return fBirthDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BirthDate", fBirthDate, value)
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

    Dim fFirstName As String
    Public Property FirstName() As String
        Get
            Return fFirstName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstName", fFirstName, value)
        End Set
    End Property

    Dim fSecondName As String
    Public Property SecondName() As String
        Get
            Return fSecondName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondName", fSecondName, value)
        End Set
    End Property

    Dim fFirstLastName As String
    Public Property FirstLastName() As String
        Get
            Return fFirstLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstLastName", fFirstLastName, value)
        End Set
    End Property

    Dim fSecondLastName As String
    Public Property SecondLastName() As String
        Get
            Return fSecondLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondLastName", fSecondLastName, value)
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

    Dim fAnnulmentUser As String
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime?
    Public Property AnnulmentDate() As DateTime?
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    Dim fReversalReasonDescription As String
    Public Property ReversalReasonDescription() As String
        Get
            Return fReversalReasonDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReversalReasonDescription", fReversalReasonDescription, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
