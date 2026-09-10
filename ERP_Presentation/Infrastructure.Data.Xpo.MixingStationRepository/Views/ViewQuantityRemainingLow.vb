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

<Persistent("MixingStation.ViewQuantityRemainingLow")>
Partial Public Class ViewQuantityRemainingLowXpo
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

	Dim fRowNumber As Integer
	Public Property RowNumber() As Integer
		Get
			Return fRowNumber
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("RowNumber", fRowNumber, value)
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

	Dim fProductId As Integer
	Public Property ProductId() As Integer
		Get
			Return fProductId
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("ProductId", fProductId, value)
		End Set
	End Property

	Dim fProductFullName As String
	Public Property ProductFullName() As String
		Get
			Return fProductFullName
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("ProductFullName", fProductFullName, value)
		End Set
	End Property

	Dim fBatchSerialId As Integer
	Public Property BatchSerialId() As Integer
		Get
			Return fBatchSerialId
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("BatchSerialId", fBatchSerialId, value)
		End Set
	End Property

	Dim fBatchSerialCode As String
	Public Property BatchSerialCode() As String
		Get
			Return fBatchSerialCode
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("BatchSerialCode", fBatchSerialCode, value)
		End Set
	End Property

	Dim fOpeningDate As String
	Public Property OpeningDate() As String
		Get
			Return fOpeningDate
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("OpeningDate", fOpeningDate, value)
		End Set
	End Property

	Dim fVctostability As String
	Public Property Vctostability() As String
		Get
			Return fVctostability
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("Vctostability", fVctostability, value)
		End Set
	End Property

	Dim fStability As Integer
	Public Property Stability() As Integer
		Get
			Return fStability
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("Stability", fStability, value)
		End Set
	End Property

	Dim fExpirationDate As String
	Public Property ExpirationDate() As String
		Get
			Return fExpirationDate
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("ExpirationDate", fExpirationDate, value)
		End Set
	End Property

	Dim fConcentration As Decimal
	Public Property Concentration() As Decimal
		Get
			Return fConcentration
		End Get
		Set(ByVal value As Decimal)
			SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
		End Set
	End Property

	Dim fConcentrationWithMeasurementUnit As String
	Public Property ConcentrationWithMeasurementUnit() As String
		Get
			Return fConcentrationWithMeasurementUnit
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("ConcentrationWithMeasurementUnit", fConcentrationWithMeasurementUnit, value)
		End Set
	End Property

	Dim fRemnantVolume As Decimal
	Public Property RemnantVolume() As Decimal
		Get
			Return fRemnantVolume
		End Get
		Set(ByVal value As Decimal)
			SetPropertyValue(Of Decimal)("RemnantVolume", fRemnantVolume, value)
		End Set
	End Property

	Dim fQuantity As Decimal
	Public Property Quantity() As Decimal
		Get
			Return fQuantity
		End Get
		Set(ByVal value As Decimal)
			SetPropertyValue(Of Decimal)("Quantity", fQuantity, value)
		End Set
	End Property

	Dim fCauseReprocessingRejectionId As Integer
	Public Property CauseReprocessingRejectionId() As Integer
		Get
			Return fCauseReprocessingRejectionId
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("CauseReprocessingRejectionId", fCauseReprocessingRejectionId, value)
		End Set
	End Property

	Dim fCauseReprocessingRejectionCode As String
	Public Property CauseReprocessingRejectionCode() As String
		Get
			Return fCauseReprocessingRejectionCode
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("CauseReprocessingRejectionCode", fCauseReprocessingRejectionCode, value)
		End Set
	End Property

	Dim fCauseReprocessingRejectionName As String
	Public Property CauseReprocessingRejectionName() As String
		Get
			Return fCauseReprocessingRejectionName
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("CauseReprocessingRejectionName", fCauseReprocessingRejectionName, value)
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

	Dim fWareHouseRemaining As String
	Public Property WareHouseRemaining() As String
		Get
			Return fWareHouseRemaining
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("WareHouseRemaining", fWareHouseRemaining, value)
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

	Dim fIdWarehouse As Integer
	Public Property IdWarehouse() As Integer
		Get
			Return fIdWarehouse
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("IdWarehouse", fIdWarehouse, value)
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

	Dim fStatus As Integer
	Public Property Status() As Integer
		Get
			Return fStatus
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(Of Integer)("Status", fStatus, value)
		End Set
	End Property

	Dim fQP As String
	Public Property QP() As String
		Get
			Return fQP
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("QP", fQP, value)
		End Set
	End Property

	Dim fQC As String
	Public Property QC() As String
		Get
			Return fQC
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("QC", fQC, value)
		End Set
	End Property
#End Region
End Class