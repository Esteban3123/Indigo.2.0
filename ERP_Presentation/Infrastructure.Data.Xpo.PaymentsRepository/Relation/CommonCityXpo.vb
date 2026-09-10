Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

'<Indices(@"Code;State")> _
<Persistent("Common.City")> _
Public Class CommonCityXpo
    Inherits XPLiteObject

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
    Dim fDepartamentId As CommonDepartmentXpo
    <Association("CommonCityXpoReferencesCommonDepartmentXpo")> _
    Public Property DepartamentId() As CommonDepartmentXpo
        Get
            Return fDepartamentId
        End Get
        Set(ByVal value As CommonDepartmentXpo)
            SetPropertyValue(Of CommonDepartmentXpo)("DepartamentId", fDepartamentId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed("DepartamentId", "IX_City")> _
    <Size(5)> _
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
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <Association("Maintenance_SupplierReferencesCommonCityXpo", GetType(Maintenance_Supplier))> _
    Public ReadOnly Property Maintenance_Supplier() As XPCollection(Of Maintenance_Supplier)
        Get
            Return GetCollection(Of Maintenance_Supplier)("Maintenance_Supplier")
        End Get
    End Property

    <Association("CommonPersonXpoPReferencesCommonCityXpo", GetType(CommonPersonXpoP))> _
    Public ReadOnly Property CommonPersonXpoP() As XPCollection(Of CommonPersonXpoP)
        Get
            Return GetCollection(Of CommonPersonXpoP)("CommonPersonXpoP")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
