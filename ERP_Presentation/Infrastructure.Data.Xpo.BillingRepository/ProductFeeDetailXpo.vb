'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Andres Alarcon
' Created          : 2023-05-30
'
' Copyright        : (c) . All rights reserved.
'*************************************

Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository

<Persistent("Billing.ProductFeeDetail")>
Public Class ProductFeeDetailXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fProductAndServiceFeeId As Integer
    Public Property ProductAndServiceFeeId() As Integer
        Get
            Return fProductAndServiceFeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductAndServiceFeeId", fProductAndServiceFeeId, value)
        End Set
    End Property

    Dim fProductId As InventoryRepository.InventoryProductXpo
    Public Property ProductId() As InventoryRepository.InventoryProductXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As InventoryRepository.InventoryProductXpo)
            SetPropertyValue(Of InventoryRepository.InventoryProductXpo)("ProductId", fProductId, value)
        End Set
    End Property

    <PersistentAlias("ProductId.Code")>
    Public ReadOnly Property Code() As String
        Get
            Return Me.EvaluateAlias("Code").ToString()
        End Get
    End Property

    <PersistentAlias("ProductId.Name")>
    Public ReadOnly Property Name() As String
        Get
            Return Me.EvaluateAlias("Name").ToString()
        End Get
    End Property

    <PersistentAlias("ProductId.PackagingUnitId.CodeName")>
    Public ReadOnly Property PackagingUnitId_CodeName() As String
        Get
            Return Me.EvaluateAlias("PackagingUnitId_CodeName")
        End Get
    End Property

    <PersistentAlias("ProductId.MeasurementUnitId.CodeName")>
    Public ReadOnly Property MeasurementUnitId_CodeName() As String
        Get
            Return Me.EvaluateAlias("MeasurementUnitId_CodeName")
        End Get
    End Property

    <PersistentAlias("ProductId.FinalProductCost")>
    Public ReadOnly Property FinalProductCost() As Decimal
        Get
            Return Me.EvaluateAlias("FinalProductCost").ToString()
        End Get
    End Property

    <PersistentAlias("ProductId.POSProduct")>
    Public ReadOnly Property POSProduct() As Boolean
        Get
            Return Me.EvaluateAlias("POSProduct").ToString()
        End Get
    End Property

    Dim fRateType As Byte
    Public Property RateType() As Byte
        Get
            Return fRateType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RateType", fRateType, value)
        End Set
    End Property

    Dim fPercentageType As Byte
    Public Property PercentageType() As Byte
        Get
            Return fPercentageType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PercentageType", fPercentageType, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fFinalDate As DateTime
    Public Property FinalDate() As DateTime
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalDate ", fFinalDate, value)
        End Set
    End Property

    Dim fSalePrice As Decimal
    Public Property SalePrice() As Decimal
        Get
            Return fSalePrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalePrice", fSalePrice, value)
        End Set
    End Property

    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region
End Class
