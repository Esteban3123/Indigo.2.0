Imports DevExpress.Xpo

<Persistent("Cost.ViewCostReportDistributionDirectCostDetail")>
Public Class ViewCostReportDistributionDirectCostDetailXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As String
    <Key>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fDistributionDirectCostId As ViewCostReportDistributionDirectCostXpo
    <Association("ViewCostReportDistributionDirectCostDetail_References_ViewCostReportDistributionDirectCost")>
    Public Property DistributionDirectCostId() As ViewCostReportDistributionDirectCostXpo
        Get
            Return fDistributionDirectCostId
        End Get
        Set(ByVal value As ViewCostReportDistributionDirectCostXpo)
            SetPropertyValue(Of ViewCostReportDistributionDirectCostXpo)("DistributionDirectCostId", fDistributionDirectCostId, value)
        End Set
    End Property

    Dim fProductionCenterCodeName As String
    Public Property ProductionCenterCodeName() As String
        Get
            Return fProductionCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionCenterCodeName", fProductionCenterCodeName, value)
        End Set
    End Property

    Dim fCenterType As Byte
    Public Property CenterType() As Byte
        Get
            Return fCenterType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CenterType", fCenterType, value)
        End Set
    End Property

    Dim fCenterTypeName As String
    Public Property CenterTypeName() As String
        Get
            Return fCenterTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterTypeName", fCenterTypeName, value)
        End Set
    End Property

    Dim fMainAccountNumberName As String
    Public Property MainAccountNumberName() As String
        Get
            Return fMainAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumberName", fMainAccountNumberName, value)
        End Set
    End Property

    Dim fCostCenterCodeName As String
    Public Property CostCenterCodeName() As String
        Get
            Return fCostCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCodeName", fCostCenterCodeName, value)
        End Set
    End Property

    Dim fMeasurementUnitCodeName As String
    Public Property MeasurementUnitCodeName() As String
        Get
            Return fMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitCodeName", fMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
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
