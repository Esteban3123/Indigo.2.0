#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.DischargeBillBudget")>
Public Class DischargeBillBudgetXpo
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

    Dim fDischargeBillId As DischargeBillXpo
    <Association("DischargeBillBudget_Reference_DischargeBill")>
    Public Property DischargeBillId() As DischargeBillXpo
        Get
            Return fDischargeBillId
        End Get
        Set(ByVal value As DischargeBillXpo)
            SetPropertyValue(Of DischargeBillXpo)("DischargeBillId", fDischargeBillId, value)
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