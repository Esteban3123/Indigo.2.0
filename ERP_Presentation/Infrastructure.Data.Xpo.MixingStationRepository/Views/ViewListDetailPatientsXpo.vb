'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/01/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListDetailPatients")>
Partial Public Class ViewListDetailPatientsXpo
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

    Dim fRequestMixingStationDetailPatientsId As Integer?
    Public Property RequestMixingStationDetailPatientsId() As Integer?
        Get
            Return fRequestMixingStationDetailPatientsId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RequestMixingStationDetailPatientsId", fRequestMixingStationDetailPatientsId, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailId As Integer?
    Public Property RequestMixingStationDetailId() As Integer?
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
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

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bed", fBed, value)
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

    Dim fStatus As Integer?
    Public Property Status() As Integer?
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Status", fStatus, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailPatientsStatusName As String
    Public Property RequestMixingStationDetailPatientsStatusName() As String
        Get
            Return fRequestMixingStationDetailPatientsStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestMixingStationDetailPatientsStatusName", fRequestMixingStationDetailPatientsStatusName, value)
        End Set
    End Property

    Dim fStatusHCPRESCRA As Integer?
    Public Property StatusHCPRESCRA() As Integer?
        Get
            Return fStatusHCPRESCRA
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("StatusHCPRESCRA", fStatusHCPRESCRA, value)
        End Set
    End Property

    Dim fStatusNameHCPRESCRA As String
    Public Property StatusNameHCPRESCRA() As String
        Get
            Return fStatusNameHCPRESCRA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusNameHCPRESCRA", fStatusNameHCPRESCRA, value)
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

    Dim fCodePackage As String
    Public Property CodePackage() As String
        Get
            Return fCodePackage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodePackage", fCodePackage, value)
        End Set
    End Property

    Dim fNamePackage As String
    Public Property NamePackage() As String
        Get
            Return fNamePackage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NamePackage", fNamePackage, value)
        End Set
    End Property

    Dim fATCCode As String
    Public Property ATCCode() As String
        Get
            Return fATCCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ATCCode", fATCCode, value)
        End Set
    End Property

    Dim fATCName As String
    Public Property ATCName() As String
        Get
            Return fATCName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ATCName", fATCName, value)
        End Set
    End Property
    Dim fDoseTypeCodeName As String
    Public Property DoseTypeCodeName() As String
        Get
            Return fDoseTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DoseTypeCodeName", fDoseTypeCodeName, value)
        End Set
    End Property
    Dim fRequestType As String
    Public Property RequestType() As String
        Get
            Return fRequestType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestType", fRequestType, value)
        End Set
    End Property
    Dim fLabelTypeName As String
    Public Property LabelTypeName() As String
        Get
            Return fLabelTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LabelTypeName", fLabelTypeName, value)
        End Set
    End Property

    Dim fObservations As String

    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fCampaignRawMaterialExist As Integer

    Public Property CampaignRawMaterialExist() As Integer
        Get
            Return fCampaignRawMaterialExist
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignRawMaterialExist", fCampaignRawMaterialExist, value)
        End Set
    End Property

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
        End Set
    End Property

    Dim fUnitDoseTypeCodeName As String
    Public Property UnitDoseTypeCodeName As String
        Get
            Return fUnitDoseTypeCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("UnitDoseTypeCodeName", fUnitDoseTypeCodeName, value)
        End Set
    End Property

    Dim fUnitDoseClass As Integer
    Public Property UnitDoseClass As Integer
        Get
            Return fUnitDoseClass
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseClass", fUnitDoseClass, value)
        End Set
    End Property

    Dim fLabelType As Byte?
    Public Property LabelType As Byte?
        Get
            Return fLabelType
        End Get
        Set(value As Byte?)
            SetPropertyValue(Of Byte?)("LabelType", fLabelType, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode As String
        Get
            Return fBatchCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fPackageDescription As String
    Public Property PackageDescription As String
        Get
            Return fPackageDescription
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("PackageDescription", fPackageDescription, value)
        End Set
    End Property

    Dim fFlagQualityDefect As Boolean
    Public Property FlagQualityDefect As Boolean
        Get
            Return fFlagQualityDefect
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("FlagQualityDefect", fFlagQualityDefect, value)
        End Set
    End Property

    Dim fHasProductionDeffect As Boolean
    Public Property HasProductionDeffect As Boolean
        Get
            Return fHasProductionDeffect
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HasProductionDeffect", fHasProductionDeffect, value)
        End Set
    End Property
End Class