
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListHCFARMEPDtoConfirmationUnitDose")>
Partial Public Class ViewListHCFARMEPDtoConfirmationUnitDoseXpo
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

    Dim fID As Integer
    <Key(True)>
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    Dim fServiceName As String
    Public Property ServiceName() As String
        Get
            Return fServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceName", fServiceName, value)
        End Set
    End Property

    Dim fServiceId As Integer
    Public Property ServiceId() As Integer
        Get
            Return fServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceId", fServiceId, value)
        End Set
    End Property

    Dim fServiceDescription As String
    Public Property ServiceDescription() As String
        Get
            Return fServiceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceDescription", fServiceDescription, value)
        End Set
    End Property

    Dim fDosage As Decimal
    Public Property Dosage() As Decimal
        Get
            Return fDosage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Dosage", fDosage, value)
        End Set
    End Property

    Dim fMeasurementUnitId As Integer?
    Public Property MeasurementUnitId() As Integer?
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

    Dim fMeasurementUnitDescription As String
    Public Property MeasurementUnitDescription() As String
        Get
            Return fMeasurementUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitDescription", fMeasurementUnitDescription, value)
        End Set
    End Property

    Dim fMeasurementUnitName As String
    Public Property MeasurementUnitName() As String
        Get
            Return fMeasurementUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitName", fMeasurementUnitName, value)
        End Set
    End Property

    Dim fComponentType As Byte
    Public Property ComponentType() As Byte
        Get
            Return fComponentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComponentType", fComponentType, value)
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

    Dim fAGRUPAQUETE As Guid
    Public Property AGRUPAQUETE() As Guid
        Get
            Return fAGRUPAQUETE
        End Get
        Set(ByVal value As Guid)
            SetPropertyValue(Of Guid)("AGRUPAQUETE", fAGRUPAQUETE, value)
        End Set
    End Property

    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComponentType", fUnitType, value)
        End Set
    End Property

    Dim fMeasurementUnitAbbreviation As String
    Public Property MeasurementUnitAbbreviation() As String
        Get
            Return fMeasurementUnitAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitAbbreviation", fMeasurementUnitAbbreviation, value)
        End Set
    End Property

    Dim fFormulationType As Byte
    Public Property FormulationType() As Byte
        Get
            Return fFormulationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FormulationType", fFormulationType, value)
        End Set
    End Property

    Dim fWeight As Decimal?
    Public Property Weight() As Decimal?
        Get
            Return fWeight
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("Weight", fWeight, value)
        End Set
    End Property


    Dim fWeightMeasureUnit As Integer?
    Public Property WeightMeasureUnit() As Integer?
        Get
            Return fWeightMeasureUnit
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("WeightMeasureUnit", fWeightMeasureUnit, value)
        End Set
    End Property

    Dim fWeightMeasureAbbreviation As String
    Public Property WeightMeasureAbbreviation() As String
        Get
            Return fWeightMeasureAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WeightMeasureAbbreviation", fWeightMeasureAbbreviation, value)
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

    Dim fVolumeMeasureUnit As Integer?
    Public Property VolumeMeasureUnit() As Integer?
        Get
            Return fVolumeMeasureUnit
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("VolumeMeasureUnit", fVolumeMeasureUnit, value)
        End Set
    End Property

    Dim fVolumeMeasureAbbreviation As String
    Public Property VolumeMeasureAbbreviation() As String
        Get
            Return fVolumeMeasureAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VolumeMeasureAbbreviation", fVolumeMeasureAbbreviation, value)
        End Set
    End Property

    Dim fMeasurementUnitDescriptionWeight As String
    Public Property MeasurementUnitDescriptionWeight() As String
        Get
            Return fMeasurementUnitDescriptionWeight
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitDescriptionWeight", fMeasurementUnitDescriptionWeight, value)
        End Set
    End Property

    Dim fMeasurementUnitDescriptionVolume As String
    Public Property MeasurementUnitDescriptionVolume() As String
        Get
            Return fMeasurementUnitDescriptionVolume
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitDescriptionVolume", fMeasurementUnitDescriptionVolume, value)
        End Set
    End Property

    Dim fNPT As Boolean
    Public Property NPT() As Boolean
        Get
            Return fNPT
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NPT", fNPT, value)
        End Set
    End Property

    Dim fNPTId As Integer?
    Public Property NPTId() As Integer?
        Get
            Return fNPTId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("NPTId", fNPTId, value)
        End Set
    End Property
End Class