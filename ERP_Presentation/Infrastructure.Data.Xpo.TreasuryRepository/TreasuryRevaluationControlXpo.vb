Imports DevExpress.Xpo

<Persistent("Treasury.TreasuryRevaluationControl")>
Public Class TreasuryRevaluationControlXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fMonth As Integer
    <Size(20)>
    <Persistent("Month")>
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")>
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set

    End Property

#End Region

#Region "Navigations Properties"

    <Association("TreasuryRevaluationControlXpo_References_TreasuryRevaluationXpo", GetType(TreasuryRevaluationXpo))>
    Public ReadOnly Property TreasuryRevaluationXpo() As XPCollection(Of TreasuryRevaluationXpo)
        Get
            Return GetCollection(Of TreasuryRevaluationXpo)("TreasuryRevaluationXpo")
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
