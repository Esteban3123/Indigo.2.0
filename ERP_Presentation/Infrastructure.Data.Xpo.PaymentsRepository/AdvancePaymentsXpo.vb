'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de pago usado en los servicios Xpo
''' </summary>
<Persistent("Payments.AdvancePayments")> _
Public Class AdvancePaymentsXpo
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

    Dim fDocumentDate As DateTime
    <Size(100)> _
    <Persistent("DocumentDate")> _
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Size(100)> _
    <Persistent("Value")> _
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("Value", fValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    <Size(100)> _
    <Persistent("Balance")> _
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("Balance", fBalance, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")>
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fPUC As PUCServiceXpo
    <Association("AdvancePaymentsReferencesPUC")> _
    Public Property IdAccount() As PUCServiceXpo
        Get
            Return fPUC
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdAccount", fPUC, value)
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

    Dim fIdSupplier As Maintenance_Supplier
    <Association("MaintenanceSupplierReferencesAdvancePayments")> _
    Public Property IdSupplier() As Maintenance_Supplier
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("IdSupplier", fIdSupplier, value)
        End Set
    End Property

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_AdvancePayments")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

#End Region
#Region "Association"
    <Association("PaymentsRevaluationDetailReferencesAdvancePayments", GetType(PaymentsRevaluationDetailXpo))>
    Public ReadOnly Property PaymentsRevaluationDetail() As XPCollection(Of PaymentsRevaluationDetailXpo)
        Get
            Return GetCollection(Of PaymentsRevaluationDetailXpo)("PaymentsRevaluationDetail")
        End Get
    End Property

    <Association("PaymentsTransferReferencesAdvancePayments", GetType(PaymentTransferXpo))>
    Public ReadOnly Property PaymentsTransfer() As XPCollection(Of PaymentTransferXpo)
        Get
            Return GetCollection(Of PaymentTransferXpo)("PaymentsTransfer")
        End Get
    End Property

    ''' <summary>
    ''' El XPO usado en el p reporte es con otro Xpo, por consiguiente se realiza nueva asociación
    ''' </summary>
    ''' <returns></returns>
    <Association("PaymentsPaymentTransferReferencesPaymentsAdvancePaymentsXpo", GetType(PaymentsPaymentTransfer))>
    Public ReadOnly Property PaymentsTransferReport() As XPCollection(Of PaymentsPaymentTransfer)
        Get
            Return GetCollection(Of PaymentsPaymentTransfer)("PaymentsTransferReport")
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
