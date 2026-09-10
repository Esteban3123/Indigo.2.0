Public Class SubLargeDetail

    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(value As Integer)
            fId = value
        End Set
    End Property

    Dim fIsInvoiced As Boolean
    Public Property IsInvoiced As Boolean
        Get
            Return fIsInvoiced
        End Get
        Set(value As Boolean)
            fIsInvoiced = value
        End Set
    End Property

    Dim fInvoiceId As Integer
    Public Property InvoiceId As Integer
        Get
            Return fInvoiceId
        End Get
        Set(value As Integer)
            fInvoiceId = value
        End Set
    End Property

    Dim fRevenueControlDetailId As Integer
    Public Property RevenueControlDetailId As Integer
        Get
            Return fRevenueControlDetailId
        End Get
        Set(value As Integer)
            fRevenueControlDetailId = value
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber As String
        Get
            Return fAdmissionNumber
        End Get
        Set(value As String)
            fAdmissionNumber = value
        End Set
    End Property

    Dim fLiquidateMasterAccount As Boolean
    Public Property LiquidateMasterAccount As Boolean
        Get
            Return fLiquidateMasterAccount
        End Get
        Set(value As Boolean)
            fLiquidateMasterAccount = value
        End Set
    End Property

    Dim fIsMasterAccount As Boolean
    Public Property IsMasterAccount As Boolean
        Get
            Return fIsMasterAccount
        End Get
        Set(value As Boolean)
            fIsMasterAccount = value
        End Set
    End Property

End Class
