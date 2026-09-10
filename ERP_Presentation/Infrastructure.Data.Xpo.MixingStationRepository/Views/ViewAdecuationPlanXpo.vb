'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldán
' Created          : 2022-08-01
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewAdecuationPlan")>
Partial Public Class ViewAdecuationPlanXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Long
    <Key(True)>
    Public Property Id() As Long
        Get
            Return fId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Id", fId, value)
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

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fItemCode As String
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCode", fItemCode, value)
        End Set
    End Property

    Dim fItemName As String
    Public Property ItemName() As String
        Get
            Return fItemName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemName", fItemName, value)
        End Set
    End Property

    Dim fMeasurementUnitAbbreviation As String
    Public Property MeasurementUnitAbbreviation() As String
        Get
            Return fMeasurementUnitAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitAbbreviation", fMeasurementUnitAbbreviation, value)
        End Set
    End Property

    Dim fRequiredQuantity As Integer
    Public Property RequiredQuantity() As Integer
        Get
            Return fRequiredQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequiredQuantity", fRequiredQuantity, value)
        End Set
    End Property

    Dim fPackageCode As String
    Public Property PackageCode() As String
        Get
            Return fPackageCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageCode", fPackageCode, value)
        End Set
    End Property

    Dim fAtcId As Integer?
    Public Property AtcId() As Integer?
        Get
            Return fAtcId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fSupplieId As Integer?
    Public Property SupplieId() As Integer?
        Get
            Return fSupplieId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("SupplieId", fSupplieId, value)
        End Set
    End Property

    Dim fProductId As Integer?
    Public Property ProductId() As Integer?
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ProductId", fProductId, value)
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

    Dim fMeasurementUnitId As Integer
    Public Property MeasurementUnitId() As Integer
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

    Dim fComponentType As Byte
    Public Property ComponentType() As Byte
        Get
            Return fComponentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComponentType", fComponentType, value)
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

    Dim fVehicle As Boolean
    Public Property Vehicle() As Boolean
        Get
            Return fVehicle
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Vehicle", fVehicle, value)
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

    Dim fConcentration As Decimal
    Public Property Concentration() As Decimal
        Get
            Return fConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
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

    <NonPersistent>
    Public Property RequiredQuantityUnit As Integer
    <NonPersistent>
    Public Property RequiredQuantityWithMeasure As String
    <NonPersistent>
    Public Property GroupTitle As String
    <NonPersistent>
    Public Property RemanenteMeasureUnit As String
    <NonPersistent>
    Public Property BatchData As List(Of BatchData)

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class

Public Class BatchData
    Public Property BatchCode As String
    Public Property Quantity As Integer
    Public Property MeasurementUnit As String
End Class