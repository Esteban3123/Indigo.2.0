Imports DevExpress.Xpo

<Persistent("MixingStation.ExternalPatientPreparationDetail")>
Public Class ExternalPatientPreparationDetailXpo
    Inherits XPLiteObject

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

    Dim fExternalPatientPreparationId As ExternalPatientPreparationXpo
    <Association("ExternalPatientPreparationDetail_Reference_ExternalPatientPreparation")>
    Public Property ExternalPatientPreparationId() As ExternalPatientPreparationXpo
        Get
            Return fExternalPatientPreparationId
        End Get
        Set(ByVal value As ExternalPatientPreparationXpo)
            SetPropertyValue(Of ExternalPatientPreparationXpo)("ExternalPatientPreparationId", fExternalPatientPreparationId, value)
        End Set
    End Property

    Dim fitemType As Integer
    Public Property itemType() As Integer
        Get
            Return fitemType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("itemType", fitemType, value)
        End Set
    End Property

    Dim fAtcId As MixingStationATCXpo
    <Association("ExternalPatientPreparation_Reference_ATC")>
    Public Property AtcId() As MixingStationATCXpo
        Get
            Return fAtcId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fSupplieId As MixingStationSupplieXpo
    <Association("ExternalPatientPreparationDetail_Reference_InventorySupplie")>
    Public Property SupplieId() As MixingStationSupplieXpo
        Get
            Return fSupplieId
        End Get
        Set(ByVal value As MixingStationSupplieXpo)
            SetPropertyValue(Of MixingStationSupplieXpo)("SupplieId", fSupplieId, value)
        End Set
    End Property

    Dim fProductId As MixingStationProductXpo
    <Association("ExternalPatientPreparationDetail_Reference_InventoryProduct")>
    Public Property ProductId() As MixingStationProductXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As MixingStationProductXpo)
            SetPropertyValue(Of MixingStationProductXpo)("ProductId", fProductId, value)
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

    Dim fQuantity As Decimal?
    Public Property Quantity() As Decimal?
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fMeasurementUnitId As MixingStationMeasurementUnitXpo
    <Association("ExternalPatientPreparationDetail_Reference_MeasurementUnit")>
    Public Property MeasurementUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("MeasurementUnitId", fMeasurementUnitId, value)
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

    Dim fVolumeMeasureUnitId As MixingStationMeasurementUnitXpo
    <Association("ExternalPatientPreparationDetail_Reference_VolumeMeasureUnit")>
    Public Property VolumeMeasureUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fVolumeMeasureUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("VolumeMeasureUnitId", fVolumeMeasureUnitId, value)
        End Set
    End Property

#End Region

End Class
