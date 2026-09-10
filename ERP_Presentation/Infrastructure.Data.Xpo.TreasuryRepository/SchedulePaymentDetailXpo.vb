Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.SchedulePaymentDetail")>
Public Class SchedulePaymentDetailXpo
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

    Dim fSchedulePaymentId As SchedulePaymentXpo
    <Association("SchedulePaymentDetailXpoReferencesSchedulePaymentXpo")>
    Public Property SchedulePaymentId() As SchedulePaymentXpo
        Get
            Return fSchedulePaymentId
        End Get
        Set(ByVal value As SchedulePaymentXpo)
            SetPropertyValue(Of SchedulePaymentXpo)("SchedulePaymentId", fSchedulePaymentId, value)
        End Set
    End Property

    Dim fAmountPaid As Decimal
    Public Property AmountPaid() As Decimal
        Get
            Return fAmountPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPaid", fAmountPaid, value)
        End Set
    End Property

    Dim fAmountPercent As Decimal
    Public Property AmountPercent() As Decimal
        Get
            Return fAmountPercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPercent", fAmountPercent, value)
        End Set
    End Property

    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("SchedulePaymentDetailXpoXpoReferencesCommonThirdPartyReportXpo")>
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fIdThirdParty, value)
        End Set
    End Property

    Dim fPaymentsAccountPayableXpo As PaymentsAccountPayableXpo
    <Persistent("AccountPayableId")>
    <Association("AccountPayableXpoReferencesSchedulePaymentDetailXpo")>
    Public Property PaymentsAccountPayableXpo() As PaymentsAccountPayableXpo
        Get
            Return fPaymentsAccountPayableXpo
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue("PaymentsAccountPayableXpo", fPaymentsAccountPayableXpo, value)
        End Set
    End Property

    <PersistentAlias("PaymentsAccountPayableXpo.CurrencyAbbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("PaymentsAccountPayableXpo.CurrencyId")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
