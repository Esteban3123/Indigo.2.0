Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPayrollWithholding")> _
Public Class PayrollViewPayrollWithholdingReportXpo
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
    Dim fIdEmpleado As Integer
    Public Property IdEmpleado() As Integer
        Get
            Return fIdEmpleado
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEmpleado", fIdEmpleado, value)
        End Set
    End Property
    Dim fIdentificacion As String
    <Size(15)> _
    Public Property Identificacion() As String
        Get
            Return fIdentificacion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Identificacion", fIdentificacion, value)
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
    Dim fProcedimiento As Byte
    Public Property Procedimiento() As Byte
        Get
            Return fProcedimiento
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Procedimiento", fProcedimiento, value)
        End Set
    End Property
    Dim fFechaLiquidacion As DateTime
    Public Property FechaLiquidacion() As DateTime
        Get
            Return fFechaLiquidacion
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaLiquidacion", fFechaLiquidacion, value)
        End Set
    End Property
    Dim fIdGrupo As Integer
    Public Property IdGrupo() As Integer
        Get
            Return fIdGrupo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdGrupo", fIdGrupo, value)
        End Set
    End Property
    Dim fCodigoGrupo As String
    <Size(20)> _
    Public Property CodigoGrupo() As String
        Get
            Return fCodigoGrupo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoGrupo", fCodigoGrupo, value)
        End Set
    End Property
    Dim fNombreGrupo As String
    <Size(150)> _
    Public Property NombreGrupo() As String
        Get
            Return fNombreGrupo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombreGrupo", fNombreGrupo, value)
        End Set
    End Property
    Dim fTotalDevengado As Decimal
    Public Property TotalDevengado() As Decimal
        Get
            Return fTotalDevengado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDevengado", fTotalDevengado, value)
        End Set
    End Property
    Dim fTotalDevengadoRTF As Decimal
    Public Property TotalDevengadoRTF() As Decimal
        Get
            Return fTotalDevengadoRTF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDevengadoRTF", fTotalDevengadoRTF, value)
        End Set
    End Property
    Dim fAportePension As Decimal
    Public Property AportePension() As Decimal
        Get
            Return fAportePension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AportePension", fAportePension, value)
        End Set
    End Property
    Dim fAportePensionVoluntaria As Decimal
    Public Property AportePensionVoluntaria() As Decimal
        Get
            Return fAportePensionVoluntaria
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AportePensionVoluntaria", fAportePensionVoluntaria, value)
        End Set
    End Property
    Dim fFonsoSolidaridad As Decimal
    Public Property FonsoSolidaridad() As Decimal
        Get
            Return fFonsoSolidaridad
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FonsoSolidaridad", fFonsoSolidaridad, value)
        End Set
    End Property
    Dim fAFC As Decimal
    Public Property AFC() As Decimal
        Get
            Return fAFC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AFC", fAFC, value)
        End Set
    End Property
    Dim fAportesSalud As Decimal
    Public Property AportesSalud() As Decimal
        Get
            Return fAportesSalud
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AportesSalud", fAportesSalud, value)
        End Set
    End Property
    Dim fMedicinaPrepagada As Decimal
    Public Property MedicinaPrepagada() As Decimal
        Get
            Return fMedicinaPrepagada
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MedicinaPrepagada", fMedicinaPrepagada, value)
        End Set
    End Property
    Dim fDeduccionDependiente As Decimal
    Public Property DeduccionDependiente() As Decimal
        Get
            Return fDeduccionDependiente
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeduccionDependiente", fDeduccionDependiente, value)
        End Set
    End Property
    Dim fDeduccionVivienda As Decimal
    Public Property DeduccionVivienda() As Decimal
        Get
            Return fDeduccionVivienda
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeduccionVivienda", fDeduccionVivienda, value)
        End Set
    End Property
    Dim fBaseGravable As Integer
    Public Property BaseGravable() As Integer
        Get
            Return fBaseGravable
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BaseGravable", fBaseGravable, value)
        End Set
    End Property
    Dim fTotalRetencion As Decimal
    Public Property TotalRetencion() As Decimal
        Get
            Return fTotalRetencion
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalRetencion", fTotalRetencion, value)
        End Set
    End Property
    Dim fArticulo As Byte
    Public Property Articulo() As Byte
        Get
            Return fArticulo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Articulo", fArticulo, value)
        End Set
    End Property
    Dim fDeclarantType As Byte
    Public Property DeclarantType() As Byte
        Get
            Return fDeclarantType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DeclarantType", fDeclarantType, value)
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
