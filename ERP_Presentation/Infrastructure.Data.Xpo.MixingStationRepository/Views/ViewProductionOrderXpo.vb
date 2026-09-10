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

<Persistent("MixingStation.ViewProductionOrder")>
Partial Public Class ViewProductionOrderXpo
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fCampaignId As Integer
    Public Property CampaignId() As Integer
        Get
            Return fCampaignId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignId", fCampaignId, value)
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

    Dim fCampaignDescription As String
    Public Property CampaignDescription() As String
        Get
            Return fCampaignDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CampaignDescription", fCampaignDescription, value)
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

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
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

    Dim fProductionScheduleCode As String
    Public Property ProductionScheduleCode() As String
        Get
            Return fProductionScheduleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionScheduleCode", fProductionScheduleCode, value)
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

    Dim fCampaignStatusName As String
    Public Property CampaignStatusName() As String
        Get
            Return fCampaignStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CampaignStatusName", fCampaignStatusName, value)
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

    Dim fProcessingDate As Date?
    Public Property ProcessingDate() As Date?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property


    Dim fLabelConfirmationDate As Date?
    Public Property LabelConfirmationDate() As Date?
        Get
            Return fLabelConfirmationDate
        End Get
        Set(value As Date?)
            SetPropertyValue(Of Date?)("LabelConfirmationDate", fLabelConfirmationDate, value)
        End Set
    End Property

    Dim fCampaignCreationDate As Date
    Public Property CampaignCreationDate() As Date
        Get
            Return fCampaignCreationDate
        End Get
        Set(value As Date)
            SetPropertyValue(Of Date)("CampaignCreationDate", fCampaignCreationDate, value)
        End Set
    End Property

    Dim fUnitDoseTypeClass As Integer
    Public Property UnitDoseTypeClass() As Integer
        Get
            Return fUnitDoseTypeClass
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeClass", fUnitDoseTypeClass, value)
        End Set
    End Property

    Dim fWorkingAreaName As String
    Public Property WorkingAreaName() As String
        Get
            Return fWorkingAreaName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("WorkingAreaName", fWorkingAreaName, value)
        End Set
    End Property

    Dim fIsManagedLabel As Boolean
    Public Property IsManagedLabel() As Boolean
        Get
            Return fIsManagedLabel
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsManagedLabel", fIsManagedLabel, value)
        End Set
    End Property

End Class