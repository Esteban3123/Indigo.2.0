#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Payroll.Position")>
Public Class PayrollPositionReportXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Cost_CostActivityStepPayroll_References_Payroll_Position", GetType(CostActivityStepPayrollReportXpo))>
    Public ReadOnly Property Activities() As XPCollection(Of CostActivityStepPayrollReportXpo)
        Get
            Return GetCollection(Of CostActivityStepPayrollReportXpo)("Activities")
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

#End Region

End Class