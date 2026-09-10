'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/01/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListRawMaterial")>
Partial Public Class ViewListRawMaterialXpo
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

    Dim fRowPrincipalItemType As Integer
    Public Property RowPrincipalItemType() As Integer
        Get
            Return fRowPrincipalItemType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RowPrincipalItemType", fRowPrincipalItemType, value)
        End Set
    End Property

    Dim fRowPrincipalItemTypeName As String
    Public Property RowPrincipalItemTypeName() As String
        Get
            Return fRowPrincipalItemTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RowPrincipalItemTypeName", fRowPrincipalItemTypeName, value)
        End Set
    End Property

    Dim fRowPrincipalItemId As Integer
    Public Property RowPrincipalItemId() As Integer
        Get
            Return fRowPrincipalItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RowPrincipalItemId", fRowPrincipalItemId, value)
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

    Dim fMainMedicine As Boolean
    Public Property MainMedicine() As Boolean
        Get
            Return fMainMedicine
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MainMedicine", fMainMedicine, value)
        End Set
    End Property

    Dim fThinner As Boolean
    Public Property Thinner() As Boolean
        Get
            Return fThinner
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Thinner", fThinner, value)
        End Set
    End Property

    Dim fVehicle As Boolean
    Public Property Vehicle() As Boolean
        Get
            Return fVehicle
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Vehicle", fVehicle, value)
        End Set
    End Property

    Dim fGroupName As String
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailId As Integer
    Public Property RequestMixingStationDetailId() As Integer
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
        End Set
    End Property

    Dim fMeasurementUnitCodeName As String
    Public Property MeasurementUnitCodeName() As String
        Get
            Return fMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitCodeName", fMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fComponentTypeName As String
    Public Property ComponentTypeName() As String
        Get
            Return fComponentTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ComponentTypeName", fComponentTypeName, value)
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

    Dim fVolumeMeasurementUnitCodeName As String
    Public Property VolumeMeasurementUnitCodeName() As String
        Get
            Return fVolumeMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VolumeMeasurementUnitCodeName", fVolumeMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fConcentrationMeasurementUnitCodeName As String
    Public Property ConcentrationMeasurementUnitCodeName() As String
        Get
            Return fConcentrationMeasurementUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConcentrationMeasurementUnitCodeName", fConcentrationMeasurementUnitCodeName, value)
        End Set
    End Property

    Dim fConcentration As String
    Public Property Concentration() As String
        Get
            Return fConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As Decimal
    Public Property VolumeTotalOrder() As Decimal
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fPhotoProtectionName As String
    Public Property PhotoProtectionName() As String
        Get
            Return fPhotoProtectionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PhotoProtectionName", fPhotoProtectionName, value)
        End Set
    End Property

    Dim fPreparationTypeName As String
    Public Property PreparationTypeName() As String
        Get
            Return fPreparationTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PreparationTypeName", fPreparationTypeName, value)
        End Set
    End Property

    Dim fRefrigeratedTerm As Integer
    Public Property RefrigeratedTerm() As Integer
        Get
            Return fRefrigeratedTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RefrigeratedTerm", fRefrigeratedTerm, value)
        End Set
    End Property

    Dim fEnvironmentalTemperatureTerm As Integer
    Public Property EnvironmentalTemperatureTerm() As Integer
        Get
            Return fEnvironmentalTemperatureTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EnvironmentalTemperatureTerm", fEnvironmentalTemperatureTerm, value)
        End Set
    End Property

    Dim fPurge As Decimal
    Public Property Purge() As Decimal
        Get
            Return fPurge
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Purge", fPurge, value)
        End Set
    End Property

    Dim fPreparationInstructions As String
    Public Property PreparationInstructions() As String
        Get
            Return fPreparationInstructions
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PreparationInstructions", fPreparationInstructions, value)
        End Set
    End Property

    Dim fSpecialConsiderations As String
    Public Property SpecialConsiderations() As String
        Get
            Return fSpecialConsiderations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialConsiderations", fSpecialConsiderations, value)
        End Set
    End Property
End Class