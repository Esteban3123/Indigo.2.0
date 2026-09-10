Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ViewReportPaymentPlan")> _
Public Class BudgetViewReportPaymentPlanReportXpo
    Inherits XPLiteObject
    Dim fAutoIncrementing As Long
    <Key(True)> _
    Public Property AutoIncrementing() As Long
        Get
            Return fAutoIncrementing
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("AutoIncrementing", fAutoIncrementing, value)
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
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fspendingCategoryName As String
    <Size(300)> _
    Public Property spendingCategoryName() As String
        Get
            Return fspendingCategoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("spendingCategoryName", fspendingCategoryName, value)
        End Set
    End Property
    Dim favailabilityNumber As String
    <Size(20)> _
    Public Property availabilityNumber() As String
        Get
            Return favailabilityNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("availabilityNumber", favailabilityNumber, value)
        End Set
    End Property
    Dim fnumberCommitment As String
    <Size(20)> _
    Public Property numberCommitment() As String
        Get
            Return fnumberCommitment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("numberCommitment", fnumberCommitment, value)
        End Set
    End Property
    Dim fobligationNumber As String
    <Size(20)> _
    Public Property obligationNumber() As String
        Get
            Return fobligationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("obligationNumber", fobligationNumber, value)
        End Set
    End Property
    Dim fpaymentNumber As String
    Public Property paymentNumber() As String
        Get
            Return fpaymentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("paymentNumber", fpaymentNumber, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(5000)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fPaymentDate As DateTime
    <Persistent("PaymentDate")> _
    Public Property PaymentDate() As DateTime
        Get
            Return fPaymentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PaymentDate", fPaymentDate, value)
        End Set
    End Property
    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property
    Dim fcommitmentValue As Decimal
    Public Property commitmentValue() As Decimal
        Get
            Return fcommitmentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("commitmentValue", fcommitmentValue, value)
        End Set
    End Property
    Dim fobligationValue As Decimal
    Public Property obligationValue() As Decimal
        Get
            Return fobligationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("obligationValue", fobligationValue, value)
        End Set
    End Property
    Dim fTotalPaymentOrder As Decimal
    Public Property TotalPaymentOrder() As Decimal
        Get
            Return fTotalPaymentOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaymentOrder", fTotalPaymentOrder, value)
        End Set
    End Property
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fValidity As Integer
    Public Property Validity() As Integer
        Get
            Return fValidity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Validity", fValidity, value)
        End Set
    End Property
    Dim fCategory As String
    <Size(20)> _
    Public Property Category() As String
        Get
            Return fCategory
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Category", fCategory, value)
        End Set
    End Property
    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
