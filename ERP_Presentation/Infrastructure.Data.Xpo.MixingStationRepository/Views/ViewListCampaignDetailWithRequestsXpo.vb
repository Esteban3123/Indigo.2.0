'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListCampaignDetailWithRequests")>
Partial Public Class ViewListCampaignDetailWithRequestsXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
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

    Dim fRequestMixingStationDetailId As Integer
    Public Property RequestMixingStationDetailId() As Integer
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
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

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fRequestCode As String
    Public Property RequestCode() As String
        Get
            Return fRequestCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestCode", fRequestCode, value)
        End Set
    End Property

    Dim fRequestType As Integer
    Public Property RequestType() As Integer
        Get
            Return fRequestType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestType", fRequestType, value)
        End Set
    End Property

    Dim fRequestTypeName As String
    Public Property RequestTypeName() As String
        Get
            Return fRequestTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestTypeName", fRequestTypeName, value)
        End Set
    End Property

    Dim fRequestDate As DateTime
    Public Property RequestDate() As DateTime
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RequestDate", fRequestDate, value)
        End Set
    End Property

    Dim fRequestUser As String
    Public Property RequestUser() As String
        Get
            Return fRequestUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestUser", fRequestUser, value)
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

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fExistenceQuantity As Integer
    Public Property ExistenceQuantity() As Integer
        Get
            Return fExistenceQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExistenceQuantity", fExistenceQuantity, value)
        End Set
    End Property

    Dim fRequestQuantity As Integer
    Public Property RequestQuantity() As Integer
        Get
            Return fRequestQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestQuantity", fRequestQuantity, value)
        End Set
    End Property

    Dim fProduceQuantity As Integer
    Public Property ProduceQuantity() As Integer
        Get
            Return fProduceQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProduceQuantity", fProduceQuantity, value)
        End Set
    End Property

    Dim fControlNumber As String
    Public Property ControlNumber() As String
        Get
            Return fControlNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ControlNumber", fControlNumber, value)
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

    Dim fCareCenterCodeName As String
    Public Property CareCenterCodeName() As String
        Get
            Return fCareCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCodeName", fCareCenterCodeName, value)
        End Set
    End Property

    Dim fStringIds As String
    Public Property StringIds() As String
        Get
            Return fStringIds
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StringIds", fStringIds, value)
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fStatusHCPRESCRA As Integer
    Public Property StatusHCPRESCRA() As Integer
        Get
            Return fStatusHCPRESCRA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StatusHCPRESCRA", fStatusHCPRESCRA, value)
        End Set
    End Property

    <PersistentAlias("Iif(StatusHCPRESCRA = 1, 'Iniciado', Iif(StatusHCPRESCRA=2, 'Ciclo completado', Iif(StatusHCPRESCRA=3, 'Tratamiento descontinuado', iif(StatusHCPRESCRA=4, 'Tratamiento suspendido', iif(StatusHCPRESCRA=5, 'Plan de Manejo Externo', Iif(StatusHCPRESCRA=6, 'Medicamento solicitado sin existencia en el kardex', 'Tratamiento terminado por salida del paciente'))))))")>
    Public ReadOnly Property StatusNameHCPRESCRA() As String
        Get
            Return Convert.ToString(EvaluateAlias("StatusNameHCPRESCRA"))
        End Get
    End Property

    Dim fLabelType As Byte?
    Public Property LabelType() As Byte?
        Get
            Return fLabelType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("LabelType", fLabelType, value)
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

    Dim fItemId As Integer
    Public Property ItemId() As Integer
        Get
            Return fItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemId", fItemId, value)
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

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bed", fBed, value)
        End Set
    End Property

    Dim fManageQuantity As Integer
    Public Property ManageQuantity() As Integer
        Get
            Return fManageQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManageQuantity", fManageQuantity, value)
        End Set
    End Property

    Dim fIsReadjustment As Integer
    Public Property IsReadjustment() As Integer
        Get
            Return fIsReadjustment
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IsReadjustment", fIsReadjustment, value)
        End Set
    End Property

    Dim fHasReadjustment As Boolean
    Public Property HasReadjustment() As Boolean
        Get
            Return fHasReadjustment
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasReadjustment", fHasReadjustment, value)
        End Set
    End Property

End Class