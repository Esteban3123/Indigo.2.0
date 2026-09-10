Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.VReportBudget")> _
Public Class BudgetVReportBudget
    Inherits XPLiteObject
    Dim fId As String
    <Key(True)> _
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(20)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fFechaFactura As DateTime
    Public Property FechaFactura() As DateTime
        Get
            Return fFechaFactura
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaFactura", fFechaFactura, value)
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
    Dim fNombre As String
    <Size(300)> _
    Public Property Nombre() As String
        Get
            Return fNombre
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nombre", fNombre, value)
        End Set
    End Property
    Dim fRegimen As String
    <Size(50)> _
    Public Property Regimen() As String
        Get
            Return fRegimen
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Regimen", fRegimen, value)
        End Set
    End Property
    Dim fConsecutivoRadicado As Integer?
    Public Property ConsecutivoRadicado() As Integer?
        Get
            Return fConsecutivoRadicado
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ConsecutivoRadicado", fConsecutivoRadicado, value)
        End Set
    End Property
    Dim fFechaRadicado As DateTime
    Public Property FechaRadicado() As DateTime
        Get
            Return fFechaRadicado
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaRadicado", fFechaRadicado, value)
        End Set
    End Property
    Dim fCodigoCruce As String
    <Size(20)> _
    Public Property CodigoCruce() As String
        Get
            Return fCodigoCruce
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoCruce", fCodigoCruce, value)
        End Set
    End Property
    Dim fFechaCruce As DateTime
    Public Property FechaCruce() As DateTime
        Get
            Return fFechaCruce
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaCruce", fFechaCruce, value)
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
    Dim fTotal As Decimal
    Public Property Total() As Decimal
        Get
            Return fTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Total", fTotal, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
