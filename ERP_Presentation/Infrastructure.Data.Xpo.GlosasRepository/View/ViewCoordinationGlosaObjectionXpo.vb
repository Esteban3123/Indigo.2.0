Imports DevExpress.Xpo

<Persistent("Glosas.ViewCoordinationGlosaObjection")>
Public Class ViewCoordinationGlosaObjectionXpo
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

    Dim fGlosaObjectionsReceptionCId As Integer
    Public Property GlosaObjectionsReceptionCId() As Integer
        Get
            Return fGlosaObjectionsReceptionCId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GlosaObjectionsReceptionCId", fGlosaObjectionsReceptionCId, value)
        End Set
    End Property

    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fDocumentType As String
    Public Property DocumentType() As String
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fPortfolioGlosaId As Integer
    Public Property PortfolioGlosaId() As Integer
        Get
            Return fPortfolioGlosaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioGlosaId", fPortfolioGlosaId, value)
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

    Dim fIngressNumber As String
    Public Property IngressNumber() As String
        Get
            Return fIngressNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IngressNumber", fIngressNumber, value)
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

    Dim fState As String
    Public Property State() As String
        Get
            Return fState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("State", fState, value)
        End Set
    End Property

    Dim fStatePortfolio As Byte
    Public Property StatePortfolio() As Byte
        Get
            Return fStatePortfolio
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatePortfolio", fStatePortfolio, value)
        End Set
    End Property

    Dim fStateRecord As Boolean
    Public Property StateRecord() As Boolean
        Get
            Return fStateRecord
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StateRecord", fStateRecord, value)
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

    Dim fValueReiterated As Decimal
    Public Property ValueReiterated() As Decimal
        Get
            Return fValueReiterated
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueReiterated", fValueReiterated, value)
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

    Dim fValueAcceptedSecondInstance As Decimal
    Public Property ValueAcceptedSecondInstance() As Decimal
        Get
            Return fValueAcceptedSecondInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedSecondInstance", fValueAcceptedSecondInstance, value)
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

    Dim fImportunityCauseId As Integer?
    Public Property ImportunityCauseId() As Integer?
        Get
            Return fImportunityCauseId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ImportunityCauseId", fImportunityCauseId, value)
        End Set
    End Property

    Dim fImportunityCauseCodeName As String
    Public Property ImportunityCauseCodeName() As String
        Get
            Return fImportunityCauseCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ImportunityCauseCodeName", fImportunityCauseCodeName, value)
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

#End Region

#Region "Builder"

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
