'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Inventory.InventoryMeasurementUnit")>
Partial Public Class MixingStationMeasurementUnitXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitType", fUnitType, value)
        End Set
    End Property

    Dim fAbbreviation As String
    Public Property Abbreviation() As String
        Get
            Return fAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("PackageDetailReferencesMeasurementUnit", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailXpo")
        End Get
    End Property

    <Association("PackageDetailReferencesReconstitutionVolumeMeasurementUnit", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailReconstitutionVolumeXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailVolumeXpo")
        End Get
    End Property

    <Association("PackageDetailReferencesVehiculeVolumeMeasurementUnit", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailVehiculeVolumeXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailVolumeXpo")
        End Get
    End Property

    <Association("PackageDetailReferencesVolumeMeasurementUnit", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailVolumeXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailVolumeXpo")
        End Get
    End Property

    <Association("PackagePersonalizedDetailReferencesMeasurementUnit", GetType(PackagePersonalizedDetailXpo))>
    Public ReadOnly Property PackagePersonalizedDetailXpo() As XPCollection(Of PackagePersonalizedDetailXpo)
        Get
            Return GetCollection(Of PackagePersonalizedDetailXpo)("PackagePersonalizedDetailXpo")
        End Get
    End Property

    <Association("PackageReferencesConcentrationMeasurementUnit", GetType(MixinStationPackageXpo))>
    Public ReadOnly Property MixinStationPackageXpo() As XPCollection(Of MixinStationPackageXpo)
        Get
            Return GetCollection(Of MixinStationPackageXpo)("MixinStationPackageXpo")
        End Get
    End Property

    <Association("DilutionFactorsReferencesMeasurementUnitId", GetType(DilutionFactorsXpo))>
    Public ReadOnly Property DilutionFactorsXpo() As XPCollection(Of DilutionFactorsXpo)
        Get
            Return GetCollection(Of DilutionFactorsXpo)("DilutionFactorsXpo")
        End Get
    End Property

    <Association("DilutionFactorsDetailReferencesMeasurementUnitId", GetType(DilutionFactorsDetailXpo))>
    Public ReadOnly Property DilutionFactorsDetailXpo() As XPCollection(Of DilutionFactorsDetailXpo)
        Get
            Return GetCollection(Of DilutionFactorsDetailXpo)("DilutionFactorsDetailXpo")
        End Get
    End Property

    <Association("PackageReferencesConcentrationMeasurementUnit2", GetType(MixinStationPackageXpo))>
    Public ReadOnly Property MixinStationPackageXpo2() As XPCollection(Of MixinStationPackageXpo)
        Get
            Return GetCollection(Of MixinStationPackageXpo)("MixinStationPackageXpo2")
        End Get
    End Property

    <Association("ExternalPatientPreparation_Reference_MeasurementUnit", GetType(ExternalPatientPreparationXpo))>
    Public ReadOnly Property ExternalPatientPreparationsXpo() As XPCollection(Of ExternalPatientPreparationXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationXpo)("ExternalPatientPreparationsXpo")
        End Get
    End Property

    <Association("ExternalPatientPreparationDetail_Reference_MeasurementUnit", GetType(ExternalPatientPreparationDetailXpo))>
    Public ReadOnly Property ExternalPatientPreparationDetailsXpo() As XPCollection(Of ExternalPatientPreparationDetailXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationDetailXpo)("ExternalPatientPreparationDetailsXpo")
        End Get
    End Property

    <Association("ExternalPatientPreparationDetail_Reference_VolumeMeasureUnit", GetType(ExternalPatientPreparationDetailXpo))>
    Public ReadOnly Property ExternalPatientPreparationVolumeDetailsXpo() As XPCollection(Of ExternalPatientPreparationDetailXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationDetailXpo)("ExternalPatientPreparationDetailsXpo")
        End Get
    End Property

End Class