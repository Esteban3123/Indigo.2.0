Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPayStub")> _
Public Class PayrollVPayStubReportXpo
    Inherits XPLiteObject
    Dim fPayrollDId As Integer
    <Key(True)> _
    Public Property PayrollDId() As Integer
        Get
            Return fPayrollDId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PayrollDId", fPayrollDId, value)
        End Set
    End Property
    Dim fPayrollCId As Integer
    Public Property PayrollCId() As Integer
        Get
            Return fPayrollCId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PayrollCId", fPayrollCId, value)
        End Set
    End Property
    Dim fAgreementsId As Integer
    Public Property AgreementsId() As Integer
        Get
            Return fAgreementsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AgreementsId", fAgreementsId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fConcepto As String
    <Size(3)> _
    Public Property Concepto() As String
        Get
            Return fConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concepto", fConcepto, value)
        End Set
    End Property
    Dim fNumberShares As Integer
    Public Property NumberShares() As Integer
        Get
            Return fNumberShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberShares", fNumberShares, value)
        End Set
    End Property
    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
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
    Dim fPeriodJCB As Decimal
    Public Property PeriodJCB() As Decimal
        Get
            Return fPeriodJCB
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PeriodJCB", fPeriodJCB, value)
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
    Dim fDaysWorked As Byte
    Public Property DaysWorked() As Byte
        Get
            Return fDaysWorked
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DaysWorked", fDaysWorked, value)
        End Set
    End Property
    Dim fCodigoPosition As String
    <Size(20)> _
    Public Property CodigoPosition() As String
        Get
            Return fCodigoPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoPosition", fCodigoPosition, value)
        End Set
    End Property
    Dim fNamePosition As String
    <Size(80)> _
    Public Property NamePosition() As String
        Get
            Return fNamePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NamePosition", fNamePosition, value)
        End Set
    End Property
    Dim fConceptDetail As String
    <Size(300)> _
    Public Property ConceptDetail() As String
        Get
            Return fConceptDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptDetail", fConceptDetail, value)
        End Set
    End Property
    Dim fTotalNumberHours As Integer
    Public Property TotalNumberHours() As Integer
        Get
            Return fTotalNumberHours
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalNumberHours", fTotalNumberHours, value)
        End Set
    End Property
    Dim fConceptType As Char
    Public Property ConceptType() As Char
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fConceptCode As String
    <Size(3)> _
    Public Property ConceptCode() As String
        Get
            Return fConceptCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptCode", fConceptCode, value)
        End Set
    End Property
    Dim fQuantity As Decimal
    Public Property Quantity() As Decimal
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quantity", fQuantity, value)
        End Set
    End Property
    Dim fConceptTotalValue As Decimal
    Public Property ConceptTotalValue() As Decimal
        Get
            Return fConceptTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ConceptTotalValue", fConceptTotalValue, value)
        End Set
    End Property
    Dim fTotalPaid As Decimal
    Public Property TotalPaid() As Decimal
        Get
            Return fTotalPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaid", fTotalPaid, value)
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
    Dim fRegisterStatus As Char
    Public Property RegisterStatus() As Char
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property
    Dim fGrupo As String
    <Size(171)> _
    Public Property Grupo() As String
        Get
            Return fGrupo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Grupo", fGrupo, value)
        End Set
    End Property
    Dim fCountAgree As Integer
    Public Property CountAgree() As Integer
        Get
            Return fCountAgree
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CountAgree", fCountAgree, value)
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
    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property
    Dim fVacationStartDate As DateTime
    Public Property VacationStartDate() As DateTime
        Get
            Return fVacationStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationStartDate", fVacationStartDate, value)
        End Set
    End Property
    Dim fVacationEndDate As DateTime
    Public Property VacationEndDate() As DateTime
        Get
            Return fVacationEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationEndDate", fVacationEndDate, value)
        End Set
    End Property
    Dim fTakenDays As Byte
    Public Property TakenDays() As Byte
        Get
            Return fTakenDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TakenDays", fTakenDays, value)
        End Set
    End Property

    Dim fAccruedValue As Decimal
    Public Property AccruedValue As Decimal
        Get
            Return fAccruedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccruedValue", fAccruedValue, value)
        End Set
    End Property
    Dim fDeductedValue As Decimal
    Public Property DeductedValue As Decimal
        Get
            Return fDeductedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductedValue", fDeductedValue, value)
        End Set
    End Property


    <NonPersistent>
    Public ReadOnly Property ShowBalance() As Boolean
        Get
            Return AgreementsId <> 0
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
