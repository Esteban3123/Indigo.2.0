Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.ConceptGlosas")> _
Public Class CommonConceptGlosas
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
    <Size(3)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fNameCode As String
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),NameSpecific)")>
    Public ReadOnly Property NameCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NameCode"))
        End Get
    End Property

    Dim fNameGeneral As String
    Public Property NameGeneral() As String
        Get
            Return fNameGeneral
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameGeneral", fNameGeneral, value)
        End Set
    End Property
    Dim fApplication As String
    <Size(1000)> _
    Public Property Application() As String
        Get
            Return fApplication
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Application", fApplication, value)
        End Set
    End Property
    Dim fType As Char
    Public Property Type() As Char
        Get
            Return fType
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Type", fType, value)
        End Set
    End Property
    Dim fNameSpecific As String
    <Size(200)> _
    Public Property NameSpecific() As String
        Get
            Return fNameSpecific
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSpecific", fNameSpecific, value)
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

    <Association("Glosas_JustificationTemplateReferencesCommon_ConceptGlosas", GetType(GlosasJustificationTemplateXpo))> _
    Public ReadOnly Property Glosas_JustificationTemplates() As XPCollection(Of GlosasJustificationTemplateXpo)
        Get
            Return GetCollection(Of GlosasJustificationTemplateXpo)("Glosas_JustificationTemplates")
        End Get
    End Property
    <Association("Glosas_GlosaMovementDevolutionsReferencesCommon_ConceptGlosas", GetType(GlosaMovementDevolutionsXpo))> _
    Public ReadOnly Property Glosas_GlosaMovementDevolutionsCollection() As XPCollection
        Get
            Return GetCollection("Glosas_GlosaMovementDevolutionsCollection")
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
