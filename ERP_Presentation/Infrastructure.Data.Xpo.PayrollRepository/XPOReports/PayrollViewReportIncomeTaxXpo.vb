Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportIncomeTax")>
Public Class PayrollViewReportIncomeTaxXpo
    Inherits XPLiteObject

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)(NameOf(Id), fId, value)
        End Set
    End Property

    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)(NameOf(PayrollDateLiquidated), fPayrollDateLiquidated, value)
        End Set
    End Property

    Dim fIdentificationNumber As String
    <Size(20)>
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)(NameOf(IdentificationNumber), fIdentificationNumber, value)
        End Set
    End Property

    Dim fEmployeeName As String
    <Size(203)>
    Public Property EmployeeName() As String
        Get
            Return fEmployeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)(NameOf(EmployeeName), fEmployeeName, value)
        End Set
    End Property

    Dim fTotalBaseRetention As Decimal
    Public Property TotalBaseRetention() As Decimal
        Get
            Return fTotalBaseRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)(NameOf(TotalBaseRetention), fTotalBaseRetention, value)
        End Set
    End Property

    Dim fConceptRentPaid As Decimal
    Public Property ConceptRentPaid() As Decimal
        Get
            Return fConceptRentPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)(NameOf(ConceptRentPaid), fConceptRentPaid, value)
        End Set
    End Property

    Dim fConceptWithholding As Decimal
    Public Property ConceptWithholding() As Decimal
        Get
            Return fConceptWithholding
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)(NameOf(ConceptWithholding), fConceptWithholding, value)
        End Set
    End Property

    Dim fSupplementaryPension As Decimal
    Public Property SupplementaryPension() As Decimal
        Get
            Return fSupplementaryPension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)(NameOf(SupplementaryPension), fSupplementaryPension, value)
        End Set
    End Property

    Dim fTaxCredits As Decimal
    Public Property TaxCredits() As Decimal
        Get
            Return fTaxCredits
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)(NameOf(TaxCredits), fTaxCredits, value)
        End Set
    End Property

    Dim fCode As String
    <Size(2)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)(NameOf(Code), fCode, value)
        End Set
    End Property

#End Region

End Class
