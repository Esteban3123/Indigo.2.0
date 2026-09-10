Imports DevExpress.Xpo

<Persistent("Payroll.BranchOffice")> _
Public Class PayrollBranchOffice
    Inherits XPLiteObject


#Region "Properties"
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
    <Size(3)>
    <Persistent("Code")>
    Public Property Codigo() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fCompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CompanyId", fCompanyId, value)
        End Set
    End Property
    Dim fName As String
    <Size(50)>
    <Persistent("Name")>
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fAddress As String
    <Size(80)>
    Public Property Address() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Address", fAddress, value)
        End Set
    End Property
    Dim fTelephone As String
    <Size(15)>
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
#End Region

#Region "Custom Members"
    'columna que devuelve el nit y el nombre concatenado
    <Size(53)>
    <PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
#End Region

#Region "Association"

    <Association("Maintenance_WorkOrderReferencesPayroll_PayrollBranchOffice", GetType(Maintenance_WorkOrder))>
    Public ReadOnly Property Maintenance_WorkOrder() As XPCollection(Of Maintenance_WorkOrder)
        Get
            Return GetCollection(Of Maintenance_WorkOrder)("Maintenance_WorkOrder")
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
