'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' conceptos de egresos usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.ExpenseConcepts")> _
Public Class ExpenseConceptXpo
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
    Dim fNature As Byte
    <Persistent("Nature")> _
    Public Property Nature() As Byte
        Get
            Return fNature
            'If fNature = 1 Then
            '    Return ResourceManager.GetString("AccountNatureDebit")
            'Else
            '    Return ResourceManager.GetString("AccountNatureCredit")
            'End If
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    <PersistentAlias("Iif(Nature = 1, 'Débito', 'Crédito')")>
    Public ReadOnly Property NatureName() As String
        Get
            'If fNature = 1 Then
            '    Return ResourceManager.GetString("AccountNatureDebit")
            'Else
            '    Return ResourceManager.GetString("AccountNatureCredit")
            'End If
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property
    Dim fIdMainAccount As PUCServiceXpo
    <Association("TreasuryExpenseConceptReferencesPUC")> _
    Public Property IdMainAccount() As PUCServiceXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdMainAccount", fIdMainAccount, value)
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
    Dim fBehavior As Integer
    <Persistent("Behavior")> _
    Public Property Behavior() As Integer
        Get
            Return fBehavior
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Behavior", fBehavior, value)
        End Set
    End Property
    <PersistentAlias("Iif(Behavior = 1, 'Traslado entre Bancos', Iif(Behavior = 2, 'Caja Menor',Iif(Behavior = 3, 'Cancelacion/Anticipo de Facturas CxP', Iif(Behavior = 4, 'Devolutivos de Anticipos RC',Iif(Behavior = 5, 'Reembolso de Caja Menor',Iif(Behavior = 6, 'Ninguno', ''))))))")>
    Public ReadOnly Property BehaviorName As String
        Get
            'Select Case fBehavior
            '    Case 1
            '        Return ResourceManager.GetString("BehaviorTransferBetweenBanks", "Treasury")
            '    Case 2
            '        Return ResourceManager.GetString("BehaviorPettyCash", "Treasury")
            '    Case 3
            '        Return ResourceManager.GetString("BehaviorPaymentAdvancePaymentInvoices", "Treasury")
            '    Case 4
            '        Return ResourceManager.GetString("BehaviorReturningImprestRC", "Treasury")
            '    Case 5
            '        Return ResourceManager.GetString("BehaviorPettyCashReimbursement", "Treasury")
            '    Case 6
            '        Return ResourceManager.GetString("BehaviorNone", "Treasury")
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("BehaviorName"))
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
    <PersistentAlias("concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("TreasuryExpConceptCRegisterRelationExpenseConcept", GetType(ExpenseConceptCashRegisterXpo))>
    Public ReadOnly Property ExpenseConceptCashRegisterXpo() As XPCollection(Of ExpenseConceptCashRegisterXpo)
        Get
            Return GetCollection(Of ExpenseConceptCashRegisterXpo)("ExpenseConceptCashRegisterXpo")
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
    <Association("TreasuryExpenseConceptsCashFlowConcept")>
    Public Property IdCashFlowConcept() As CashFlowConceptXpo
        Get
            Return fIdCashFlowConcept
        End Get
        Set(ByVal value As CashFlowConceptXpo)
            SetPropertyValue(Of CashFlowConceptXpo)("IdCashFlowConcept", fIdCashFlowConcept, value)
        End Set
    End Property

    Dim fTaxManagement As Boolean
    <Persistent("TaxManagement")>
    Public Property TaxManagement() As Boolean
        Get
            Return fTaxManagement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TaxManagement", fTaxManagement, value)
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
