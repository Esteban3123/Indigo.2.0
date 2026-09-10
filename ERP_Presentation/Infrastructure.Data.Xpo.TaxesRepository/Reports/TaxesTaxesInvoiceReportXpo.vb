Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesInvoice")> _
Public Class TaxesTaxesInvoiceReportXpo
    Inherits XPLiteObject
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
    Dim fValidity As Integer
    Public Property Validity() As Integer
        Get
            Return fValidity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Validity", fValidity, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fTaxesConsecutiveBillingId As Integer
    Public Property TaxesConsecutiveBillingId() As Integer
        Get
            Return fTaxesConsecutiveBillingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TaxesConsecutiveBillingId", fTaxesConsecutiveBillingId, value)
        End Set
    End Property
    Dim fTaxesPropertyId As TaxesTaxesPropertyReportXpo
    <Association("Taxes_TaxesInvoiceReferencesTaxes_Property")> _
    Public Property TaxesPropertyId() As TaxesTaxesPropertyReportXpo
        Get
            Return fTaxesPropertyId
        End Get
        Set(ByVal value As TaxesTaxesPropertyReportXpo)
            SetPropertyValue(Of TaxesTaxesPropertyReportXpo)("TaxesPropertyId", fTaxesPropertyId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Taxes_TaxesInvoiceReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fAppraisal As Decimal
    Public Property Appraisal() As Decimal
        Get
            Return fAppraisal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Appraisal", fAppraisal, value)
        End Set
    End Property
    Dim fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxValue", fTaxValue, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
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
    <Association("Taxes_TaxesInvoiceDetailReferencesTaxes_TaxesInvoice", GetType(TaxesTaxesInvoiceDetailReportXpo))> _
    Public ReadOnly Property TaxesTaxesInvoiceDetailReportXpo() As XPCollection(Of TaxesTaxesInvoiceDetailReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesInvoiceDetailReportXpo)("TaxesTaxesInvoiceDetailReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
