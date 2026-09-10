'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Giovanny Plazas Lozano
' Created          : 09-09-2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.RequestPackageDetailStatus")>
Partial Public Class RequestPackageDetailStatusXpo
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

    Dim fRequestMixingStationDetaiId As Integer
    <PersistentAlias("RequestMixingStationDetail.Id")>
    Public ReadOnly Property RequestMixingStationDetailId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("RequestMixingStationDetailId"))
        End Get
    End Property

    Dim fRequestMixingStationDetailObjetc As RequestMixingStationDetailXpo
    <Persistent("RequestMixingStationDetailId")>
    <Association("RequestPackageDetailStatus_References_RquestMixingStationDetail")>
    Public Property RequestMixingStationDetail() As RequestMixingStationDetailXpo
        Get
            Return fRequestMixingStationDetailObjetc
        End Get
        Set(ByVal value As RequestMixingStationDetailXpo)
            SetPropertyValue(Of RequestMixingStationDetailXpo)("RequestMixingStationDetailId", fRequestMixingStationDetailObjetc, value)
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

    Dim fPackagePersonalizedId As Integer?
    Public Property PackagePersonalizedId() As Integer?
        Get
            Return fPackagePersonalizedId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PackagePersonalizedId", fPackagePersonalizedId, value)
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

    Dim fQualityStatus As Byte
    Public Property QualityStatus() As Byte
        Get
            Return fQualityStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("QualityStatus", fQualityStatus, value)
        End Set
    End Property

    Dim fDispensingWarehouseId As Integer?
    Public Property DispensingWarehouseId() As Integer?
        Get
            Return fDispensingWarehouseId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DispensingWarehouseId", fDispensingWarehouseId, value)
        End Set
    End Property

    Dim fPhysicalInventoryId As Integer?
    Public Property PhysicalInventoryId() As Integer?
        Get
            Return fPhysicalInventoryId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PhysicalInventoryId", fPhysicalInventoryId, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
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

    Dim fSendTo As Byte
    Public Property SendTo() As Byte
        Get
            Return fSendTo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SendTo", fSendTo, value)
        End Set
    End Property

    Dim fVerificationTagUser As String
    Public Property VerificationTagUser() As String
        Get
            Return fVerificationTagUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VerificationTagUser", fVerificationTagUser, value)
        End Set
    End Property

    Dim fVerificationTagDate As DateTime?
    Public Property VerificationTagDate() As DateTime?
        Get
            Return fVerificationTagDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("VerificationTagDate", fVerificationTagDate, value)
        End Set
    End Property

#Region "PersistentAlias"
    <PersistentAlias("IIF(Status = 1,'Producción',Status = 2,'Terminado',Status= 3,'Liberado',Status= 4,'Reproceso',Status = 5, 'Rechazado', 'Anulado')")>
    Public ReadOnly Property StatusNameRequest() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusNameRequest"))
        End Get
    End Property
#End Region

#Region "Association"

    <Association("CampaignDetailUsersReferencesCampaignDetail", GetType(CampaignDetailUsersXpo))>
    Public ReadOnly Property CampaignDetailUsersXpo() As XPCollection(Of CampaignDetailUsersXpo)
        Get
            Return GetCollection(Of CampaignDetailUsersXpo)("CampaignDetailUsersXpo")
        End Get
    End Property

#End Region


End Class