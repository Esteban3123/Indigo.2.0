Imports DevExpress.Xpo

<Persistent("Maintenance.ViewMaintenanceResponsibleItemCatalogs")>
Public Class ViewMaintenanceResponsibleItemCatalogsXpo
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

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fResponsibleRole As Byte
    Public Property ResponsibleRole() As Byte
        Get
            Return fResponsibleRole
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ResponsibleRole", fResponsibleRole, value)
        End Set
    End Property

    Dim fItemCatalogIds As String
    Public Property ItemCatalogIds() As String
        Get
            Return fItemCatalogIds
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCatalogIds", fItemCatalogIds, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("CONCAT(ThirdPartyNit, ' - ', ThirdPartyName)")>
    Public ReadOnly Property ThirdPartyNitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ThirdPartyNitName"))
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
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
