Imports System
Imports DevExpress.Xpo

<Persistent("Glosas.SpecificConcept")> _
Public Class GlosasSpecificConceptsXpo
    Inherits XPLiteObject

    Dim f_Id As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return f_Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("_Id", f_Id, value)
        End Set
    End Property


    Dim f_IdGeneralConcept As Integer
    <Persistent("IdGeneralConcept")> _
    Public Property IdGeneralConcept() As Integer
        Get
            Return f_IdGeneralConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdGeneralConcept", f_IdGeneralConcept, value)
        End Set
    End Property


    Dim fCode As Integer
    <Persistent("Code")> _
     <Size(2)> _
    Public Property Codigo() As Integer
        Get
            Return fCode
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(50)> _
    <Persistent("Name")> _
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fNameCode As String
    <Size(50)> _
    <PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")>
    Public ReadOnly Property NameCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NameCode"))
        End Get
    End Property

    Dim fState As Boolean
    <Persistent("State")> _
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <Association("Glosas_JustificationTemplateReferencesGlosas_SpecificConcept", GetType(GlosasJustificationTemplateXpo))> _
    Public ReadOnly Property Glosas_JustificationTemplates() As XPCollection(Of GlosasJustificationTemplateXpo)
        Get
            Return GetCollection(Of GlosasJustificationTemplateXpo)("Glosas_JustificationTemplates")
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
