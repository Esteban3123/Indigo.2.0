Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.PayrollSettings")> _
Public Class PayrollPayrollSettingsReportXpo
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
    Dim fCreeTax As Boolean
    Public Property CreeTax() As Boolean
        Get
            Return fCreeTax
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CreeTax", fCreeTax, value)
        End Set
    End Property
    Dim fVacationFormulates As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property VacationFormulates() As String
        Get
            Return fVacationFormulates
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VacationFormulates", fVacationFormulates, value)
        End Set
    End Property
    Dim fBonificationVacationFormulates As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property BonificationVacationFormulates() As String
        Get
            Return fBonificationVacationFormulates
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BonificationVacationFormulates", fBonificationVacationFormulates, value)
        End Set
    End Property
    Dim fVacationIncentiveFormulates As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property VacationIncentiveFormulates() As String
        Get
            Return fVacationIncentiveFormulates
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VacationIncentiveFormulates", fVacationIncentiveFormulates, value)
        End Set
    End Property
    Dim fVacationalIncreaseFormulates As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property VacationalIncreaseFormulates() As String
        Get
            Return fVacationalIncreaseFormulates
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VacationalIncreaseFormulates", fVacationalIncreaseFormulates, value)
        End Set
    End Property
    Dim fPayrollChiefThirdPartyId As CommonThirdParty
    <Association("Payroll_PayrollSettingsReferencesCommon_ThirdParty")> _
    Public Property PayrollChiefThirdPartyId() As CommonThirdParty
        Get
            Return fPayrollChiefThirdPartyId
        End Get
        Set(ByVal value As CommonThirdParty)
            SetPropertyValue(Of CommonThirdParty)("PayrollChiefThirdPartyId", fPayrollChiefThirdPartyId, value)
        End Set
    End Property





    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
