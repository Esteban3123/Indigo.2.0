Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportIncentiveAccountingWithConcepts")> _
Public Class PayrollViewReportIncentiveAccountingWithConcepts
    Inherits XPLiteObject
    Dim fIncentivePaymentDetailId As Integer
    <Key(True)> _
    Public Property IncentivePaymentDetailId() As Integer
        Get
            Return fIncentivePaymentDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncentivePaymentDetailId", fIncentivePaymentDetailId, value)
        End Set
    End Property
    Dim fIncentivePaymentId As Integer
    Public Property IncentivePaymentId() As Integer
        Get
            Return fIncentivePaymentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncentivePaymentId", fIncentivePaymentId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
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
    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property
    Dim fRetentionValue As Decimal
    Public Property RetentionValue() As Decimal
        Get
            Return fRetentionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionValue", fRetentionValue, value)
        End Set
    End Property
    Dim fTotalAccrued As Decimal
    Public Property TotalAccrued() As Decimal
        Get
            Return fTotalAccrued
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccrued", fTotalAccrued, value)
        End Set
    End Property
    Dim fTotalDeducted As Decimal
    Public Property TotalDeducted() As Decimal
        Get
            Return fTotalDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducted", fTotalDeducted, value)
        End Set
    End Property
    Dim fPaidValue As Decimal
    Public Property PaidValue() As Decimal
        Get
            Return fPaidValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaidValue", fPaidValue, value)
        End Set
    End Property
    Dim fConceptCode As String
    <Size(4)> _
    Public Property ConceptCode() As String
        Get
            Return fConceptCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptCode", fConceptCode, value)
        End Set
    End Property
    Dim fConceptName As String
    <Size(50)> _
    Public Property ConceptName() As String
        Get
            Return fConceptName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptName", fConceptName, value)
        End Set
    End Property
    Dim fValConceptAccruDeduc As Decimal
    Public Property ValConceptAccruDeduc() As Decimal
        Get
            Return fValConceptAccruDeduc
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValConceptAccruDeduc", fValConceptAccruDeduc, value)
        End Set
    End Property
    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
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
    Dim fPeriodInitialDate As DateTime
    Public Property PeriodInitialDate() As DateTime
        Get
            Return fPeriodInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodInitialDate", fPeriodInitialDate, value)
        End Set
    End Property
    Dim fPeriodEndDate As DateTime
    Public Property PeriodEndDate() As DateTime
        Get
            Return fPeriodEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodEndDate", fPeriodEndDate, value)
        End Set
    End Property
    Dim fEmployeeeId As Integer
    Public Property EmployeeeId() As Integer
        Get
            Return fEmployeeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeeId", fEmployeeeId, value)
        End Set
    End Property
    Dim fPeriod As Char
    Public Property Period() As Char
        Get
            Return fPeriod
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Period", fPeriod, value)
        End Set
    End Property

    Dim fBranchOfficeID As Integer
    Public Property BranchOfficeID() As Integer
        Get
            Return fBranchOfficeID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeID", fBranchOfficeID, value)
        End Set
    End Property



    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
