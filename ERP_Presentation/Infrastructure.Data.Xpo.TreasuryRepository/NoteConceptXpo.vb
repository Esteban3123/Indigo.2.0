'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-04-2014
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
<Persistent("Treasury.NoteConcepts")> _
Public Class NoteConceptXpo
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
    Dim fDescription As String
    <Size(255)> _
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fAffectBudget As Boolean
    <Persistent("AffectBudget")> _
    Public Property AffectBudget() As Boolean
        Get
            Return fAffectBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectBudget", fAffectBudget, value)
        End Set
    End Property
    Dim fAutoCollect As Boolean
    <Persistent("AutoCollect")> _
    Public Property AutoCollect() As Boolean
        Get
            Return fAutoCollect
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AutoCollect", fAutoCollect, value)
        End Set
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
    Dim fNature As Byte
    <Persistent("Nature")>
    Public Property Nature() As Byte
        Get
            If fNature = 1 Then
                Return 1 ''ResourceManager.GetString("AccountNatureDebit")
            ElseIf fNature = 2 Then
                Return 2 ''ResourceManager.GetString("AccountNatureCredit")
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    <PersistentAlias("Iif(Nature = 1, 'Debito', Iif(Nature = 2, 'Credito', ''))")>
    Public ReadOnly Property NatureName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property
    <NonPersistent()>
    Public ReadOnly Property NatureValue() As String
        Get
            Return fNature
        End Get
    End Property
    Dim fIdMainAccount As PUCServiceXpo
    <Association("TreasuryNoteConceptReferencesTreasuryPUC")> _
    Public Property IdMainAccount() As PUCServiceXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    <Size(50)> _
    <PersistentAlias("concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
    Dim fAffectCashFlowConcept As Boolean
    <Persistent("AffectCashFlowConcept")>
    Public Property AffectCashFlowConcept() As Boolean
        Get
            Return fAffectCashFlowConcept
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectCashFlowConcept", fAffectCashFlowConcept, value)
        End Set
    End Property

    Dim fIdCashFlowConcept As CashFlowConceptXpo
    <Association("TreasuryNoteConceptsCashFlowConcept")>
    Public Property IdCashFlowConcept() As CashFlowConceptXpo
        Get
            Return fIdCashFlowConcept
        End Get
        Set(ByVal value As CashFlowConceptXpo)
            SetPropertyValue(Of CashFlowConceptXpo)("IdCashFlowConcept", fIdCashFlowConcept, value)
        End Set
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
