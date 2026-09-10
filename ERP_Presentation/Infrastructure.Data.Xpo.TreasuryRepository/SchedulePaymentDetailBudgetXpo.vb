#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.SchedulePaymentDetailBudget")>
Public Class SchedulePaymentDetailBudgetXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fSchedulePaymentDetailId As Integer
    <Persistent("SchedulePaymentDetailId")>
    Public Property SchedulePaymentDetailId() As Integer
        Get
            Return fSchedulePaymentDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SchedulePaymentDetailId", fSchedulePaymentDetailId, value)
        End Set
    End Property

    Dim fObligationDetailId As Integer
    <Persistent("ObligationDetailId")>
    Public Property ObligationDetailId() As Integer
        Get
            Return fObligationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ObligationDetailId", fObligationDetailId, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")>
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class