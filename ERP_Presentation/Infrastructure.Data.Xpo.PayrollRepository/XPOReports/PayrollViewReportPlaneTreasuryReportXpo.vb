Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPlaneTreasury")> _
Public Class PayrollViewReportPlaneTreasuryReportXpo
    Inherits XPLiteObject
    Dim fRow As Long
    <Key(True)> _
    Public Property Row() As Long
        Get
            Return fRow
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Row", fRow, value)
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
    Dim fTotalPaid As Decimal
    Public Property TotalPaid() As Decimal
        Get
            Return fTotalPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaid", fTotalPaid, value)
        End Set
    End Property
    Dim fBankAccountNumber As String
    <Size(20)> _
    Public Property BankAccountNumber() As String
        Get
            Return fBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccountNumber", fBankAccountNumber, value)
        End Set
    End Property
    Dim fNameBank As String
    <Size(320)> _
    Public Property NameBank() As String
        Get
            Return fNameBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameBank", fNameBank, value)
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
    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
        End Set
    End Property
    Dim fLiquidationPeriod As Char
    Public Property LiquidationPeriod() As Char
        Get
            Return fLiquidationPeriod
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("LiquidationPeriod", fLiquidationPeriod, value)
        End Set
    End Property
    Dim fNameGroup As String
    <Size(150)> _
    Public Property NameGroup() As String
        Get
            Return fNameGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameGroup", fNameGroup, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
