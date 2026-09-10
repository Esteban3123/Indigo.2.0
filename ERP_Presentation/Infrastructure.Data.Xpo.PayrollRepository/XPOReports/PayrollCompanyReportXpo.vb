Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Company")> _
Public Class PayrollCompanyReportXpo
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
    Dim fNit As String
    <Size(50)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdParty
    <Association("Payroll_CompanyReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdParty
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdParty)
            SetPropertyValue(Of CommonThirdParty)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fLegalRepresentative As String
    <Size(50)> _
    Public Property LegalRepresentative() As String
        Get
            Return fLegalRepresentative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LegalRepresentative", fLegalRepresentative, value)
        End Set
    End Property
    Dim fCityId As Integer
    Public Property CityId() As Integer
        Get
            Return fCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CityId", fCityId, value)
        End Set
    End Property
    Dim fPayrollType As Boolean
    Public Property PayrollType() As Boolean
        Get
            Return fPayrollType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PayrollType", fPayrollType, value)
        End Set
    End Property
    Dim fAgreementType As Boolean
    Public Property AgreementType() As Boolean
        Get
            Return fAgreementType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AgreementType", fAgreementType, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
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
    Dim fModificationUser As String
    <Size(20)> _
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
    <Association("Payroll_AgreementsCReferencesPayroll_Company", GetType(PayrollAgreementsCReportXpo))> _
    Public ReadOnly Property Payroll_AgreementsCs() As XPCollection(Of PayrollAgreementsCReportXpo)
        Get
            Return GetCollection(Of PayrollAgreementsCReportXpo)("Payroll_AgreementsCs")
        End Get
    End Property




    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
