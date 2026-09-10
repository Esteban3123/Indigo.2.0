'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Felipe Aros Escobar
' Created          : 30/09/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewQualityControl")>
Partial Public Class ViewQualityControlXpo
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

    Dim fRequestPackageDetailStatusIds As String
    Public Property RequestPackageDetailStatusIds() As String
        Get
            Return fRequestPackageDetailStatusIds
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestPackageDetailStatusIds", fRequestPackageDetailStatusIds, value)
        End Set
    End Property

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fDate As Date
    Public Property DateCampaign() As Date
        Get
            Return fDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DateCampaign", fDate, value)
        End Set
    End Property

    Dim fLot As String
    Public Property Lot() As String
        Get
            Return fLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lot", fLot, value)
        End Set
    End Property

    Dim fFinishedProductCode As String
    Public Property FinishedProductCode() As String
        Get
            Return fFinishedProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinishedProductCode", fFinishedProductCode, value)
        End Set
    End Property

    Dim fFinishedProductName As String
    Public Property FinishedProductName() As String
        Get
            Return fFinishedProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinishedProductName", fFinishedProductName, value)
        End Set
    End Property

    Dim fExpirationDate As Date
    Public Property ExpirationDate() As Date
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property

    Dim fQualitySupervisor As String
    Public Property QualitySupervisor() As String
        Get
            Return fQualitySupervisor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QualitySupervisor", fQualitySupervisor, value)
        End Set
    End Property

    Dim fState As String
    Public Property State() As String
        Get
            Return fState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("State", fState, value)
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

    Dim fQualityStatus As String
    Public Property QualityStatus() As String
        Get
            Return fQualityStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QualityStatus", fQualityStatus, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
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

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName() As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
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

    Dim fUnitDoseClass As Integer
    Public Property UnitDoseClass() As Integer
        Get
            Return fUnitDoseClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseClass", fUnitDoseClass, value)
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

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
        End Set
    End Property

    Dim fFlagDefectClasificationCritical As Boolean?
    Public Property FlagDefectClasificationCritical() As Boolean?
        Get
            Return fFlagDefectClasificationCritical
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("FlagDefectClasificationCritical", fFlagDefectClasificationCritical, value)
        End Set
    End Property


    Dim fLabelType As Byte
    Public Property LabelType() As Byte
        Get
            Return fLabelType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LabelType", fLabelType, value)
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

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fCampaignStatus As Byte
    Public Property CampaignStatus() As Byte
        Get
            Return fCampaignStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CampaignStatus", fCampaignStatus, value)
        End Set
    End Property

    Dim fRequestPackageDetailStatusState As Byte
    Public Property RequestPackageDetailStatusState() As Byte
        Get
            Return fRequestPackageDetailStatusState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RequestPackageDetailStatusState", fRequestPackageDetailStatusState, value)
        End Set
    End Property

    Dim fFlagQualification As Byte
    Public Property FlagQualification() As Byte
        Get
            Return fFlagQualification
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FlagQualification", fFlagQualification, value)
        End Set
    End Property

    Dim fWhitoutDefects As Integer
    Public Property WhitoutDefects() As Integer
        Get
            Return fWhitoutDefects
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WhitoutDefects", fWhitoutDefects, value)
        End Set
    End Property

    Dim fCriticalDefects As Integer
    Public Property CriticalDefects() As Integer
        Get
            Return fCriticalDefects
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CriticalDefects", fCriticalDefects, value)
        End Set
    End Property

End Class