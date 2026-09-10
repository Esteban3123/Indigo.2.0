Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportExpeditionCDPAndRPIncentivePayment")>
Public Class ViewReportExpeditionCDPAndRPIncentivePayment
    Inherits XPLiteObject
    Dim fRow As Long
    <Key(True)>
    Public Property Row() As Long
        Get
            Return fRow
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Row", fRow, value)
        End Set
    End Property
    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fName As String
    <Size(50)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fConceptTotalValue As Decimal
    Public Property ConceptTotalValue() As Decimal
        Get
            Return fConceptTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ConceptTotalValue", fConceptTotalValue, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
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

    Dim fBranchOfficeID As Integer
    Public Property BranchOfficeID() As Integer
        Get
            Return fBranchOfficeID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PayrollDateLiquidated", fBranchOfficeID, value)
        End Set
    End Property

    Dim fBranchOfficeCode As String
    Public Property BranchOfficeCode() As String
        Get
            Return fBranchOfficeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PayrollDateLiquidated", fBranchOfficeCode, value)
        End Set
    End Property

    Dim fBranchOffice As String
    Public Property BranchOffice() As String
        Get
            Return fBranchOffice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PayrollDateLiquidated", fBranchOffice, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
