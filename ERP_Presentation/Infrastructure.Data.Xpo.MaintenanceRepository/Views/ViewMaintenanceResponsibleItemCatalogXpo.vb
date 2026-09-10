Imports DevExpress.Xpo

<Persistent("Maintenance.ViewMaintenanceResponsibleItemCatalog")>
Public Class ViewMaintenanceResponsibleItemCatalogXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRowId As String
    <Key(True)>
    Public Property RowId() As String
        Get
            Return fRowId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RowId", fRowId, value)
        End Set
    End Property

    Dim fId As Integer
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

    Dim fResponsibleId As Integer
    Public Property ResponsibleId() As Integer
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Code, ' - ', Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
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
