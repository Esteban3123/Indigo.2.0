Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ManualConceptsDetail")> _
Public Class PayrollManualConceptsDetailReportXpo
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
    Dim fManualConceptId As PayrollManualConceptsReportXpo
    <Association("Payroll_ManualConceptsDetailReferencesPayroll_ManualConcepts")> _
    Public Property ManualConceptId() As PayrollManualConceptsReportXpo
        Get
            Return fManualConceptId
        End Get
        Set(ByVal value As PayrollManualConceptsReportXpo)
            SetPropertyValue(Of PayrollManualConceptsReportXpo)("ManualConceptId", fManualConceptId, value)
        End Set
    End Property
    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
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
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
