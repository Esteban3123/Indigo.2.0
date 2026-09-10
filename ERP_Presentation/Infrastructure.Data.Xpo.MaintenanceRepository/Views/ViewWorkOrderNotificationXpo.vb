Imports DevExpress.Xpo

<Persistent("Maintenance.ViewWorkOrderNotification")>
Public Class ViewWorkOrderNotificationXpo
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

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fEntityId As Integer?
    Public Property EntityId() As Integer?
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fWorkOrderId As Integer?
    Public Property WorkOrderId() As Integer?
        Get
            Return fWorkOrderId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("WorkOrderId", fWorkOrderId, value)
        End Set
    End Property

    Dim fTypeName As String
    Public Property TypeName() As String
        Get
            Return fTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fTypeName, value)
        End Set
    End Property

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName", fThirdPartyNitName, value)
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

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
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

    Dim fShippingDate As DateTime
    Public Property ShippingDate() As DateTime
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ShippingDate", fShippingDate, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class