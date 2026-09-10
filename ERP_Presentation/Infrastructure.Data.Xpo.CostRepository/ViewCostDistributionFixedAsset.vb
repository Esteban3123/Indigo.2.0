#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.ViewCostDistributionFixedAsset")>
Public Class ViewCostDistributionFixedAsset
    Inherits XPLiteObject

#Region "Members"

    Dim fFixedAssetPhysicalAssetId As Integer
    <Key(True)>
    Public Property FixedAssetPhysicalAssetId() As Integer
        Get
            Return fFixedAssetPhysicalAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
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

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property

    Dim fFixedAssetItemCodeDescription As String
    Public Property FixedAssetItemCodeDescription() As String
        Get
            Return fFixedAssetItemCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FixedAssetItemCodeDescription", fFixedAssetItemCodeDescription, value)
        End Set
    End Property

    Dim fFixedAssetLocationCodeName As String
    Public Property FixedAssetLocationCodeName() As String
        Get
            Return fFixedAssetLocationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FixedAssetLocationCodeName", fFixedAssetLocationCodeName, value)
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

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Plate, ' - ', FixedAssetItemCodeDescription)")>
    Public ReadOnly Property FixedAssetName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FixedAssetName"))
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
