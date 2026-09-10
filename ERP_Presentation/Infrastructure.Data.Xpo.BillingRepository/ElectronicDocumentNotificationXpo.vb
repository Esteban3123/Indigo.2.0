Imports DevExpress.Xpo

<Persistent("Billing.ElectronicDocumentNotification")>
Public Class ElectronicDocumentNotificationXpo
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

    Dim fElectronicDocumentId As ElectronicDocumentXpo
    <Association("ElectronicDocumentNotification_References_ElectronicDocument")>
    Public Property ElectronicDocumentId() As ElectronicDocumentXpo
        Get
            Return fElectronicDocumentId
        End Get
        Set(ByVal value As ElectronicDocumentXpo)
            SetPropertyValue(Of ElectronicDocumentXpo)("ElectronicDocumentId", fElectronicDocumentId, value)
        End Set
    End Property

    Dim fEmail As String
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Email", fEmail, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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

    Dim fShippingDate As DateTime?
    Public Property ShippingDate() As DateTime?
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ShippingDate", fShippingDate, value)
        End Set
    End Property

#End Region

#Region "Attributes Extends"

    <PersistentAlias("Iif(Status = 1, 'Enviado', 'Sin Enviar')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class