'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PayrollRepository
' Author           : Juan P. Daza
' Created          : 2023-10-23
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region
<Persistent("Payroll.ViewBankFileIncentivePaymentConfirmated")>
Public Class BankFileIncentivePaymentConfirmXpo
    Inherits XPLiteObject

#Region "Members"

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


    Dim fIncentivePaymentId As Integer
    Public Property IncentivePaymentId() As Integer
        Get
            Return fIncentivePaymentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncentivePaymentId", fIncentivePaymentId, value)
        End Set
    End Property

    Dim fBankFileId As Integer
    Public Property BankFileId() As Integer
        Get
            Return fBankFileId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BankFileId", fBankFileId, value)
        End Set
    End Property


    Dim fPosition As String
    Public Property Position() As String
        Get
            Return fPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Position", fPosition, value)
        End Set
    End Property

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property


    Dim fEmployeeName As String
    Public Property EmployeeName() As String
        Get
            Return fEmployeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeName", fEmployeeName, value)
        End Set
    End Property

    Dim fBank As String
    Public Property Bank() As String
        Get
            Return fBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bank", fBank, value)
        End Set
    End Property

    Dim fBankAccount As String
    Public Property BankAccount() As String
        Get
            Return fBankAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccount", fBankAccount, value)
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

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fValid As Boolean
    Public Property Valid() As Boolean
        Get
            Return fValid
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Valid", fValid, value)
        End Set
    End Property

    Dim fRegisterStatus As Integer
    Public Property RegisterStatus() As Integer
        Get
            Return fRegisterStatus

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property

    Dim fPeriod As String
    Public Property Period As String
        Get
            Return fPeriod

        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Period", fPeriod, value)
        End Set
    End Property

    Dim fPeriodEndDate As DateTime
    Public Property PeriodEndDate As DateTime
        Get
            Return fPeriodEndDate

        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodEndDate", fPeriodEndDate, value)
        End Set
    End Property
    Dim fGroupName As String
    Public Property GroupName As String
        Get
            Return fGroupName

        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    Dim fFunctionalUnitName As String
    Public Property FunctionalUnitName As String
        Get
            Return fFunctionalUnitName

        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
        End Set
    End Property

    Dim fTotalAccrued As Decimal
    Public Property TotalAccrued As Decimal
        Get
            Return fTotalAccrued

        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccrued", fTotalAccrued, value)
        End Set
    End Property

    Dim fTotalDeducted As Decimal
    Public Property TotalDeducted As Decimal
        Get
            Return fTotalDeducted

        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducted", fTotalDeducted, value)
        End Set
    End Property

    Dim fBasicSalary As Decimal
    Public Property BasicSalary As Decimal
        Get
            Return fBasicSalary

        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property

    Dim fPositionId As Integer
    Public Property PositionId As Integer
        Get
            Return fPositionId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PositionId", fPositionId, value)
        End Set
    End Property

    Dim fContractId As Integer
    Public Property ContractId As Integer
        Get
            Return fContractId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fEmployeeId As Integer
    Public Property EmployeeId As Integer
        Get
            Return fEmployeeId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId As Integer
        Get
            Return fFunctionalUnitId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property

    Dim fBankId As Integer
    Public Property BankId As Integer
        Get
            Return fBankId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BankId", fBankId, value)
        End Set
    End Property

    Dim fGroupId As Integer
    Public Property GroupId As Integer
        Get
            Return fGroupId

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fEmployeeBankTypeAccount As Integer
    Public Property EmployeeBankTypeAccount As Integer
        Get
            Return EmployeeBankTypeAccount

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeBankTypeAccount", fEmployeeBankTypeAccount, value)
        End Set
    End Property

    Dim fBankFileStatus As Integer
    Public Property BankFileStatus As Integer
        Get
            Return BankFileStatus

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BankFileStatus", fBankFileStatus, value)
            If value = 2 Then
                SelectOption = 1
            ElseIf value = 1 Then
                SelectOption = 0
            End If
        End Set
    End Property

    Dim fProcess As Integer
    Public Property Process As Integer
        Get
            Return fProcess

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Process", fProcess, value)
        End Set
    End Property

    <NonPersistent>
    Public Property SelectOption As Boolean = 0


#End Region

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
End Class
