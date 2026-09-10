'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Conceptos de recibos de caja usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CashReceiptConcepts")> _
Public Class CashReceiptConceptXpo
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
    Dim fAffectation As Byte
    <Persistent("Affectation")> _
    Public Property Affectation() As Byte
        Get
            'Select Case fAffectation
            '    Case 1
            '        fAffectation = ResourceManager.GetString("AffectPortfoNone", "Treasury")
            '    Case 2
            '        fAffectation = ResourceManager.GetString("AffectsPortfolio", "Treasury")
            '    Case 3
            '        fAffectation = ResourceManager.GetString("RepaymentAdvances", "Treasury")
            'End Select
            Return fAffectation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Affectation", fAffectation, value)
        End Set
    End Property

    <PersistentAlias("Iif(Affectation = 1, 'Ninguno', Iif(Affectation = 2, 'Cancelación / Abonos Facturas CxC',Iif(Affectation = 3, 'Reintegro de Anticipos a Proveedores', Iif(Affectation = 4, 'Reintegro de Cuentas Por Pagar', ''))))")>
    Public ReadOnly Property AffectationName As String
        Get
            'Select Case fAffectation
            '    Case 1
            '        Return ResourceManager.GetString("AffectPortfoNone", "Treasury")
            '    Case 2
            '        Return ResourceManager.GetString("AffectsPortfolio", "Treasury")
            '    Case 3
            '        Return ResourceManager.GetString("RepaymentAdvances", "Treasury")
            '    Case 4
            '        Return "Reintegro de Cuentas Por Pagar"
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("AffectationName"))
        End Get
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
    Dim fDiscount As Boolean
    <Persistent("Discount")> _
    Public Property Discount() As Boolean
        Get
            Return fDiscount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Discount", fDiscount, value)
        End Set
    End Property
    Dim fNature As String
    <Persistent("Nature")> _
    Public Property Nature() As String
        Get
            If fNature = 1 Then
                Return ResourceManager.GetString("AccountNatureDebit")
            Else
                Return ResourceManager.GetString("AccountNatureCredit")
            End If
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nature", fNature, value)
        End Set
    End Property


    Dim fIdMainAccount As PUCServiceXpo
    <Association("TreasuryCashReceiptConceptRelationPUC")> _
    Public Property IdMainAccount() As PUCServiceXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdMainAccount", fIdMainAccount, value)
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
    <Association("TreasuryCashReceiptConceptCashFlowConcept")>
    Public Property IdCashFlowConcept() As CashFlowConceptXpo
        Get
            Return fIdCashFlowConcept
        End Get
        Set(ByVal value As CashFlowConceptXpo)
            SetPropertyValue(Of CashFlowConceptXpo)("IdCashFlowConcept", fIdCashFlowConcept, value)
        End Set
    End Property

    <Association("CashReceiptConceptUser_References_CashReceiptConcept", GetType(CashReceiptConceptUserXpo))>
    Public ReadOnly Property CashReceiptConceptUsersXpo() As XPCollection(Of CashReceiptConceptUserXpo)
        Get
            Return GetCollection(Of CashReceiptConceptUserXpo)("CashReceiptConceptUsersXpo")
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
