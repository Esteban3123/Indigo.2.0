Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.AccountPayableDetailConcept")> _
Public Class PaymentsAccountPayableDetailConceptXpo
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
    Dim fIdAccountPayable As PaymentsAccountPayableXpo
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesPaymentsAccountPayableXpo")> _
    Public Property IdAccountPayable() As PaymentsAccountPayableXpo
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fIdConceptAccountPayable As PaymentsAccountPayableConceptsXpo
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesPaymentsAccountPayableConceptsXpo")> _
    Public Property IdConceptAccountPayable() As PaymentsAccountPayableConceptsXpo
        Get
            Return fIdConceptAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayableConceptsXpo)
            SetPropertyValue(Of PaymentsAccountPayableConceptsXpo)("IdConceptAccountPayable", fIdConceptAccountPayable, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesCommonThirdPartyReportXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesPayrollCostCenterXpo")> _
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fBillingValue As Decimal
    Public Property BillingValue() As Decimal
        Get
            Return fBillingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillingValue", fBillingValue, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fIdRetentionConcept As Integer
    Public Property IdRetentionConcept() As Integer
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(500)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fDeferredCausation As Boolean
    Public Property DeferredCausation() As Boolean
        Get
            Return fDeferredCausation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DeferredCausation", fDeferredCausation, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
