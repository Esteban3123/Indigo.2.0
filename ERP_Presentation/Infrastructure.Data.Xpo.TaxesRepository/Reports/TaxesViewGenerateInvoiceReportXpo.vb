Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.VReportGenerateInvoice")> _
Public Class TaxesViewGenerateInvoiceReportXpo
    Inherits XPLiteObject
    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
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
    Dim fTaId As Integer
    Public Property TaId() As Integer
        Get
            Return fTaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TaId", fTaId, value)
        End Set
    End Property
    Dim fDeadLine As DateTime
    Public Property DeadLine() As DateTime
        Get
            Return fDeadLine
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DeadLine", fDeadLine, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(270)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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
    Dim fTercero As String
    <Size(300)> _
    Public Property Tercero() As String
        Get
            Return fTercero
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tercero", fTercero, value)
        End Set
    End Property
    Dim fCode As String
    <Size(50)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fBarCode As String
    <Size(8000)> _
    Public Property BarCode() As String
        Get
            Return fBarCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BarCode", fBarCode, value)
        End Set
    End Property
    Dim fTypeCode As String
    <Size(2)> _
    Public Property TypeCode() As String
        Get
            Return fTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeCode", fTypeCode, value)
        End Set
    End Property
    Dim fSection As String
    <Size(4)> _
    Public Property Section() As String
        Get
            Return fSection
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Section", fSection, value)
        End Set
    End Property
    Dim fBlock As String
    <Size(8)> _
    Public Property Block() As String
        Get
            Return fBlock
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Block", fBlock, value)
        End Set
    End Property
    Dim fLandArea As String
    <Size(46)> _
    Public Property LandArea() As String
        Get
            Return fLandArea
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LandArea", fLandArea, value)
        End Set
    End Property
    Dim fHectare As Decimal
    Public Property Hectare() As Decimal
        Get
            Return fHectare
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Hectare", fHectare, value)
        End Set
    End Property
    Dim fBuildArea As String
    <Size(46)> _
    Public Property BuildArea() As String
        Get
            Return fBuildArea
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuildArea", fBuildArea, value)
        End Set
    End Property
    Dim fAddres As String
    <Size(200)> _
    Public Property Addres() As String
        Get
            Return fAddres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addres", fAddres, value)
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
    Dim fValidity As Integer
    Public Property Validity() As Integer
        Get
            Return fValidity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Validity", fValidity, value)
        End Set
    End Property
    Dim fAppraisal As Decimal
    Public Property Appraisal() As Decimal
        Get
            Return fAppraisal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Appraisal", fAppraisal, value)
        End Set
    End Property
    Dim fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxValue", fTaxValue, value)
        End Set
    End Property
    Dim fInterest As Integer
    Public Property Interest() As Integer
        Get
            Return fInterest
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Interest", fInterest, value)
        End Set
    End Property
    Dim fTotal As Integer
    Public Property Total() As Integer
        Get
            Return fTotal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Total", fTotal, value)
        End Set
    End Property
    Dim fIdProperty As Integer
    Public Property IdProperty() As Integer
        Get
            Return fIdProperty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdProperty", fIdProperty, value)
        End Set
    End Property
    Dim fCodeLiquidationConcept As String
    <Size(20)> _
    Public Property CodeLiquidationConcept() As String
        Get
            Return fCodeLiquidationConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeLiquidationConcept", fCodeLiquidationConcept, value)
        End Set
    End Property
    Dim fLiquidationConcept As String
    <Size(265)> _
    Public Property LiquidationConcept() As String
        Get
            Return fLiquidationConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LiquidationConcept", fLiquidationConcept, value)
        End Set
    End Property
    Dim fPreviousValidity As Integer
    Public Property PreviousValidity() As Integer
        Get
            Return fPreviousValidity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PreviousValidity", fPreviousValidity, value)
        End Set
    End Property
    Dim fPercentageDiscount As String
    <Size(25)> _
    Public Property PercentageDiscount() As String
        Get
            Return fPercentageDiscount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PercentageDiscount", fPercentageDiscount, value)
        End Set
    End Property
    Dim fVrDscto As Decimal
    Public Property VrDscto() As Decimal
        Get
            Return fVrDscto
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VrDscto", fVrDscto, value)
        End Set
    End Property
    Dim fVrBaseDscto As Decimal
    Public Property VrBaseDscto() As Decimal
        Get
            Return fVrBaseDscto
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VrBaseDscto", fVrBaseDscto, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
