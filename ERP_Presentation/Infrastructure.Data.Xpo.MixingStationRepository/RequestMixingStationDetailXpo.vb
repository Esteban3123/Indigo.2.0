'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/03/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.RequestMixingStationDetail")>
Partial Public Class RequestMixingStationDetailXpo
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

    Dim fPackageId As Integer?
    Public Property PackageId() As Integer?
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PackageId", fPackageId, value)
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
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

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
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

    Dim fUnitDoseTypeId As Integer
    <PersistentAlias("UnitDoseType.Id")>
    Public ReadOnly Property UnitDoseTypeId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("UnitDoseTypeId"))
        End Get
    End Property

    Dim fUnitDoseTypeObject As MixinStationUnitDoseTypeXpo
    <Persistent("UnitDoseTypeId")>
    <Association("RequestMixingStationDetail_References_UnitDoseType")>
    Public Property UnitDoseType() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeObject
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeObject, value)
        End Set
    End Property

    Dim fRequestPackageDetailStatusId As RequestPackageDetailStatusXpo
    Public Property RequestPackageDetailStatusId() As RequestPackageDetailStatusXpo
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As RequestPackageDetailStatusXpo)
            SetPropertyValue(Of RequestPackageDetailStatusXpo)("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
        End Set
    End Property

    <Association("RequestPackageDetailStatus_References_RquestMixingStationDetail", GetType(RequestPackageDetailStatusXpo))>
    Public ReadOnly Property RequestPackageDetailStatusXpo() As XPCollection(Of RequestPackageDetailStatusXpo)
        Get
            Return GetCollection(Of RequestPackageDetailStatusXpo)("RequestPackageDetailStatusXpo")
        End Get
    End Property

End Class