#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Billing.InvoiceEntityCapitatedDistribution")> _
Partial Public Class InvoiceEntityCapitatedDistributionXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fInvoiceEntityCapitatedId As InvoiceEntityCapitatedXpo
    <Association("Billing_InvoiceEntityCapitatedDistribution_References_Billing_InvoiceEntityCapitated")> _
    Public Property InvoiceEntityCapitatedId() As InvoiceEntityCapitatedXpo
        Get
            Return fInvoiceEntityCapitatedId
        End Get
        Set(ByVal value As InvoiceEntityCapitatedXpo)
            SetPropertyValue(Of InvoiceEntityCapitatedXpo)("InvoiceEntityCapitatedId", fInvoiceEntityCapitatedId, value)
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

    Dim fTotalControlValue As Decimal
    Public Property TotalControlValue() As Decimal
        Get
            Return fTotalControlValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalControlValue", fTotalControlValue, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', Status = 4, 'Reversado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

End Class