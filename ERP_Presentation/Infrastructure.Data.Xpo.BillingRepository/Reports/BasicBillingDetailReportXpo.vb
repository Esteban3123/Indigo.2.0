Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BasicBillingDetail")>
Public Class BasicBillingDetailReportXpo
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

    Dim fBasicBillingId As BasicBillingReportXpo
    <Association("BasicBillingDetail_References_BasicBilling")>
    Public Property BasicBillingId() As BasicBillingReportXpo
        Get
            Return fBasicBillingId
        End Get
        Set(ByVal value As BasicBillingReportXpo)
            SetPropertyValue(Of BasicBillingReportXpo)("BasicBillingId", fBasicBillingId, value)
        End Set
    End Property

    Dim fDetailType As Byte
    Public Property DetailType() As Byte
        Get
            Return fDetailType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DetailType", fDetailType, value)
        End Set
    End Property

    <PersistentAlias("Iif(DetailType = 1, 'Producto', Iif(DetailType = 2, 'Servicio',Iif(DetailType = 3, 'Activo Fijo', 'Parte de Activo Fijo')))")>
    Public ReadOnly Property DetailTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DetailTypeName"))
        End Get
    End Property

    Dim fProductId As InventoryProductReportXpo
    <Association("BasicBillingDetail_References_Product")>
    Public Property ProductId() As InventoryProductReportXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As InventoryProductReportXpo)
            SetPropertyValue(Of InventoryProductReportXpo)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fBillingConceptId As BillingConceptReportXpo
    <Association("BasicBillingDetail_References_BillingConcept")>
    Public Property BillingConceptId() As BillingConceptReportXpo
        Get
            Return fBillingConceptId
        End Get
        Set(ByVal value As BillingConceptReportXpo)
            SetPropertyValue(Of BillingConceptReportXpo)("BillingConceptId", fBillingConceptId, value)
        End Set
    End Property

    Dim fServicesProvidedId As BillingConceptReportXpo
    <Association("BasicBillingDetailServicesProvided_References_BillingConcept")>
    Public Property ServicesProvidedId() As BillingConceptReportXpo
        Get
            Return fServicesProvidedId
        End Get
        Set(ByVal value As BillingConceptReportXpo)
            SetPropertyValue(Of BillingConceptReportXpo)("ServicesProvidedId", fServicesProvidedId, value)
        End Set
    End Property

    Dim fPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("BasicBillingDetail_References_PhysicalAsset")>
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("PhysicalAssetId", fPhysicalAssetId, value)
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

    Dim fPrice As Decimal
    Public Property Price() As Decimal
        Get
            Return fPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Price", fPrice, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fPercentageDiscount As Decimal
    Public Property PercentageDiscount() As Decimal
        Get
            Return fPercentageDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageDiscount", fPercentageDiscount, value)
        End Set
    End Property

    Dim fPercentageIVA As Decimal
    Public Property PercentageIVA() As Decimal
        Get
            Return fPercentageIVA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageIVA", fPercentageIVA, value)
        End Set
    End Property

#End Region

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