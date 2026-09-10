Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering


<Persistent("Payroll.Foreclousure")>
Public Class PayrollForeclousureXpo
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
    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdEmployee As PayrollEmployeeXpo
    <Association("PayrolForeclousureReferencesPayrollEmployee")>
    Public Property IdEmployee() As PayrollEmployeeXpo
        Get
            Return fIdEmployee
        End Get
        Set(ByVal value As PayrollEmployeeXpo)
            SetPropertyValue(Of PayrollEmployeeXpo)("IdEmployee", fIdEmployee, value)
        End Set
    End Property
    Dim fIdJudgment As PayrollCompanyXpo
    <Association("PayrollForeclousureReferencesPayrollCompany")>
    Public Property IdJudgment() As PayrollCompanyXpo
        Get
            Return fIdJudgment
        End Get
        Set(ByVal value As PayrollCompanyXpo)
            SetPropertyValue(Of PayrollCompanyXpo)("IdJudgment", fIdJudgment, value)
        End Set
    End Property
    Dim fIdConcept As Integer
    Public Property IdConcept() As Integer
        Get
            Return fIdConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdConcept", fIdConcept, value)
        End Set
    End Property
    Dim fComments As String
    <Size(250)>
    Public Property Comment() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComments, value)
        End Set
    End Property
    Dim fForeclousureType As Byte
    Public Property ForeclousureType() As Byte
        Get
            Return fForeclousureType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ForeclousureType", fForeclousureType, value)
        End Set
    End Property
    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property
    Dim fQuoteNumber As Integer
    Public Property QuoteNumber() As Integer
        Get
            Return fQuoteNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuoteNumber", fQuoteNumber, value)
        End Set
    End Property
    Dim fState As String
    <Size(1)>
    Public Property State() As String
        Get
            Return fState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("State", fState, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

