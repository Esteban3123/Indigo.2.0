'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/01/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListDashboardConfirmationUnitDose")>
Partial Public Class ViewListDashboardConfirmationUnitDoseXpo
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

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fCareCenterDescription As String
    Public Property CareCenterDescription() As String
        Get
            Return fCareCenterDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterDescription", fCareCenterDescription, value)
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

    Dim fServiceCode As String
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
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

    Dim fServiceDescription As String
    Public Property ServiceDescription() As String
        Get
            Return fServiceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceDescription", fServiceDescription, value)
        End Set
    End Property

    Dim fTotalQuantity As Integer
    Public Property TotalQuantity() As Integer
        Get
            Return fTotalQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalQuantity", fTotalQuantity, value)
        End Set
    End Property

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
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

    Dim fPackageId As Integer
    Public Property PackageId() As Integer
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fPackageCode As String
    Public Property PackageCode() As String
        Get
            Return fPackageCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageCode", fPackageCode, value)
        End Set
    End Property

    Dim fPackageName As String
    Public Property PackageName() As String
        Get
            Return fPackageName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageName", fPackageName, value)
        End Set
    End Property

    Dim fPackageDescription As String
    Public Property PackageDescription() As String
        Get
            Return fPackageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageDescription", fPackageDescription, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fUnitDoseTypeCode As String
    Public Property UnitDoseTypeCode() As String
        Get
            Return fUnitDoseTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeCode", fUnitDoseTypeCode, value)
        End Set
    End Property

    Dim fUnitDoseTypeName As String
    Public Property UnitDoseTypeName() As String
        Get
            Return fUnitDoseTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeName", fUnitDoseTypeName, value)
        End Set
    End Property

    Dim fUnitDoseTypeDescription As String
    Public Property UnitDoseTypeDescription() As String
        Get
            Return fUnitDoseTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeDescription", fUnitDoseTypeDescription, value)
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

    Dim fMeasurementUnitCode As String
    Public Property MeasurementUnitCode() As String
        Get
            Return fMeasurementUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitCode", fMeasurementUnitCode, value)
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

    Dim fMeasurementUnitDescription As String
    Public Property MeasurementUnitDescription() As String
        Get
            Return fMeasurementUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitDescription", fMeasurementUnitDescription, value)
        End Set
    End Property

    Dim fConfirmationUnitDoseId As Integer
    Public Property ConfirmationUnitDoseId() As Integer
        Get
            Return fConfirmationUnitDoseId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConfirmationUnitDoseId", fConfirmationUnitDoseId, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fProductionLineCode As String
    Public Property ProductionLineCode() As String
        Get
            Return fProductionLineCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCode", fProductionLineCode, value)
        End Set
    End Property

    Dim fProductionLineName As String
    Public Property ProductionLineName() As String
        Get
            Return fProductionLineName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineName", fProductionLineName, value)
        End Set
    End Property

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName() As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
        End Set
    End Property

    Dim fSourceType As Integer
    Public Property SourceType() As Integer
        Get
            Return fSourceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SourceType", fSourceType, value)
        End Set
    End Property

    Dim fSourceName As String
    Public Property SourceName() As String
        Get
            Return fSourceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SourceName", fSourceName, value)
        End Set
    End Property

    Dim fPackagePersonalizedId As Integer?
    Public Property PackagePersonalizedId() As Integer?
        Get
            Return fPackagePersonalizedId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PackagePersonalizedId", fPackagePersonalizedId, value)
        End Set
    End Property

    Dim fPackagePersonalizedCodeName As String
    Public Property PackagePersonalizedCodeName() As String
        Get
            Return fPackagePersonalizedCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackagePersonalizedCodeName", fPackagePersonalizedCodeName, value)
        End Set
    End Property

    Dim fAGRUPAQUETE As String
    Public Property AGRUPAQUETE() As String
        Get
            Return fAGRUPAQUETE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AGRUPAQUETE", fAGRUPAQUETE, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
        End Set
    End Property

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bed", fBed, value)
        End Set
    End Property

    Dim fAdministrationRoute As String
    Public Property AdministrationRoute() As String
        Get
            Return fAdministrationRoute
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationRoute", fAdministrationRoute, value)
        End Set
    End Property

    Dim fProductNPT As Boolean
    Public Property ProductNPT() As Boolean
        Get
            Return fProductNPT
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductNPT", fProductNPT, value)
        End Set
    End Property

    Dim fFunctionalUnitCode As String
    Public Property FunctionalUnitCode() As String
        Get
            Return fFunctionalUnitCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("FunctionalUnitCode", fFunctionalUnitCode, value)
        End Set
    End Property

    Dim fFuncionalUnitCodeName As String
    Public Property FuncionalUnitCodeName() As String
        Get
            Return fFuncionalUnitCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("FuncionalUnitCodeName", fFuncionalUnitCodeName, value)
        End Set
    End Property

    Dim fStatusHCPRESCRA As Integer
    Public Property StatusHCPRESCRA() As Integer
        Get
            Return fStatusHCPRESCRA
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("StatusHCPRESCRA", fStatusHCPRESCRA, value)
        End Set
    End Property

    Dim fStatusNameHCPRESCRA As String
    Public Property StatusNameHCPRESCRA() As String
        Get
            Return fStatusNameHCPRESCRA
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("StatusNameHCPRESCRA", fStatusNameHCPRESCRA, value)
        End Set
    End Property

    Private fCodeSusceptibleMixingStation As String
    Public Property CodeSusceptibleMixingStation() As String
        Get
            Return fCodeSusceptibleMixingStation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSusceptibleMixingStation", fCodeSusceptibleMixingStation, value)
        End Set
    End Property

    Private fOriginOrder As Integer
    Public Property OriginOrder() As Integer
        Get
            Return fOriginOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OriginOrder", fOriginOrder, value)
        End Set
    End Property

    Dim fRequestDate As Date?
    Public Property RequestDate() As Date?
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("RequestDate", fRequestDate, value)
        End Set
    End Property

    Private fAdmissionCode As String
    Public Property AdmissionCode() As String
        Get
            Return fAdmissionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCode", fAdmissionCode, value)
        End Set
    End Property

    Dim fFECALTPAC As Date?
    Public Property FECALTPAC() As Date?
        Get
            Return fFECALTPAC
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("FECALTPAC", fFECALTPAC, value)
        End Set
    End Property

    Private fMSClass As Integer
    Public Property MSClass() As Integer
        Get
            Return fMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", fMSClass, value)
        End Set
    End Property

    Private fSafeStatus As Byte
    Public Property SafeStatus() As Byte
        Get
            Return fSafeStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SafeStatus", fSafeStatus, value)
        End Set
    End Property

    Private fNPTVerified As Boolean
    Public Property NPTVerified() As Boolean
        Get
            Return fNPTVerified
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NPTVerified", fNPTVerified, value)
        End Set
    End Property

    <PersistentAlias("Iif(SafeStatus = 1, 'Seguro', SafeStatus = 2, 'Inseguro', 'Ninguno')")>
    Public ReadOnly Property SafeStatusDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("SafeStatusDescription"))
        End Get
    End Property

    <PersistentAlias("Iif(fMSClass = 9, 'Mezcla Magistral', concat(Dosage,' ',MeasurementUnitName))")>
    Public ReadOnly Property DosageMeasurementUnitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DosageMeasurementUnitName"))
        End Get
    End Property

    Private fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Private fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

End Class