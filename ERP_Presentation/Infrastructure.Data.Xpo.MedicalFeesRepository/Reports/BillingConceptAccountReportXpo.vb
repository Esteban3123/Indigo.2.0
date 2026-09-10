Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingConceptAccount")> _
Public Class BillingConceptAccountReportXpo
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
    Dim fBillingConceptId As BillingConceptReportXpo
    <Association("Billing_BillingConceptAccountReferencesBilling_BillingConcept")> _
    Public Property BillingConceptId() As BillingConceptReportXpo
        Get
            Return fBillingConceptId
        End Get
        Set(ByVal value As BillingConceptReportXpo)
            SetPropertyValue(Of BillingConceptReportXpo)("BillingConceptId", fBillingConceptId, value)
        End Set
    End Property
    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitType", fUnitType, value)
        End Set
    End Property
    Dim fEntityIncomeAccountId As Integer
    Public Property EntityIncomeAccountId() As Integer
        Get
            Return fEntityIncomeAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityIncomeAccountId", fEntityIncomeAccountId, value)
        End Set
    End Property
    Dim fIndividualIncomeAccountId As Integer
    Public Property IndividualIncomeAccountId() As Integer
        Get
            Return fIndividualIncomeAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IndividualIncomeAccountId", fIndividualIncomeAccountId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
