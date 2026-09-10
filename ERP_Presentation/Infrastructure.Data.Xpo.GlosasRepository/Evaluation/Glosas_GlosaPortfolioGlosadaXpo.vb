Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaPortfolioGlosada")> _
Partial Public Class Glosas_GlosaPortfolioGlosadaXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fInvoiceNumber As String
    <Indexed(Name:="IX_GlosaPortfolioGlosada", Unique:=True)>
    <Size(50)>
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Dim fBalanceInvoice As Decimal
    Public Property BalanceInvoice() As Decimal
        Get
            Return fBalanceInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceInvoice", fBalanceInvoice, value)
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

    Dim fBalanceGlosaAcceptedEAPB As Decimal
    Public Property BalanceGlosaAcceptedEAPB() As Decimal
        Get
            Return fBalanceGlosaAcceptedEAPB
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceGlosaAcceptedEAPB", fBalanceGlosaAcceptedEAPB, value)
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

    Dim fBalanceLegal As Decimal
    Public Property BalanceLegal() As Decimal
        Get
            Return fBalanceLegal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceLegal", fBalanceLegal, value)
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

    Dim fContractName As String
    <Size(200)>
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property

    Dim fContractCode As String
    <Size(10)>
    Public Property ContractCode() As String
        Get
            Return fContractCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCode", fContractCode, value)
        End Set
    End Property

    Dim fPlanCode As String
    <Size(15)>
    Public Property PlanCode() As String
        Get
            Return fPlanCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanCode", fPlanCode, value)
        End Set
    End Property

    Dim fUserNameInvoice As String
    <Size(200)>
    Public Property UserNameInvoice() As String
        Get
            Return fUserNameInvoice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserNameInvoice", fUserNameInvoice, value)
        End Set
    End Property

    Dim fIngressNumber As String
    <Size(15)>
    Public Property IngressNumber() As String
        Get
            Return fIngressNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IngressNumber", fIngressNumber, value)
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

    Dim fPatientName As String
    <Size(200)>
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientCode As String
    <Size(20)>
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
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

    Dim fRadicatedNumber As String
    <Size(50)>
    Public Property RadicatedNumber() As String
        Get
            Return fRadicatedNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RadicatedNumber", fRadicatedNumber, value)
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

    Dim fAccountantAccountCustomers As String
    <Size(30)>
    Public Property AccountantAccountCustomers() As String
        Get
            Return fAccountantAccountCustomers
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountantAccountCustomers", fAccountantAccountCustomers, value)
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

    Dim fPortfolioAge As Integer
    Public Property PortfolioAge() As Integer
        Get
            Return fPortfolioAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioAge", fPortfolioAge, value)
        End Set
    End Property

    Dim fNit As String
    <Size(15)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fTempState As Byte
    Public Property TempState() As Byte
        Get
            Return fTempState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TempState", fTempState, value)
        End Set
    End Property

    Dim fEvaluationDateGlosa As DateTime
    Public Property EvaluationDateGlosa() As DateTime
        Get
            Return fEvaluationDateGlosa
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EvaluationDateGlosa", fEvaluationDateGlosa, value)
        End Set
    End Property

    Dim fCoordinationDateGlosa As DateTime
    Public Property CoordinationDateGlosa() As DateTime
        Get
            Return fCoordinationDateGlosa
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CoordinationDateGlosa", fCoordinationDateGlosa, value)
        End Set
    End Property

    Dim fEvaluationDateReiteration As DateTime
    Public Property EvaluationDateReiteration() As DateTime
        Get
            Return fEvaluationDateReiteration
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EvaluationDateReiteration", fEvaluationDateReiteration, value)
        End Set
    End Property

    Dim fCoordinationDateReiteration As DateTime
    Public Property CoordinationDateReiteration() As DateTime
        Get
            Return fCoordinationDateReiteration
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CoordinationDateReiteration", fCoordinationDateReiteration, value)
        End Set
    End Property

    Dim fStatusTotal As Byte
    Public Property StatusTotal() As Byte
        Get
            Return fStatusTotal
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusTotal", fStatusTotal, value)
        End Set
    End Property

    Dim fImportunityCauseId As GlosasImportunityCausesXpo
    <Association("Glosas_GlosaPortfolioGlosada_References_Glosas_ImportunityCauses")>
    Public Property ImportunityCauseId() As GlosasImportunityCausesXpo
        Get
            Return fImportunityCauseId
        End Get
        Set(ByVal value As GlosasImportunityCausesXpo)
            SetPropertyValue(Of GlosasImportunityCausesXpo)("ImportunityCauseId", fImportunityCauseId, value)
        End Set
    End Property

#End Region

#Region "Association Members"

    Dim fResponsibleEvaluationGlosa As Glosas_ResponsibleXpo
    <Association("Glosas_GlosaPortfolioGlosadaReferencesGlosas_Responsible2")>
    Public Property ResponsibleEvaluationGlosa() As Glosas_ResponsibleXpo
        Get
            Return fResponsibleEvaluationGlosa
        End Get
        Set(ByVal value As Glosas_ResponsibleXpo)
            SetPropertyValue(Of Glosas_ResponsibleXpo)("ResponsibleEvaluationGlosa", fResponsibleEvaluationGlosa, value)
        End Set
    End Property

    Dim fResponsibleCoordinationGlosa As Glosas_ResponsibleXpo
    <Association("Glosas_GlosaPortfolioGlosadaReferencesGlosas_Responsible")>
    Public Property ResponsibleCoordinationGlosa() As Glosas_ResponsibleXpo
        Get
            Return fResponsibleCoordinationGlosa
        End Get
        Set(ByVal value As Glosas_ResponsibleXpo)
            SetPropertyValue(Of Glosas_ResponsibleXpo)("ResponsibleCoordinationGlosa", fResponsibleCoordinationGlosa, value)
        End Set
    End Property

    Dim fResponsibleEvaluationReiteration As Glosas_ResponsibleXpo
    <Association("Glosas_GlosaPortfolioGlosadaReferencesGlosas_Responsible3")>
    Public Property ResponsibleEvaluationReiteration() As Glosas_ResponsibleXpo
        Get
            Return fResponsibleEvaluationReiteration
        End Get
        Set(ByVal value As Glosas_ResponsibleXpo)
            SetPropertyValue(Of Glosas_ResponsibleXpo)("ResponsibleEvaluationReiteration", fResponsibleEvaluationReiteration, value)
        End Set
    End Property

    Dim fResponsibleCoordinationReiteration As Glosas_ResponsibleXpo
    <Association("Glosas_GlosaPortfolioGlosadaReferencesGlosas_Responsible1")>
    Public Property ResponsibleCoordinationReiteration() As Glosas_ResponsibleXpo
        Get
            Return fResponsibleCoordinationReiteration
        End Get
        Set(ByVal value As Glosas_ResponsibleXpo)
            SetPropertyValue(Of Glosas_ResponsibleXpo)("ResponsibleCoordinationReiteration", fResponsibleCoordinationReiteration, value)
        End Set
    End Property

    <Association("Glosas_GlosaObjectionsReceptionDReferencesGlosas_GlosaPortfolioGlosada", GetType(Glosas_GlosaObjectionsReceptionD))>
    Public ReadOnly Property Glosas_GlosaObjectionsReceptionD() As XPCollection(Of Glosas_GlosaObjectionsReceptionD)
        Get
            Return GetCollection(Of Glosas_GlosaObjectionsReceptionD)("Glosas_GlosaObjectionsReceptionD")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class