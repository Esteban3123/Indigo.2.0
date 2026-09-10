Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.Company")> _
Public Class PayrollCompanyXpo
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
    <Persistent("Nit")> _
    Public Property Codigo() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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
    Dim fName As String
    <Size(50)> _
     <Persistent("Name")> _
    Public Property Descripcion() As String
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
    <Association("PayrollAgreementsCReferencesPayrollCompany", GetType(PayrollAgreementsCXpo))>
    Public ReadOnly Property PayrollAgreements() As XPCollection(Of PayrollAgreementsCXpo)
        Get
            Return GetCollection(Of PayrollAgreementsCXpo)("PayrollAgreements")
        End Get
    End Property

    <Association("PayrollForeclousureReferencesPayrollCompany", GetType(PayrollForeclousureXpo))>
    Public ReadOnly Property PayrollForeclousure() As XPCollection(Of PayrollForeclousureXpo)
        Get
            Return GetCollection(Of PayrollForeclousureXpo)("PayrollForeclousure")
        End Get
    End Property

    Dim fState As Boolean
    <Indexed(Name:="PK_Company_CodeState")> _
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <Association("PayrollBranchOfficeReferencesPayrollCompany", GetType(PayrollBranchOffice))> _
    Public ReadOnly Property PayrollBranchOffice() As XPCollection(Of PayrollBranchOffice)
        Get
            Return GetCollection(Of PayrollBranchOffice)("PayrollBranchOffice")
        End Get
    End Property

    <Association("Payroll_BankFile_References_Payroll_Company", GetType(PayrollBankFileXpo))> _
    Public ReadOnly Property PayrollBankFiles() As XPCollection(Of PayrollBankFileXpo)
        Get
            Return GetCollection(Of PayrollBankFileXpo)("PayrollBankFiles")
        End Get
    End Property

#Region "Custom Properties"

    <PersistentAlias("Concat(Codigo, ' - ', Descripcion)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
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
