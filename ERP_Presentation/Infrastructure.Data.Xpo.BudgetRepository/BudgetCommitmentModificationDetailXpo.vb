Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.CommitmentModificationDetail")> _
Public Class BudgetCommitmentModificationDetailXpo
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
    Dim fCommitmentModificationId As BudgetCommitmentModificationXpo
    <Association("BudgetCommitmentModificationDetailXpoReferencesBudgetCommitmentModificationXpo")> _
    Public Property CommitmentModificationId() As BudgetCommitmentModificationXpo
        Get
            Return fCommitmentModificationId
        End Get
        Set(ByVal value As BudgetCommitmentModificationXpo)
            SetPropertyValue(Of BudgetCommitmentModificationXpo)("CommitmentModificationId", fCommitmentModificationId, value)
        End Set
    End Property
    Dim fCommitmentDetailId As BudgetCommitmentDetailXpo
    <Association("BudgetCommitmentModificationDetailXpoReferencesBudgetCommitmentDetailXpo")> _
    Public Property CommitmentDetailId() As BudgetCommitmentDetailXpo
        Get
            Return fCommitmentDetailId
        End Get
        Set(ByVal value As BudgetCommitmentDetailXpo)
            SetPropertyValue(Of BudgetCommitmentDetailXpo)("CommitmentDetailId", fCommitmentDetailId, value)
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
