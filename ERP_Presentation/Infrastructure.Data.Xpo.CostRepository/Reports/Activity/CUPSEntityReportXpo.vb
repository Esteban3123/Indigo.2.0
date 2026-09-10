#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Contract.CUPSEntity")>
Public Class CUPSEntityReportXpo
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Cost_CostActivity_References_Contract_CUPSEntity", GetType(CostActivityReportXpo))>
    Public ReadOnly Property Activities() As XPCollection(Of CostActivityReportXpo)
        Get
            Return GetCollection(Of CostActivityReportXpo)("Activities")
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