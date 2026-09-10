Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.VReportDeferredCausation")> _
Public Class PaymentsVReportDeferredCausationXpo
    Inherits XPLiteObject
    Dim fdeferredCausationId As Integer
    <Key(True)> _
    Public Property deferredCausationId() As Integer
        Get
            Return fdeferredCausationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("deferredCausationId", fdeferredCausationId, value)
        End Set
    End Property
    Dim fpaymentMonth As Integer
    Public Property paymentMonth() As Integer
        Get
            Return fpaymentMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("paymentMonth", fpaymentMonth, value)
        End Set
    End Property
    Dim fpaymentYear As Integer
    Public Property paymentYear() As Integer
        Get
            Return fpaymentYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("paymentYear", fpaymentYear, value)
        End Set
    End Property
    Dim faccountPayableCode As String
    <Size(20)> _
    Public Property accountPayableCode() As String
        Get
            Return faccountPayableCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("accountPayableCode", faccountPayableCode, value)
        End Set
    End Property
    Dim faccountPayableBillNumber As String
    Public Property accountPayableBillNumber() As String
        Get
            Return faccountPayableBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("accountPayableBillNumber", faccountPayableBillNumber, value)
        End Set
    End Property
    Dim fsupplierDescription As String
    <Size(318)> _
    Public Property supplierDescription() As String
        Get
            Return fsupplierDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("supplierDescription", fsupplierDescription, value)
        End Set
    End Property
    Dim fperiodMonth As String
    <Size(8)> _
    Public Property periodMonth() As String
        Get
            Return fperiodMonth
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("periodMonth", fperiodMonth, value)
        End Set
    End Property
    Dim fvaluePeriod As Decimal
    Public Property valuePeriod() As Decimal
        Get
            Return fvaluePeriod
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("valuePeriod", fvaluePeriod, value)
        End Set
    End Property
    Dim fvalueNotArmotize As Decimal
    Public Property valueNotArmotize() As Decimal
        Get
            Return fvalueNotArmotize
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("valueNotArmotize", fvalueNotArmotize, value)
        End Set
    End Property
    Dim fvalueArmotize As Decimal
    Public Property valueArmotize() As Decimal
        Get
            Return fvalueArmotize
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("valueArmotize", fvalueArmotize, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
