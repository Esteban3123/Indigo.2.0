'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 06-11-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' concepto de notas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CashFlowConcept")>
Public Class CashFlowConceptXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fNameConcept As String
    <Size(100)>
    <Persistent("NameConcept")>
    Public Property NameConcept() As String
        Get
            Return fNameConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameConcept", fNameConcept, value)
        End Set
    End Property

    Dim fStatusConcept As Boolean
    <Persistent("StatusConcept")>
    Public Property StatusConcept() As Boolean
        Get
            Return fStatusConcept
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StatusConcept", fStatusConcept, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    <Persistent("CreationDate")>
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    <PersistentAlias("Iif(StatusConcept, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property
    <Size(120)>
    <PersistentAlias("concat(Code,' - ',NameConcept)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
    Dim fTypeConcept As Byte
    <Persistent("TypeConcept")>
    Public Property TypeConcept() As Byte
        Get
            Return fTypeConcept
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeConcept", fTypeConcept, value)
        End Set
    End Property

    <PersistentAlias("Iif(TypeConcept = 1, 'Ingreso', TypeConcept = 2, 'Egreso', '')")>
    Public ReadOnly Property TypeConceptName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeConceptName"))
        End Get
    End Property

    Dim fActivity As Byte
    <Persistent("Activity")>
    Public Property Activity() As Byte
        Get
            Return fActivity
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Activity", fActivity, value)
        End Set
    End Property

    <PersistentAlias("Iif(Activity = 1, 'Inversión', Activity = 2, 'Operación', Activity = 3, 'Financiación','')")>
    Public ReadOnly Property ActivityName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ActivityName"))
        End Get
    End Property

    <Association("TreasuryCashReceiptConceptCashFlowConcept", GetType(CashReceiptConceptXpo))>
    Public ReadOnly Property CashReceiptConceptXpo() As XPCollection(Of CashReceiptConceptXpo)
        Get
            Return GetCollection(Of CashReceiptConceptXpo)("CashReceiptConceptXpo")
        End Get
    End Property

    <Association("TreasuryExpenseConceptsCashFlowConcept", GetType(ExpenseConceptXpo))>
    Public ReadOnly Property ExpenseConceptXpo() As XPCollection(Of ExpenseConceptXpo)
        Get
            Return GetCollection(Of ExpenseConceptXpo)("ExpenseConceptXpo")
        End Get
    End Property

    <Association("TreasuryNoteConceptsCashFlowConcept", GetType(NoteConceptXpo))>
    Public ReadOnly Property NoteConceptXpo() As XPCollection(Of NoteConceptXpo)
        Get
            Return GetCollection(Of NoteConceptXpo)("NoteConceptXpo")
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
