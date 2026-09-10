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
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Payments.AccountPayable")> _
Public Class AccountPayableXpo
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

    Dim fEntityId As Integer?
    <Persistent("EntityId")>
    Public Property EntityId() As Integer?
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String
    <Persistent("EntityName")>
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fCostDistributionDirectCostId As Integer
    <Persistent("CostDistributionDirectCostId")>
    Public Property CostDistributionDirectCostId() As Integer
        Get
            Return fCostDistributionDirectCostId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostDistributionDirectCostId", fCostDistributionDirectCostId, value)
        End Set
    End Property

    Dim fPUC As PUCServiceXpo
    <Association("AccountPayableReferencesPUC")> _
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

    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("MaintenanceThidrPartyReferencesAccountPayable")>
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    <PersistentAlias("IdThirdParty.NitName")> _
    Public ReadOnly Property ThirdPartyFullName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ThirdPartyFullName"))
        End Get
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fIdSupplier As Maintenance_Supplier
    <Association("MaintenanceSupplierReferencesAccountPayable")> _
    Public Property IdSupplier() As Maintenance_Supplier
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("IdSupplier", fIdSupplier, value)
        End Set
    End Property

    Dim fIdFilingUnit As FilingUnitXpo
    <Association("FilingUnitId_Accountpayable")> _
    Public Property FilingUnitId() As FilingUnitXpo
        Get
            Return fIdFilingUnit
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("FilingUnitId", fIdFilingUnit, value)
        End Set
    End Property

    Dim fBillNumber As String
    <Size(100)> _
    <Persistent("BillNumber")> _
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
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

    Dim fBillDate As DateTime
    <Size(100)> _
    <Persistent("BillDate")> _
    Public Property BillDate() As DateTime
        Get
            Return fBillDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("BillDate", fBillDate, value)
        End Set
    End Property

    Dim fExpirationDate As DateTime
    <Size(100)> _
    <Persistent("ExpirationDate")> _
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ExpirationDate", fExpirationDate, value)
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

    Dim fIdOperatingUnit As Integer
    <Persistent("IdOperatingUnit")> _
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property

    Dim fDeductibleIva As Boolean?
    <Persistent("DeductibleIva")>
    Public Property DeductibleIva() As Boolean?
        Get
            Return fDeductibleIva
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("DeductibleIva", fDeductibleIva, value)
        End Set
    End Property
    Dim fTaxRegistration As Byte?
    <Persistent("TaxRegistration")>
    Public Property TaxRegistration() As Byte?
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("TaxRegistration", fTaxRegistration, value)
        End Set
    End Property
    Dim fInvoiceValue As Decimal
    Public Property InvoiceValue() As Decimal
        Get
            Return fInvoiceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValue", fInvoiceValue, value)
        End Set
    End Property

    <Size(100)>
    <PersistentAlias("concat([BillNumber],' - ', [Currency.Abbreviation], ' - ', [Balance])")>
    Public ReadOnly Property CodeBillNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeBillNumber"))
        End Get
    End Property

    Dim fSelectedItem As Boolean = False
    <NonPersistent()>
    Public Property SelectedItem As Boolean
        Get
            Return fSelectedItem
        End Get
        Set(value As Boolean)
            fSelectedItem = value
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get

            Return Convert.ToInt32(IIf(EvaluateAlias("CurrencyId") Is Nothing OrElse EvaluateAlias("CurrencyId") = 0,
                                       CrossCutting.Base.SessionValues.Instance.OfficialCurrencyId,
                                       EvaluateAlias("CurrencyId")))
        End Get
    End Property

    Dim fServicePeriodDate As DateTime
    <Persistent("ServicePeriodDate")>
    Public Property ServicePeriodDate() As DateTime
        Get
            Return fServicePeriodDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ServicePeriodDate", fServicePeriodDate, value)
        End Set
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property Abbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("Abbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("Abbreviation")))
        End Get
    End Property

    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),BillNumber)")>
    Public ReadOnly Property BillCurrencyBalance() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("BillCurrencyBalance"))
        End Get
    End Property
#End Region

#Region "Associations"
    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_AccountPayableXpo")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <Association("AccountPayableShareReferencesAccountPayable", GetType(AccountPayableSharesXpo))>
    Public ReadOnly Property AccountPayableSharesXpo() As XPCollection(Of AccountPayableSharesXpo)
        Get
            Return GetCollection(Of AccountPayableSharesXpo)("AccountPayableSharesXpo")
        End Get
    End Property

    <Association("PaymentsPaymentNotesReferencesAccountPayableXpo", GetType(PaymentsPaymentNotes))>
    Public ReadOnly Property PaymentsPaymentNotes() As XPCollection(Of PaymentsPaymentNotes)
        Get
            Return GetCollection(Of PaymentsPaymentNotes)("PaymentsPaymentNotes")
        End Get
    End Property

    <Association("PaymentsRevaluationDetailReferencesAccountPayable", GetType(PaymentsRevaluationDetailXpo))>
    Public ReadOnly Property PaymentsRevaluationDetail() As XPCollection(Of PaymentsRevaluationDetailXpo)
        Get
            Return GetCollection(Of PaymentsRevaluationDetailXpo)("PaymentsRevaluationDetail")
        End Get
    End Property

    <Association("View_SchedulePaymentXpoReferencesAccountPayable", GetType(View_SchedulePaymentXpo))>
    Public ReadOnly Property View_SchedulePaymentXpo() As XPCollection(Of View_SchedulePaymentXpo)
        Get
            Return GetCollection(Of View_SchedulePaymentXpo)("View_SchedulePaymentXpo")
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
