Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("InteropCost.LogisticsProductionCenterRecordDetail")> _
Public Class InteropCostLogisticsProductionCenterRecordDetailReportXpo
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
    Dim fLogisticsProductionCenterRecordId As InteropCostLogisticsProductionCenterRecordReportXpo
    <Association("InteropCost_LogisticsProductionCenterRecordDetailReferencesInteropCost_LogisticsProductionCenterRecord")> _
    Public Property LogisticsProductionCenterRecordId() As InteropCostLogisticsProductionCenterRecordReportXpo
        Get
            Return fLogisticsProductionCenterRecordId
        End Get
        Set(ByVal value As InteropCostLogisticsProductionCenterRecordReportXpo)
            SetPropertyValue(Of InteropCostLogisticsProductionCenterRecordReportXpo)("LogisticsProductionCenterRecordId", fLogisticsProductionCenterRecordId, value)
        End Set
    End Property
    Dim fProductionCenterId As InteropCostProductionCenterReportXpo
    <Association("InteropCost_LogisticsProductionCenterRecordDetailReferencesInteropCost_ProductionCenter")> _
    Public Property ProductionCenterId() As InteropCostProductionCenterReportXpo
        Get
            Return fProductionCenterId
        End Get
        Set(ByVal value As InteropCostProductionCenterReportXpo)
            SetPropertyValue(Of InteropCostProductionCenterReportXpo)("ProductionCenterId", fProductionCenterId, value)
        End Set
    End Property
    Dim fInventoryMeasurementUnitId As Integer
    Public Property InventoryMeasurementUnitId() As Integer
        Get
            Return fInventoryMeasurementUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryMeasurementUnitId", fInventoryMeasurementUnitId, value)
        End Set
    End Property
    Dim fCount As Decimal
    Public Property Count() As Decimal
        Get
            Return fCount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Count", fCount, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
