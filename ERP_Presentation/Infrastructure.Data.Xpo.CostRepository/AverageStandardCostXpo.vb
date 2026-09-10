Imports DevExpress.Xpo

<Persistent("Cost.StandarCost")>
Public Class AverageStandardCostXpo
    Inherits XPLiteObject

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

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

    Dim fValidity As DateTime
    Public Property Validity() As DateTime
        Get
            Return fValidity
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("Validity", fValidity, value)
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

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property
#End Region

#Region "Navigations"
    <Association("Cost_StandarCostDetails_References_StandardCost", GetType(StandardCostDetailsXpo))>
    Public ReadOnly Property StandardCostDetails() As XPCollection(Of StandardCostDetailsXpo)
        Get
            Return GetCollection(Of StandardCostDetailsXpo)("StandardCostDetails")
        End Get
    End Property
#End Region

End Class
