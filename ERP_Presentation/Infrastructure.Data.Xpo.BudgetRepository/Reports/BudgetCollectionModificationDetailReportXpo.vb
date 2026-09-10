Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.CollectionModificationDetail")> _
Public Class BudgetCollectionModificationDetailReportXpo
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
    Dim fCollectionModificationId As BudgetCollectionModificationReportXpo
    <Association("Budget_CollectionModificationDetailReferencesBudget_CollectionModification")> _
    Public Property CollectionModificationId() As BudgetCollectionModificationReportXpo
        Get
            Return fCollectionModificationId
        End Get
        Set(ByVal value As BudgetCollectionModificationReportXpo)
            SetPropertyValue(Of BudgetCollectionModificationReportXpo)("CollectionModificationId", fCollectionModificationId, value)
        End Set
    End Property
    Dim fCollectionDetailId As BudgetCollectionDetailReportXpo
    <Association("Budget_CollectionModificationDetailReferencesBudget_CollectionDetail")> _
    Public Property CollectionDetailId() As BudgetCollectionDetailReportXpo
        Get
            Return fCollectionDetailId
        End Get
        Set(ByVal value As BudgetCollectionDetailReportXpo)
            SetPropertyValue(Of BudgetCollectionDetailReportXpo)("CollectionDetailId", fCollectionDetailId, value)
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
