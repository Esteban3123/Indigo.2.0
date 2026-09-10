Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportFixedTransaction")> _
Public Class FixedAssetVReportFixedTransactionReportXpo
    Inherits XPLiteObject
    Dim fRow As Long
    <Key(True)> _
    Public Property Row() As Long
        Get
            Return fRow
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Row", fRow, value)
        End Set
    End Property
    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fContributionType As String
    <Size(80)>
    Public Property ContributionType() As String
        Get
            Return fContributionType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContributionType", fContributionType, value)
        End Set
    End Property
    Dim fAdress As String
    <Size(100)>
    Public Property Adress() As String
        Get
            Return fAdress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Adress", fAdress, value)
        End Set
    End Property
    Dim fPhone As String
    <Size(15)>
    Public Property Phone() As String
        Get
            Return fPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Phone", fPhone, value)
        End Set
    End Property
    Dim fGenerateAccountPayable As Boolean
    Public Property GenerateAccountPayable() As Boolean
        Get
            Return fGenerateAccountPayable
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("GenerateAccountPayable", fGenerateAccountPayable, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
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
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fLifeTime As Integer
    Public Property LifeTime() As Integer
        Get
            Return fLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LifeTime", fLifeTime, value)
        End Set
    End Property
    Dim fType As String
    <Size(15)> _
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property
    Dim fNeto As Decimal
    Public Property Neto() As Decimal
        Get
            Return fNeto
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Neto", fNeto, value)
        End Set
    End Property
    Dim fAcount As String
    <Size(50)> _
    Public Property Acount() As String
        Get
            Return fAcount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Acount", fAcount, value)
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
    Dim fDiscountPercentage As Decimal
    Public Property DiscountPercentage() As Decimal
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercentage", fDiscountPercentage, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(1000)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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
    Dim fIVA As Decimal
    Public Property IVA() As Decimal
        Get
            Return fIVA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IVA", fIVA, value)
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
    Dim fWithholdingICA As Decimal
    Public Property WithholdingICA() As Decimal
        Get
            Return fWithholdingICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingICA", fWithholdingICA, value)
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
    Dim fRetefteMinBase As Decimal
    Public Property RetefteMinBase() As Decimal
        Get
            Return fRetefteMinBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetefteMinBase", fRetefteMinBase, value)
        End Set
    End Property
    Dim fReteicaMinBase As Decimal
    Public Property ReteicaMinBase() As Decimal
        Get
            Return fReteicaMinBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ReteicaMinBase", fReteicaMinBase, value)
        End Set
    End Property
    Dim fReteivaMinBase As Decimal
    Public Property ReteivaMinBase() As Decimal
        Get
            Return fReteivaMinBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ReteivaMinBase", fReteivaMinBase, value)
        End Set
    End Property
    Dim fRetefteRate As Decimal
    Public Property RetefteRate() As Decimal
        Get
            Return fRetefteRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetefteRate", fRetefteRate, value)
        End Set
    End Property
    Dim fReteicaRate As Decimal
    Public Property ReteicaRate() As Decimal
        Get
            Return fReteicaRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ReteicaRate", fReteicaRate, value)
        End Set
    End Property
    Dim fReteivaRate As Decimal
    Public Property ReteivaRate() As Decimal
        Get
            Return fReteivaRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ReteivaRate", fReteivaRate, value)
        End Set
    End Property
    Dim fOperatingUnitId
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fCurrencyId
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
