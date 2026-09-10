Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPaymentRelationshipByEmployee")> _
Public Class PayrollViewReportPaymentRelationshipByEmployeeReportXpo
    Inherits XPLiteObject
    Dim fId As Long
    <Key(True)> _
    Public Property Id() As Long
        Get
            Return fId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Id", fId, value)
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
    Dim fCedula As String
    <Size(20)> _
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
    Dim fFecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return fFecha
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("Fecha", fFecha, value)
        End Set
    End Property
    Dim fSalarioBasico As Decimal
    Public Property SalarioBasico() As Decimal
        Get
            Return fSalarioBasico
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalarioBasico", fSalarioBasico, value)
        End Set
    End Property
    Dim fDiasNomina As Integer
    Public Property DiasNomina() As Integer
        Get
            Return fDiasNomina
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DiasNomina", fDiasNomina, value)
        End Set
    End Property
    Dim fDiasLab As Integer
    Public Property DiasLab() As Integer
        Get
            Return fDiasLab
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DiasLab", fDiasLab, value)
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
    Dim fTotalDeducido As Decimal
    Public Property TotalDeducido() As Decimal
        Get
            Return fTotalDeducido
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducido", fTotalDeducido, value)
        End Set
    End Property
    Dim fTotalPagado As Decimal
    Public Property TotalPagado() As Decimal
        Get
            Return fTotalPagado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPagado", fTotalPagado, value)
        End Set
    End Property
    Dim fCodeFunctionalUnit As String
    <Size(20)> _
    Public Property CodeFunctionalUnit() As String
        Get
            Return fCodeFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeFunctionalUnit", fCodeFunctionalUnit, value)
        End Set
    End Property
    Dim fCodeGroup As String
    <Size(20)> _
    Public Property CodeGroup() As String
        Get
            Return fCodeGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeGroup", fCodeGroup, value)
        End Set
    End Property
    Dim fCodeCostCenter As String
    <Size(20)> _
    Public Property CodeCostCenter() As String
        Get
            Return fCodeCostCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCostCenter", fCodeCostCenter, value)
        End Set
    End Property
    Dim fNameCostCenter As String
    <Size(200)> _
    Public Property NameCostCenter() As String
        Get
            Return fNameCostCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCostCenter", fNameCostCenter, value)
        End Set
    End Property
    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
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
