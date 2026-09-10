Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.MainAccountLevels")> _
Public Class GeneralLedgerMainAccountsLevelsXpo
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

    Dim fCode As String
    <Size(3)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(80)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fLevel As Integer
    '<Indexed("IX_AccountLevel_Code")> _
    Public Property Level() As Integer
        Get
            Return fLevel
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Level", fLevel, value)
        End Set
    End Property
    Dim fLength As Integer
    Public Property Length() As Integer
        Get
            Return fLength
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Length", fLength, value)
        End Set
    End Property
    Dim fdigits As Integer
    Public Property digits() As Integer
        Get
            Return fdigits
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("digits", fdigits, value)
        End Set
    End Property
    <Association("MainAccountsXpoReferencesGeneralLedgerMainAccountsLevelsXpo", GetType(MainAccountsXpo))> _
    Public ReadOnly Property MainAccountsXpoCollection() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("MainAccountsXpoCollection")
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
