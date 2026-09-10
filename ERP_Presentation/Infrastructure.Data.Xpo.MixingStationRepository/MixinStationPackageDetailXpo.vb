'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 07-06-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.PackageDetail")>
Partial Public Class MixinStationPackageDetailXpo
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

    Dim fPackageId As MixinStationPackageXpo
    <Association("PackageDetailReferencesPackage")>
    Public Property PackageId() As MixinStationPackageXpo
        Get
            Return fPackageId
        End Get
        Set(ByVal value As MixinStationPackageXpo)
            SetPropertyValue(Of MixinStationPackageXpo)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fProductId As MixingStationProductXpo
    <Association("PackageDetailReferencesProduct")>
    Public Property ProductId() As MixingStationProductXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As MixingStationProductXpo)
            SetPropertyValue(Of MixingStationProductXpo)("ProductId", fProductId, value)
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

    Dim fMeasurementUnitId As MixingStationMeasurementUnitXpo
    <Association("PackageDetailReferencesMeasurementUnit")>
    Public Property MeasurementUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

    Private fVolumeMeasureUnitObj As MixingStationMeasurementUnitXpo
    <Persistent("VolumeMeasureUnit")>
    <Association("PackageDetailReferencesVolumeMeasurementUnit")>
    Public Property VolumeMeasureUnitObj() As MixingStationMeasurementUnitXpo
        Get
            Return fVolumeMeasureUnitObj
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue("VolumeMeasureUnitObj", fVolumeMeasureUnitObj, value)
        End Set
    End Property

    Dim fAtcId As MixingStationATCXpo
    <Association("PackageDetailReferencesATC")>
    Public Property AtcId() As MixingStationATCXpo
        Get
            Return fAtcId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fDilution As Decimal
    Public Property Dilution() As Decimal
        Get
            Return fDilution
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Dilution", fDilution, value)
        End Set
    End Property

    Dim fConcentration As Decimal
    Public Property Concentration() As Decimal
        Get
            Return fConcentration
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fAmountTime As Integer
    Public Property AmountTime() As Integer
        Get
            Return fAmountTime
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("AmountTime", fAmountTime, value)
        End Set
    End Property

    Dim fTimeUnit As Byte
    Public Property TimeUnit() As Byte
        Get
            Return fTimeUnit
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("TimeUnit", fTimeUnit, value)
        End Set
    End Property

    Dim fNPTItemOrder As Byte?
    Public Property NPTItemOrder() As Byte?
        Get
            Return fNPTItemOrder
        End Get
        Set(value As Byte?)
            SetPropertyValue(Of Byte?)("NPTItemOrder", fNPTItemOrder, value)
        End Set
    End Property

    Dim fVolumeTotal As Decimal
    Public Property VolumeTotal() As Decimal
        Get
            Return fVolumeTotal
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotal", fVolumeTotal, value)
        End Set
    End Property

    Dim fSupplieId As MixingStationSupplieXpo
    <Association("PackageDetailReferencesSupplie")>
    Public Property SupplieId() As MixingStationSupplieXpo
        Get
            Return fSupplieId
        End Get
        Set(ByVal value As MixingStationSupplieXpo)
            SetPropertyValue(Of MixingStationSupplieXpo)("SupplieId", fSupplieId, value)
        End Set
    End Property

    Dim fComponentType As Integer
    Public Property ComponentType() As Integer
        Get
            Return fComponentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ComponentType", fComponentType, value)
        End Set
    End Property


    <PersistentAlias("IIf(ComponentType = 1,
    IIf(MainMedicine = True, 
        'Medicamento ppal',
        IIf(Thinner = True, 'Reconstituyente', 'Vehiculo')),
    IIf(ComponentType = 2, 'Insumo', 'Producto'))")>
    Public ReadOnly Property ComponentTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ComponentTypeName"))
        End Get
    End Property

    <PersistentAlias("iif(ComponentType = 1, AtcId.CodeName, iif(ComponentType = 2, SupplieId.CodeName, ProductId.CodeName))")>
    Public ReadOnly Property ComponentName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ComponentName"))
        End Get
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

    Dim fComplementaryMedicine As Boolean
    ''' <summary>
    ''' Indica si el detalle es un medicamento complementario (solo para Dashboard Confirmación Dosis Unitarias)
    ''' Esta propiedad NO se persiste en la tabla PackageDetail, solo existe para uso en memoria.
    ''' El guardado real se hace en PackagePersonalizedDetail cuando es paquete personalizado.
    ''' </summary>
    <NonPersistent>
    Public Property ComplementaryMedicine() As Boolean
        Get
            Return fComplementaryMedicine
        End Get
        Set(ByVal value As Boolean)
            fComplementaryMedicine = value
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

    Dim fVolume As Decimal?
    Public Property Volume() As Decimal?
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("Volume", fVolume, value)
        End Set
    End Property

    Private fPreparationType As Byte?
    Public Property PreparationType() As Byte?
        Get
            Return fPreparationType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("PreparationType", fPreparationType, value)
        End Set
    End Property

    <PersistentAlias("VolumeMeasureUnitObj.Id")>
    Public ReadOnly Property VolumeMeasureUnit() As Integer?
        Get
            If VolumeMeasureUnit IsNot Nothing Then
                Return CType(Convert.ChangeType(EvaluateAlias("VolumeMeasureUnit"), Nullable.GetUnderlyingType(GetType(Integer?))), Integer?)
            End If
        End Get
    End Property

End Class