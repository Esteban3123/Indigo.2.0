Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("InteropCost.ViewLogisticsProductionCenterRecordDetail")> _
Public Class InteropCostViewLogisticsProductionCenterRecordDetailReportXpo
    Inherits XPLiteObject
    Dim fId As Long
    <Key(True)> _
    Public Property Id() As Long
        Get
            Return fId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Id", fId, value)
        End Set
    End Property
    Dim fCenterRecordId As Integer
    Public Property CenterRecordId() As Integer
        Get
            Return fCenterRecordId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CenterRecordId", fCenterRecordId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fProductionCenter As String
    <Size(123)> _
    Public Property ProductionCenter() As String
        Get
            Return fProductionCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionCenter", fProductionCenter, value)
        End Set
    End Property
    Dim fMeasurementUnit As String
    <Size(123)> _
    Public Property MeasurementUnit() As String
        Get
            Return fMeasurementUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnit", fMeasurementUnit, value)
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
