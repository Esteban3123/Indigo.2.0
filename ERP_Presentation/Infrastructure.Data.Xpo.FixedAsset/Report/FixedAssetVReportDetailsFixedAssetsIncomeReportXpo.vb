Imports DevExpress.Xpo

<Persistent("FixedAsset.VReportDetailsFixedAssetsIncome")> _
Public Class FixedAssetVReportDetailsFixedAssetsIncomeReportXpo
    Inherits XPLiteObject

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
        End Set
    End Property

    Dim fType As String
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property

    Dim fFecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return fFecha
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("Fecha", fFecha, value)
        End Set
    End Property

    Dim fDocumento As String
    Public Property Documento() As String
        Get
            Return fDocumento
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Documento", fDocumento, value)
        End Set
    End Property

    Dim fOrdenDeCompra As String
    Public Property OrdenDeCompra() As String
        Get
            Return fOrdenDeCompra
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OrdenDeCompra", fOrdenDeCompra, value)
        End Set
    End Property

    Dim fOrigen As String
    Public Property Origen() As String
        Get
            Return fOrigen
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Origen", fOrigen, value)
        End Set
    End Property

    Dim fDestino As String
    Public Property Destino() As String
        Get
            Return fDestino
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Destino", fDestino, value)
        End Set
    End Property

    Dim fPlaca As String
    Public Property Placa() As String
        Get
            Return fPlaca
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Placa", fPlaca, value)
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

    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
        End Set
    End Property

    Dim fcant As Integer
    Public Property cant() As Integer
        Get
            Return fcant
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("cant", fcant, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fTrademark As String
    Public Property Trademark() As String
        Get
            Return fTrademark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Trademark", fTrademark, value)
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

    Dim fIvaValue As Decimal
    Public Property IvaValue() As Decimal
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValue", fIvaValue, value)
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fProveedor As String
    Public Property Proveedor() As String
        Get
            Return fProveedor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Proveedor", fProveedor, value)
        End Set
    End Property

    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
        End Set
    End Property


    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Dim fCodeAbbreviationISO As String
    Public Property CodeAbbreviationISO() As String
        Get
            Return fCodeAbbreviationISO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAbbreviationISO", fCodeAbbreviationISO, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
