Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.Address")>
Partial Public Class CommonAddressXpo
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

    Dim fIdPerson As CommonPersonXpo
    <Association("Common_Address_References_Common_Person")>
    Public Property IdPerson() As CommonPersonXpo
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As CommonPersonXpo)
            SetPropertyValue(Of CommonPersonXpo)("IdPerson", fIdPerson, value)
        End Set
    End Property

    Dim fAddress As String
    <Persistent("Addresss")>
    Public Property Address() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addresss", fAddress, value)
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

    <PersistentAlias("CONCAT(DepartmentId.Descripcion, ' - ', CityId.Descripcion, ', ', Address)")>
    Public ReadOnly Property DepartmentCityAddress() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DepartmentCityAddress"))
        End Get
    End Property

    Dim fDepartmentId As CommonDepartmentXpo
    <Association("Common_Address_References_Common_Department")>
    Public Property DepartmentId() As CommonDepartmentXpo
        Get
            Return fDepartmentId
        End Get
        Set(ByVal value As CommonDepartmentXpo)
            SetPropertyValue(Of CommonDepartmentXpo)("DepartmentId", fDepartmentId, value)
        End Set
    End Property

    Dim fCityId As CommonCityXpo
    <Association("Common_Address_References_Common_City")>
    Public Property CityId() As CommonCityXpo
        Get
            Return fCityId
        End Get
        Set(ByVal value As CommonCityXpo)
            SetPropertyValue(Of CommonCityXpo)("CityId", fCityId, value)
        End Set
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

