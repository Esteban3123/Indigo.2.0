'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldán
' Created          : 2022-09-16
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("Payments.ViewElectronicDocumentSupportRpt")>
Partial Public Class ViewElectronicDocumentSupportRptXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Private fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Code", fCode, value)
        End Set
    End Property

    Private fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Private fNameCustomer As String
    Public Property NameCustomer() As String
        Get
            Return fNameCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NameCustomer", fNameCustomer, value)
        End Set
    End Property

    Private fNitCustomer As String
    Public Property NitCustomer() As String
        Get
            Return fNitCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NitCustomer", fNitCustomer, value)
        End Set
    End Property

    Private fAddresssCustomer As String
    Public Property AddresssCustomer() As String
        Get
            Return fAddresssCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AddresssCustomer", fAddresssCustomer, value)
        End Set
    End Property

    Private fPhoneCustomer As String
    Public Property PhoneCustomer() As String
        Get
            Return fPhoneCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PhoneCustomer", fPhoneCustomer, value)
        End Set
    End Property

    Private fCityCustomer As String
    Public Property CityCustomer() As String
        Get
            Return fCityCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CityCustomer", fCityCustomer, value)
        End Set
    End Property

    Private fNitSupplier As String
    Public Property NitSupplier() As String
        Get
            Return fNitSupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NitSupplier", fNitSupplier, value)
        End Set
    End Property

    Private fNameSupplier As String
    Public Property NameSupplier() As String
        Get
            Return fNameSupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NameSupplier", fNameSupplier, value)
        End Set
    End Property

    Private fAddresssSupplier As String
    Public Property AddresssSupplier() As String
        Get
            Return fAddresssSupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AddresssSupplier", fAddresssSupplier, value)
        End Set
    End Property

    Private fPhoneSuppler As String
    Public Property PhoneSuppler() As String
        Get
            Return fPhoneSuppler
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PhoneSuppler", fPhoneSuppler, value)
        End Set
    End Property

    Private fCitySupplier As String
    Public Property CitySupplier() As String
        Get
            Return fCitySupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CitySupplier", fCitySupplier, value)
        End Set
    End Property

    Private fIdParentDocument As Integer
    Public Property IdParentDocument() As Integer
        Get
            Return fIdParentDocument
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("IdParentDocument", fIdParentDocument, value)
        End Set
    End Property

    Private fDocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Private fTerm As Integer
    Public Property Term() As Integer
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Term", fTerm, value)
        End Set
    End Property

    Private fPaymentWay As String
    Public Property PaymentWay() As String
        Get
            Return fPaymentWay
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PaymentWay", fPaymentWay, value)
        End Set
    End Property

    Private fPaymentMethod As String
    Public Property PaymentMethod() As String
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property

    Private fState As String
    Public Property State() As String
        Get
            Return fState
        End Get
        Set(ByVal value As String)
            SetPropertyValue("State", fState, value)
        End Set
    End Property

    Private fParentDocumentCode As String
    Public Property ParentDocumentCode() As String
        Get
            Return fParentDocumentCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ParentDocumentCode", fParentDocumentCode, value)
        End Set
    End Property

    Private fValidationDate As Date?
    Public Property ValidationDate() As Date?
        Get
            Return fValidationDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue("ValidationDate", fValidationDate, value)
        End Set
    End Property

    Private fCUDS As String
    Public Property CUDS() As String
        Get
            Return fCUDS
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CUDS", fCUDS, value)
        End Set
    End Property

    Private fQR As String
    Public Property QR() As String
        Get
            Return fQR
        End Get
        Set(ByVal value As String)
            SetPropertyValue("QR", fQR, value)
        End Set
    End Property

    Private fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Observations", fObservations, value)
        End Set
    End Property

    Private fSubTotalValue As Decimal
    Public Property SubTotalValue() As Decimal
        Get
            Return fSubTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("SubTotalValue", fSubTotalValue, value)
        End Set
    End Property

    Private fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("TaxValue", fTaxValue, value)
        End Set
    End Property

    Private fTotal As Decimal
    Public Property Total() As Decimal
        Get
            Return fTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Total", fTotal, value)
        End Set
    End Property

    Private fResolutionNumber As String
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property

    Private fResolutionDate As String
    Public Property ResolutionDate() As String
        Get
            Return fResolutionDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResolutionDate", fResolutionDate, value)
        End Set
    End Property

    Private fInvoicePrefix As String
    Public Property InvoicePrefix() As String
        Get
            Return fInvoicePrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InvoicePrefix", fInvoicePrefix, value)
        End Set
    End Property

    Private fInitialInvoice As Long
    Public Property InitialInvoice() As Long
        Get
            Return fInitialInvoice
        End Get
        Set(ByVal value As Long)
            SetPropertyValue("InitialInvoice", fInitialInvoice, value)
        End Set
    End Property

    Private fFinalInvoice As Long
    Public Property FinalInvoice() As Long
        Get
            Return fFinalInvoice
        End Get
        Set(ByVal value As Long)
            SetPropertyValue("FinalInvoice", fFinalInvoice, value)
        End Set
    End Property

    Private fInitialDate As String
    Public Property InitialDate() As String
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InitialDate", fInitialDate, value)
        End Set
    End Property

    Private fFinalDate As String
    Public Property FinalDate() As String
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FinalDate", fFinalDate, value)
        End Set
    End Property

    Private fConcept As String
    Public Property Concept() As String
        Get
            Return fConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Concept", fConcept, value)
        End Set
    End Property

    Private fAccount As String
    Public Property Account() As String
        Get
            Return fAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Account", fAccount, value)
        End Set
    End Property

    Private fThirdParty As String
    Public Property ThirdParty() As String
        Get
            Return fThirdParty
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ThirdParty", fThirdParty, value)
        End Set
    End Property

    Private fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Nit", fNit, value)
        End Set
    End Property

    Private fCostCenter As String
    Public Property CostCenter() As String
        Get
            Return fCostCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CostCenter", fCostCenter, value)
        End Set
    End Property

    Private fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("DebitValue", fDebitValue, value)
        End Set
    End Property

    Private fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("CreditValue", fCreditValue, value)
        End Set
    End Property

    Private fRetencionType As Byte
    Public Property RetencionType() As Byte
        Get
            Return fRetencionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue("RetencionType", fRetencionType, value)
        End Set
    End Property

    Private fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EntityName", fEntityName, value)
        End Set
    End Property

    Private fSourceEntityId As Integer
    Public Property SourceEntityId() As Integer
        Get
            Return fSourceEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("SourceEntityId", fSourceEntityId, value)
        End Set
    End Property

    Private fSourceEntityCode As String
    Public Property SourceEntityCode() As String
        Get
            Return fSourceEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SourceEntityCode", fSourceEntityCode, value)
        End Set
    End Property

    Private fSourceEntityName As String
    Public Property SourceEntityName() As String
        Get
            Return fSourceEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SourceEntityName", fSourceEntityName, value)
        End Set
    End Property

End Class