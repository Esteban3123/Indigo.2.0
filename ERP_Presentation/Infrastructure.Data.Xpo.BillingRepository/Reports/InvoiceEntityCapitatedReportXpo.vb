Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.InvoiceEntityCapitated")> _
Public Class InvoiceEntityCapitatedReportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fCareGroupId As CareGroupReportXpo
    <Association("Billing_EntityCapitatedReferencesContract_CareGroup")> _
        Public Property CareGroupId() As CareGroupReportXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As CareGroupReportXpo)
            SetPropertyValue(Of CareGroupReportXpo)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fUserNumber As Integer
    Public Property UserNumber() As Integer
        Get
            Return fUserNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserNumber", fUserNumber, value)
        End Set
    End Property

    Dim fUserValue As Decimal
    Public Property UserValue() As Decimal
        Get
            Return fUserValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UserValue", fUserValue, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
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

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    Dim fDiscountValue As Decimal
    Public Property DiscountValue() As Decimal
        Get
            Return fDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValue", fDiscountValue, value)
        End Set
    End Property

    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property


    Dim fInvoicePeriod As Byte?
    Public Property InvoicePeriod() As Byte?
        Get
            Return fInvoicePeriod
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("InvoicePeriod", fInvoicePeriod, value)
        End Set
    End Property

    Dim fPreviousRIPSInvoice As Integer?
    Public Property PreviousRIPSInvoice() As Integer?
        Get
            Return fPreviousRIPSInvoice
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PreviousRIPSInvoice", fPreviousRIPSInvoice, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCopaymentAmount As Decimal
    Public Property CopaymentAmount() As Decimal
        Get
            Return fCopaymentAmount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CopaymentAmount", fCopaymentAmount, value)
        End Set
    End Property

    Dim fModeratingFeeAmount As Decimal
    Public Property ModeratingFeeAmount() As Decimal
        Get
            Return fModeratingFeeAmount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ModeratingFeeAmount", fModeratingFeeAmount, value)
        End Set
    End Property

    Dim fSharedPaymentAmount As Decimal
    Public Property SharedPaymentAmount() As Decimal
        Get
            Return fSharedPaymentAmount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SharedPaymentAmount", fSharedPaymentAmount, value)
        End Set
    End Property

    <NonPersistent()>
    Public ReadOnly Property CollectionsTotalValue As Decimal
        Get
            Return CopaymentAmount + ModeratingFeeAmount + SharedPaymentAmount
        End Get
    End Property



#End Region

#Region "Navigations"

    Dim fInvoiceId As BillingVReportInvoice
    <Association("VReportInvoice_Reference_EntityCapitatedReferencesBilling")>
    Public Property InvoiceId() As BillingVReportInvoice
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As BillingVReportInvoice)
            SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fBillingAuthorizationId As BillingAuthorizationReportXpo
    <Association("Billing_EntityCapitatedReferencesBillingAuthorization")> _
    Public Property BillingAuthorizationId() As BillingAuthorizationReportXpo
        Get
            Return fBillingAuthorizationId
        End Get
        Set(ByVal value As BillingAuthorizationReportXpo)
            SetPropertyValue(Of BillingAuthorizationReportXpo)("BillingAuthorizationId", fBillingAuthorizationId, value)
        End Set
    End Property

    <Association("Billing_InvoiceEntityCapitatedDistribution_References_Billing_InvoiceEntityCapitated", GetType(InvoiceEntityCapitatedDistributionReportXpo))> _
    Public ReadOnly Property InvoiceEntityCapitatedDistributions() As XPCollection(Of InvoiceEntityCapitatedDistributionReportXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedDistributionReportXpo)("InvoiceEntityCapitatedDistributions")
        End Get
    End Property

    <Association("Billing_InvoiceEntityCapitatedGroupers_References_InvoiceEntityCapitated", GetType(InvoiceEntityCapitatedGroupersReportXpo))>
    Public ReadOnly Property InvoiceEntityCapitatedGroupers() As XPCollection(Of InvoiceEntityCapitatedGroupersReportXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedGroupersReportXpo)("InvoiceEntityCapitatedGroupers")
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
