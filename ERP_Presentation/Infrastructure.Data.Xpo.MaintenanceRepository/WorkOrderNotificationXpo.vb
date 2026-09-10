Imports DevExpress.Xpo

<Persistent("Maintenance.WorkOrderNotification")>
Public Class WorkOrderNotificationXpo
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

    Dim fWorkOrderId As Integer
    Public Property WorkOrderId() As Integer
        Get
            Return fWorkOrderId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkOrderId", fWorkOrderId, value)
        End Set
    End Property

    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
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

#Region "Attributes Extends"

    <PersistentAlias("Iif(
Type = 1, 'Orden de Trabajo Asignada',
Type = 2, 'Orden de Trabajo Terminada',
Type = 3, 'Orden de Trabajo Anulada',
Type = 4, 'Orden de Trabajo Aceptada',
Type = 5, 'Orden de Trabajo Rechazada')")>
    Public ReadOnly Property TypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

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