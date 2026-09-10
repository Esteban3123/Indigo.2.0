Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportPurchaseOrder")> _
Public Class FixedAssetVReportPurchaseOrderXpo
    Inherits XPLiteObject

    Dim fidDetailPurchaseOrder As Integer
    <Key(True)> _
    Public Property idDetailPurchaseOrder() As Integer
        Get
            Return fidDetailPurchaseOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("idDetailPurchaseOrder", fidDetailPurchaseOrder, value)
        End Set
    End Property
    Dim fidPurchaseOrder As Integer
    Public Property idPurchaseOrder() As Integer
        Get
            Return fidPurchaseOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("idPurchaseOrder", fidPurchaseOrder, value)
        End Set
    End Property
    Dim fcodePurchaseOrder As String
    <Size(20)> _
    Public Property codePurchaseOrder() As String
        Get
            Return fcodePurchaseOrder
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("codePurchaseOrder", fcodePurchaseOrder, value)
        End Set
    End Property
    Dim fdatePurchaseOrder As DateTime
    Public Property datePurchaseOrder() As DateTime
        Get
            Return fdatePurchaseOrder
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("datePurchaseOrder", fdatePurchaseOrder, value)
        End Set
    End Property
    Dim fdateDeliver As DateTime
    Public Property dateDeliver() As DateTime
        Get
            Return fdateDeliver
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("dateDeliver", fdateDeliver, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fstatusPurchaseOrder As Byte
    Public Property statusPurchaseOrder() As Byte
        Get
            Return fstatusPurchaseOrder
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("statusPurchaseOrder", fstatusPurchaseOrder, value)
        End Set
    End Property
    Dim fdetailPurchaseOrder As String
    <Size(300)> _
    Public Property detailPurchaseOrder() As String
        Get
            Return fdetailPurchaseOrder
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("detailPurchaseOrder", fdetailPurchaseOrder, value)
        End Set
    End Property
    Dim fcodeEquipment As String
    <Size(20)> _
    Public Property codeEquipment() As String
        Get
            Return fcodeEquipment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("codeEquipment", fcodeEquipment, value)
        End Set
    End Property
    Dim fdescriptionEquipment As String
    Public Property descriptionEquipment() As String
        Get
            Return fdescriptionEquipment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("descriptionEquipment", fdescriptionEquipment, value)
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
    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
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
    Dim fIvaValue As Decimal
    Public Property IvaValue() As Decimal
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValue", fIvaValue, value)
        End Set
    End Property
    Dim fdescriptionThirdParty As String
    <Size(318)> _
    Public Property descriptionThirdParty() As String
        Get
            Return fdescriptionThirdParty
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("descriptionThirdParty", fdescriptionThirdParty, value)
        End Set
    End Property
    Dim fcitySupplier As String
    <Size(110)> _
    Public Property citySupplier() As String
        Get
            Return fcitySupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("citySupplier", fcitySupplier, value)
        End Set
    End Property
    Dim fThirdPartyAddress As String
    Public Property ThirdPartyAddress() As String
        Get
            Return fThirdPartyAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyAddress", fThirdPartyAddress, value)
        End Set
    End Property
    Dim fThirdPartyPhone As String
    <Size(15)>
    Public Property ThirdPartyPhone() As String
        Get
            Return fThirdPartyPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyPhone", fThirdPartyPhone, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceFixedAssetVReportPurchaseOrderXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property


    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
