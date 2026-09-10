#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

<Persistent("Payroll.BankFile")>
Public Class PayrollBankFileXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fCompanyId As PayrollCompanyXpo
    <Association("Payroll_BankFile_References_Payroll_Company")> _
    Public Property CompanyId() As PayrollCompanyXpo
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As PayrollCompanyXpo)
            SetPropertyValue(Of PayrollCompanyXpo)("CompanyId", fCompanyId, value)
        End Set
    End Property

    Dim fLiquidationDate As DateTime
    Public Property LiquidationDate() As DateTime
        Get
            Return fLiquidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LiquidationDate", fLiquidationDate, value)
        End Set
    End Property

    Dim fEntityBankAccountId As TreasuryEntityBankAccountXpo
    <Association("Payroll_BankFile_References_Treasury_EntityBankAccountId")>
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    <Association("Payroll_BankFileDetail_References_BankFile", GetType(PayrollBankFileDetailXpo))>
    Public ReadOnly Property PayrollBankDetail() As XPCollection(Of PayrollBankFileDetailXpo)
        Get
            Return GetCollection(Of PayrollBankFileDetailXpo)("PayrollBankDetail")
        End Get
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fProcess As Integer
    Public Property Process() As Integer
        Get
            Return fProcess
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Process", fProcess, value)
        End Set
    End Property

    Dim fPeriodIncentivePayment As Nullable(Of Byte)
    Public Property PeriodIncentivePayment() As Nullable(Of Byte)
        Get
            Return fPeriodIncentivePayment
        End Get
        Set(ByVal value As Nullable(Of Byte))
            SetPropertyValue(Of Nullable(Of Byte))("PeriodIncentivePayment", fPeriodIncentivePayment, value)
        End Set
    End Property

    Dim fYearIncentivePayment As Nullable(Of Integer)
    Public Property YearIncentivePayment() As Nullable(Of Integer)
        Get
            Return fYearIncentivePayment
        End Get
        Set(ByVal value As Nullable(Of Integer))
            SetPropertyValue(Of Nullable(Of Integer))("YearIncentivePayment", fYearIncentivePayment, value)
        End Set
    End Property


#End Region

#Region "Custom Properties"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property


    <PersistentAlias("Iif(Process = 1, 'Prima', Process = 2, 'Nómina', '')")>
    Public ReadOnly Property ProcessName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ProcessName"))
        End Get
    End Property


#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

End Class