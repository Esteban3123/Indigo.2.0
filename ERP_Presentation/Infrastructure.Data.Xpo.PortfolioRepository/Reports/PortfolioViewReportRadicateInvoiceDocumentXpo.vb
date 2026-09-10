Imports DevExpress.Xpo

<Persistent("Portfolio.ViewReportRadicateInvoiceDocument")>
Public Class PortfolioViewReportRadicateInvoiceDocumentXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fradicateInoviceDId As Integer
    <Key(True)>
    Public Property radicateInoviceDId() As Integer
        Get
            Return fradicateInoviceDId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("radicateInoviceDId", fradicateInoviceDId, value)
        End Set
    End Property

    Dim fradicateInvoiceCId As Integer
    Public Property radicateInvoiceCId() As Integer
        Get
            Return fradicateInvoiceCId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("radicateInvoiceCId", fradicateInvoiceCId, value)
        End Set
    End Property

    Dim fRadicatedConsecutive As Integer
    Public Property RadicatedConsecutive() As Integer
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedConsecutive", fRadicatedConsecutive, value)
        End Set
    End Property

    Dim fRadicatedDate As DateTime
    Public Property RadicatedDate() As DateTime
        Get
            Return fRadicatedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedDate", fRadicatedDate, value)
        End Set
    End Property

    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
        End Set
    End Property

    Dim fradicateInvoiceComment As String
    Public Property radicateInvoiceComment() As String
        Get
            Return fradicateInvoiceComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("radicateInvoiceComment", fradicateInvoiceComment, value)
        End Set
    End Property

    Dim fcustomerNit As String
    Public Property customerNit() As String
        Get
            Return fcustomerNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("customerNit", fcustomerNit, value)
        End Set
    End Property

    Dim fcustomerName As String
    Public Property customerName() As String
        Get
            Return fcustomerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("customerName", fcustomerName, value)
        End Set
    End Property

    Dim fEPSCode As String
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
        End Set
    End Property

    Dim fUserCodeName As String
    Public Property UserCodeName() As String
        Get
            Return fUserCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCodeName", fUserCodeName, value)
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

    Dim fIngressDate As DateTime
    Public Property IngressDate() As DateTime
        Get
            Return fIngressDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IngressDate", fIngressDate, value)
        End Set
    End Property

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fContractCode As String
    Public Property ContractCode() As String
        Get
            Return fContractCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCode", fContractCode, value)
        End Set
    End Property

    Dim fContractNumber As String
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fInvoiceValueEntity As Decimal
    Public Property InvoiceValueEntity() As Decimal
        Get
            Return fInvoiceValueEntity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValueEntity", fInvoiceValueEntity, value)
        End Set
    End Property

    Dim fInvoiceValuePacient As Decimal
    Public Property InvoiceValuePacient() As Decimal
        Get
            Return fInvoiceValuePacient
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValuePacient", fInvoiceValuePacient, value)
        End Set
    End Property

    Dim fCreditNoteValue As Decimal
    Public Property CreditNoteValue() As Decimal
        Get
            Return fCreditNoteValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditNoteValue", fCreditNoteValue, value)
        End Set
    End Property

    Dim fBalanceInvoice As Decimal
    Public Property BalanceInvoice() As Decimal
        Get
            Return fBalanceInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceInvoice", fBalanceInvoice, value)
        End Set
    End Property

    Dim fradicateInvoiceStatus As Byte
    Public Property radicateInvoiceStatus() As Byte
        Get
            Return fradicateInvoiceStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("radicateInvoiceStatus", fradicateInvoiceStatus, value)
        End Set
    End Property

    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property

    Dim fentityTypeName As String
    Public Property entityTypeName() As String
        Get
            Return fentityTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("entityTypeName", fentityTypeName, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
