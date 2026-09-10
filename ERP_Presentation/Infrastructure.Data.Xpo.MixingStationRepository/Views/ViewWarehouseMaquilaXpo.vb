'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Giovanny Plazas Lozano
' Created          : 05/08/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewWarehouseMaquila")>
Partial Public Class ViewWarehouseMaquilaXpo
    Inherits XPLiteObject

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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
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

    Dim fSource As Byte
    Public Property Source() As Byte
        Get
            Return fSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Source", fSource, value)
        End Set
    End Property

    Dim fWarehouseId As Integer
    Public Property WarehouseId() As Integer
        Get
            Return fWarehouseId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WarehouseId", fWarehouseId, value)
        End Set
    End Property

    Dim fWarehouseMaquilaCodeName As String
    Public Property WarehouseMaquilaCodeName() As String
        Get
            Return fWarehouseMaquilaCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WarehouseMaquilaCodeName", fWarehouseMaquilaCodeName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fExternalCareCenterCode As String
    Public Property ExternalCareCenterCode() As String
        Get
            Return fExternalCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ExternalCareCenterCode", fExternalCareCenterCode, value)
        End Set
    End Property

    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fCustomerNitName As String
    Public Property CustomerNitName() As String
        Get
            Return fCustomerNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerNitName", fCustomerNitName, value)
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