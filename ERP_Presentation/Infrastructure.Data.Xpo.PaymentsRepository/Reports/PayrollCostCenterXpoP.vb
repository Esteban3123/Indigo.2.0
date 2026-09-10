Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.CostCenter")> _
Public Class PayrollCostCenterXpoP
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesPayrollCostCenterXpoP", GetType(PaymentsAccountPayableDetailConceptXpoP))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpoP() As XPCollection(Of PaymentsAccountPayableDetailConceptXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpoP)("PaymentsAccountPayableDetailConceptXpoP")
        End Get
    End Property
    <Association("PaymentsAccountPayableReferencesPayrollCostCenterXpoP", GetType(PaymentsAccountPayable))> _
    Public ReadOnly Property PaymentsAccountPayable() As XPCollection(Of PaymentsAccountPayable)
        Get
            Return GetCollection(Of PaymentsAccountPayable)("PaymentsAccountPayable")
        End Get
    End Property
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesPayrollCostCenterXpoP", GetType(PaymentsPaymentsNoteDetailsXpo))> _
    Public ReadOnly Property PaymentsPaymentsNoteDetailsXpo() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("PaymentsPaymentsNoteDetailsXpo")
        End Get
    End Property
    <Association("PaymentsPaymentTransferDetailReferencesPayrollCostCenterXpoP", GetType(PaymentsPaymentTransferDetail))> _
    Public ReadOnly Property PaymentsPaymentTransferDetail() As XPCollection(Of PaymentsPaymentTransferDetail)
        Get
            Return GetCollection(Of PaymentsPaymentTransferDetail)("PaymentsPaymentTransferDetail")
        End Get
    End Property
    <Association("PaymentsPaymentTransferReferencesPayrollCostCenterXpoP", GetType(PaymentsPaymentTransfer))> _
    Public ReadOnly Property PaymentsPaymentTransfer() As XPCollection(Of PaymentsPaymentTransfer)
        Get
            Return GetCollection(Of PaymentsPaymentTransfer)("PaymentsPaymentTransfer")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesPayrollCostCenterXpoP", GetType(PaymentsInitialBalanceAdvanceXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAdvanceXpo() As XPCollection(Of PaymentsInitialBalanceAdvanceXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAdvanceXpo)("PaymentsInitialBalanceAdvanceXpo")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesPayrollCostCenterXpoP", GetType(PaymentsInitialBalanceAccountPayableXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAccountPayableXpo() As XPCollection(Of PaymentsInitialBalanceAccountPayableXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAccountPayableXpo)("PaymentsInitialBalanceAccountPayableXpo")
        End Get
    End Property
    <Association("PaymentsDeferredCausationXpoReferencesPayrollCostCenterXpoP", GetType(PaymentsDeferredCausationXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationXpo() As XPCollection(Of PaymentsDeferredCausationXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationXpo)("PaymentsDeferredCausationXpo")
        End Get
    End Property
    <Association("PaymentsAdvancePaymentsXpoReferencesPayrollCostCenterXpoP", GetType(PaymentsAdvancePaymentsXpo))> _
    Public ReadOnly Property PaymentsAdvancePaymentsXpo() As XPCollection(Of PaymentsAdvancePaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAdvancePaymentsXpo)("PaymentsAdvancePaymentsXpo")
        End Get
    End Property
    <Association("Payments_PaymentTransferOtherConceptReferencesPayroll_CostCenter", GetType(PaymentsPaymentTransferOtherConcept))> _
    Public ReadOnly Property PaymentsPaymentTransferOtherConcept() As XPCollection(Of PaymentsPaymentTransferOtherConcept)
        Get
            Return GetCollection(Of PaymentsPaymentTransferOtherConcept)("PaymentsPaymentTransferOtherConcept")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
