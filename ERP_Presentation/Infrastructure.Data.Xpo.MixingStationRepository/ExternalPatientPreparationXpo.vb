Imports DevExpress.Xpo

<Persistent("MixingStation.ExternalPatientPreparation")>
Public Class ExternalPatientPreparationXpo
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

    Dim fRequestUnitDoseExternalCareCenterPatientId As RequestUnitDoseExternalCareCenterPatientXpo
    <Association("ExternalPatientPreparation_Reference_RequestUnitDoseExternalCareCenterPatient")>
    Public Property RequestUnitDoseExternalCareCenterPatientId() As RequestUnitDoseExternalCareCenterPatientXpo
        Get
            Return fRequestUnitDoseExternalCareCenterPatientId
        End Get
        Set(ByVal value As RequestUnitDoseExternalCareCenterPatientXpo)
            SetPropertyValue(Of RequestUnitDoseExternalCareCenterPatientXpo)("RequestUnitDoseExternalCareCenterPatientId", fRequestUnitDoseExternalCareCenterPatientId, value)
        End Set
    End Property

    Dim fPreparationsRequested As Integer
    Public Property PreparationsRequested() As Integer
        Get
            Return fPreparationsRequested
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PreparationsRequested", fPreparationsRequested, value)
        End Set
    End Property

    Dim fPreparationTypeId As Integer?
    Public Property PreparationTypeId() As Integer?
        Get
            Return fPreparationTypeId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PreparationTypeId", fPreparationTypeId, value)
        End Set
    End Property

    Dim fAdministrationRouteId As MixingStationAdministrationRouteXpo
    <Association("ExternalPatientPreparation_Reference_AdministrationRoute")>
    Public Property AdministrationRouteId() As MixingStationAdministrationRouteXpo
        Get
            Return fAdministrationRouteId
        End Get
        Set(ByVal value As MixingStationAdministrationRouteXpo)
            SetPropertyValue(Of MixingStationAdministrationRouteXpo)("AdministrationRouteId", fAdministrationRouteId, value)
        End Set
    End Property

    Dim fAssociatedPackageId As MixinStationPackageXpo
    <Association("ExternalPatientPreparation_Reference_Package")>
    Public Property AssociatedPackageId() As MixinStationPackageXpo
        Get
            Return fAssociatedPackageId
        End Get
        Set(ByVal value As MixinStationPackageXpo)
            SetPropertyValue(Of MixinStationPackageXpo)("AssociatedPackageId", fAssociatedPackageId, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As Decimal
    Public Property VolumeTotalOrder() As Decimal
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fTotalPreparedUnitMeasurementId As MixingStationMeasurementUnitXpo
    <Association("ExternalPatientPreparation_Reference_MeasurementUnit")>
    Public Property TotalPreparedUnitMeasurementId() As MixingStationMeasurementUnitXpo
        Get
            Return fTotalPreparedUnitMeasurementId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("TotalPreparedUnitMeasurementId", fTotalPreparedUnitMeasurementId, value)
        End Set
    End Property

    Dim fConcentration As String
    Public Property Concentration() As String
        Get
            Return fConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

#End Region

#Region "Relationship Members"

    <Association("ExternalPatientPreparationDetail_Reference_ExternalPatientPreparation", GetType(ExternalPatientPreparationDetailXpo))>
    Public ReadOnly Property ExternalPatientPreparationDetailsXpo() As XPCollection(Of ExternalPatientPreparationDetailXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationDetailXpo)("ExternalPatientPreparationDetailsXpo")
        End Get
    End Property

#End Region

End Class
