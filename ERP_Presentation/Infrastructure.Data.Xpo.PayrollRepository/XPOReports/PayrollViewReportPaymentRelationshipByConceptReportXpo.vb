Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPaymentRelationshipByConcept")> _
Public Class PayrollViewReportPaymentRelationshipByConceptReportXpo
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
    Dim fCodigoConcepto As String
    <Size(4)> _
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
    Dim fValor As Decimal
    Public Property Valor() As Decimal
        Get
            Return fValor
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Valor", fValor, value)
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
    Dim fIdConcept As Integer
    Public Property IdConcept() As Integer
        Get
            Return fIdConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdConcept", fIdConcept, value)
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
