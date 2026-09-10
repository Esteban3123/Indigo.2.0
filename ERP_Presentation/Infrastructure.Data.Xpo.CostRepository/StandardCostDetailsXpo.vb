Imports DevExpress.Xpo

<Persistent("Cost.StandarCostDetails")>
Public Class StandardCostDetailsXpo
    Inherits XPLiteObject

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

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

    Dim fStandarCostId As AverageStandardCostXpo
    <Association("Cost_StandarCostDetails_References_StandardCost")>
    Public Property StandarCostId As AverageStandardCostXpo
        Get
            Return fStandarCostId
        End Get
        Set(value As AverageStandardCostXpo)
            SetPropertyValue("StandarCostId", fStandarCostId, value)
        End Set
    End Property

    Dim fStandarCostValue As Decimal
    Public Property StandarCostValue() As Decimal
        Get
            Return fStandarCostValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("StandarCostValue", fStandarCostValue, value)
        End Set
    End Property

    Dim fFixedAssetValue As Decimal
    Public Property FixedAssetValue() As Decimal
        Get
            Return fFixedAssetValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("FixedAssetValue", fFixedAssetValue, value)
        End Set
    End Property

    Dim fPayrollValue As Decimal
    Public Property PayrollValue() As Decimal
        Get
            Return fPayrollValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("PayrollValue", fPayrollValue, value)
        End Set
    End Property

    Dim fInventoryValue As Decimal
    Public Property InventoryValue() As Decimal
        Get
            Return fInventoryValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("InventoryValue", fInventoryValue, value)
        End Set
    End Property

    Dim fAdditionalCost As Decimal
    Public Property AdditionalCost() As Decimal
        Get
            Return fAdditionalCost
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("AdditionalCost", fAdditionalCost, value)
        End Set
    End Property

    Dim fCostActivityId As CostActivityXpo
    <Association("Cost_StandarCostDetails_References_CostActivity")>
    Public Property CostActivityId() As CostActivityXpo
        Get
            Return fCostActivityId
        End Get
        Set(value As CostActivityXpo)
            SetPropertyValue("CostActivityId", fCostActivityId, value)
        End Set
    End Property

#End Region

End Class
