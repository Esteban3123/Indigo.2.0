Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.OperatingUnit")> _
Public Class CommonOperatingUnitReportXpo
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
    Dim fIdUnit As CommonOperatingUnitReportXpo
    <Association("Common_OperatingUnitReferencesCommon_OperatingUnit")> _
    Public Property IdUnit() As CommonOperatingUnitReportXpo
        Get
            Return fIdUnit
        End Get
        Set(ByVal value As CommonOperatingUnitReportXpo)
            SetPropertyValue(Of CommonOperatingUnitReportXpo)("IdUnit", fIdUnit, value)
        End Set
    End Property
    Dim fUnitName As String
    Public Property UnitName() As String
        Get
            Return fUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitName", fUnitName, value)
        End Set
    End Property
    Dim fUnitCode As String
    '<Indexed(Name:="IX_OperatingUnit", Unique:=True)> _
    <Size(5)> _
    Public Property UnitCode() As String
        Get
            Return fUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitCode", fUnitCode, value)
        End Set
    End Property
    Dim fIPSCode As String
    <Size(20)> _
    Public Property IPSCode() As String
        Get
            Return fIPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSCode", fIPSCode, value)
        End Set
    End Property
    Dim fAddress As String
    Public Property Address() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Address", fAddress, value)
        End Set
    End Property
    Dim fPhone As String
    <Size(20)> _
    Public Property Phone() As String
        Get
            Return fPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Phone", fPhone, value)
        End Set
    End Property
    Dim fEmail As String
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Email", fEmail, value)
        End Set
    End Property
    Dim fEmailAudit As String
    Public Property EmailAudit() As String
        Get
            Return fEmailAudit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmailAudit", fEmailAudit, value)
        End Set
    End Property
    Dim fIdCity As Integer
    Public Property IdCity() As Integer
        Get
            Return fIdCity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCity", fIdCity, value)
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
    <Association("Common_OperatingUnitReferencesCommon_OperatingUnit", GetType(CommonOperatingUnitReportXpo))> _
    Public ReadOnly Property Common_OperatingUnitCollection() As XPCollection(Of CommonOperatingUnitReportXpo)
        Get
            Return GetCollection(Of CommonOperatingUnitReportXpo)("Common_OperatingUnitCollection")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
