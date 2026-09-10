Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VSurcharges")> _
Public Class PayrollVSurchargesReportXpo
    Inherits XPLiteObject

    Dim fRowString As String
    <Key(True)> _
    Public Property RowString() As String
        Get
            Return fRowString
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RowString", fRowString, value)
        End Set
    End Property

    Dim fCedula As String
    <Size(15)> _
    Public Property Cedula() As String
        Get
            Return fCedula
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Cedula", fCedula, value)
        End Set
    End Property
    Dim fNombreEmpleado As String
    <Size(300)> _
    Public Property NombreEmpleado() As String
        Get
            Return fNombreEmpleado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreEmpleado", fNombreEmpleado, value)
        End Set
    End Property
    Dim fCodigoConcepto As String
    <Size(3)> _
    Public Property CodigoConcepto() As String
        Get
            Return fCodigoConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoConcepto", fCodigoConcepto, value)
        End Set
    End Property
    Dim fNombreConcepto As String
    <Size(50)> _
    Public Property NombreConcepto() As String
        Get
            Return fNombreConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreConcepto", fNombreConcepto, value)
        End Set
    End Property
    Dim fTotalHoras As Decimal
    Public Property TotalHoras() As Decimal
        Get
            Return fTotalHoras
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalHoras", fTotalHoras, value)
        End Set
    End Property
    Dim fValorTotal As Decimal
    Public Property ValorTotal() As Decimal
        Get
            Return fValorTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValorTotal", fValorTotal, value)
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
    Dim fEstado As Char
    Public Property Estado() As Char
        Get
            Return fEstado
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Estado", fEstado, value)
        End Set
    End Property
    Dim fGrupoCod As String
    Public Property GrupoCod() As String
        Get
            Return fGrupoCod
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GrupoCod", fGrupoCod, value)
        End Set
    End Property
    Dim fGrupoN As String
    Public Property GrupoN() As String
        Get
            Return fGrupoN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GrupoN", fGrupoN, value)
        End Set
    End Property
    Dim fEmpleado As Integer
    Public Property Empleado() As Integer
        Get
            Return fEmpleado
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Empleado", fEmpleado, value)
        End Set
    End Property
    Dim fContratoP As Integer
    Public Property ContratoP() As Integer
        Get
            Return fContratoP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContratoP", fContratoP, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Long
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
