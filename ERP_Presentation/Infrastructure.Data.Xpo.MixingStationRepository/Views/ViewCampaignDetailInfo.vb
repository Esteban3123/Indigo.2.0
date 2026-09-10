'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : 
' Created          : 
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewCampaignDetailInfo")>
Partial Public Class ViewCampaignDetailInfoXpo
	Inherits XPLiteObject

#Region "Builders"
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Public Sub New()
		MyBase.New(Session.DefaultSession)
	End Sub

	Public Overrides Sub AfterConstruction()
		MyBase.AfterConstruction()
	End Sub

#End Region

#Region "Properties"
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

	Dim fCampaingId As Integer
	Public Property CampaingId() As Integer
		Get
			Return fCampaingId
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("CampaingId", fCampaingId, value)
		End Set
	End Property

	Dim fIdMixingStation As Integer
	Public Property IdMixingStation() As Integer
		Get
			Return fIdMixingStation
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("IdMixingStation", fIdMixingStation, value)
		End Set
	End Property

	Dim fMixingStation As String
	Public Property MixingStation() As String
		Get
			Return fMixingStation
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("MixingStation", fMixingStation, value)
		End Set
	End Property

	Dim fWareHouseRemaining As String
	Public Property WareHouseRemaining() As String
		Get
			Return fWareHouseRemaining
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("WareHouseRemaining", fWareHouseRemaining, value)
		End Set
	End Property

	Dim fIdWarehouse As Integer
	Public Property IdWarehouse() As Integer
		Get
			Return fIdWarehouse
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("IdWarehouse", fIdWarehouse, value)
		End Set
	End Property

#End Region
End Class