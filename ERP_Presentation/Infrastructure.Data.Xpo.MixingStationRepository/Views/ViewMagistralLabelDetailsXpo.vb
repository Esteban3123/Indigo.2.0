'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2025-05-05
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewMagistralLabelDetails")>
Public Class ViewMagistralLabelDetailsXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fRequestPackageDetailStatusId As ViewMagistralLabelXpo
    <Association("ViewMagistralLabelXpo_References_ViewMagistralLabelDetailsXpo")>
    Public Property RequestPackageDetailStatusId() As ViewMagistralLabelXpo
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As ViewMagistralLabelXpo)
            SetPropertyValue(Of ViewMagistralLabelXpo)("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
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

    Dim fAtcId As Integer
    Public Property AtcId() As Integer
        Get
            Return fAtcId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fAbbreviationNameAtc As String
    Public Property AbbreviationNameAtc() As String
        Get
            Return fAbbreviationNameAtc
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AbbreviationNameAtc", fAbbreviationNameAtc, value)
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

    Dim fMeasurementUnit As String
    Public Property MeasurementUnit() As String
        Get
            Return fMeasurementUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnit", fMeasurementUnit, value)
        End Set
    End Property

    Dim fQuantityWithUnitMeasurement As String
    Public Property QuantityWithUnitMeasurement() As String
        Get
            Return fQuantityWithUnitMeasurement
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuantityWithUnitMeasurement", fQuantityWithUnitMeasurement, value)
        End Set
    End Property

    Dim fMainMedicine As Byte
    Public Property MainMedicine() As Byte
        Get
            Return fMainMedicine
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MainMedicine", fMainMedicine, value)
        End Set
    End Property

    Dim fVehicle As Byte
    Public Property Vehicle() As Byte
        Get
            Return fVehicle
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Vehicle", fVehicle, value)
        End Set
    End Property

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

End Class