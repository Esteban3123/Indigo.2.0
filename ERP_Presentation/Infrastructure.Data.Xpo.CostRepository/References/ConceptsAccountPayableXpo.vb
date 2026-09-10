'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de pago usado en los servicios Xpo
''' </summary>
<Persistent("Payments.AccountPayableConcepts")> _
Public Class ConceptsAccountPayableXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fDeferredCausation As Boolean
    <Persistent("DeferredCausation")> _
    Public Property DeferredCausation() As Boolean
        Get
            Return fDeferredCausation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DeferredCausation", fDeferredCausation, value)
        End Set
    End Property

    Dim fHandlesRetention As Boolean
    <Persistent("HandlesRetention")> _
    Public Property HandlesRetention() As Boolean
        Get
            Return fHandlesRetention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesRetention", fHandlesRetention, value)
        End Set
    End Property

    <PersistentAlias("iif(HandlesRetention, 'Si', 'No')")> _
    Public ReadOnly Property HandlesRetentionName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("HandlesRetentionName"))
        End Get
    End Property

    Dim fEmployeeCategoryRetention As Boolean
    <Persistent("EmployeeCategoryRetention")> _
    Public Property EmployeeCategoryRetention() As Boolean
        Get
            Return fEmployeeCategoryRetention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("EmployeeCategoryRetention", fEmployeeCategoryRetention, value)
        End Set
    End Property

    Dim fConceptType As Integer
    <Persistent("ConceptType")> _
    Public Property ConceptType() As Integer
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConceptType", fConceptType, value)
        End Set
    End Property

    <PersistentAlias("iif(ConceptType=1, 'General', 'Especifico')")> _
    Public ReadOnly Property ConceptTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ConceptTypeName"))
        End Get
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("CostSettingReferencesAccountPayableConcept", GetType(CostSettingXpo))> _
    Public ReadOnly Property CostSettingXpo() As XPCollection(Of CostSettingXpo)
        Get
            Return GetCollection(Of CostSettingXpo)("CostSettingXpo")
        End Get
    End Property

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
