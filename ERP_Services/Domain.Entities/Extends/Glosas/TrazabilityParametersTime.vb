Public Class TrazabilityParametersTime

    Private _id As Integer
    Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Private _invoiceNumber As String

    Property InvoiceNumber As String
        Get
            Return _invoiceNumber
        End Get
        Set(value As String)
            _invoiceNumber = value
        End Set
    End Property

    Private _documentDate As DateTime

    Property DocumentDate As DateTime
        Get
            Return _documentDate
        End Get
        Set(value As DateTime)
            _documentDate = value
        End Set
    End Property

    Private _dateResponseDocument As Nullable(Of DateTime)

    Property DateResponseDocument As Nullable(Of DateTime)
        Get
            Return _dateResponseDocument
        End Get
        Set(value As Nullable(Of DateTime))
            _dateResponseDocument = value
        End Set
    End Property

    Private _completeDate As Nullable(Of DateTime)

    Property CompleteDate As Nullable(Of DateTime)
        Get
            Return _completeDate
        End Get
        Set(value As Nullable(Of DateTime))
            _completeDate = value
        End Set
    End Property

    Private _confirmerUser As Nullable(Of Integer)

    Property ConfirmerUser As Nullable(Of Integer)
        Get
            Return _confirmerUser
        End Get
        Set(value As Nullable(Of Integer))
            _confirmerUser = value
        End Set
    End Property

    Private _glosaPortFolio As GlosaPortfolioGlosada

    Property GlosaPortFolio As GlosaPortfolioGlosada
        Get
            Return _glosaPortFolio
        End Get
        Set(value As GlosaPortfolioGlosada)
            _glosaPortFolio = value
        End Set
    End Property

    Private _maxTimeResponse As Nullable(Of Byte)
    Property MaxTimeResponse As Nullable(Of Byte)
        Get
            Return _maxTimeResponse
        End Get
        Set(value As Nullable(Of Byte))
            _maxTimeResponse = value
        End Set
    End Property

    Private _maxTimeExtemporaneousGlosa As Nullable(Of Byte)
    Property MaxTimeExtemporaneousGlosa As Nullable(Of Byte)
        Get
            Return _maxTimeExtemporaneousGlosa
        End Get
        Set(value As Nullable(Of Byte))
            _maxTimeExtemporaneousGlosa = value
        End Set
    End Property

    Private _maxTimeSendingDocumentResponse As Nullable(Of Byte)
    Property MaxTimeSendingDocumentResponse As Nullable(Of Byte)
        Get
            Return _maxTimeSendingDocumentResponse
        End Get
        Set(value As Nullable(Of Byte))
            _maxTimeSendingDocumentResponse = value
        End Set
    End Property

End Class
