Imports DevExpress.Xpo

<Persistent("Payroll.BranchOffice")>
Public Class BranchOfficeXpo
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

#Region "Navigation Properties"

    <Association("Payroll_FunctionalUnit_References_Payroll_BranchOffice", GetType(FunctionalUnitXpo))>
    Public ReadOnly Property Payroll_FunctionalUnits() As XPCollection(Of FunctionalUnitXpo)
        Get
            Return GetCollection(Of FunctionalUnitXpo)("Payroll_FunctionalUnits")
        End Get
    End Property

#End Region

#Region "Builder"

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
