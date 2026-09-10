Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Address")> _
Public Class CommonAddressXpo
    Inherits XPLiteObject

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
    <Association("Common_AddressReferencesCommon_Person")>
    Public Property IdPerson() As CommonPersonXpo
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As CommonPersonXpo)
            SetPropertyValue(Of CommonPersonXpo)("IdPerson", fIdPerson, value)
        End Set
    End Property

    Dim fAddresss As String
    Public Property Addresss() As String
        Get
            Return fAddresss
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addresss", fAddresss, value)
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

    Dim fSynchronized As Char
    Public Property Synchronized() As Char
        Get
            Return fSynchronized
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Synchronized", fSynchronized, value)
        End Set
    End Property

    Dim fDepartmentId As CommonDepartmentXpo
    <Association("Address_References_Department")>
    Public Property DepartmentId() As CommonDepartmentXpo
        Get
            Return fDepartmentId
        End Get
        Set(ByVal value As CommonDepartmentXpo)
            SetPropertyValue(Of CommonDepartmentXpo)("DepartmentId", fDepartmentId, value)
        End Set
    End Property

    Dim fCityId As CommonCityXpo
    <Association("Address_References_City")>
    Public Property CityId() As CommonCityXpo
        Get
            Return fCityId
        End Get
        Set(ByVal value As CommonCityXpo)
            SetPropertyValue(Of CommonCityXpo)("CityId", fCityId, value)
        End Set
    End Property

    <PersistentAlias("concat(Trim(CityId.Name), ' - ', Trim(DepartmentId.Name))")>
    Public ReadOnly Property CityDepartment As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CityDepartment"))
        End Get
    End Property

#Region "Navigation"

    <Association("BasicBilling_References_Address", GetType(BasicBillingReportXpo))>
    Public ReadOnly Property BasicBillings() As XPCollection(Of BasicBillingReportXpo)
        Get
            Return GetCollection(Of BasicBillingReportXpo)("BasicBillings")
        End Get
    End Property

#End Region

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
