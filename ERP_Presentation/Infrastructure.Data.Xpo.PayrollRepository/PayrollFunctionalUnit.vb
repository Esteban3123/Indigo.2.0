Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.FunctionalUnit")> _
Public Class PayrollFunctionalUnit
    Inherits XPLiteObject

#Region "Members"

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
    <Indexed(Name:="IX_FunctionalUnit", Unique:=True)> _
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

    Dim fAccountingStructureId As Integer
    Public Property AccountingStructureId() As Integer
        Get
            Return fAccountingStructureId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("AccountingStructureId", fAccountingStructureId, value)
        End Set
    End Property

    Dim fBranchOfficeId As PayrollBranchOffice
    <Association("PayrollFunctionalUnitReferencesPayrollBranchOffice")> _
    Public Property BranchOfficeId() As PayrollBranchOffice
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As PayrollBranchOffice)
            SetPropertyValue(Of PayrollBranchOffice)("BranchOfficeId", fBranchOfficeId, value)
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

    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitType", fUnitType, value)
        End Set
    End Property

    ''' <summary>
    ''' Variable que se utiliza para saber que item esta
    ''' seleccionado en el form de grupos de atencion
    ''' NO BORRAR
    ''' </summary>
    ''' <remarks></remarks>
    Dim fSelectOption As Boolean
    <NonPersistent()> _
    Public Property SelectOption As Boolean
        Get
            Return fSelectOption
        End Get
        Set(value As Boolean)
            fSelectOption = value
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PayrollFunctionalUnitReferencesPayrollCostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("FunctionalUnitId", fCostCenterId, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

#End Region

#Region "Navigators"

    <Association("Payroll_Contract_References_Payroll_FunctionalUnit", GetType(PayrollContractXpo))> _
    Public ReadOnly Property PayrollContracts() As XPCollection(Of PayrollContractXpo)
        Get
            Return GetCollection(Of PayrollContractXpo)("PayrollContracts")
        End Get
    End Property

    <Association("Payroll_FunctionalUnitUserReferencesPayroll_FunctionalUnit", GetType(PayrollFunctionalUnitUserXpo))> _
    Public ReadOnly Property Payroll_FunctionalUnitUsers() As XPCollection(Of PayrollFunctionalUnitUserXpo)
        Get
            Return GetCollection(Of PayrollFunctionalUnitUserXpo)("Payroll_FunctionalUnitUsers")
        End Get
    End Property

    <Association("Payroll_FunctionalUnitResponsibleReferencesPayroll_FunctionalUnit", GetType(PayrollFunctionalUnitResponsibleXpo))>
    Public ReadOnly Property Payroll_FunctionalUnitResponsible() As XPCollection(Of PayrollFunctionalUnitResponsibleXpo)
        Get
            Return GetCollection(Of PayrollFunctionalUnitResponsibleXpo)("Payroll_FunctionalUnitResponsible")
        End Get
    End Property

#End Region

End Class
