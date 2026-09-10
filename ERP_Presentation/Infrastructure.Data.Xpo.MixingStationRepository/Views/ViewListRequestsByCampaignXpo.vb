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

<Persistent("MixingStation.ViewListRequestsByCampaign")>
Partial Public Class ViewListRequestsByCampaignXpo
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

    Dim fPackageId As Integer
    Public Property PackageId() As Integer
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fPackagePersonalizedId As Integer
    Public Property PackagePersonalizedId() As Integer
        Get
            Return fPackagePersonalizedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackagePersonalizedId", fPackagePersonalizedId, value)
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

    Dim fRequestMixingStationId As Integer
    Public Property RequestMixingStationId() As Integer
        Get
            Return fRequestMixingStationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationId", fRequestMixingStationId, value)
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

    Dim fContractExternalClientsId As Integer?
    Public Property ContractExternalClientsId() As Integer?
        Get
            Return fContractExternalClientsId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ContractExternalClientsId", fContractExternalClientsId, value)
        End Set
    End Property

    Dim fManagesMaquila As Boolean
    Public Property ManagesMaquila() As Boolean
        Get
            Return fManagesMaquila
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ManagesMaquila", fManagesMaquila, value)
        End Set
    End Property

    Dim fPatientFullName As String
    Public Property PatientFullName() As String
        Get
            Return fPatientFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientFullName", fPatientFullName, value)
        End Set
    End Property

End Class