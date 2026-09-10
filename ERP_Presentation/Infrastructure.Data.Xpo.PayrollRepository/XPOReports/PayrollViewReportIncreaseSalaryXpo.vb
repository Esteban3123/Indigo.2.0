Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Payroll.ViewReportIncreaseSalary")>
Public Class PayrollViewReportIncreaseSalaryXpo
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

    Dim fIdEmployee As Integer
    Public Property IdEmployee() As Integer
        Get
            Return fIdEmployee
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEmployee", fIdEmployee, value)
        End Set
    End Property

    Dim fNitEmployee As String
    <Size(20)>
    Public Property NitEmployee() As String
        Get
            Return fNitEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitEmployee", fNitEmployee, value)
        End Set
    End Property

    Dim fNameEmployee As String
    <Size(500)>
    Public Property NameEmployee() As String
        Get
            Return fNameEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameEmployee", fNameEmployee, value)
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

    Dim fNewSalary As Decimal
    Public Property NewSalary() As Decimal
        Get
            Return fNewSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NewSalary", fNewSalary, value)
        End Set
    End Property

    Dim fPercentageIncrease As Decimal
    Public Property PercentageIncrease() As Decimal
        Get
            Return fPercentageIncrease
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageIncrease", fPercentageIncrease, value)
        End Set
    End Property

    Dim fCodeGroup As String
    <Size(20)>
    Public Property CodeGroup() As String
        Get
            Return fCodeGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeGroup", fCodeGroup, value)
        End Set
    End Property

    Dim fNameGroup As String
    <Size(200)>
    Public Property NameGroup() As String
        Get
            Return fNameGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameGroup", fNameGroup, value)
        End Set
    End Property

    Dim fCodeFunctionalUnit As String
    <Size(20)>
    Public Property CodeFunctionalUnit() As String
        Get
            Return fCodeFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeFunctionalUnit", fCodeFunctionalUnit, value)
        End Set
    End Property

    Dim fNameFunctionalUnit As String
    <Size(200)>
    Public Property NameFunctionalUnit() As String
        Get
            Return fNameFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameFunctionalUnit", fNameFunctionalUnit, value)
        End Set
    End Property

    Dim fCodePosition As String
    <Size(20)>
    Public Property CodePosition() As String
        Get
            Return fCodePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodePosition", fCodePosition, value)
        End Set
    End Property

    Dim fNamePosition As String
    <Size(200)>
    Public Property NamePosition() As String
        Get
            Return fNamePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NamePosition", fNamePosition, value)
        End Set
    End Property

    Dim fCodeBranchOffice As String
    <Size(20)>
    Public Property CodeBranchOffice() As String
        Get
            Return fCodeBranchOffice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeBranchOffice", fCodeBranchOffice, value)
        End Set
    End Property

    Dim fNameBranchOffice As String
    <Size(200)>
    Public Property NameBranchOffice() As String
        Get
            Return fNameBranchOffice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameBranchOffice", fNameBranchOffice, value)
        End Set
    End Property

    Dim fCreationDate As Date
    Public Property CreationDate() As Date
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
