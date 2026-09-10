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

<Persistent("MixingStation.ViewListDashboardRequestMixingStation")>
Partial Public Class ViewListDashboardRequestMixingStationXpo
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

    Dim fRequestMixingStationId As String
    Public Property RequestMixingStationId() As String
        Get
            Return fRequestMixingStationId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestMixingStationId", fRequestMixingStationId, value)
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

    Dim fRequestCode As String
    Public Property RequestCode() As String
        Get
            Return fRequestCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestCode", fRequestCode, value)
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

    Dim fRequestUserCodeName As String
    Public Property RequestUserCodeName() As String
        Get
            Return fRequestUserCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestUserCodeName", fRequestUserCodeName, value)
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

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName() As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
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

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
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

    Dim fMSClass As Integer
    Public Property MSClass() As Integer
        Get
            Return fMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", fMSClass, value)
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

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
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

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fOrderField As Byte
    Public Property OrderField() As Byte
        Get
            Return fOrderField
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue("OrderField", fOrderField, value)
        End Set
    End Property

    Dim fConfirmationStatus As Integer
    Public Property ConfirmationStatus() As Integer
        Get
            Return fConfirmationStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("ConfirmationStatus", fConfirmationStatus, value)
        End Set
    End Property

End Class