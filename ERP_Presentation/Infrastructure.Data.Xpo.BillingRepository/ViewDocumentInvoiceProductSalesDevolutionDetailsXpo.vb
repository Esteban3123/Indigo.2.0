'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/11/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewDocumentInvoiceProductSalesDevolutionDetails")>
Public Class ViewDocumentInvoiceProductSalesDevolutionDetailsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRowId As Integer
    <Key(True)>
    Public Property RowId() As Integer
        Get
            Return fRowId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RowId", fRowId, value)
        End Set
    End Property

    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fProductDescription As String
    Public Property ProductDescription() As String
        Get
            Return fProductDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductDescription", fProductDescription, value)
        End Set
    End Property

    Dim fBatchSerialId As Integer
    Public Property BatchSerialId() As Integer
        Get
            Return fBatchSerialId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BatchSerialId", fBatchSerialId, value)
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

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fQuantityDevolution As Integer
    Public Property QuantityDevolution() As Integer
        Get
            Return fQuantityDevolution
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuantityDevolution", fQuantityDevolution, value)
        End Set
    End Property

    Dim fDocumentInvoiceProductSalesDevolutionId As Integer
    Public Property DocumentInvoiceProductSalesDevolutionId() As Integer
        Get
            Return fDocumentInvoiceProductSalesDevolutionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentInvoiceProductSalesDevolutionId", fDocumentInvoiceProductSalesDevolutionId, value)
        End Set
    End Property

    Dim fDocumentInvoiceProductSalesDetailBatchSerialId As Integer
    Public Property DocumentInvoiceProductSalesDetailBatchSerialId() As Integer
        Get
            Return fDocumentInvoiceProductSalesDetailBatchSerialId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentInvoiceProductSalesDetailBatchSerialId", fDocumentInvoiceProductSalesDetailBatchSerialId, value)
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

    Dim fRTFPercentage As Decimal
    Public Property RTFPercentage() As Decimal
        Get
            Return fRTFPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFPercentage", fRTFPercentage, value)
        End Set
    End Property

    Dim fDocumentInvoiceProductSalesDevolutionDetailId As Integer
    Public Property DocumentInvoiceProductSalesDevolutionDetailId() As Integer
        Get
            Return fDocumentInvoiceProductSalesDevolutionDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentInvoiceProductSalesDevolutionDetailId", fDocumentInvoiceProductSalesDevolutionDetailId, value)
        End Set
    End Property

    Dim fImportSource As Boolean?
    Public Property ImportSource() As Boolean?
        Get
            Return fImportSource
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("ImportSource", fImportSource, value)
        End Set
    End Property

    Dim fSourceCode As String
    Public Property SourceCode() As String
        Get
            Return fSourceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SourceCode", fSourceCode, value)
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
