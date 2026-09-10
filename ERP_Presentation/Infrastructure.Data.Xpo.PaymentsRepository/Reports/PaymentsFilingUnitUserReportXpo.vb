Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.FilingUnitUser")> _
Public Class PaymentsFilingUnitUserReportXpo
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
    Dim fFillingUnitId As PaymentsFillingUnitReportXpo
    <Association("PaymentsFillingUnitReportXpoReferencesPaymentsFilingUnitUserReportXpo")> _
    Public Property FillingUnitId() As PaymentsFillingUnitReportXpo
        Get
            Return fFillingUnitId
        End Get
        Set(ByVal value As PaymentsFillingUnitReportXpo)
            SetPropertyValue(Of PaymentsFillingUnitReportXpo)("FillingUnitId", fFillingUnitId, value)
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
    Dim fUserCode As String
    <Size(50)> _
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
