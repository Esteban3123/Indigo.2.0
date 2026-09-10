#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("FixedAsset.FixedAssetItem")>
Public Class FixedAssetItemReportXpo
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

    <Association("Cost_CostActivityStepFixedAsset_References_FixedAsset_FixedAssetItem", GetType(CostActivityStepFixedAssetReportXpo))>
    Public ReadOnly Property Activities() As XPCollection(Of CostActivityStepFixedAssetReportXpo)
        Get
            Return GetCollection(Of CostActivityStepFixedAssetReportXpo)("Activities")
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