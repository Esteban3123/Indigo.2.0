Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ObligationModificationDetail")> _
Public Class BudgetObligationModificationDetailReportXpo
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
    Dim fObligationModificationId As BudgetObligationModificationReportXpo
    <Association("Budget_ObligationModificationDetailReferencesBudget_ObligationModification")> _
    Public Property ObligationModificationId() As BudgetObligationModificationReportXpo
        Get
            Return fObligationModificationId
        End Get
        Set(ByVal value As BudgetObligationModificationReportXpo)
            SetPropertyValue(Of BudgetObligationModificationReportXpo)("ObligationModificationId", fObligationModificationId, value)
        End Set
    End Property
    Dim fObligationDetailId As BudgetObligationDetailReportXpo
    <Association("Budget_ObligationModificationDetailReferencesBudget_ObligationDetail")> _
    Public Property ObligationDetailId() As BudgetObligationDetailReportXpo
        Get
            Return fObligationDetailId
        End Get
        Set(ByVal value As BudgetObligationDetailReportXpo)
            SetPropertyValue(Of BudgetObligationDetailReportXpo)("ObligationDetailId", fObligationDetailId, value)
        End Set
    End Property
    Dim fExpiredDate As DateTime
    Public Property ExpiredDate() As DateTime
        Get
            Return fExpiredDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpiredDate", fExpiredDate, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
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
    Dim fIsLogBase As Boolean
    Public Property IsLogBase() As Boolean
        Get
            Return fIsLogBase
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsLogBase", fIsLogBase, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
