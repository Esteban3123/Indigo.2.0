Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.AgreementsD")>
Public Class PayrollAgreementsDReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fAgreementsCId As PayrollAgreementsCReportXpo
    <Association("Payroll_AgreementsDReferencesPayroll_AgreementsC")>
    Public Property AgreementsCId() As PayrollAgreementsCReportXpo
        Get
            Return fAgreementsCId
        End Get
        Set(ByVal value As PayrollAgreementsCReportXpo)
            SetPropertyValue(Of PayrollAgreementsCReportXpo)("AgreementsCId", fAgreementsCId, value)
        End Set
    End Property
    Dim fShareValuePaid As Decimal
    Public Property ShareValuePaid() As Decimal
        Get
            Return fShareValuePaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ShareValuePaid", fShareValuePaid, value)
        End Set
    End Property
    Dim fDatePayment As DateTime
    Public Property DatePayment() As DateTime
        Get
            Return fDatePayment
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DatePayment", fDatePayment, value)
        End Set
    End Property
    Dim fTypePayment As Byte
    Public Property TypePayment() As Byte
        Get
            Return fTypePayment
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypePayment", fTypePayment, value)
        End Set
    End Property

    ' Propiedad no persistente
    Dim fPayrollLiquidationDetailPayrollDate As DateTime
    <NonPersistent>
    Public Property PayrollLiquidationDetailPayrollDate() As DateTime
        Get
            Return fPayrollLiquidationDetailPayrollDate
        End Get
        Set(ByVal value As DateTime)
            fPayrollLiquidationDetailPayrollDate = value
        End Set
    End Property

    Dim fStateShare As String
    <Size(250)> _
    Public Property StateShare() As String
        Get
            Return fStateShare
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StateShare", fStateShare, value)
        End Set
    End Property
    <Association("Payroll_LiquidationDetailReferencesPayrollAgreementsDReportXpo", GetType(PayrollLiquidationDetail))> _
    Public ReadOnly Property PayrollLiquidationDetail() As XPCollection(Of PayrollLiquidationDetail)
        Get
            Return GetCollection(Of PayrollLiquidationDetail)("PayrollLiquidationDetail")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub


End Class
