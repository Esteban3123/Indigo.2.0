'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldán Lozano
' Created          : 2022-08-17
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

Imports DevExpress.Xpo

<Persistent("MixingStation.ViewNonConformingProduct")>
Partial Public Class ViewNonConformingProductXpo
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


    Dim fSource As Byte
    Public Property Source() As Byte
        Get
            Return fSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue("Source", fSource, value)
        End Set
    End Property

    Dim fSourceName As String
    Public Property SourceName() As String
        Get
            Return fSourceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SourceName", fSourceName, value)
        End Set
    End Property

    Dim fRequestCode As String
    Public Property RequestCode() As String
        Get
            Return fRequestCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RequestCode", fRequestCode, value)
        End Set
    End Property

    Dim fRequestDate As Date
    Public Property RequestDate() As Date
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("RequestDate", fRequestDate, value)
        End Set
    End Property

    Dim fProductionScheduleCode As String
    Public Property ProductionScheduleCode() As String
        Get
            Return fProductionScheduleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ProductionScheduleCode", fProductionScheduleCode, value)
        End Set
    End Property

    Dim fUserName As String
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("UserName", fUserName, value)
        End Set
    End Property

    Dim fRequestPackageDetailstatus As String
    Public Property RequestPackageDetailstatus() As String
        Get
            Return fRequestPackageDetailstatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RequestPackageDetailstatus", fRequestPackageDetailstatus, value)
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

    Private fUFUDESCRI As String
    Public Property UFUDESCRI() As String
        Get
            Return fUFUDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUDESCRI", fUFUDESCRI, value)
        End Set
    End Property

    Private fNOMCENATE As String
    Public Property NOMCENATE() As String
        Get
            Return fNOMCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMCENATE", fNOMCENATE, value)
        End Set
    End Property

End Class