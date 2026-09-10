Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.PortfolioNoteConcept")> _
Public Class PortfolioPortfolioNoteConceptXpo
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
    Dim fCode As String
    <Indexed(Name:="IX_NoteConcept_1", Unique:=True)> _
    <Size(20)> _
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
    <Size(50)> _
    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
    Dim fIdAccount As MainAccountsXpo
    <Association("Portfolio_PortfolioNoteConceptReferencesGeneralLedger_MainAccounts")> _
    Public Property IdAccount() As MainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fNoteType As Integer
    <Persistent("NoteType")> _
    Public Property NoteType() As Integer
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NoteType", fNoteType, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
