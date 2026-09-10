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

<Persistent("MixingStation.Package")>
Partial Public Class MixinStationPackageXpo
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
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(200)>
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <NonPersistent>
    Public Property IsStandard() As Boolean

    <PersistentAlias("iif(State=True, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    Dim fDescription As String
    <Size(SizeAttribute.Unlimited)>
    <Persistent("Description")>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    <PersistentAlias("concat(Code,' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fVehicleOptimization As Byte
    Public Property VehicleOptimization() As Byte
        Get
            Return fVehicleOptimization
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("VehicleOptimization", fVehicleOptimization, value)
        End Set
    End Property

    Dim fStorage As Byte
    Public Property Storage() As Byte
        Get
            Return fStorage
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("Storage", fStorage, value)
        End Set
    End Property

    Dim fConcentrationAntibiotic As String
    Public Property ConcentrationAntibiotic() As String
        Get
            Return fConcentrationAntibiotic
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ConcentrationAntibiotic", fConcentrationAntibiotic, value)
        End Set
    End Property

    Dim fVolumeTotalPrepared As Decimal
    Public Property VolumeTotalPrepared() As Decimal
        Get
            Return fVolumeTotalPrepared
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalPrepared", fVolumeTotalPrepared, value)
        End Set
    End Property

    Dim fMeasurementPreparedId As Integer
    Public Property MeasurementPreparedId As Integer
        Get
            Return fMeasurementPreparedId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("MeasurementPreparedId", fMeasurementPreparedId, value)
        End Set
    End Property

    Dim fATCId As Integer?
    Public Property ATCId() As Integer?
        Get
            Return fATCId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fRiskLevelId As MixingStationRiskLevelXpo
    <Association("PackageReferencesRiskLevel")>
    Public Property RiskLevelId() As MixingStationRiskLevelXpo
        Get
            Return fRiskLevelId
        End Get
        Set(ByVal value As MixingStationRiskLevelXpo)
            SetPropertyValue(Of MixingStationRiskLevelXpo)("RiskLevelId", fRiskLevelId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("PackageReferencesUnitDoseType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fNptId As HCPARNUTCXpo
    <Association("PackageReferencesHCPARNUTC")>
    Public Property NptId() As HCPARNUTCXpo
        Get
            Return fNptId
        End Get
        Set(ByVal value As HCPARNUTCXpo)
            SetPropertyValue(Of HCPARNUTCXpo)("NptId", fNptId, value)
        End Set
    End Property

    Dim fProductId As MixingStationProductXpo
    <Association("PackageReferencesProduct")>
    Public Property ProductId() As MixingStationProductXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As MixingStationProductXpo)
            SetPropertyValue(Of MixingStationProductXpo)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fConcentrationMeasurementUnitId As MixingStationMeasurementUnitXpo
    <Association("PackageReferencesConcentrationMeasurementUnit")>
    Public Property ConcentrationMeasurementUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fConcentrationMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("ConcentrationMeasurementUnitId", fConcentrationMeasurementUnitId, value)
        End Set
    End Property

    Dim fVolumeTotalOrderMeasurementUnitId As MixingStationMeasurementUnitXpo
    <Association("PackageReferencesConcentrationMeasurementUnit2")>
    Public Property VolumeTotalOrderMeasurementUnitId() As MixingStationMeasurementUnitXpo
        Get
            Return fVolumeTotalOrderMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("VolumeTotalOrderMeasurementUnitId", fVolumeTotalOrderMeasurementUnitId, value)
        End Set
    End Property

    Dim fCodeAlternative As String
    <Size(30)>
    <Persistent("CodeAlternative")>
    Public Property CodeAlternative() As String
        Get
            Return fCodeAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternative", fCodeAlternative, value)
        End Set
    End Property

    Dim fCodeAlternativeTwo As String
    <Size(20)>
    <Persistent("CodeAlternativeTwo")>
    Public Property CodeAlternativeTwo() As String
        Get
            Return fCodeAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternativeTwo", fCodeAlternativeTwo, value)
        End Set
    End Property

    Dim fProductGroupId As Integer
    <Persistent("ProductGroupId")>
    Public Property ProductGroupId() As Integer
        Get
            Return fProductGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductGroupId", fProductGroupId, value)
        End Set
    End Property

    Dim fProductSubGroupId As Integer
    <Persistent("ProductSubGroupId")>
    Public Property ProductSubGroupId() As Integer
        Get
            Return fProductSubGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductSubGroupId", fProductSubGroupId, value)
        End Set
    End Property

    Dim fManufacturerId As Integer
    <Persistent("ManufacturerId")>
    Public Property ManufacturerId() As Integer
        Get
            Return fManufacturerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManufacturerId", fManufacturerId, value)
        End Set
    End Property

    Dim fCodeSICE As String
    <Size(20)>
    <Persistent("CodeSICE")>
    Public Property CodeSICE() As String
        Get
            Return fCodeSICE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSICE", fCodeSICE, value)
        End Set
    End Property

    Dim fPOSProduct As Boolean
    Public Property POSProduct() As Boolean
        Get
            Return fPOSProduct
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("POSProduct", fPOSProduct, value)
        End Set
    End Property

    Dim fBillingGroupId As Integer
    <Persistent("BillingGroupId")>
    Public Property BillingGroupId() As Integer
        Get
            Return fBillingGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingGroupId", fBillingGroupId, value)
        End Set
    End Property

    Dim fProductControl As Boolean
    Public Property ProductControl() As Boolean
        Get
            Return fProductControl
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductControl", fProductControl, value)
        End Set
    End Property

    Dim fProductWithPriceControl As Boolean
    Public Property ProductWithPriceControl() As Boolean
        Get
            Return fProductWithPriceControl
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductWithPriceControl", fProductWithPriceControl, value)
        End Set
    End Property

    Dim fAuthorizationByOrderNumber As Integer
    <Persistent("AuthorizationByOrderNumber")>
    Public Property AuthorizationByOrderNumber() As Integer
        Get
            Return fAuthorizationByOrderNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationByOrderNumber", fAuthorizationByOrderNumber, value)
        End Set
    End Property

    Dim fMaximumControlPeriod As Boolean
    Public Property MaximumControlPeriod() As Boolean
        Get
            Return fMaximumControlPeriod
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MaximumControlPeriod", fMaximumControlPeriod, value)
        End Set
    End Property

    Dim fOsmolarityTotal As Decimal
    <Size(100)>
    <Persistent("OsmolarityTotal")>
    Public Property OsmolarityTotal() As Decimal
        Get
            Return fOsmolarityTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("OsmolarityTotal", fOsmolarityTotal, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As Decimal
    <Size(100)>
    <Persistent("VolumeTotalOrder")>
    Public Property VolumeTotalOrder() As Decimal
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fVolumeTotalOrderPurga As Decimal
    <Size(100)>
    <Persistent("VolumeTotalOrderPurga")>
    Public Property VolumeTotalOrderPurga() As Decimal
        Get
            Return fVolumeTotalOrderPurga
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalOrderPurga", fVolumeTotalOrderPurga, value)
        End Set
    End Property

    Dim fWeightTotalSolution As Decimal
    <Size(100)>
    <Persistent("WeightTotalSolution")>
    Public Property WeightTotalSolution() As Decimal
        Get
            Return fWeightTotalSolution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WeightTotalSolution", fWeightTotalSolution, value)
        End Set
    End Property

    Dim fPurge As Decimal
    Public Property Purge() As Decimal
        Get
            Return fPurge
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Purge", fPurge, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fPreparationInstructions As String
    Public Property PreparationInstructions() As String
        Get
            Return fPreparationInstructions
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PreparationInstructions", fPreparationInstructions, value)
        End Set
    End Property

    Dim fSpecialConsiderations As String
    Public Property SpecialConsiderations() As String
        Get
            Return fSpecialConsiderations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialConsiderations", fSpecialConsiderations, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fPersonalizedMasterPreparation As Boolean
    Public Property PersonalizedMasterPreparation() As Boolean
        Get
            Return fPersonalizedMasterPreparation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PersonalizedMasterPreparation", fPersonalizedMasterPreparation, value)
        End Set
    End Property

    Dim fPhotoProtection As Boolean
    Public Property PhotoProtection() As Boolean
        Get
            Return fPhotoProtection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PhotoProtection", fPhotoProtection, value)
        End Set
    End Property

    Dim fConcentration As Decimal
    Public Property Concentration() As Decimal
        Get
            Return fConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fPreparationType As Integer
    Public Property PreparationType() As Integer
        Get
            Return fPreparationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PreparationType", fPreparationType, value)
        End Set
    End Property

    Dim fStabilityHour As TimeSpan?
    Public Property StabilityHour() As TimeSpan?
        Get
            Return fStabilityHour
        End Get
        Set(ByVal value As TimeSpan?)
            SetPropertyValue(Of TimeSpan?)("StabilityHour", fStabilityHour, value)
        End Set
    End Property

    Dim fLabelType As Byte?
    Public Property LabelType() As Byte?
        Get
            Return fLabelType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("LabelType", fLabelType, value)
        End Set
    End Property

    Dim fEnvironmentalTemperatureTerm As Integer
    Public Property EnvironmentalTemperatureTerm() As Integer
        Get
            Return fEnvironmentalTemperatureTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EnvironmentalTemperatureTerm", fEnvironmentalTemperatureTerm, value)
        End Set
    End Property

    Dim fMainDrugId As Integer?
    Public Property MainDrugId() As Integer?
        Get
            Return fMainDrugId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MainDrugId", fMainDrugId, value)
        End Set
    End Property

    <Association("PackageDetailReferencesPackage", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailXpo")
        End Get
    End Property

    <Association("RequestUnitDoseInventoryDetailReferencesPackage", GetType(RequestUnitDoseInventoryDetailXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryDetailXpo() As XPCollection(Of RequestUnitDoseInventoryDetailXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryDetailXpo)("RequestUnitDoseInventoryDetailXpo")
        End Get
    End Property

    <Association("MaquilaReferencesPackage", GetType(RequestUnitDoseExternalCareCenterMaquilaXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterMaquilaXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)("RequestUnitDoseExternalCareCenterMaquilaXpo")
        End Get
    End Property

    <Association("ExternalPatientPreparation_Reference_Package", GetType(ExternalPatientPreparationXpo))>
    Public ReadOnly Property ExternalPatientPreparationsXpo() As XPCollection(Of ExternalPatientPreparationXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationXpo)("ExternalPatientPreparationsXpo")
        End Get
    End Property

End Class