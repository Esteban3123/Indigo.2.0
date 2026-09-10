Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableDetailConcept")> _
Public Class PaymentsAccountPayableDetailConceptXpoP
    Inherits XPLiteObject
#Region "Members"
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
    Dim fIdAccountPayable As PaymentsAccountPayable
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesPaymentsAccountPayable")> _
    Public Property IdAccountPayable() As PaymentsAccountPayable
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fIdConceptAccountPayable As PaymentsAccountPayableConceptsXpoP
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesPaymentsAccountPayableConceptsXpoP")> _
    Public Property IdConceptAccountPayable() As PaymentsAccountPayableConceptsXpoP
        Get
            Return fIdConceptAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayableConceptsXpoP)
            SetPropertyValue(Of PaymentsAccountPayableConceptsXpoP)("IdConceptAccountPayable", fIdConceptAccountPayable, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpoP
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesPayrollCostCenterXpoP")> _
    Public Property IdCostCenter() As PayrollCostCenterXpoP
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("IdCostCenter", fIdCostCenter, value)
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
    <Size(500)>
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

    Dim fIsDirectCost As Boolean
    Public Property IsDirectCost() As Boolean
        Get
            Return fIsDirectCost
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDirectCost", fIsDirectCost, value)
        End Set
    End Property

    Dim fRateIva As GeneralLedgerIVAXpo
    <Association("GeneralLedger_References_GeneralLedgerIVAXpo")>
    Public Property RateIva() As GeneralLedgerIVAXpo
        Get
            Return fRateIva
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("RateIva", fRateIva, value)
        End Set
    End Property

    Dim fIvaValue As Decimal?
    Public Property IvaValue() As Decimal?
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("IvaValue", fIvaValue, value)
        End Set
    End Property

    Dim fTotalConcept As Decimal?
    Public Property TotalConcept() As Decimal?
        Get
            Return fTotalConcept
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalConcept", fTotalConcept, value)
        End Set
    End Property
#End Region

#Region "Association"
    Dim fIdRetentionConcept As GeneralLedgerRetentionConceptsReportXpo
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesGeneralLedgerRetentionConceptsReportXpo")>
    Public Property IdRetentionConcept() As GeneralLedgerRetentionConceptsReportXpo
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As GeneralLedgerRetentionConceptsReportXpo)
            SetPropertyValue(Of GeneralLedgerRetentionConceptsReportXpo)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property

    <Association("Payments_AccountPayableDetailConceptLiquidationReferencesPayments_AccountPayableDetailConcept", GetType(PaymentsAccountPayableConceptLiquidationXpo))>
    Public ReadOnly Property PaymentsAccountPayableConceptLiquidationXpo() As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableConceptLiquidationXpo)("PaymentsAccountPayableConceptLiquidationXpo")
        End Get
    End Property
#End Region

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
