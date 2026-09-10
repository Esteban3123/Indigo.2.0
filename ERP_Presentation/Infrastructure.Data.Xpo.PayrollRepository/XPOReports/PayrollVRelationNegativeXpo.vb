Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VRelationNegative")> _
Public Class PayrollVRelationNegativeXpo
    Inherits XPLiteObject
    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
        End Set
    End Property
    Dim fNit As String
    <Key()> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fTotalDevengados As Decimal
    Public Property TotalDevengados() As Decimal
        Get
            Return fTotalDevengados
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDevengados", fTotalDevengados, value)
        End Set
    End Property
    Dim fTotalDeducidos As Decimal
    Public Property TotalDeducidos() As Decimal
        Get
            Return fTotalDeducidos
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducidos", fTotalDeducidos, value)
        End Set
    End Property
    Dim fDiferencia As Decimal
    Public Property Diferencia() As Decimal
        Get
            Return fDiferencia
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Diferencia", fDiferencia, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
