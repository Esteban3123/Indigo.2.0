Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.ViewListFixedAssetEntryItemDetail")> _
Public Class ViewListFixedAssetEntryItemDetail
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCodeNameItem As String
    Public Property CodeNameItem() As String
        Get
            Return fCodeNameItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameItem", fCodeNameItem, value)
        End Set
    End Property

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fFixedAssetEntryId As Integer
    Public Property FixedAssetEntryId() As Integer
        Get
            Return fFixedAssetEntryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryId", fFixedAssetEntryId, value)
        End Set
    End Property

    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
        End Set
    End Property

    Dim fSubTotalValue As Decimal
    Public Property SubTotalValue() As Decimal
        Get
            Return fSubTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalValue", fSubTotalValue, value)
        End Set
    End Property

    Dim fFixedAssetEntryItemId As Integer
    Public Property FixedAssetEntryItemId() As Integer
        Get
            Return fFixedAssetEntryItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryItemId", fFixedAssetEntryItemId, value)
        End Set
    End Property

    Dim fFixedAssetEntryItemDetailId As Integer
    Public Property FixedAssetEntryItemDetailId() As Integer
        Get
            Return fFixedAssetEntryItemDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryItemDetailId", fFixedAssetEntryItemDetailId, value)
        End Set
    End Property

    Dim fFixedAssetEntryDevolutionDetailId As Integer
    Public Property FixedAssetEntryDevolutionDetailId() As Integer
        Get
            Return fFixedAssetEntryDevolutionDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetEntryDevolutionDetailId", fFixedAssetEntryDevolutionDetailId, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SelectOption", fSelectOption, value)
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

    Dim fIvaPercentage As Decimal
    Public Property IvaPercentage() As Decimal
        Get
            Return fIvaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaPercentage", fIvaPercentage, value)
        End Set
    End Property

    Dim fIvaValue As Decimal
    <DbType("numeric(20,4)")>
    Public Property IvaValue() As Decimal
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValue", fIvaValue, value)
        End Set
    End Property

    Dim fDiscountPercentage As Decimal
    Public Property DiscountPercentage() As Decimal
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercentage", fDiscountPercentage, value)
        End Set
    End Property

    Dim fDiscountValue As Decimal
    Public Property DiscountValue() As Decimal
        Get
            Return fDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValue", fDiscountValue, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fRTFPercentage As Decimal
    Public Property RTFPercentage() As Decimal
        Get
            Return fRTFPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFPercentage", fRTFPercentage, value)
        End Set
    End Property

    Dim fRTFValue As Decimal
    Public Property RTFValue() As Decimal
        Get
            Return fRTFValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFValue", fRTFValue, value)
        End Set
    End Property

    Dim fWithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return fWithholdingTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingTax", fWithholdingTax, value)
        End Set
    End Property

    Dim fWithholdingICA As Decimal
    Public Property WithholdingICA() As Decimal
        Get
            Return fWithholdingICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingICA", fWithholdingICA, value)
        End Set
    End Property

    Dim fRetentionSource As Decimal
    Public Property RetentionSource() As Decimal
        Get
            Return fRetentionSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionSource", fRetentionSource, value)
        End Set
    End Property

    Dim fSubTotalValueSource As Decimal
    Public Property SubTotalValueSource() As Decimal
        Get
            Return fSubTotalValueSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalValueSource", fSubTotalValueSource, value)
        End Set
    End Property

    Dim fIvaValueSource As Decimal
    Public Property IvaValueSource() As Decimal
        Get
            Return fIvaValueSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValueSource", fIvaValueSource, value)
        End Set
    End Property

     Dim fDiscountValueSource As Decimal
    Public Property DiscountValueSource() As Decimal
        Get
            Return fDiscountValueSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValueSource", fDiscountValueSource, value)
        End Set
    End Property

    Dim fTotalValueSource As Decimal
    Public Property TotalValueSource() As Decimal
        Get
            Return fTotalValueSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValueSource", fTotalValueSource, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


