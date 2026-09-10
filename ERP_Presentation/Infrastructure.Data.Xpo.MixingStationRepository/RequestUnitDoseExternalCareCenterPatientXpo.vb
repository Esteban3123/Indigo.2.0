'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.RequestUnitDoseExternalCareCenterPatient")>
Partial Public Class RequestUnitDoseExternalCareCenterPatientXpo
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

    Dim fRequestUnitDoseExternalCareCenterId As RequestUnitDoseExternalCareCenterXpo
    <Association("PatientReferencesCMConfigurationRequestUnitDoseExternalCareCenter")>
    Public Property RequestUnitDoseExternalCareCenterId() As RequestUnitDoseExternalCareCenterXpo
        Get
            Return fRequestUnitDoseExternalCareCenterId
        End Get
        Set(ByVal value As RequestUnitDoseExternalCareCenterXpo)
            SetPropertyValue(Of RequestUnitDoseExternalCareCenterXpo)("RequestUnitDoseExternalCareCenterId", fRequestUnitDoseExternalCareCenterId, value)
        End Set
    End Property

    Dim fPatientExternalCareCenterId As PatientExternalCareCenterXpo
    <Association("PatientReferencesPECC")>
    Public Property PatientExternalCareCenterId() As PatientExternalCareCenterXpo
        Get
            Return fPatientExternalCareCenterId
        End Get
        Set(ByVal value As PatientExternalCareCenterXpo)
            SetPropertyValue(Of PatientExternalCareCenterXpo)("PatientExternalCareCenterId", fPatientExternalCareCenterId, value)
        End Set
    End Property

    Dim fExternalFunctionalUnitCode As String
    Public Property ExternalFunctionalUnitCode() As String
        Get
            Return fExternalFunctionalUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ExternalFunctionalUnitCode", fExternalFunctionalUnitCode, value)
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

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("RequestUnitDoseExternalCareCenterPatientReferencesUnitDoseType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fNptId As HCPARNUTCXpo
    <Association("RequestUnitDoseExternalCareCenterPatientReferencesHCPARNUTC")>
    Public Property NptId() As HCPARNUTCXpo
        Get
            Return fNptId
        End Get
        Set(ByVal value As HCPARNUTCXpo)
            SetPropertyValue(Of HCPARNUTCXpo)("NptId", fNptId, value)
        End Set
    End Property

#End Region

#Region "Relationship Members"

    <Association("ExternalPatientPreparation_Reference_RequestUnitDoseExternalCareCenterPatient", GetType(ExternalPatientPreparationXpo))>
    Public ReadOnly Property ExternalPatientPreparationsXpo() As XPCollection(Of ExternalPatientPreparationXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationXpo)("ExternalPatientPreparationsXpo")
        End Get
    End Property

#End Region

End Class