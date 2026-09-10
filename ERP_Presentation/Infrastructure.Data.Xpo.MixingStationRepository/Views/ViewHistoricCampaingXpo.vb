'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 31/08/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewHistoricCampaing")>
Partial Public Class ViewHistoricCampaingXpo
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

    Dim fWorkingAreaName As String
    Public Property WorkingAreaName() As String
        Get
            Return fWorkingAreaName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("WorkingAreaName", fWorkingAreaName, value)
        End Set
    End Property

    Dim fMateriaPrimaStock As Integer
    Public Property MateriaPrimaStock() As Integer
        Get
            Return fMateriaPrimaStock
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MateriaPrimaStock", fMateriaPrimaStock, value)
        End Set
    End Property

    Dim fAlmacen As Integer
    Public Property Almacen() As Integer
        Get
            Return fAlmacen
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Almacen", fAlmacen, value)
        End Set
    End Property

    Dim fRemanente As Integer
    Public Property Remanente() As Integer
        Get
            Return fRemanente
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Remanente", fRemanente, value)
        End Set
    End Property

    Dim fNameUnitDoseType As String
    Public Property NameUnitDoseType() As String
        Get
            Return fNameUnitDoseType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameUnitDoseType", fNameUnitDoseType, value)
        End Set
    End Property

    Dim fMSClass As Integer
    Public Property MSClass() As Integer
        Get
            Return fMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", fMSClass, value)
        End Set
    End Property

End Class