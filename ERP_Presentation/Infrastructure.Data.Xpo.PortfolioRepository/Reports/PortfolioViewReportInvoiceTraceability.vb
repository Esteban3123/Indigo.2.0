Imports DevExpress.Xpo

<Persistent("Portfolio.ViewReportInvoiceTraceability")>
Public Class PortfolioViewReportInvoiceTraceability
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
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

    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
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

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fPortfolioStatus As Byte
    Public Property PortfolioStatus() As Byte
        Get
            Return fPortfolioStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PortfolioStatus", fPortfolioStatus, value)
        End Set
    End Property

    Dim fPortfolioStatusName As String
    Public Property PortfolioStatusName() As String
        Get
            Return fPortfolioStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioStatusName", fPortfolioStatusName, value)
        End Set
    End Property

    Dim fCustomerId As Integer?
    Public Property CustomerId() As Integer?
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fCustomerNit As String
    Public Property CustomerNit() As String
        Get
            Return fCustomerNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerNit", fCustomerNit, value)
        End Set
    End Property

    Dim fCustomerName As String
    Public Property CustomerName() As String
        Get
            Return fCustomerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerName", fCustomerName, value)
        End Set
    End Property

    Dim fCustomerStatusName As String
    Public Property CustomerStatusName() As String
        Get
            Return fCustomerStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerStatusName", fCustomerStatusName, value)
        End Set
    End Property

    Dim fCustomerEPSCode As String
    Public Property CustomerEPSCode() As String
        Get
            Return fCustomerEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerEPSCode", fCustomerEPSCode, value)
        End Set
    End Property

    Dim fObjectionsReceptionConsecutive As Integer?
    Public Property ObjectionsReceptionConsecutive() As Integer?
        Get
            Return fObjectionsReceptionConsecutive
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ObjectionsReceptionConsecutive", fObjectionsReceptionConsecutive, value)
        End Set
    End Property

    Dim fObjectionsReceptionRadicatedDate As DateTime?
    Public Property ObjectionsReceptionRadicatedDate() As DateTime?
        Get
            Return fObjectionsReceptionRadicatedDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ObjectionsReceptionRadicatedDate", fObjectionsReceptionRadicatedDate, value)
        End Set
    End Property

    Dim fObjectionsReceptionDocumentDate As DateTime?
    Public Property ObjectionsReceptionDocumentDate() As DateTime?
        Get
            Return fObjectionsReceptionDocumentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ObjectionsReceptionDocumentDate", fObjectionsReceptionDocumentDate, value)
        End Set
    End Property

    Dim fObjectionsReceptionConfirmDate As DateTime?
    Public Property ObjectionsReceptionConfirmDate() As DateTime?
        Get
            Return fObjectionsReceptionConfirmDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ObjectionsReceptionConfirmDate", fObjectionsReceptionConfirmDate, value)
        End Set
    End Property

    Dim fObjectionsReceptionComment As String
    Public Property ObjectionsReceptionComment() As String
        Get
            Return fObjectionsReceptionComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObjectionsReceptionComment", fObjectionsReceptionComment, value)
        End Set
    End Property

    Dim fObjectionsReceptionUserCreation As String
    Public Property ObjectionsReceptionUserCreation() As String
        Get
            Return fObjectionsReceptionUserCreation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObjectionsReceptionUserCreation", fObjectionsReceptionUserCreation, value)
        End Set
    End Property

    Dim fObjectionsReceptionStatusName As String
    Public Property ObjectionsReceptionStatusName() As String
        Get
            Return fObjectionsReceptionStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObjectionsReceptionStatusName", fObjectionsReceptionStatusName, value)
        End Set
    End Property

    Dim fConciliationConsecutive As Integer?
    Public Property ConciliationConsecutive() As Integer?
        Get
            Return fConciliationConsecutive
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ConciliationConsecutive", fConciliationConsecutive, value)
        End Set
    End Property

    Dim fConciliationDate As DateTime?
    Public Property ConciliationDate() As DateTime?
        Get
            Return fConciliationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ConciliationDate", fConciliationDate, value)
        End Set
    End Property

    Dim fConciliationDocumentDate As DateTime?
    Public Property ConciliationDocumentDate() As DateTime?
        Get
            Return fConciliationDocumentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ConciliationDocumentDate", fConciliationDocumentDate, value)
        End Set
    End Property

    Dim fConciliationConfirmDate As DateTime?
    Public Property ConciliationConfirmDate() As DateTime?
        Get
            Return fConciliationConfirmDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ConciliationConfirmDate", fConciliationConfirmDate, value)
        End Set
    End Property

    Dim fConciliationStatusName As String
    Public Property ConciliationStatusName() As String
        Get
            Return fConciliationStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConciliationStatusName", fConciliationStatusName, value)
        End Set
    End Property

    Dim fEntityValue As Decimal
    Public Property EntityValue() As Decimal
        Get
            Return fEntityValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("EntityValue", fEntityValue, value)
        End Set
    End Property

    Dim fPatientValue As Decimal
    Public Property PatientValue() As Decimal
        Get
            Return fPatientValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientValue", fPatientValue, value)
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

    Dim fValueGlosado As Decimal
    Public Property ValueGlosado() As Decimal
        Get
            Return fValueGlosado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosado", fValueGlosado, value)
        End Set
    End Property

    Dim fValueAcceptedFirstInstance As Decimal
    Public Property ValueAcceptedFirstInstance() As Decimal
        Get
            Return fValueAcceptedFirstInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedFirstInstance", fValueAcceptedFirstInstance, value)
        End Set
    End Property

    Dim fValueReiterated As Decimal
    Public Property ValueReiterated() As Decimal
        Get
            Return fValueReiterated
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueReiterated", fValueReiterated, value)
        End Set
    End Property

    Dim fValueAcceptedSecondInstance As Decimal
    Public Property ValueAcceptedSecondInstance() As Decimal
        Get
            Return fValueAcceptedSecondInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedSecondInstance", fValueAcceptedSecondInstance, value)
        End Set
    End Property

    Dim fValueAcceptedIPSconciliation As Decimal
    Public Property ValueAcceptedIPSconciliation() As Decimal
        Get
            Return fValueAcceptedIPSconciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedIPSconciliation", fValueAcceptedIPSconciliation, value)
        End Set
    End Property

    Dim fValueAcceptedEAPBconciliation As Decimal
    Public Property ValueAcceptedEAPBconciliation() As Decimal
        Get
            Return fValueAcceptedEAPBconciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedEAPBconciliation", fValueAcceptedEAPBconciliation, value)
        End Set
    End Property

    Dim fBalanceGlosa As Decimal
    Public Property BalanceGlosa() As Decimal
        Get
            Return fBalanceGlosa
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceGlosa", fBalanceGlosa, value)
        End Set
    End Property

    Dim fValuePayments As Decimal
    Public Property ValuePayments() As Decimal
        Get
            Return fValuePayments
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePayments", fValuePayments, value)
        End Set
    End Property

    Dim fLegalTransferValue As Decimal
    Public Property LegalTransferValue() As Decimal
        Get
            Return fLegalTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LegalTransferValue", fLegalTransferValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fPortfolioGlosadaRadicatedNumber As String
    Public Property PortfolioGlosadaRadicatedNumber() As String
        Get
            Return fPortfolioGlosadaRadicatedNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioGlosadaRadicatedNumber", fPortfolioGlosadaRadicatedNumber, value)
        End Set
    End Property

    Dim fPortfolioGlosadaRadicateDate As DateTime?
    Public Property PortfolioGlosadaRadicateDate() As DateTime?
        Get
            Return fPortfolioGlosadaRadicateDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("PortfolioGlosadaRadicateDate", fPortfolioGlosadaRadicateDate, value)
        End Set
    End Property

    Dim fPortfolioGlosadaAccountCustomer As String
    Public Property PortfolioGlosadaAccountCustomer() As String
        Get
            Return fPortfolioGlosadaAccountCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioGlosadaAccountCustomer", fPortfolioGlosadaAccountCustomer, value)
        End Set
    End Property

    Dim fPortfolioGlosadaDate As DateTime?
    Public Property PortfolioGlosadaDate() As DateTime?
        Get
            Return fPortfolioGlosadaDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("PortfolioGlosadaDate", fPortfolioGlosadaDate, value)
        End Set
    End Property

    Dim fPortfolioGlosadaId As Integer?
    Public Property PortfolioGlosadaId() As Integer?
        Get
            Return fPortfolioGlosadaId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PortfolioGlosadaId", fPortfolioGlosadaId, value)
        End Set
    End Property

    Dim fVoucherName As String
    Public Property VoucherName() As String
        Get
            Return fVoucherName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherName", fVoucherName, value)
        End Set
    End Property

    Dim fMovesCode As String
    Public Property MovesCode() As String
        Get
            Return fMovesCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MovesCode", fMovesCode, value)
        End Set
    End Property

    Dim fBillValueInitial As Decimal
    Public Property BillValueInitial() As Decimal
        Get
            Return fBillValueInitial
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillValueInitial", fBillValueInitial, value)
        End Set
    End Property

    Dim fMovesDebit As Decimal
    Public Property MovesDebit() As Decimal
        Get
            Return fMovesDebit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesDebit", fMovesDebit, value)
        End Set
    End Property

    Dim fMovesCredit As Decimal
    Public Property MovesCredit() As Decimal
        Get
            Return fMovesCredit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesCredit", fMovesCredit, value)
        End Set
    End Property

    Dim fBillCurrentBalance As Decimal
    Public Property BillCurrentBalance() As Decimal
        Get
            Return fBillCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillCurrentBalance", fBillCurrentBalance, value)
        End Set
    End Property


#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
