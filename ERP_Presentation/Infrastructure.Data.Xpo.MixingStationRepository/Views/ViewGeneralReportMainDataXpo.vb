'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2023-10-25
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewGeneralReportMainData")>
Partial Public Class ViewGeneralReportMainDataXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fKeyView As String
    <Key(True)>
    Public Property KeyView() As String
        Get
            Return fKeyView
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("KeyView", fKeyView, value)
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

    Dim fMixingStationName As String
    Public Property MixingStationName() As String
        Get
            Return fMixingStationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MixingStationName", fMixingStationName, value)
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

    Dim fUnitDoseType As String
    Public Property UnitDoseType() As String
        Get
            Return fUnitDoseType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseType", fUnitDoseType, value)
        End Set
    End Property

    Dim fProductionLine As String
    Public Property ProductionLine() As String
        Get
            Return fProductionLine
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLine", fProductionLine, value)
        End Set
    End Property

    Dim fWorkArea As String
    Public Property WorkArea() As String
        Get
            Return fWorkArea
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkArea", fWorkArea, value)
        End Set
    End Property

    Dim fCampaignStatus As String
    Public Property CampaignStatus() As String
        Get
            Return fCampaignStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CampaignStatus", fCampaignStatus, value)
        End Set
    End Property

    Dim fProcessingDate As Date
    Public Property ProcessingDate() As Date
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fFinishDate As Date
    Public Property FinishDate() As Date
        Get
            Return fFinishDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("FinishDate", fFinishDate, value)
        End Set
    End Property

    Dim fDuration As String
    Public Property Duration() As String
        Get
            Return fDuration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Duration", fDuration, value)
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

    Dim fTechnicalDirector As String
    Public Property TechnicalDirector() As String
        Get
            Return fTechnicalDirector
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TechnicalDirector", fTechnicalDirector, value)
        End Set
    End Property

    Dim fQualityChemical As String
    Public Property QualityChemical() As String
        Get
            Return fQualityChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QualityChemical", fQualityChemical, value)
        End Set
    End Property

    Dim fProductionChemical As String
    Public Property ProductionChemical() As String
        Get
            Return fProductionChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionChemical", fProductionChemical, value)
        End Set
    End Property

    Dim fProductionAssistant As String
    Public Property ProductionAssistant() As String
        Get
            Return fProductionAssistant
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionAssistant", fProductionAssistant, value)
        End Set
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class