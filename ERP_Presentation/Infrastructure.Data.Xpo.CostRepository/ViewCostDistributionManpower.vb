#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.ViewCostDistributionManpower")>
Public Class ViewCostDistributionManpower
    Inherits XPLiteObject

#Region "Members"

    Dim fUUID As String
    <Key(True)>
    Public Property UUID() As String
        Get
            Return fUUID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UUID", fUUID, value)
        End Set
    End Property

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Integer
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property

    Dim fManpowerType As Integer
    Public Property ManpowerType() As Integer
        Get
            Return fManpowerType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManpowerType", fManpowerType, value)
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

    Dim fGroupCodeName As String
    Public Property GroupCodeName() As String
        Get
            Return fGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCodeName", fGroupCodeName, value)
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

    Dim fPositionCodeName As String
    Public Property PositionCodeName() As String
        Get
            Return fPositionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PositionCodeName", fPositionCodeName, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(ManpowerType = 1, 'Empleado', ManpowerType = 2, 'Contratista', '')")>
    Public ReadOnly Property ManpowerTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ManpowerTypeName"))
        End Get
    End Property

    <PersistentAlias("concat(ManpowerType, '-', EntityId)")>
    Public ReadOnly Property ManpowerTypeId As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ManpowerTypeId"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
