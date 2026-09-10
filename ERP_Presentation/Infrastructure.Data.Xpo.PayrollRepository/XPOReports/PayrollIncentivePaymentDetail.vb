Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.IncentivePaymentDetail")> _
Public Class PayrollIncentivePaymentDetail
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
    Dim fIncentivePaymentId As PayrollIncentivePayment
    <Association("Payroll_IncentivePaymentDetailReferencesPayroll_IncentivePayment")> _
    Public Property IncentivePaymentId() As PayrollIncentivePayment
        Get
            Return fIncentivePaymentId
        End Get
        Set(ByVal value As PayrollIncentivePayment)
            SetPropertyValue(Of PayrollIncentivePayment)("IncentivePaymentId", fIncentivePaymentId, value)
        End Set
    End Property
    Dim fConceptId As PayrollConcept
    <Association("Payroll_IncentivePaymentDetailReferencesPayroll_Concept")> _
    Public Property ConceptId() As PayrollConcept
        Get
            Return fConceptId
        End Get
        Set(ByVal value As PayrollConcept)
            SetPropertyValue(Of PayrollConcept)("ConceptId", fConceptId, value)
        End Set
    End Property
    Dim fAccruedValue As Decimal
    Public Property AccruedValue() As Decimal
        Get
            Return fAccruedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccruedValue", fAccruedValue, value)
        End Set
    End Property
    Dim fDeductedValue As Decimal
    Public Property DeductedValue() As Decimal
        Get
            Return fDeductedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductedValue", fDeductedValue, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
