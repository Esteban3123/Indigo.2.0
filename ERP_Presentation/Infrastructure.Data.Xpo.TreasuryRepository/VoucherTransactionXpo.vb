'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-06-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports System.Globalization
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.VoucherTransaction")> _
Public Class VoucherTransactionXpo
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
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("TreasuryVoucherTransactionReferencePayrollThird")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    <PersistentAlias("Concat(Code, ' - ', VoucherClass)")> _
    Public ReadOnly Property CodeClass() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeClass"))
        End Get
    End Property

    Dim fVoucherClass As Integer
    <Persistent("VoucherClass")> _
    Public Property VoucherClass() As Integer
        Get
            Return fVoucherClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherClass", fVoucherClass, value)
        End Set
    End Property
    <PersistentAlias("Iif(VoucherClass = 1, 'Pago', Iif(VoucherClass = 2, 'Reembolso',Iif(VoucherClass = 3, 'Traslado', '')))")>
    Public ReadOnly Property VoucherClassName() As String
        Get
            'Select Case fVoucherClass
            '    Case 1
            '        Return ResourceManager.GetString("VoucherClassPayment", "Treasury")
            '    Case 2
            '        Return ResourceManager.GetString("VoucherClassRefund", "Treasury")
            '    Case 3
            '        Return ResourceManager.GetString("DocumentTypeConsignmentTransfer", "Treasury")
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("VoucherClassName"))
        End Get
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

    Dim fIdEntityBankAccount As Integer
    <Persistent("IdEntityBankAccount")>
    Public Property IdEntityBankAccount() As Integer
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property

    Dim fIdCostCenter As Integer
    <Persistent("IdCostCenter")> _
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fExpenseType As Byte
    <Persistent("ExpenseType")> _
    Public Property ExpenseType() As Byte
        Get
            Return fExpenseType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ExpenseType", fExpenseType, value)
        End Set
    End Property
    Dim fPaymentMethod As Byte
    <Persistent("PaymentMethod")> _
    Public Property PaymentMethod() As Byte
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property

    Dim fNoteNumber As String
    <Persistent("NoteNumber")> _
    Public Property NoteNumber() As String
        Get
            Return fNoteNumber
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("NoteNumber", fNoteNumber, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    <Persistent("DocumentDate")> _
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fDetail As String
    <Persistent("Detail")> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fValue As Decimal
    <Persistent("Value")> _
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fStatus As Byte
    <Persistent("Status")> _
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fTaxByMilValue As Decimal
    <Persistent("TaxByMilValue")> _
    Public Property TaxByMilValue() As Decimal
        Get
            Return fTaxByMilValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxByMilValue", fTaxByMilValue, value)
        End Set
    End Property
    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', Iif(Status = 4, 'Reversado', ''))))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("Treasury_TreasuryNoteReferencesTreasury_VoucherTransaction", GetType(TreasuryNoteXpo))>
    Public ReadOnly Property Treasury_TreasuryNotes() As XPCollection(Of TreasuryNoteXpo)
        Get
            Return GetCollection(Of TreasuryNoteXpo)("Treasury_TreasuryNotes")
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceVoucherTransactionXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
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
