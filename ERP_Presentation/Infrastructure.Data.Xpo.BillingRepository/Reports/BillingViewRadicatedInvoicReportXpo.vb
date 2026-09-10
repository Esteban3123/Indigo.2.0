Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ViewReportRadicateInvoice")> _
Public Class BillingViewRadicatedInvoicReportXpo
    Inherits XPLiteObject
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
    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
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
    Dim fInvoiceNumber As String
    <Size(15)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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
    Dim fPatientCode As String
    <Size(15)>
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property
    Dim fPatientFullName As String
    <Size(200)>
    Public Property PatientFullName() As String
        Get
            Return fPatientFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientFullName", fPatientFullName, value)
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
    Dim fInvoiceCategoryId As Integer
    Public Property InvoiceCategoryId() As Integer
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceCategoryId", fInvoiceCategoryId, value)
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
    Dim fTotalInvoice As Decimal
    Public Property TotalInvoice() As Decimal
        Get
            Return fTotalInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalInvoice", fTotalInvoice, value)
        End Set
    End Property
    Dim fDiscount As Decimal
    Public Property Discount() As Decimal
        Get
            Return fDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Discount", fDiscount, value)
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
    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(17)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fThirdParty As String
    <Size(300)> _
    Public Property ThirdParty() As String
        Get
            Return fThirdParty
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdParty", fThirdParty, value)
        End Set
    End Property
    Dim fAdmissionNumber As String
    <Size(10)> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property
    Dim fInvoicedUser As String
    <Size(20)> _
    Public Property InvoicedUser() As String
        Get
            Return fInvoicedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicedUser", fInvoicedUser, value)
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
    Dim fRadicateInvoiceIdC As Integer
    Public Property RadicateInvoiceIdC() As Integer
        Get
            Return fRadicateInvoiceIdC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicateInvoiceIdC", fRadicateInvoiceIdC, value)
        End Set
    End Property
    Dim fInvoicedUserName As String
    Public Property InvoicedUserName() As String
        Get
            Return fInvoicedUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InvoicedUserName", fInvoicedUserName, value)
        End Set
    End Property

    Dim fNOMCENATE As String
    Public Property NOMCENATE() As String
        Get
            Return fNOMCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NOMCENATE", fNOMCENATE, value)
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


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
