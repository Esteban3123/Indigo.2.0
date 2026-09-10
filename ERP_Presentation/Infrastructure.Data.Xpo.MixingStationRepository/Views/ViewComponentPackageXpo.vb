'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldán
' Created          : 2021-09-07
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewComponentPackage")>
Partial Public Class ViewComponentPackageXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fPackageId As Integer
    Public Property PackageId() As Integer
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fConcentrationMeasurementUnitCodeName As String
    Public Property ConcentrationMeasurementUnitCodeName() As String
        Get
            Return fConcentrationMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConcentrationMeasurementUnitCodeName", fConcentrationMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fConcentrationMeasurementUnitId As Integer?
    Public Property ConcentrationMeasurementUnitId() As Integer?
        Get
            Return fConcentrationMeasurementUnitId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ConcentrationMeasurementUnitId", fConcentrationMeasurementUnitId, value)
        End Set
    End Property

    Dim fVolumeMeasurementUnitCodeName As String
    Public Property VolumeMeasurementUnitCodeName() As String
        Get
            Return fVolumeMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VolumeMeasurementUnitCodeName", fVolumeMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fVolumeTotalOrderMeasurementUnitId As Integer?
    Public Property VolumeTotalOrderMeasurementUnitId() As Integer?
        Get
            Return fVolumeTotalOrderMeasurementUnitId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("VolumeTotalOrderMeasurementUnitId", fVolumeTotalOrderMeasurementUnitId, value)
        End Set
    End Property

    Dim fAtcId As Integer?
    Public Property AtcId() As Integer?
        Get
            Return fAtcId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fSupplyId As Integer?
    Public Property SupplyId() As Integer?
        Get
            Return fSupplyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("SupplyId", fSupplyId, value)
        End Set
    End Property

    Dim fProductId As Integer?
    Public Property ProductId() As Integer?
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ProductId", fProductId, value)
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

    Dim fVolumeMeasurementUnitDetailCodeName As String
    Public Property VolumeMeasurementUnitDetailCodeName() As String
        Get
            Return fVolumeMeasurementUnitDetailCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VolumeMeasurementUnitDetailCodeName", fVolumeMeasurementUnitDetailCodeName, value)
        End Set
    End Property

    Dim fComponentTypeName As String
    Public Property ComponentTypeName() As String
        Get
            Return fComponentTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ComponentTypeName", fComponentTypeName, value)
        End Set
    End Property

    Dim fQuantity As Decimal
    Public Property Quantity() As Decimal
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fComponentType As Byte?
    Public Property ComponentType() As Byte?
        Get
            Return fComponentType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("ComponentType", fComponentType, value)
        End Set
    End Property

    Dim fThinner As Boolean
    Public Property Thinner() As Boolean
        Get
            Return fThinner
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Thinner", fThinner, value)
        End Set
    End Property

    Dim fVehicle As Boolean
    Public Property Vehicle() As Boolean
        Get
            Return fVehicle
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Vehicle", fVehicle, value)
        End Set
    End Property

    Dim fMainMedicine As Boolean
    Public Property MainMedicine() As Boolean
        Get
            Return fMainMedicine
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MainMedicine", fMainMedicine, value)
        End Set
    End Property

End Class