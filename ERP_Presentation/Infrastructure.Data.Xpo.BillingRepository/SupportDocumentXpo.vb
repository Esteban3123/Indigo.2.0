Imports DevExpress.Xpo

<Persistent("Billing.ViewElectronicSupportDocument")>
Public Class SupportDocumentXpo
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

    Dim fDocumentType As String
    Public Property DocumentType() As String
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Private fEntityId As Integer?
    Public Property EntityId As Integer?
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue("EntityId", fEntityId, value)
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fSupplierThirdPartyId As ThirdPartyXpo
    <Association("SupportDocument_References_SupplierThirdParty")>
    Public Property SupplierThirdPartyId() As ThirdPartyXpo
        Get
            Return fSupplierThirdPartyId
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue(Of ThirdPartyXpo)("SupplierThirdPartyId", fSupplierThirdPartyId, value)
        End Set
    End Property

    Dim fStatusElectronic As Byte
    Public Property StatusElectronic() As Byte
        Get
            Return fStatusElectronic
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusElectronic", fStatusElectronic, value)
        End Set
    End Property

    Dim fStatusNote As Byte
    Public Property StatusNote() As Byte
        Get
            Return fStatusNote
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusNote", fStatusNote, value)
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

    Dim fShippingDate As DateTime
    Public Property ShippingDate() As DateTime
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ShippingDate", fShippingDate, value)
        End Set
    End Property

    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
        End Set
    End Property

#End Region

#Region "Navigation"

    <Association("ElectronicSupportDocumentDetail_References_ElectronicDocument", GetType(DocumentSupportDetailXpo))>
    Public ReadOnly Property DocumentSupportDetail() As XPCollection(Of DocumentSupportDetailXpo)
        Get
            Return GetCollection(Of DocumentSupportDetailXpo)("DocumentSupportDetail")
        End Get
    End Property



#End Region

#Region "Atributes Extends"

    <PersistentAlias("SupplierThirdPartyId.Id")>
    Public ReadOnly Property ThirdPartyId As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ThirdPartyId"))
        End Get
    End Property

    <PersistentAlias("IIF(EntityName = 'AccountPayable','Cuentas por Pagar',IIF(EntityName = 'VoucherTransaction', 'Comprobante de Egreso','Ninguno'))")>
    Public ReadOnly Property EntityNameSource() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("EntityNameSource"))
        End Get
    End Property

    <PersistentAlias("Iif(StatusElectronic = 0, 'Erróneo', StatusElectronic = 1, 'Registrado', StatusElectronic = 2, 'Enviado', StatusElectronic = 3, 'Valido', StatusElectronic = 4, 'Invalido', 'Procesando')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(StatusNote = 0, 'Erróneo', StatusNote = 1, 'Registrado', StatusNote = 2, 'Enviado', StatusNote = 3, 'Valido', StatusNote = 4, 'Invalido', 'Procesando')")>
    Public ReadOnly Property StatusNameNote As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusNameNote"))
        End Get
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class
