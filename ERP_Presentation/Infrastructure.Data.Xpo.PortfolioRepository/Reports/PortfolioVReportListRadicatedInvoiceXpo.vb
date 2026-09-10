Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

Public Structure RadicatedInvoiceKey

    <Persistent("InvoiceNumber")> _
    Public Property InvoiceNumber As String

    <Persistent("RadicatedConsecutive")> _
    Public Property RadicatedConsecutive As Integer

End Structure


<Persistent("Portfolio.VReportListRadicatedInvoice")> _
Public Class PortfolioVReportListRadicatedInvoiceXpo
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As RadicatedInvoiceKey

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property
    Dim fNitCustomer As String
    <Size(15)> _
    Public Property NitCustomer() As String
        Get
            Return fNitCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitCustomer", fNitCustomer, value)
        End Set
    End Property
    Dim fNameCustomer As String
    Public Property NameCustomer() As String
        Get
            Return fNameCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCustomer", fNameCustomer, value)
        End Set
    End Property
    Dim fStatus As Char
    Public Property Status() As Char
        Get
            Return fStatus
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Status", fStatus, value)
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
    Dim fContractCode As String
    <Size(15)> _
    Public Property ContractCode() As String
        Get
            Return fContractCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCode", fContractCode, value)
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
    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
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
    Dim fInvoiceNumber As String
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fPatientCode As String
    <Size(15)> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property
    Dim fPatientName As String
    <Size(200)> _
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
    Dim fCreditNoteValue As Decimal
    Public Property CreditNoteValue() As Decimal
        Get
            Return fCreditNoteValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditNoteValue", fCreditNoteValue, value)
        End Set
    End Property
    Dim fRegimen As Byte
    Public Property Regimen() As Byte
        Get
            Return fRegimen
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Regimen", fRegimen, value)
        End Set
    End Property
    Dim fCodeCareGroup As String
    <Size(20)> _
    Public Property CodeCareGroup() As String
        Get
            Return fCodeCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCareGroup", fCodeCareGroup, value)
        End Set
    End Property
    Dim fNameCareGroup As String
    Public Property NameCareGroup() As String
        Get
            Return fNameCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCareGroup", fNameCareGroup, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
