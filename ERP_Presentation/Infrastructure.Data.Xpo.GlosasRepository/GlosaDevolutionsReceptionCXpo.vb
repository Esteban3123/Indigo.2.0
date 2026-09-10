Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaDevolutionsReceptionC")> _
Partial Public Class GlosaDevolutionsReceptionCXpo
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
    Dim fRadicatedConsecutive As Integer
    Public Property RadicatedConsecutive() As Integer
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedConsecutive", fRadicatedConsecutive, value)
        End Set
    End Property
    Dim fDocumentNumber As String
    <Size(30)> _
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fCustomerId As GlosasCustomerXpo
    <Association("Glosas_GlosaDevolutionsReceptionCReferencesCommon_Customer")> _
    Public Property CustomerId() As GlosasCustomerXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As GlosasCustomerXpo)
            SetPropertyValue(Of GlosasCustomerXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fRadicatedDate As DateTime
    Public Property RadicatedDate() As DateTime
        Get
            Return fRadicatedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedDate", fRadicatedDate, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fComment As String
    <Size(250)> _
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property
    Dim fState As Char
    Public Property State() As Char
        Get
            Return fState
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("State", fState, value)
        End Set
    End Property
    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
        End Set
    End Property
    Dim fReceivesDevolution As String
    <Size(50)> _
    Public Property ReceivesDevolution() As String
        Get
            Return fReceivesDevolution
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivesDevolution", fReceivesDevolution, value)
        End Set
    End Property
    Dim fReceivesDevolutionPosition As String
    <Size(50)> _
    Public Property ReceivesDevolutionPosition() As String
        Get
            Return fReceivesDevolutionPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivesDevolutionPosition", fReceivesDevolutionPosition, value)
        End Set
    End Property
    Dim fPersonSends As String
    <Size(50)> _
    Public Property PersonSends() As String
        Get
            Return fPersonSends
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonSends", fPersonSends, value)
        End Set
    End Property
    Dim fPersonSendsPosition As String
    <Size(50)> _
    Public Property PersonSendsPosition() As String
        Get
            Return fPersonSendsPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonSendsPosition", fPersonSendsPosition, value)
        End Set
    End Property
    <Association("Glosas_GlosaDevolutionsReceptionDReferencesGlosas_GlosaDevolutionsReceptionC", GetType(GlosaDevolutionsReceptionDXpo))> _
    Public ReadOnly Property Glosas_GlosaDevolutionsReceptionDs() As XPCollection(Of GlosaDevolutionsReceptionDXpo)
        Get
            Return GetCollection(Of GlosaDevolutionsReceptionDXpo)("Glosas_GlosaDevolutionsReceptionDs")
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

