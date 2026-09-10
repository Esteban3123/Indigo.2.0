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

<Persistent("MixingStation.PackagePersonalizedDetail")>
Partial Public Class PackagePersonalizedDetailXpo
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

    Dim fPackagePersonalizedId As Integer
    Public Property PackagePersonalizedId() As Integer
        Get
            Return fPackagePersonalizedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackagePersonalizedId", fPackagePersonalizedId, value)
        End Set
    End Property

    Dim fProductId As MixingStationProductXpo
    <Association("PackagePersonalizedDetailReferencesProduct")>
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
    <Association("PackagePersonalizedDetailReferencesMeasurementUnit")>
    Public Property MeasurementUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

    Dim fAtcId As MixingStationATCXpo
    <Association("PackagePersonalizedDetailReferencesATC")>
    Public Property AtcId() As MixingStationATCXpo
        Get
            Return fAtcId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fSupplieId As MixingStationSupplieXpo
    <Association("PackagePersonalizedDetailReferencesSupplie")>
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

    <PersistentAlias("iif(ComponentType=1, 'Medicamento', iif(SupplieId is not null, SupplieId.CodeName, ProductId.CodeName))")>
    Public ReadOnly Property ComponentTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ComponentTypeName"))
        End Get
    End Property

    <PersistentAlias("iif(AtcId is not null, AtcId.CodeName, iif(ComponentType=2, 'Insumo', 'Producto'))")>
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

    Dim fThinner As Boolean
    Public Property Thinner() As Boolean
        Get
            Return fThinner
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Thinner", fThinner, value)
        End Set
    End Property

    Dim fComplementaryMedicine As Boolean?
    Public Property ComplementaryMedicine() As Boolean?
        Get
            Return fComplementaryMedicine
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("ComplementaryMedicine", fComplementaryMedicine, value)
        End Set
    End Property

End Class