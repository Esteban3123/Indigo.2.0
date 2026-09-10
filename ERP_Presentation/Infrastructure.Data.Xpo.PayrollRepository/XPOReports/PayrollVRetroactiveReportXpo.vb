Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VRetroactive")> _
Public Class PayrollVRetroactiveReportXpo
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
    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
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
    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fEmployeeName As String
    <Size(300)> _
    Public Property EmployeeName() As String
        Get
            Return fEmployeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeName", fEmployeeName, value)
        End Set
    End Property
    Dim fNitEmployee As String
    <Size(20)> _
    Public Property NitEmployee() As String
        Get
            Return fNitEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitEmployee", fNitEmployee, value)
        End Set
    End Property
    Dim fPosition As String
    <Size(103)> _
    Public Property Position() As String
        Get
            Return fPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Position", fPosition, value)
        End Set
    End Property
    Dim fRegisterStatus As Integer
    Public Property RegisterStatus() As Integer
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property
    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property
    Dim fPayrollDays As Integer
    Public Property PayrollDays() As Integer
        Get
            Return fPayrollDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PayrollDays", fPayrollDays, value)
        End Set
    End Property
    Dim fWorkedDays As Integer
    Public Property WorkedDays() As Integer
        Get
            Return fWorkedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkedDays", fWorkedDays, value)
        End Set
    End Property
    Dim fInabilityDays As Integer
    Public Property InabilityDays() As Integer
        Get
            Return fInabilityDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InabilityDays", fInabilityDays, value)
        End Set
    End Property
    Dim fVacationDays As Integer
    Public Property VacationDays() As Integer
        Get
            Return fVacationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VacationDays", fVacationDays, value)
        End Set
    End Property
    Dim fConceptId As Integer
    Public Property ConceptId() As Integer
        Get
            Return fConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConceptId", fConceptId, value)
        End Set
    End Property
    Dim fCodeConcept As String
    <Size(4)> _
    Public Property CodeConcept() As String
        Get
            Return fCodeConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeConcept", fCodeConcept, value)
        End Set
    End Property
    Dim fNameConcept As String
    <Size(50)> _
    Public Property NameConcept() As String
        Get
            Return fNameConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameConcept", fNameConcept, value)
        End Set
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fRecharges As Integer
    Public Property Recharges() As Integer
        Get
            Return fRecharges
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Recharges", fRecharges, value)
        End Set
    End Property
    Dim fValueConcept As Decimal
    Public Property ValueConcept() As Decimal
        Get
            Return fValueConcept
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueConcept", fValueConcept, value)
        End Set
    End Property
    Dim fRetroactiveDate As DateTime
    Public Property RetroactiveDate() As DateTime
        Get
            Return fRetroactiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RetroactiveDate", fRetroactiveDate, value)
        End Set
    End Property
    'Propiedad Añadida
    Dim fNumEmploye As Integer
    <NonPersistent()>
    Public Property NumEmploye() As Integer
        Get
            Return fNumEmploye
        End Get
        Set(ByVal value As Integer)
            Me.fNumEmploye = value
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
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
