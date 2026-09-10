#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivity")>
Partial Public Class CostActivityReportXpo
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

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fCUPSEntityId As CUPSEntityReportXpo
    <Association("Cost_CostActivity_References_Contract_CUPSEntity")>
    Public Property CUPSEntityId() As CUPSEntityReportXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As CUPSEntityReportXpo)
            SetPropertyValue(Of CUPSEntityReportXpo)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fCreationUser As String
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

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Me.EvaluateAlias("CodeName")
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Cost_CostActivityProductionCenter_References_Cost_CostActivity", GetType(CostActivityProductionCenterReportXpo))>
    Public ReadOnly Property CostActivityProductionCenters() As XPCollection(Of CostActivityProductionCenterReportXpo)
        Get
            Return GetCollection(Of CostActivityProductionCenterReportXpo)("CostActivityProductionCenters")
        End Get
    End Property
	
	<Association("Cost_CostActivityStep_References_Cost_CostActivity", GetType(CostActivityStepReportXpo))>
    Public ReadOnly Property CostActivitySteps() As XPCollection(Of CostActivityStepReportXpo)
        Get
            Return GetCollection(Of CostActivityStepReportXpo)("CostActivitySteps")
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
