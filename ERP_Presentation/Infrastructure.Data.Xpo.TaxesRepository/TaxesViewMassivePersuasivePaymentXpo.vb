Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.VMassivePersuasivePayment")> _
Public Class TaxesViewMassivePersuasivePaymentXpo
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

    Dim fTipo As Byte
    Public Property Tipo() As Byte
        Get
            Return fTipo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Tipo", fTipo, value)
        End Set
    End Property


    Dim fTipoImpuesto As String
    Public Property TipoImpuesto() As String
        Get
            Return fTipoImpuesto
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("TipoImpuesto", fTipoImpuesto, value)
        End Set
    End Property

    Dim fFactura As String
    Public Property Factura() As String
        Get
            Return fFactura
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Factura", fFactura, value)
        End Set
    End Property

    Dim fNitTercero As String
    Public Property NitTercero() As String
        Get
            Return fNitTercero
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("NitTercero", fNitTercero, value)
        End Set
    End Property

    Dim fNombreTercero As String
    Public Property NombreTercero() As String
        Get
            Return fNombreTercero
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("NombreTercero", fNombreTercero, value)
        End Set
    End Property

    Dim fFechaVencimiento As String
    Public Property FechaVencimiento() As String
        Get
            Return fFechaVencimiento
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("FechaVencimiento", fFechaVencimiento, value)
        End Set
    End Property

    Dim fValor As Decimal
    Public Property Valor() As Decimal
        Get
            Return fValor
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Valor", fValor, value)
        End Set
    End Property

    Dim fSaldo As Decimal
    Public Property Saldo() As String
        Get
            Return fSaldo
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Saldo", fSaldo, value)
        End Set
    End Property

    Dim fVigencia As String
    Public Property Vigencia() As String
        Get
            Return fVigencia
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Vigencia", fVigencia, value)
        End Set
    End Property



    
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
