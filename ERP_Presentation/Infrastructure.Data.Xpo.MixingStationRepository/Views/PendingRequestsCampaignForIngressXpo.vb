'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 13/02/2024
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.PendingRequestsCampaignForIngress")>
Partial Public Class PendingRequestsCampaignForIngressXpo
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

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fCampaignStatus As Integer
    Public Property CampaignStatus() As Integer
        Get
            Return fCampaignStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignStatus", fCampaignStatus, value)
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

    Dim fNUMINGRES As String
    Public Property NUMINGRES() As String
        Get
            Return fNUMINGRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMINGRES", fNUMINGRES, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

End Class