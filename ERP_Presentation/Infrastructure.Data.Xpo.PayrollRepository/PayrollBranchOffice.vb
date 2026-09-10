Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.BranchOffice")> _
Public Class PayrollBranchOffice
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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
    Dim fCode As String
    <Size(3)> _
    <Persistent("Code")> _
    Public Property Codigo() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fCompanyId As PayrollCompanyXpo
    <Association("PayrollBranchOfficeReferencesPayrollCompany")> _
    Public Property CompanyId() As PayrollCompanyXpo
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As PayrollCompanyXpo)
            SetPropertyValue(Of PayrollCompanyXpo)("CompanyId", fCompanyId, value)
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
    Dim fAddress As String
    <Size(80)> _
    Public Property Address() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Address", fAddress, value)
        End Set
    End Property
    Dim fTelephone As String
    <Size(15)> _
    Public Property Telephone() As String
        Get
            Return fTelephone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Telephone", fTelephone, value)
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
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <Association("PayrollFunctionalUnitReferencesPayrollBranchOffice", GetType(PayrollFunctionalUnit))>
    Public ReadOnly Property PayrollFunctionalUnit() As XPCollection(Of PayrollFunctionalUnit)
        Get
            Return GetCollection(Of PayrollFunctionalUnit)("PayrollFunctionalUnit")
        End Get
    End Property

    Private _payrollService As PayrollRepository.PayrollServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de nomina
    ''' </summary>
    Public ReadOnly Property PayrollService As PayrollRepository.PayrollServicesXpoEx
        Get
            If Me._payrollService Is Nothing Then
                _payrollService = New PayrollRepository.PayrollServicesXpoEx()
            End If
            Return Me._payrollService
        End Get
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(53)>
    <PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("ThirdPartyBranchOfficeReferencesBranchOffice", GetType(ThirdPartyBranchOfficeXpo))>
    Public ReadOnly Property ThirdPartyBranchOfficeXpo() As XPCollection(Of ThirdPartyBranchOfficeXpo)
        Get
            Return GetCollection(Of ThirdPartyBranchOfficeXpo)("ThirdPartyBranchOfficeXpo")
        End Get
    End Property

End Class
