'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.RetentionConcepts")> _
Public Class RetentionConceptXpo
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
    Dim fRetention As Integer
    <Size(1)> _
    <Persistent("Retention")> _
    Public Property Retention() As Integer
        Get
            Return fRetention
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Retention", fRetention, value)
        End Set
    End Property
    Dim fMinBase As Decimal
    <Persistent("MinBase")> _
    Public Property MinBase() As Decimal
        Get
            Return fMinBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MinBase", fMinBase, value)
        End Set
    End Property
    Dim fRate As Decimal
    <Persistent("Rate")>
    Public Property Rate() As Decimal
        Get
            Return fRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Rate", fRate, value)
        End Set
    End Property
    Dim fTypeRounding As Byte
    <Persistent("TypeRounding")>
    Public Property TypeRounding() As Byte
        Get
            Return fTypeRounding
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeRounding", fTypeRounding, value)
        End Set
    End Property
    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property
    Dim fCodeName As String
    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("ConceptReferencesRetentionConcept", GetType(ConceptsAccountPayableXpo))>
    Public ReadOnly Property ConceptsAccountPayableXpo() As XPCollection(Of ConceptsAccountPayableXpo)
        Get
            Return GetCollection(Of ConceptsAccountPayableXpo)("ConceptsAccountPayableXpo")
        End Get
    End Property

    <Association("ThreeEightThreeRetentionConceptIdRetentionConcept", GetType(ConceptsAccountPayableXpo))>
    Public ReadOnly Property ConceptsAccountPayableThreeEightThreeRetentionConceptIdXpo() As XPCollection(Of ConceptsAccountPayableXpo)
        Get
            Return GetCollection(Of ConceptsAccountPayableXpo)("ConceptsAccountPayableThreeEightThreeRetentionConceptIdXpo")
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
