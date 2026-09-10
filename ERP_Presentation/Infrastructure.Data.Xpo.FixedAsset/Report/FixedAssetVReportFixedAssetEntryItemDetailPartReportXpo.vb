Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportFixedAssetEntryItemDetailPart")> _
Public Class FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo
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
    Dim fIdEntry As FixedAssetFixedAssetEntryReportXpo

    <Association("VReportFixedAssetEntryDocumentSupport_References_FixedAssetEntry")>
    Public Property IdEntry() As FixedAssetFixedAssetEntryReportXpo
        Get
            Return fIdEntry
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryReportXpo)("IdEntry", fIdEntry, value)
        End Set
    End Property
    Dim fStatus As String
    <Size(10)> _
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property
    Dim fAdmisionNumber As String
    <Size(20)> _
    Public Property AdmisionNumber() As String
        Get
            Return fAdmisionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmisionNumber", fAdmisionNumber, value)
        End Set
    End Property
    Dim fEntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return fEntryDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EntryDate", fEntryDate, value)
        End Set
    End Property
    Dim fNameSupplier As String
    Public Property NameSupplier() As String
        Get
            Return fNameSupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSupplier", fNameSupplier, value)
        End Set
    End Property
    Dim fCodeSupplier As String
    <Size(20)> _
    Public Property CodeSupplier() As String
        Get
            Return fCodeSupplier
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSupplier", fCodeSupplier, value)
        End Set
    End Property
    Dim fAddress As String
    Public Property Address() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Address", fAddress, value)
        End Set
    End Property
    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property
    Dim fCodeCxP As String
    <Size(20)> _
    Public Property CodeCxP() As String
        Get
            Return fCodeCxP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCxP", fCodeCxP, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fCityName As String
    Public Property CityName() As String
        Get
            Return fCityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CityName", fCityName, value)
        End Set
    End Property
    Dim fGetLocationResponsible As Byte
    Public Property GetLocationResponsible() As Byte
        Get
            Return fGetLocationResponsible
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetLocationResponsible", fGetLocationResponsible, value)
        End Set
    End Property
    Dim fPhone As String
    <Size(15)> _
    Public Property Phone() As String
        Get
            Return fPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Phone", fPhone, value)
        End Set
    End Property
    Dim fCodeResponsible As String
    <Size(20)> _
    Public Property CodeResponsible() As String
        Get
            Return fCodeResponsible
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeResponsible", fCodeResponsible, value)
        End Set
    End Property
    Dim fNameResponsible As String
    <Size(300)> _
    Public Property NameResponsible() As String
        Get
            Return fNameResponsible
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameResponsible", fNameResponsible, value)
        End Set
    End Property
    Dim fCodeLocation As String
    <Size(20)> _
    Public Property CodeLocation() As String
        Get
            Return fCodeLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeLocation", fCodeLocation, value)
        End Set
    End Property
    Dim fNameLocation As String
    <Size(50)> _
    Public Property NameLocation() As String
        Get
            Return fNameLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameLocation", fNameLocation, value)
        End Set
    End Property
    Dim fCodeItem As String
    <Size(20)> _
    Public Property CodeItem() As String
        Get
            Return fCodeItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeItem", fCodeItem, value)
        End Set
    End Property
    Dim fDescriptionItem As String
    <Size(300)> _
    Public Property DescriptionItem() As String
        Get
            Return fDescriptionItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionItem", fDescriptionItem, value)
        End Set
    End Property
    Dim fCodeLocationDetail As String
    <Size(20)> _
    Public Property CodeLocationDetail() As String
        Get
            Return fCodeLocationDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeLocationDetail", fCodeLocationDetail, value)
        End Set
    End Property
    Dim fNameLocationDetail As String
    <Size(50)> _
    Public Property NameLocationDetail() As String
        Get
            Return fNameLocationDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameLocationDetail", fNameLocationDetail, value)
        End Set
    End Property
    Dim fPlate As String
    <Size(50)> _
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
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
    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
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
    Dim fValueDiscount As Decimal
    Public Property ValueDiscount() As Decimal
        Get
            Return fValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscount", fValueDiscount, value)
        End Set
    End Property
    Dim fIva As Decimal
    Public Property Iva() As Decimal
        Get
            Return fIva
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Iva", fIva, value)
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
    Dim fRetentionOther As Decimal
    Public Property RetentionOther() As Decimal
        Get
            Return fRetentionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionOther", fRetentionOther, value)
        End Set
    End Property
    Dim fDeductionOther As Decimal
    Public Property DeductionOther() As Decimal
        Get
            Return fDeductionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductionOther", fDeductionOther, value)
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
    Dim fFreightValue As Decimal
    Public Property FreightValue() As Decimal
        Get
            Return fFreightValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightValue", fFreightValue, value)
        End Set
    End Property
    Dim fFreightIVAValue As Decimal
    Public Property FreightIVAValue() As Decimal
        Get
            Return fFreightIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAValue", fFreightIVAValue, value)
        End Set
    End Property
    Dim fTotal As Decimal
    Public Property Total() As Decimal
        Get
            Return fTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Total", fTotal, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(1000)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fCodePartAccesorieConsumible As String
    <Size(20)> _
    Public Property CodePartAccesorieConsumible() As String
        Get
            Return fCodePartAccesorieConsumible
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodePartAccesorieConsumible", fCodePartAccesorieConsumible, value)
        End Set
    End Property
    Dim fNamePartAccesorieConsumible As String
    <Size(50)> _
    Public Property NamePartAccesorieConsumible() As String
        Get
            Return fNamePartAccesorieConsumible
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NamePartAccesorieConsumible", fNamePartAccesorieConsumible, value)
        End Set
    End Property
    Dim fDepreciatePart As Boolean
    Public Property DepreciatePart() As Boolean
        Get
            Return fDepreciatePart
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DepreciatePart", fDepreciatePart, value)
        End Set
    End Property
    Dim fValuePart As Decimal
    Public Property ValuePart() As Decimal
        Get
            Return fValuePart
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePart", fValuePart, value)
        End Set
    End Property
    Dim fUserCode As String
    <Size(20)> _
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property
    Dim fFullname As String
    <Size(250)> _
    Public Property Fullname() As String
        Get
            Return fFullname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Fullname", fFullname, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
