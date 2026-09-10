Imports DevExpress.Xpo

<Persistent("Billing.InvoiceEntityCapitated")> _
Partial Public Class InvoiceEntityCapitatedXpo
    Inherits XPLiteObject

#Region"Members"

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
    <Size(20)> _
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

    Dim fCareGroupId As CareGroupXpo
    <Association("Billing_InvoiceEntityCapitatedReferencesContract_CareGroup")> _
    Public Property CareGroupId() As CareGroupXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As CareGroupXpo)
            SetPropertyValue(Of CareGroupXpo)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fUserNumber As Integer
    Public Property UserNumber() As Integer
        Get
            Return fUserNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserNumber", fUserNumber, value)
        End Set
    End Property

    Dim fUserValue As Decimal
    Public Property UserValue() As Decimal
        Get
            Return fUserValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UserValue", fUserValue, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fInvoiceId As InvoiceXpo
    <Association("Billing_InvoiceEntityCapitatedReferencesBilling_Invoice")> _
    Public Property InvoiceId() As InvoiceXpo
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As InvoiceXpo)
            SetPropertyValue(Of InvoiceXpo)("InvoiceId", fInvoiceId, value)
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

    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
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

    Dim fPreviousRIPSInvoice As InvoiceEntityCapitatedXpo
    <Association("Billing_InvoiceEntityCapitatedWithHimself")>
    Public Property PreviousRIPSInvoice As InvoiceEntityCapitatedXpo
        Get
            Return fPreviousRIPSInvoice
        End Get
        Set(value As InvoiceEntityCapitatedXpo)
            SetPropertyValue(Of InvoiceEntityCapitatedXpo)("PreviousRIPSInvoice", fPreviousRIPSInvoice, value)
        End Set
    End Property

    Dim fInvoicePeriod As Byte?
    Public Property InvoicePeriod() As Byte?
        Get
            Return fInvoicePeriod
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("InvoicePeriod", fInvoicePeriod, value)
        End Set
    End Property
#End Region

#Region "Custom Members"

    <PersistentAlias("iif(Status = 1,'Registrado',iif(Status = 2,'Confirmado', iif(Status = 3, 'Anulado', iif(Status = 5,'Confirmado RIPS Final','Reversado'))))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Billing_InvoiceEntityCapitatedDistribution_References_Billing_InvoiceEntityCapitated", GetType(InvoiceEntityCapitatedDistributionXpo))>
    Public ReadOnly Property InvoiceEntityCapitatedDistributions() As XPCollection(Of InvoiceEntityCapitatedDistributionXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedDistributionXpo)("InvoiceEntityCapitatedDistributions")
        End Get
    End Property

    <Association("Billing_InvoiceEntityCapitatedWithHimself", GetType(InvoiceEntityCapitatedXpo))>
    Public ReadOnly Property PreviousInvoices() As XPCollection(Of InvoiceEntityCapitatedXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedXpo)("PreviousInvoices")
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