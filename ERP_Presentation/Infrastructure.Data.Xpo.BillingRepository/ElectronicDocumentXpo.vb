Imports DevExpress.Xpo

<Persistent("Billing.ViewElectronicDocument")>
Public Class ElectronicDocumentXpo
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

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fCustomerPartyId As ThirdPartyXpo
    <Association("ElectronicDocument_References_CustomerThirdParty")>
    Public Property CustomerPartyId() As ThirdPartyXpo
        Get
            Return fCustomerPartyId
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue(Of ThirdPartyXpo)("CustomerPartyId", fCustomerPartyId, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
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

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
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

    Dim fShippingDate As DateTime
    Public Property ShippingDate() As DateTime
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ShippingDate", fShippingDate, value)
        End Set
    End Property

    Dim fPrefix As String
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property

    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fCUFE As String
    Public Property CUFE() As String
        Get
            Return fCUFE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUFE", fCUFE, value)
        End Set
    End Property

    Dim fCenterAttention As String
    Public Property CenterAttention() As String
        Get
            Return fCenterAttention
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttention", fCenterAttention, value)
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

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
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

    Dim fNoteTypeDetail As Boolean
    Public Property NoteTypeDetail() As Boolean
        Get
            Return fNoteTypeDetail
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NoteTypeDetail", fNoteTypeDetail, value)
        End Set
    End Property

#End Region

#Region "Attributes Extends"

    <PersistentAlias("CustomerPartyId.Id")>
    Public ReadOnly Property ThirdPartyId As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ThirdPartyId"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 0, 'Erróneo', Status = 1, 'Registrado', Status = 2, 'Enviado', Status = 3, 'Valido', Status = 4, 'Invalido', 'Procesando')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(
DocumentType = 1, 'Factura EAPB con Contrato', 
DocumentType = 2, 'Factura EAPB Sin Contrato', 
DocumentType = 3, 'Factura Particular', 
DocumentType = 4, 'Factura Capitada', 
DocumentType = 5, 'Control de Capitacion', 
DocumentType = 6, 'Factura Basica', 
DocumentType = 7, 'Factura de Venta de Productos', 
DocumentType = 91, 'Nota Credito', 
DocumentType = 92, 'Nota Debito', 
DocumentType = 93, 'Nota Credito', 
DocumentType = 94, 'Nota Debito', 
DocumentType = 98, 'Nota Debito', 
DocumentType = 99, 'Nota Credito', 
'')")>
    Public ReadOnly Property DocumentTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
        End Get
    End Property

    <PersistentAlias("Concat(Prefix, DocumentNumber)")>
    Public ReadOnly Property DocumentNumberWithPrefix As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentNumberWithPrefix"))
        End Get
    End Property

#End Region

#Region "Navigation"

    <Association("ElectronicDocumentDetail_References_ElectronicDocument", GetType(ElectronicDocumentDetailXpo))>
    Public ReadOnly Property ElectronicDocumentDetails() As XPCollection(Of ElectronicDocumentDetailXpo)
        Get
            Return GetCollection(Of ElectronicDocumentDetailXpo)("ElectronicDocumentDetails")
        End Get
    End Property

    <Association("ElectronicDocumentNotification_References_ElectronicDocument", GetType(ElectronicDocumentNotificationXpo))>
    Public ReadOnly Property ElectronicDocumentNotifications() As XPCollection(Of ElectronicDocumentNotificationXpo)
        Get
            Return GetCollection(Of ElectronicDocumentNotificationXpo)("ElectronicDocumentNotifications")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class