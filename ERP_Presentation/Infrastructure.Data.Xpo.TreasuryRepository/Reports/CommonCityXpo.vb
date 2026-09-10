Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

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
    Dim fDepartamentId As Integer
    Public Property DepartamentId() As Integer
        Get
            Return fDepartamentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepartamentId", fDepartamentId, value)
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

    <Association("TreasuryEntityBankAccountsXpoReferencesCommonCityXpo", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountsXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountsXpo")
        End Get
    End Property

    <Association("CommonSuplierReportXpoReferencesCommonCityXpo", GetType(CommonSuplierReportXpo))> _
    Public ReadOnly Property CommonSuplierReportXpo() As XPCollection(Of CommonSuplierReportXpo)
        Get
            Return GetCollection(Of CommonSuplierReportXpo)("CommonSuplierReportXpo")
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
