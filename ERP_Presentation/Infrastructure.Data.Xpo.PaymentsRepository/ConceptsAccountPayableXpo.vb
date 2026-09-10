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

    'Dim fThreeEightThreeRetentionConceptId As RetentionConceptXpo
    '<Association("ThreeEightThreeReferencesRetentionConcept")> _
    'Public Property ThreeEightThreeRetentionConceptId() As RetentionConceptXpo
    '    Get
    '        Return fThreeEightThreeRetentionConceptId
    '    End Get
    '    Set(ByVal value As RetentionConceptXpo)
    '        SetPropertyValue(Of RetentionConceptXpo)("ThreeEightThreeRetentionConceptId", fThreeEightThreeRetentionConceptId, value)
    '    End Set
    'End Property

    'Dim fThreeEightFourRetentionConceptId As RetentionConceptXpo
    '<Association("ThreeEightFourReferencesRetentionConcept")> _
    'Public Property ThreeEightFourRetentionConceptId() As RetentionConceptXpo
    '    Get
    '        Return fThreeEightFourRetentionConceptId
    '    End Get
    '    Set(ByVal value As RetentionConceptXpo)
    '        SetPropertyValue(Of RetentionConceptXpo)("ThreeEightFourRetentionConceptId", fThreeEightFourRetentionConceptId, value)
    '    End Set
    'End Property

    Dim fRetentionConceptId As RetentionConceptXpo
    <Association("RetentionConceptAPCReferencesRetentionConcept")> _
    Public Property RetentionConceptId() As RetentionConceptXpo
        Get
            Return fRetentionConceptId
        End Get
        Set(ByVal value As RetentionConceptXpo)
            SetPropertyValue(Of RetentionConceptXpo)("RetentionConceptId", fRetentionConceptId, value)
        End Set
    End Property

    <PersistentAlias("RetentionConceptId.CodeName")> _
    Public ReadOnly Property RetentionConceptCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RetentionConceptCodeName"))
        End Get
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property


    Dim fHandleTaxes As Boolean
    <Persistent("HandleTaxes")>
    Public Property HandleTaxes() As Boolean
        Get
            Return fHandleTaxes
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandleTaxes", fHandleTaxes, value)
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

    Dim fPUC As PUCServiceXpo
    <Association("ConceptReferencesPUC")> _
    Public Property IdAccount() As PUCServiceXpo
        Get
            Return fPUC
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdAccount", fPUC, value)
        End Set
    End Property

    <PersistentAlias("IdAccount.NumberName")> _
    Public ReadOnly Property AccountNumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AccountNumberName"))
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
