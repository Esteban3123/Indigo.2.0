
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Payroll.ViewPayrollKardex")>
Public Class PayrollViewXpoReportKardex
    Inherits XPLiteObject
    Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property
    Dim fIdEmployee As Integer
    Public Property IdEmployee() As Integer
        Get
            Return fIdEmployee
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEmployee", fIdEmployee, value)
        End Set
    End Property
    Dim fNit As String
    <Size(200)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(400)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fMes As Date
    Public Property Mes() As Date
        Get
            Return fMes
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("Mes", fMes, value)
        End Set
    End Property

    Dim fCodigoConcepto As String
    <Size(20)>
    Public Property CodigoConcepto() As String
        Get
            Return fCodigoConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoConcepto", fCodigoConcepto, value)
        End Set
    End Property

    Dim fNombreConcepto As String
    <Size(200)>
    Public Property NombreConcepto() As String
        Get
            Return fNombreConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreConcepto", fNombreConcepto, value)
        End Set
    End Property

    Dim fTipoConcepto As String
    <Size(100)>
    Public Property TipoConcepto() As String
        Get
            Return fTipoConcepto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TipoConcepto", fTipoConcepto, value)
        End Set
    End Property

    Dim fValorConcepto As Decimal
    Public Property ValorConcepto() As Decimal
        Get
            Return fValorConcepto
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValorConcepto", fValorConcepto, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub
    End Class

