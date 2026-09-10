Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.JustificationTemplate")> _
Partial Public Class GlosasJustificationTemplateXpo
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
    '<Indexed(Name:="IX_JustificationTemplate", Unique:=True)> _
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
    Dim fIdConcept As CommonConceptGlosas
    <Association("Glosas_JustificationTemplateReferencesCommon_ConceptGlosas")> _
    Public Property IdConcept() As CommonConceptGlosas
        Get
            Return fIdConcept
        End Get
        Set(ByVal value As CommonConceptGlosas)
            SetPropertyValue(Of CommonConceptGlosas)("IdConcept", fIdConcept, value)
        End Set
    End Property
    Dim fJustification As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Justification() As String
        Get
            Return fJustification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Justification", fJustification, value)
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
