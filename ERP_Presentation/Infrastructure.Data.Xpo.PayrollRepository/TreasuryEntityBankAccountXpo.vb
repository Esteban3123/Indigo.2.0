Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.EntityBankAccounts")> _
Public Class TreasuryEntityBankAccountXpo
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

    Dim fNumber As String
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property

    Dim fIdBank As PayrollBankXpo
    <Association("Treasury_EntityBankAccount_References_Payroll_Bank")>
    Public Property IdBank() As PayrollBankXpo
        Get
            Return fIdBank
        End Get
        Set(ByVal value As PayrollBankXpo)
            SetPropertyValue(Of PayrollBankXpo)("IdBank", fIdBank, value)
        End Set
    End Property

#End Region

#Region "Custom Members"
    
    <PersistentAlias("Concat(IdBank.Descripcion, ' - ', Number)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Payroll_BankFile_References_Treasury_EntityBankAccountId", GetType(PayrollBankFileXpo))> _
    Public ReadOnly Property BankFiles() As XPCollection(Of PayrollBankFileXpo)
        Get
            Return GetCollection(Of PayrollBankFileXpo)("BankFiles")
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
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
