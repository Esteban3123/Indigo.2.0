
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewUsersRolsCM")>
Public Class ViewUsersRolsCMXpo
    Inherits XPLiteObject

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

    Dim fCampaignDetailId As Integer
    <Persistent("CampaignDetailId")>
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fUserId As Integer
    <Persistent("UserId")>
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fNombre As String
    <Persistent("Nombre")>
    Public Property Nombre() As String
        Get
            Return fNombre
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nombre", fNombre, value)
        End Set
    End Property

    Dim fUsuario As String
    <Persistent("Usuario")>
    Public Property Usuario() As String
        Get
            Return fUsuario
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Usuario", fUsuario, value)
        End Set
    End Property

    Dim fUserRole As Byte
    <Persistent("UserRole")>
    Public Property UserRole() As Byte
        Get
            Return fUserRole
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UserRole", fUserRole, value)
        End Set
    End Property

    <PersistentAlias("IIF(UserRole=1,'Supervisor',UserRole=2,'QF Producción', UserRole=3,'Auxiliar de Central de Mezclas', 'Director Técnico')")>
    Public ReadOnly Property UserRoleName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("UserRoleName"))
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
