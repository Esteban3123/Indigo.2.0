Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaMovementDevolutions")> _
Public Class GlosaMovementDevolutionsXpo
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
    Dim fIdDevolutionsReceptionD As GlosaDevolutionsReceptionDXpo
    <Association("Glosas_GlosaMovementDevolutionsReferencesGlosas_GlosaDevolutionsReceptionD")> _
    Public Property IdDevolutionsReceptionD() As GlosaDevolutionsReceptionDXpo
        Get
            Return fIdDevolutionsReceptionD
        End Get
        Set(ByVal value As GlosaDevolutionsReceptionDXpo)
            SetPropertyValue(Of GlosaDevolutionsReceptionDXpo)("IdDevolutionsReceptionD", fIdDevolutionsReceptionD, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fDevolutionValue As Decimal
    Public Property DevolutionValue() As Decimal
        Get
            Return fDevolutionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DevolutionValue", fDevolutionValue, value)
        End Set
    End Property
    Dim fTypeDevolution As Char
    Public Property TypeDevolution() As Char
        Get
            Return fTypeDevolution
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("TypeDevolution", fTypeDevolution, value)
        End Set
    End Property
    Dim fIdConceptGlosa As CommonConceptGlosas
    <Association("Glosas_GlosaMovementDevolutionsReferencesCommon_ConceptGlosas")> _
    Public Property IdConceptGlosa() As CommonConceptGlosas
        Get
            Return fIdConceptGlosa
        End Get
        Set(ByVal value As CommonConceptGlosas)
            SetPropertyValue(Of CommonConceptGlosas)("IdConceptGlosa", fIdConceptGlosa, value)
        End Set
    End Property
    Dim fComment As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property
    Dim fAnswer As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Answer() As String
        Get
            Return fAnswer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Answer", fAnswer, value)
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
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
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
