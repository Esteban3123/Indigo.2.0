Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AvailabilityModificationDetail")> _
Public Class BudgetAvailabilityModificationDetailReportXpo
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
    Dim fAvailabilityModificationId As BudgetAvailabilityModificationReportXpo
    <Association("Budget_AvailabilityModificationDetailReferencesBudget_AvailabilityModification")> _
    Public Property AvailabilityModificationId() As BudgetAvailabilityModificationReportXpo
        Get
            Return fAvailabilityModificationId
        End Get
        Set(ByVal value As BudgetAvailabilityModificationReportXpo)
            SetPropertyValue(Of BudgetAvailabilityModificationReportXpo)("AvailabilityModificationId", fAvailabilityModificationId, value)
        End Set
    End Property
    Dim fAvailabilityDetailId As BudgetAvailabilityDetailReportXpo
    <Association("Budget_AvailabilityModificationDetailReferencesBudget_AvailabilityDetail")> _
    Public Property AvailabilityDetailId() As BudgetAvailabilityDetailReportXpo
        Get
            Return fAvailabilityDetailId
        End Get
        Set(ByVal value As BudgetAvailabilityDetailReportXpo)
            SetPropertyValue(Of BudgetAvailabilityDetailReportXpo)("AvailabilityDetailId", fAvailabilityDetailId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
