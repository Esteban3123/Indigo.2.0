'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Entidades XPO para reportes de Billing
' Adaptado para .NET 8
'***********************************************************************

Imports DevExpress.Xpo

Namespace XpoEntities

#Region "BillingVReportInvoice"

    <Persistent("Billing.VReportInvoice")>
    Public Class BillingVReportInvoice
        Inherits XPLiteObject

        Dim fId As Integer
        <Key()>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Public Property OperatingUnitId() As Integer
        Public Property InvoiceDate() As DateTime
        Public Property InvoiceExpirationDate() As DateTime
        Public Property InvoiceNumber() As String
        Public Property ElectronicInvoiceNumber() As String
        Public Property CutType() As Byte
        Public Property Nit() As String
        Public Property Name() As String
        Public Property ThirdPartyAddress() As String
        Public Property ThirdPartyPhone() As String
        Public Property CareGroup() As String
        Public Property CareGroupType() As Byte
        Public Property HealthEntityCode() As String
        Public Property DescriptionHealthAdministrator() As String
        Public Property Contract() As String
        Public Property PrintingMode() As Byte?
        Public Property ContractDate As DateTime?
        Public Property ContractInitialDate As DateTime?
        Public Property ContractEndDate As DateTime?
        Public Property CapitationInitialDate As DateTime?
        Public Property CapitationEndDate As DateTime?
        Public Property CapitationlPatientsAmount As Integer?
        Public Property CapitationPatientValue As Decimal
        Public Property TotalValue As Decimal
        Public Property TotalInvoice As Decimal
        Public Property PatientCode() As String
        Public Property PatientName() As String
        Public Property PatientFirstName() As String
        Public Property PatientSecondName() As String
        Public Property PatientFirstLastName() As String
        Public Property PatientSecondLastName() As String
        Public Property PatientType() As String
        Public Property AffiliateType() As Integer?
        Public Property PatientLevel() As String
        Public Property PatientAddress() As String
        Public Property PatientTelephoneNumber() As String
        Public Property PatientPhoneMovil() As String
        Public Property PatientEmail() As String
        Public Property PatientAge() As String
        Public Property TypeAdmission() As Integer?
        Public Property AdmissionNumber() As String
        Public Property AdmissionDate() As DateTime
        Public Property EgressDate() As DateTime
        Public Property AuthorizationNumber() As String
        Public Property ProcessingDate() As DateTime?
        Public Property ProcessLine() As String
        Public Property PacientEntity() As String
        Public Property PacientRegimen() As String
        Public Property AffiliateStatus() As String
        Public Property ERPConfirm() As String
        Public Property IPSReport() As String
        Public Property CODCENATE() As String
        Public Property CenterAttentionName() As String
        Public Property CenterAttentionCode() As String
        Public Property CenterAttentionAddress() As String
        Public Property CenterAttentionPhone() As String
        Public Property UFUIGRMED() As String
        Public Property UFUEGRMED() As String
        Public Property InvoiceCategory() As String
        Public Property DocumentType() As Byte
        Public Property IdentificationTypeCode() As String
        Public Property OutputDiagnosis() As String
        Public Property SubTotalService() As Decimal?
        Public Property ThirdPartyDiscountValue() As Decimal
        Public Property TotalPatientSalesPrice() As Decimal
        Public Property PatientDiscount() As Decimal
        Public Property TotalPatientAccountReceivable() As Decimal?
        Public Property TotalThirdPartyPortfolioAdvance() As Decimal?
        Public Property ThirdPartySalesValue() As Decimal
        Public Property UserCode() As String
        Public Property FullNameUser() As String
        Public Property Status() As Byte
        Public Property PolicyNumber() As String
        Public Property PermanentObservationOfTheInvoice() As String
        Public Property IdPay() As Integer?
        Public Property ResolutionNumber() As String
        Public Property ResolutionDate() As Date?
        Public Property ResolutionInvoicePrefix() As String
        Public Property ResolutionInitialInvoice() As Long?
        Public Property ResolutionFinalInvoice() As Long?
        Public Property ResolutionInitialDate() As Date?
        Public Property ResolutionFinalDate() As Date?
        Public Property ResolutionExpiration() As Integer
        Public Property Term() As Integer?
        Public Property PaymentMeans() As String
        Public Property PaymentMethod() As String
        Public Property LiquidationType() As Byte?
        Public Property CoverageDescription() As String
        Public Property CUFE() As String
        Public Property QR() As String
        Public Property StatusDIAN() As String
        Public Property ValidationDate() As DateTime?
        Public Property Observation() As String
        Public Property CurrencyName() As String
        Public Property CoinsuranceInsurance() As Decimal?
        Public Property CopaymentValueInsurance() As Decimal?
        Public Property DeductibleValueInsurance() As Decimal?
        Public Property PatientCoinsurance() As Decimal?
        Public Property PatientCopaymentValue() As Decimal?
        Public Property PatientDeductibleValue() As Decimal?
        Public Property IsMasterAccount() As Integer?
        Public Property ValueTax() As Decimal
        Public Property TaxDevolutionValue() As Decimal
        Public Property IdentificationName() As String
        Public Property DataDiscountPercentage As Decimal
        Public Property ConditionSalesCodeName As String
        Public Property NitWithOutDig() As String
        Public Property IsElectronicTicket() As Boolean

        <NonPersistent()>
        Public Property UserPrint() As String

        <NonPersistent()>
        Public Property SubtotalPatient() As String

        <NonPersistent()>
        Public Property SubtotalLetters() As String

        Dim fCurrencyId As CommonCurrencyXpo
        Public Property CurrencyId() As CommonCurrencyXpo
            Get
                Return fCurrencyId
            End Get
            Set(ByVal value As CommonCurrencyXpo)
                SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
            End Set
        End Property

        <Association("VReportInvoice_References_VReportInvoiceDetail", GetType(BillingVReportInvoiceDetail))>
        Public ReadOnly Property BillingVReportInvoiceDetail() As XPCollection(Of BillingVReportInvoiceDetail)
            Get
                Return GetCollection(Of BillingVReportInvoiceDetail)("BillingVReportInvoiceDetail")
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingVReportInvoiceDetail"

    <Persistent("Billing.VReportInvoiceDetail")>
    Public Class BillingVReportInvoiceDetail
        Inherits XPLiteObject

        Dim fId As String
        <Key>
        Public Property Id() As String
            Get
                Return fId
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Id", fId, value)
            End Set
        End Property

        Dim fInvoiceId As BillingVReportInvoice
        <Association("VReportInvoice_References_VReportInvoiceDetail")>
        Public Property InvoiceId() As BillingVReportInvoice
            Get
                Return fInvoiceId
            End Get
            Set(ByVal value As BillingVReportInvoice)
                SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
            End Set
        End Property

        Public Property ServiceOrderDetailId() As Integer
        Public Property BillingGroup() As String
        Public Property Code() As String
        Public Property CUPSCode() As String
        Public Property RIPSCode() As String
        Public Property CodeAlternative() As String
        Public Property CodeAlternativeTwo() As String
        Public Property CodeCUM() As String
        Public Property Name() As String
        Public Property CUPSName() As String
        Public Property RIPSName() As String
        Public Property ContractDescriptionCode() As String
        Public Property ContractDescriptionName() As String
        Public Property ServiceDate() As DateTime
        Public Property AuthorizationNumber() As String
        Public Property RecordType() As Integer
        Public Property Presentation() As Integer?
        Public Property DistributionType() As Byte
        Public Property MeasuryUnit() As String
        Public Property InvoicedQuantity() As Integer
        Public Property TotalSalesPrice() As Decimal
        Public Property SubTotalPatientSalesPrice() As Decimal
        Public Property ThirdPartySalesPrice() As Decimal
        Public Property ThirdPartyDiscount() As Decimal
        Public Property SurgicalId() As Integer?
        Public Property CodeSurgical() As String
        Public Property NameSurgical() As String
        Public Property QuantitySurgical() As Integer?
        Public Property TotalSalesPriceSurgical() As Decimal?
        Public Property CodeMipres() As String
        Public Property IdMipres() As String
        Public Property IvaPercentage() As String
        Public Property IvaTotalValue() As Decimal
        Public Property GrossValue() As Decimal
        Public Property NetWorth() As Decimal
        Public Property NetUnitValue() As Decimal
        Public Property GrandTotalSalesPrice() As Decimal
        Public Property GrandTotalDiscount() As Decimal
        Public Property GrossSubValue() As Decimal
        Public Property GrossUnitValue() As Decimal

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "CommonCurrencyXpo"

    <Persistent("Common.Currency")>
    Public Class CommonCurrencyXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property Name() As String
        Public Property Abbreviation() As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "ViewBillingNoteXpo"

    <Persistent("Billing.ViewBillingNote")>
    Public Class ViewBillingNoteXpo
        Inherits XPLiteObject

        Dim fId As Integer
        <Key()>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Public Property NoteTypeName() As String
        Public Property Code() As String
        Public Property NoteDate() As DateTime
        Public Property Observations() As String
        Public Property CustomerPartyNit() As String
        Public Property CustomerPartyName() As String
        Public Property CustomerAddress() As String
        Public Property CustomerCityName() As String
        Public Property CUDE() As String
        Public Property QR() As String
        Public Property StatusDIAN() As String
        Public Property ValidationDate() As DateTime?
        Public Property NoteType() As Byte?
        Public Property PortfolioNoteEntityName() As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "InvoiceEntityCapitatedReportXpo"

    <Persistent("Billing.InvoiceEntityCapitated")>
    Public Class InvoiceEntityCapitatedReportXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property OperatingUnitId() As Integer
        Public Property DocumentDate() As DateTime
        Public Property InitialDate() As DateTime
        Public Property EndDate() As DateTime
        Public Property UserNumber() As Integer
        Public Property UserValue() As Decimal
        Public Property TotalValue() As Decimal
        Public Property Status() As Byte
        Public Property CreationUser() As String
        Public Property CreationDate() As DateTime
        Public Property DiscountValue() As Decimal
        Public Property Observations() As String
        Public Property InvoicePeriod() As Byte?
        Public Property CurrencyId() As Integer
        Public Property CopaymentAmount() As Decimal
        Public Property ModeratingFeeAmount() As Decimal
        Public Property SharedPaymentAmount() As Decimal

        Dim fInvoiceId As InvoiceXpo
        Public Property InvoiceId() As InvoiceXpo
            Get
                Return fInvoiceId
            End Get
            Set(ByVal value As InvoiceXpo)
                SetPropertyValue(Of InvoiceXpo)("InvoiceId", fInvoiceId, value)
            End Set
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "InvoiceXpo"

    <Persistent("Billing.Invoice")>
    Public Class InvoiceXpo
        Inherits XPLiteObject

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

        Public Property InvoiceNumber() As String
        Public Property InvoiceDate() As DateTime
        Public Property CUFE() As String
        Public Property QR() As String
        Public Property TotalValue() As Decimal
        Public Property Status() As Byte
        Public Property DescriptionHealthAdministrator() As String
        Public Property IsElectronicTicket() As Boolean

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BasicBillingDetailReportXpo"

    <Persistent("Billing.BasicBillingDetail")>
    Public Class BasicBillingDetailReportXpo
        Inherits XPLiteObject

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

        Dim fBasicBillingId As BasicBillingXpo
        <Association("BasicBillingDetail_References_BasicBilling")>
        Public Property BasicBillingId() As BasicBillingXpo
            Get
                Return fBasicBillingId
            End Get
            Set(ByVal value As BasicBillingXpo)
                SetPropertyValue(Of BasicBillingXpo)("BasicBillingId", fBasicBillingId, value)
            End Set
        End Property

        Public Property DetailType() As Byte
        Public Property Quantity() As Integer
        Public Property Price() As Decimal
        Public Property Value() As Decimal
        Public Property PercentageDiscount() As Decimal
        Public Property PercentageIVA() As Decimal

        Dim fBillingConceptId As BillingConceptReportXpo
        Public Property BillingConceptId() As BillingConceptReportXpo
            Get
                Return fBillingConceptId
            End Get
            Set(ByVal value As BillingConceptReportXpo)
                SetPropertyValue(Of BillingConceptReportXpo)("BillingConceptId", fBillingConceptId, value)
            End Set
        End Property

        Dim fServicesProvidedId As BillingConceptReportXpo
        Public Property ServicesProvidedId() As BillingConceptReportXpo
            Get
                Return fServicesProvidedId
            End Get
            Set(ByVal value As BillingConceptReportXpo)
                SetPropertyValue(Of BillingConceptReportXpo)("ServicesProvidedId", fServicesProvidedId, value)
            End Set
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BasicBillingXpo"

    <Persistent("Billing.BasicBilling")>
    Public Class BasicBillingXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property DocumentDate() As DateTime
        Public Property CreationUser() As String
        Public Property Status() As Byte
        Public Property CurrencyAbbreviation() As String
        Public Property CurrencyName() As String

        Dim fInvoiceId As InvoiceXpo
        Public Property InvoiceId() As InvoiceXpo
            Get
                Return fInvoiceId
            End Get
            Set(ByVal value As InvoiceXpo)
                SetPropertyValue(Of InvoiceXpo)("InvoiceId", fInvoiceId, value)
            End Set
        End Property

        Dim fBillingAuthorizationId As BillingAuthorizationXpo
        Public Property BillingAuthorizationId() As BillingAuthorizationXpo
            Get
                Return fBillingAuthorizationId
            End Get
            Set(ByVal value As BillingAuthorizationXpo)
                SetPropertyValue(Of BillingAuthorizationXpo)("BillingAuthorizationId", fBillingAuthorizationId, value)
            End Set
        End Property

        Dim fThirdPartyEntityCopayId As ThirdPartyXpo
        Public Property ThirdPartyEntityCopayId() As ThirdPartyXpo
            Get
                Return fThirdPartyEntityCopayId
            End Get
            Set(ByVal value As ThirdPartyXpo)
                SetPropertyValue(Of ThirdPartyXpo)("ThirdPartyEntityCopayId", fThirdPartyEntityCopayId, value)
            End Set
        End Property

        Dim fConditionSalesId As Integer?
        Public Property ConditionSalesId() As Integer?
            Get
                Return fConditionSalesId
            End Get
            Set(ByVal value As Integer?)
                SetPropertyValue(Of Integer?)("ConditionSalesId", fConditionSalesId, value)
            End Set
        End Property

        <Association("BasicBillingDetail_References_BasicBilling", GetType(BasicBillingDetailReportXpo))>
        Public ReadOnly Property BasicBillingDetails() As XPCollection(Of BasicBillingDetailReportXpo)
            Get
                Return GetCollection(Of BasicBillingDetailReportXpo)("BasicBillingDetails")
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingConceptReportXpo"

    <Persistent("Billing.BillingConcept")>
    Public Class BillingConceptReportXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property Name() As String
        Public Property CodeName() As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingAuthorizationXpo"

    <Persistent("Billing.BillingAuthorization")>
    Public Class BillingAuthorizationXpo
        Inherits XPLiteObject

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

        Public Property ResolutionNumber() As String
        Public Property ResolutionDate() As DateTime
        Public Property InvoicePrefix() As String
        Public Property InitialInvoice() As Long
        Public Property FinalInvoice() As Long
        Public Property InitialDate() As DateTime
        Public Property FinalDate() As DateTime

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingInvoiceCopayXpo"

    <Persistent("Billing.InvoiceCopay")>
    Public Class BillingInvoiceCopayXpo
        Inherits XPLiteObject

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

        Dim fInvoiceId As InvoiceXpo
        Public Property InvoiceId() As InvoiceXpo
            Get
                Return fInvoiceId
            End Get
            Set(ByVal value As InvoiceXpo)
                SetPropertyValue(Of InvoiceXpo)("InvoiceId", fInvoiceId, value)
            End Set
        End Property

        Dim fBasicBillingId As BasicBillingXpo
        Public Property BasicBillingId() As BasicBillingXpo
            Get
                Return fBasicBillingId
            End Get
            Set(ByVal value As BasicBillingXpo)
                SetPropertyValue(Of BasicBillingXpo)("BasicBillingId", fBasicBillingId, value)
            End Set
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "ElectronicDocumentReportXpo"

    <Persistent("Billing.ElectronicDocument")>
    Public Class ElectronicDocumentReportXpo
        Inherits XPLiteObject

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

        Public Property EntityId() As String
        Public Property EntityName() As String
        Public Property Status() As Integer
        Public Property StatusName() As String
        Public Property ValidationDate() As DateTime?
        Public Property ShippingDate() As DateTime?

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "ConditionSalesXpo"

    <Persistent("Billing.ConditionSales")>
    Public Class ConditionSalesXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property Name() As String
        Public Property CodeName() As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "PortfolioAccountReceivableXpo"

    <Persistent("Billing.PortfolioAccountReceivable")>
    Public Class PortfolioAccountReceivableXpo
        Inherits XPLiteObject

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

        Public Property OperatingUnitId() As Integer
        Public Property InvoiceId() As Integer?
        Public Property Code() As String
        Public Property Value() As Decimal
        Public Property Status() As Byte

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingVReportInvoicePartial"

    <Persistent("Billing.VReportInvoicePartial")>
    Public Class BillingVReportInvoicePartial
        Inherits XPLiteObject

        Dim fId As Integer
        <Key()>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Public Property InvoiceNumber() As String
        Public Property InvoiceDate() As DateTime
        Public Property TotalValue() As Decimal

        <Association("VReportInvoicePartial_References_VReportInvoicePartialDetail", GetType(BillingVReportInvoicePartialDetail))>
        Public ReadOnly Property BillingVReportInvoicePartialDetail() As XPCollection(Of BillingVReportInvoicePartialDetail)
            Get
                Return GetCollection(Of BillingVReportInvoicePartialDetail)("BillingVReportInvoicePartialDetail")
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingVReportInvoicePartialDetail"

    <Persistent("Billing.VReportInvoicePartialDetail")>
    Public Class BillingVReportInvoicePartialDetail
        Inherits XPLiteObject

        Dim fId As String
        <Key>
        Public Property Id() As String
            Get
                Return fId
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Id", fId, value)
            End Set
        End Property

        Dim fInvoicePartialId As BillingVReportInvoicePartial
        <Association("VReportInvoicePartial_References_VReportInvoicePartialDetail")>
        Public Property InvoicePartialId() As BillingVReportInvoicePartial
            Get
                Return fInvoicePartialId
            End Get
            Set(ByVal value As BillingVReportInvoicePartial)
                SetPropertyValue(Of BillingVReportInvoicePartial)("InvoicePartialId", fInvoicePartialId, value)
            End Set
        End Property

        Public Property IvaPercentage() As String
        Public Property IvaTotalValue() As Decimal
        Public Property NetWorth() As Decimal

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

End Namespace
