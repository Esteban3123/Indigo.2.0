Imports DevExpress.Xpo

<Persistent("Treasury.TreasuryRevaluation")>
Public Class TreasuryRevaluationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    <PersistentAlias("TreasuryRevaluationControl.Id")>
    Public ReadOnly Property TreasuryRevaluationControlId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("TreasuryRevaluationControlId"))
        End Get
    End Property

    Dim fTreasuryRevaluationControl As TreasuryRevaluationControlXpo
    <Persistent("TreasuryRevaluationControlId")>
    <Association("TreasuryRevaluationControlXpo_References_TreasuryRevaluationXpo")>
    Public Property TreasuryRevaluationControl() As TreasuryRevaluationControlXpo
        Get
            Return fTreasuryRevaluationControl
        End Get
        Set(ByVal value As TreasuryRevaluationControlXpo)
            SetPropertyValue("TreasuryRevaluationControl", fTreasuryRevaluationControl, value)
        End Set
    End Property

    <PersistentAlias("CashRegister.Id")>
    Public ReadOnly Property CashRegisterId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CashRegisterId"))
        End Get
    End Property

    Dim fCashRegister As CashRegisterXpo
    <Persistent("CashRegisterId")>
    <Association("TreasuryCashRegister_Reference_TreasuryRevaluationXpo")>
    Public Property CashRegister() As CashRegisterXpo
        Get
            Return fCashRegister
        End Get
        Set(ByVal value As CashRegisterXpo)
            SetPropertyValue("CashRegister", fCashRegister, value)
        End Set
    End Property

    <PersistentAlias("EntityBankAccounts.Id")>
    Public ReadOnly Property BankAccountId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("BankAccountId"))
        End Get
    End Property

    Dim fEntityBankAccounts As EntityBankAccountXpo
    <Persistent("BankAccountId")>
    <Association("EntityBank_Reference_TreasuryRevaluationXpo")>
    Public Property EntityBankAccounts() As EntityBankAccountXpo
        Get
            Return fEntityBankAccounts
        End Get
        Set(ByVal value As EntityBankAccountXpo)
            SetPropertyValue("EntityBankAccounts", fEntityBankAccounts, value)
        End Set
    End Property

#End Region

#Region "Navigations Properties"
    <Association("TreasuryRevaluationXpo_References_TreasuryRevaluationDetailXpo", GetType(TreasuryRevaluationDetailXpo))>
    Public ReadOnly Property TreasuryRevaluationDetailXpo() As XPCollection(Of TreasuryRevaluationDetailXpo)
        Get
            Return GetCollection(Of TreasuryRevaluationDetailXpo)("TreasuryRevaluationDetailXpo")
        End Get
    End Property
#End Region
#Region "PersistentAlias"
    <PersistentAlias("iif(CashRegisterId is null or CashRegisterId=0,Concat('Cta. Bancaria ',EntityBankAccounts.CodeBankAccount),
                            Concat('Caja ',CashRegister.CodeName))")>
    Public ReadOnly Property CashOrBankCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CashOrBankCode"))
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
