Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.FunctionalUnitResponsible")>
Public Class PayrollFunctionalUnitResponsibleXpo
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
    Dim fFunctionalUnitId As PayrollFunctionalUnit
    <Association("Payroll_FunctionalUnitResponsibleReferencesPayroll_FunctionalUnit")>
    Public Property FunctionalUnitId() As PayrollFunctionalUnit
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As PayrollFunctionalUnit)
            SetPropertyValue(Of PayrollFunctionalUnit)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
