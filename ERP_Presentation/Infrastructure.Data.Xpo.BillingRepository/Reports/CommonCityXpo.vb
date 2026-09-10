Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

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
    <Association("Common_CityReferencesCommon_Department")> _
    Public Property DepartamentId() As CommonDepartmentXpo
        Get
            Return fDepartamentId
        End Get
        Set(ByVal value As CommonDepartmentXpo)
            SetPropertyValue(Of CommonDepartmentXpo)("DepartamentId", fDepartamentId, value)
        End Set
    End Property
    Dim fCode As String
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
    Dim fCreationUser As String
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("Common_PersonReferencesCommon_City", GetType(CommonPersonXpo))> _
    Public ReadOnly Property Common_Persons() As XPCollection(Of CommonPersonXpo)
        Get
            Return GetCollection(Of CommonPersonXpo)("Common_Persons")
        End Get
    End Property
    <Association("Common_PersonReferencesCommon_City1", GetType(CommonPersonXpo))>
    Public ReadOnly Property Common_Persons1() As XPCollection(Of CommonPersonXpo)
        Get
            Return GetCollection(Of CommonPersonXpo)("Common_Persons1")
        End Get
    End Property

    <Association("Address_References_City", GetType(CommonAddressXpo))>
    Public ReadOnly Property Addresses() As XPCollection(Of CommonAddressXpo)
        Get
            Return GetCollection(Of CommonAddressXpo)("Addresses")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
