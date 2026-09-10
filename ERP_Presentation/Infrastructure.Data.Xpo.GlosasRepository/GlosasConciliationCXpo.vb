Imports DevExpress.Xpo

<Persistent("Glosas.ConciliationC")>
Partial Public Class Glosas_ConciliationCXpo
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

    Dim fConciliationConsecutive As Decimal

    Public Property ConciliationConsecutive() As Decimal
        Get
            Return fConciliationConsecutive
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ConciliationConsecutive", fConciliationConsecutive, value)
        End Set
    End Property

    Dim fNit As Integer

    Public Property Nit() As Integer
        Get
            Return fNit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Nit", fNit, value)
        End Set
    End Property

    Dim fNitName As String

    <Size(50)>
    Public Property NitName() As String
        Get
            Return fNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitName", fNitName, value)
        End Set
    End Property

    Dim fConciliationDate As DateTime

    Public Property ConciliationDate() As DateTime
        Get
            Return fConciliationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConciliationDate", fConciliationDate, value)
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

    Dim fDocumentNumber As String

    <Size(50)>
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fComment As String

    <Size(500)>
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property

    Dim fState As Char

    <Indexed(Name:="PK_ConciliationC_State")>
    Public Property State() As Char
        Get
            Return fState
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("State", fState, value)
        End Set
    End Property

    <Association("Glosas_ConciliationDReferencesGlosas_ConciliationC", GetType(Glosas_ConciliationDXpo))>
    Public ReadOnly Property Glosas_ConciliationDs() As XPCollection(Of Glosas_ConciliationDXpo)
        Get
            Return GetCollection(Of Glosas_ConciliationDXpo)("Glosas_ConciliationDs")
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