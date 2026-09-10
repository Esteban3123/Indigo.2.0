Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Payroll.ViewReportWithholdingRetroactive")> _
Public Class PayrollViewReportWithholdingRetroactiveXpo
    Inherits XPLiteObject
    Dim fid As Integer
    <Key(True)> _
    Public Property id() As Integer
        Get
            Return fid
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("id", fid, value)
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
    Dim fIdGroup As Integer
    Public Property IdGroup() As Integer
        Get
            Return fIdGroup
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdGroup", fIdGroup, value)
        End Set
    End Property
    Dim fInitialDateRetroactive As DateTime
    Public Property InitialDateRetroactive() As DateTime
        Get
            Return fInitialDateRetroactive
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDateRetroactive", fInitialDateRetroactive, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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
    Dim fProcedimientoRete As Byte
    Public Property ProcedimientoRete() As Byte
        Get
            Return fProcedimientoRete
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProcedimientoRete", fProcedimientoRete, value)
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
    Dim fAportePension As Decimal
    Public Property AportePension() As Decimal
        Get
            Return fAportePension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AportePension", fAportePension, value)
        End Set
    End Property
    Dim fAportePensionVoluntaria As Integer
    Public Property AportePensionVoluntaria() As Integer
        Get
            Return fAportePensionVoluntaria
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AportePensionVoluntaria", fAportePensionVoluntaria, value)
        End Set
    End Property
    Dim fFondoSolidaridad As Decimal
    Public Property FondoSolidaridad() As Decimal
        Get
            Return fFondoSolidaridad
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FondoSolidaridad", fFondoSolidaridad, value)
        End Set
    End Property
    Dim fAFC As Integer
    Public Property AFC() As Integer
        Get
            Return fAFC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AFC", fAFC, value)
        End Set
    End Property
    Dim fAporteSalud As Decimal
    Public Property AporteSalud() As Decimal
        Get
            Return fAporteSalud
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AporteSalud", fAporteSalud, value)
        End Set
    End Property
    Dim fMedicinaPrepagada As Integer
    Public Property MedicinaPrepagada() As Integer
        Get
            Return fMedicinaPrepagada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicinaPrepagada", fMedicinaPrepagada, value)
        End Set
    End Property
    Dim fDeduccionDependendiente As Integer
    Public Property DeduccionDependendiente() As Integer
        Get
            Return fDeduccionDependendiente
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DeduccionDependendiente", fDeduccionDependendiente, value)
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
    Dim fBaseGravable As Decimal
    Public Property BaseGravable() As Decimal
        Get
            Return fBaseGravable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseGravable", fBaseGravable, value)
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
