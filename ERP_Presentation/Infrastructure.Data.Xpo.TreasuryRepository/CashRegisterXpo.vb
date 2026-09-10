'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
#End Region

''' <summary>
''' ciudades bancarias usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CashRegisters")> _
Public Class CashRegisterXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fType As String
    <Persistent("Type")> _
    Public Property Type() As String
        Get
            If fType = 1 Then
                Return ResourceManager.GetString("CashTypeMinor", "Treasury")
            Else
                Return ResourceManager.GetString("CashTypeMajor", "Treasury")
            End If
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property
    Dim fInitialBalance As Decimal
    <Persistent("InitialBalance")> _
    Public Property InitialBalance() As Decimal
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    <Persistent("InitialDate")> _
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fRefundDate As DateTime?
    <Persistent("RefundDate")> _
    Public Property RefundDate() As DateTime?
        Get
            Return fRefundDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("RefundDate", fRefundDate, value)
        End Set
    End Property
    Dim fAmountMax As Decimal
    <Persistent("AmountMax")> _
    Public Property AmountMax() As Decimal
        Get
            Return fAmountMax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountMax", fAmountMax, value)
        End Set
    End Property
    Dim fAmountMin As Decimal
    <Persistent("AmountMin")> _
    Public Property AmountMin() As Decimal
        Get
            Return fAmountMin
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountMin", fAmountMin, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    <Persistent("CurrentBalance")> _
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property

    Dim fIdCostCenter As CommonCostCenterXpo
    <Association("TreasuryCashRegisterReferencesCostCenter")>
    Public Property IdCostCenter() As CommonCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As CommonCostCenterXpo)
            SetPropertyValue(Of CommonCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property

    Dim fIdMainAccount As PUCServiceXpo
    <Association("TreasuryCashRegisterReferencesTreasuryPUC")>
    Public Property IdMainAccount() As PUCServiceXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return If(Convert.ToInt32(EvaluateAlias("CurrencyId")) = 0, SessionValues.Instance.OfficialCurrencyId, Convert.ToInt32(EvaluateAlias("CurrencyId")))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceCashRegisters")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    <Association("TreasuryExpConceptCRegisterRelationCashRegister", GetType(ExpenseConceptCashRegisterXpo))> _
    Public ReadOnly Property ExpenseConceptCashRegisterXpo() As XPCollection(Of ExpenseConceptCashRegisterXpo)
        Get
            Return GetCollection(Of ExpenseConceptCashRegisterXpo)("ExpenseConceptCashRegisterXpo")
        End Get
    End Property

    <Association("Treasury_RefundsReferencesTreasury_CashRegisters", GetType(RefundXpo))> _
    Public ReadOnly Property Treasury_Refundss() As XPCollection(Of RefundXpo)
        Get
            Return GetCollection(Of RefundXpo)("Treasury_Refundss")
        End Get
    End Property

    <Association("ConsitutionCashSmallerReferencesCashRegister", GetType(ConstitutionCashSmallerXpo))>
    Public ReadOnly Property ConstitutionCashSmallerXpo() As XPCollection(Of ConstitutionCashSmallerXpo)
        Get
            Return GetCollection(Of ConstitutionCashSmallerXpo)("ConstitutionCashSmallerXpo")
        End Get
    End Property

    <Association("TreasuryCashReceiptsReferenceCashRegister", GetType(CashReceiptsXpo))>
    Public ReadOnly Property CashReceipts() As XPCollection(Of CashReceiptsXpo)
        Get
            Return GetCollection(Of CashReceiptsXpo)("CashReceipts")
        End Get
    End Property

    <Association("TreasuryCashRegister_Reference_TreasuryRevaluationXpo", GetType(TreasuryRevaluationXpo))>
    Public ReadOnly Property TreasuryRevaluationXpo() As XPCollection(Of TreasuryRevaluationXpo)
        Get
            Return GetCollection(Of TreasuryRevaluationXpo)("TreasuryRevaluationXpo")
        End Get
    End Property

    Dim fCardCollectionDetails As XPCollection(Of CardCollectionDetailsXpo)
    <Association("CashReceipts-CardCollectionDetails")>
    Public ReadOnly Property CardCollectionDetails() As XPCollection(Of CardCollectionDetailsXpo)
        Get
            Return GetCollection(Of CardCollectionDetailsXpo)("CardCollectionDetails")
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
