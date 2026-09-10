Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.Position")> _
Public Class PayrollPositionXpo
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
    <Size(80)>
    <Persistent("Name")>
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fPositionLevelId As Byte
    Public Property PositionLevelId() As Byte
        Get
            Return fPositionLevelId
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PositionLevelId", fPositionLevelId, value)
        End Set
    End Property
    Dim fMinHourAmount As Integer
    Public Property MinHourAmount() As Integer
        Get
            Return fMinHourAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MinHourAmount", fMinHourAmount, value)
        End Set
    End Property
    Dim fMaxHourAmount As Integer
    Public Property MaxHourAmount() As Integer
        Get
            Return fMaxHourAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaxHourAmount", fMaxHourAmount, value)
        End Set
    End Property
    Dim fMinBasicSalary As Integer
    Public Property MinBasicSalary() As Integer
        Get
            Return fMinBasicSalary
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MinBasicSalary", fMinBasicSalary, value)
        End Set
    End Property
    Dim fMaxBasicSalary As Integer
    Public Property MaxBasicSalary() As Integer
        Get
            Return fMaxBasicSalary
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaxBasicSalary", fMaxBasicSalary, value)
        End Set
    End Property
    Dim fProfessionalRiskLevelId As PayrollProfessionalRiskXpo
    <Association("PayrollPositionReferencesPayrollProfessionalRisk")> _
    Public Property ProfessionalRiskLevelId() As PayrollProfessionalRiskXpo
        Get
            Return fProfessionalRiskLevelId
        End Get
        Set(ByVal value As PayrollProfessionalRiskXpo)
            SetPropertyValue(Of PayrollProfessionalRiskXpo)("ProfessionalRiskLevelId", fProfessionalRiskLevelId, value)
        End Set
    End Property
    Dim fSimulation As Integer
    Public Property Simulation() As Integer
        Get
            Return fSimulation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Simulation", fSimulation, value)
        End Set
    End Property
    Dim fHandlesTurnsChart As Integer
    Public Property HandlesTurnsChart() As Integer
        Get
            Return fHandlesTurnsChart
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HandlesTurnsChart", fHandlesTurnsChart, value)
        End Set
    End Property
    Dim fNightlyChargeAuthorization As Integer
    Public Property NightlyChargeAuthorization() As Integer
        Get
            Return fNightlyChargeAuthorization
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NightlyChargeAuthorization", fNightlyChargeAuthorization, value)
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

#End Region

#Region " Custom Properties"

    <PersistentAlias("Concat(Codigo, ' - ', Descripcion)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Navigators"

    <Association("Payroll_Contract_References_Payroll_Position", GetType(PayrollContractXpo))> _
    Public ReadOnly Property PayrollContracts() As XPCollection(Of PayrollContractXpo)
        Get
            Return GetCollection(Of PayrollContractXpo)("PayrollContracts")
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
