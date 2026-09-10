Imports DevExpress.Xpo

<Persistent("Glosas.GlosaObjectionsReceptionC")>
Public Class GlosasObjectionCXpo
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

    '<Indexed("RadicatedConsecutive", Name:="IX_GlosaObjectionsReceptionC", Unique:=True)> _
    <Size(30)>
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fCustomerId As GlosasCustomerXpo

    <Association("CustomerObjectionC", GetType(GlosasCustomerXpo))>
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

    <Size(250)>
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

    Dim fDateResponsePostDocument As DateTime

    Public Property DateResponsePostDocument() As DateTime
        Get
            Return fDateResponsePostDocument
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateResponsePostDocument", fDateResponsePostDocument, value)
        End Set
    End Property

    Dim fDateRadicatedDocumentReply As DateTime

    Public Property DateRadicatedDocumentReply() As DateTime
        Get
            Return fDateRadicatedDocumentReply
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateRadicatedDocumentReply", fDateRadicatedDocumentReply, value)
        End Set
    End Property

    Dim fReceivesTheSettled As String

    <Size(200)>
    Public Property ReceivesTheSettled() As String
        Get
            Return fReceivesTheSettled
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivesTheSettled", fReceivesTheSettled, value)
        End Set
    End Property

    Dim fDocumentCommentRadicated As String

    <Size(500)>
    Public Property DocumentCommentRadicated() As String
        Get
            Return fDocumentCommentRadicated
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentCommentRadicated", fDocumentCommentRadicated, value)
        End Set
    End Property

    <Association("Glosas_GlosaObjectionsReceptionDReferencesGlosas_GlosaObjectionsReceptionC", GetType(GlosasObjectionDXpo))>
    Public ReadOnly Property Glosas_GlosaObjectionsReceptionDs() As XPCollection(Of GlosasObjectionDXpo)
        Get
            Return GetCollection(Of GlosasObjectionDXpo)("Glosas_GlosaObjectionsReceptionDs")
        End Get
    End Property

#End Region

#Region "Custom Members"

    <NonPersistent>
    Public Property StateOperation As Byte

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