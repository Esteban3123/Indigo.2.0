'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListDashboardRequestMixingStationDetail")>
Partial Public Class ViewListDashboardRequestMixingStationDetailXpo
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

    Dim fRequestMixingStationId As Integer
    Public Property RequestMixingStationId() As Integer
        Get
            Return fRequestMixingStationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationId", fRequestMixingStationId, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailPatientsId As Integer
    Public Property RequestMixingStationDetailPatientsId() As Integer
        Get
            Return fRequestMixingStationDetailPatientsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationDetailPatientsId", fRequestMixingStationDetailPatientsId, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailId As Integer
    Public Property RequestMixingStationDetailId() As Integer
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
        End Set
    End Property

    Dim fItemType As Integer
    Public Property ItemType() As Integer
        Get
            Return fItemType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fItemTypeName As String
    Public Property ItemTypeName() As String
        Get
            Return fItemTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemTypeName", fItemTypeName, value)
        End Set
    End Property

    Dim fItemId As Integer
    Public Property ItemId() As Integer
        Get
            Return fItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemId", fItemId, value)
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

    Dim fUnitDoseTypeId As Integer
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fUnitDoseTypeCodeName As String
    Public Property UnitDoseTypeCodeName() As String
        Get
            Return fUnitDoseTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeCodeName", fUnitDoseTypeCodeName, value)
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

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
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

    Dim fFunctionalUnitCode As String
    Public Property FunctionalUnitCode() As String
        Get
            Return fFunctionalUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCode", fFunctionalUnitCode, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fAdministrationRouteId As Integer?
    Public Property AdministrationRouteId() As Integer?
        Get
            Return fAdministrationRouteId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AdministrationRouteId", fAdministrationRouteId, value)
        End Set
    End Property

    Dim fAdministrationRouteCodeName As String
    Public Property AdministrationRouteCodeName() As String
        Get
            Return fAdministrationRouteCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationRouteCodeName", fAdministrationRouteCodeName, value)
        End Set
    End Property

    Dim fSetStatus As Integer
    <NonPersistent()>
    Public Property SetStatus As Integer
        Get
            Return fSetStatus
        End Get
        Set(value As Integer)
            fSetStatus = value
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

End Class