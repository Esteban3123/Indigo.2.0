Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.DeferredCausationDetails")> _
Public Class PaymentsDeferredCausationDetailsXpo
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
    Dim fIdDeferredCausation As PaymentsDeferredCausationXpo
    <Association("PaymentsDeferredCausationDetailsXpoReferencesPayments_DeferredCausation")> _
    Public Property IdDeferredCausation() As PaymentsDeferredCausationXpo
        Get
            Return fIdDeferredCausation
        End Get
        Set(ByVal value As PaymentsDeferredCausationXpo)
            SetPropertyValue(Of PaymentsDeferredCausationXpo)("IdDeferredCausation", fIdDeferredCausation, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsDeferredCausationDetailsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
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
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fDateNextPeriod As DateTime
    Public Property DateNextPeriod() As DateTime
        Get
            Return fDateNextPeriod
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateNextPeriod", fDateNextPeriod, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
