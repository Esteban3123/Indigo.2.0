Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportRetroactive")> _
Public Class PayrollViewReportAccountingRetroactiveXpo
    Inherits XPLiteObject
    Dim fid As Integer
    <Key(True)> _
    Public Property id() As Integer
        Get
            Return fid
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("id", fid, value)
        End Set
    End Property
    Dim fIdTipoEmpleado As Integer
    Public Property IdTipoEmpleado() As Integer
        Get
            Return fIdTipoEmpleado
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdTipoEmpleado", fIdTipoEmpleado, value)
        End Set
    End Property
    Dim fTipoConcepto As Byte
    Public Property TipoConcepto() As Byte
        Get
            Return fTipoConcepto
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TipoConcepto", fTipoConcepto, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fNombreEmpleado As String
    <Size(300)> _
    Public Property NombreEmpleado() As String
        Get
            Return fNombreEmpleado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreEmpleado", fNombreEmpleado, value)
        End Set
    End Property
    Dim fNombreGrupo As String
    <Size(150)> _
    Public Property NombreGrupo() As String
        Get
            Return fNombreGrupo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreGrupo", fNombreGrupo, value)
        End Set
    End Property
    Dim fEmployeeType As String
    <Size(150)> _
    Public Property EmployeeType() As String
        Get
            Return fEmployeeType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeType", fEmployeeType, value)
        End Set
    End Property
    Dim fInitialDateRetroactive As DateTime
    Public Property InitialDateRetroactive() As DateTime
        Get
            Return fInitialDateRetroactive
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDateRetroactive", fInitialDateRetroactive, value)
        End Set
    End Property
    Dim fFuncionalUnitName As String
    <Size(50)> _
    Public Property FuncionalUnitName() As String
        Get
            Return fFuncionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FuncionalUnitName", fFuncionalUnitName, value)
        End Set
    End Property
    Dim fCodeCostCenter As String
    <Size(20)> _
    Public Property CodeCostCenter() As String
        Get
            Return fCodeCostCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCostCenter", fCodeCostCenter, value)
        End Set
    End Property
    Dim fNameCostCenter As String
    <Size(200)> _
    Public Property NameCostCenter() As String
        Get
            Return fNameCostCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCostCenter", fNameCostCenter, value)
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
    Dim fConcepto As String
    <Size(57)> _
    Public Property Concepto() As String
        Get
            Return fConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concepto", fConcepto, value)
        End Set
    End Property
    Dim fValorConcepto As Decimal
    Public Property ValorConcepto() As Decimal
        Get
            Return fValorConcepto
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValorConcepto", fValorConcepto, value)
        End Set
    End Property
    Dim fNitFondoSalud As String
    <Size(20)> _
    Public Property NitFondoSalud() As String
        Get
            Return fNitFondoSalud
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitFondoSalud", fNitFondoSalud, value)
        End Set
    End Property
    Dim fNombreFondoSalud As String
    <Size(123)> _
    Public Property NombreFondoSalud() As String
        Get
            Return fNombreFondoSalud
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreFondoSalud", fNombreFondoSalud, value)
        End Set
    End Property
    Dim fNitFondoPension As String
    <Size(20)> _
    Public Property NitFondoPension() As String
        Get
            Return fNitFondoPension
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitFondoPension", fNitFondoPension, value)
        End Set
    End Property
    Dim fNombreFondoPension As String
    <Size(123)> _
    Public Property NombreFondoPension() As String
        Get
            Return fNombreFondoPension
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreFondoPension", fNombreFondoPension, value)
        End Set
    End Property
    Dim fSena As Decimal
    Public Property Sena() As Decimal
        Get
            Return fSena
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Sena", fSena, value)
        End Set
    End Property
    Dim fCompensationFundValue As Decimal
    Public Property CompensationFundValue() As Decimal
        Get
            Return fCompensationFundValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CompensationFundValue", fCompensationFundValue, value)
        End Set
    End Property
    Dim fICBF As Decimal
    Public Property ICBF() As Decimal
        Get
            Return fICBF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ICBF", fICBF, value)
        End Set
    End Property
    Dim fHealthValueEmployer As Decimal
    Public Property HealthValueEmployer() As Decimal
        Get
            Return fHealthValueEmployer
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HealthValueEmployer", fHealthValueEmployer, value)
        End Set
    End Property
    Dim fHealthValueEmployee As Decimal
    Public Property HealthValueEmployee() As Decimal
        Get
            Return fHealthValueEmployee
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HealthValueEmployee", fHealthValueEmployee, value)
        End Set
    End Property
    Dim fPensionValueEmployer As Decimal
    Public Property PensionValueEmployer() As Decimal
        Get
            Return fPensionValueEmployer
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionValueEmployer", fPensionValueEmployer, value)
        End Set
    End Property
    Dim fPensionValueEmployee As Decimal
    Public Property PensionValueEmployee() As Decimal
        Get
            Return fPensionValueEmployee
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionValueEmployee", fPensionValueEmployee, value)
        End Set
    End Property
    Dim fUnemploymentProvision As Decimal
    Public Property UnemploymentProvision() As Decimal
        Get
            Return fUnemploymentProvision
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnemploymentProvision", fUnemploymentProvision, value)
        End Set
    End Property
    Dim fUnemploymentInterestProvision As Decimal
    Public Property UnemploymentInterestProvision() As Decimal
        Get
            Return fUnemploymentInterestProvision
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnemploymentInterestProvision", fUnemploymentInterestProvision, value)
        End Set
    End Property
    Dim fUnemploymentVacationProvision As Decimal
    Public Property UnemploymentVacationProvision() As Decimal
        Get
            Return fUnemploymentVacationProvision
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnemploymentVacationProvision", fUnemploymentVacationProvision, value)
        End Set
    End Property
    Dim fIncentivePaymentProvision As Decimal
    Public Property IncentivePaymentProvision() As Decimal
        Get
            Return fIncentivePaymentProvision
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IncentivePaymentProvision", fIncentivePaymentProvision, value)
        End Set
    End Property
    Dim fARL As Decimal
    Public Property ARL() As Decimal
        Get
            Return fARL
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ARL", fARL, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Long
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Dim fGroupId As Integer
    Public Property GroupId() As Long
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("GroupId", fGroupId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
