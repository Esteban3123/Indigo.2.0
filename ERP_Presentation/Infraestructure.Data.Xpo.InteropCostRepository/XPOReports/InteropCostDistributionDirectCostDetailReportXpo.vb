Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("InteropCost.DistributionDirectCostDetail")> _
Public Class InteropCostDistributionDirectCostDetailReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fDistributionDirectCostId As InteropCostDistributionDirectCostReportXpo
    <Association("InteropCost_DistributionDirectCostDetailReferencesInteropCost_DistributionDirectCost")> _
    Public Property DistributionDirectCostId() As InteropCostDistributionDirectCostReportXpo
        Get
            Return fDistributionDirectCostId
        End Get
        Set(ByVal value As InteropCostDistributionDirectCostReportXpo)
            SetPropertyValue(Of InteropCostDistributionDirectCostReportXpo)("DistributionDirectCostId", fDistributionDirectCostId, value)
        End Set
    End Property
    Dim fProductionCenterId As InteropCostProductionCenterReportXpo
    <Association("InteropCost_DistributionDirectCostDetailReferencesInteropCost_ProductionCenter")> _
    Public Property ProductionCenterId() As InteropCostProductionCenterReportXpo
        Get
            Return fProductionCenterId
        End Get
        Set(ByVal value As InteropCostProductionCenterReportXpo)
            SetPropertyValue(Of InteropCostProductionCenterReportXpo)("ProductionCenterId", fProductionCenterId, value)
        End Set
    End Property
    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fMeasurementUnitId As InventoryInventoryMeasurementUnitReportXpo
    <Association("InteropCost_DistributionDirectCostDetailReferencesInventory_InventoryMeasurementUnit")> _
    Public Property MeasurementUnitId() As InventoryInventoryMeasurementUnitReportXpo
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As InventoryInventoryMeasurementUnitReportXpo)
            SetPropertyValue(Of InventoryInventoryMeasurementUnitReportXpo)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
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


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
