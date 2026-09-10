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
Imports Infrastructure.CrossCutting.Resources

#End Region


<Persistent("Treasury.CashReceipts")> _
Public Class CashReceiptsXpo
    Inherits XPLiteObject

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

    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("TreasuryCashReceiptReferencePayrollThird")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    Dim fCollectType As Integer
    <Persistent("CollectType")> _
    Public Property CollectType() As Integer
        Get
            Return  fCollectType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CollectType", fCollectType, value)
        End Set
    End Property

    Dim fIdMainAccount As Integer
    <Persistent("IdMainAccount")>
    Public Property IdMainAccount() As Integer
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property

    Dim fIdBankAccount As EntityBankAccountXpo
    <Association("TreasuryCashReceiptsReferenceEntityBankAccount")>
    Public Property IdBankAccount() As EntityBankAccountXpo
        Get
            Return fIdBankAccount
        End Get
        Set(ByVal value As EntityBankAccountXpo)
            SetPropertyValue(Of EntityBankAccountXpo)("IdBankAccount", fIdBankAccount, value)
        End Set
    End Property

    Dim fIdCashRegister As CashRegisterXpo
    <Association("TreasuryCashReceiptsReferenceCashRegister")>
    Public Property IdCashRegister() As CashRegisterXpo
        Get
            Return fIdCashRegister
        End Get
        Set(value As CashRegisterXpo)
            SetPropertyValue(Of CashRegisterXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

    Dim fDocumentDate As Date
    <Persistent("DocumentDate")> _
    Public Property DocumentDate() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")> _
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fOperatingUnitId As Integer
    <Persistent("OperatingUnitId")> _
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fStatus As Byte
    <Persistent("Status")>
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(CollectType = 1, IdCashRegister.CurrencyAbbreviation, IdBankAccount.CurrencyAbbreviation)")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la descripción del tipo de recaudo basado en CollectType
    ''' </summary>
    ''' <returns>Retorna "Caja" si CollectType = 1, "Cuenta bancaria" si CollectType = 2</returns>
    <PersistentAlias("Iif(CollectType = 1, 'Caja', 'Cuenta bancaria')")>
    Public ReadOnly Property CollectionTypeDescription As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CollectionTypeDescription"))
        End Get
    End Property

    <Association("Treasury_TreasuryNoteReferencesTreasury_CashReceipts", GetType(TreasuryNoteXpo))>
    Public ReadOnly Property Treasury_TreasuryNotes() As XPCollection(Of TreasuryNoteXpo)
        Get
            Return GetCollection(Of TreasuryNoteXpo)("Treasury_TreasuryNotes")
        End Get
    End Property

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
