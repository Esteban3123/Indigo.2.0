Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.RecognitionModificationDetail")> _
Public Class BudgetRecognitionModificationDetailXpo
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
    Dim fRecognitionModificationId As BudgetRecognitionModificationXpo
    <Association("Budget_RecognitionModificationDetailReferencesBudget_RecognitionModification")> _
    Public Property RecognitionModificationId() As BudgetRecognitionModificationXpo
        Get
            Return fRecognitionModificationId
        End Get
        Set(ByVal value As BudgetRecognitionModificationXpo)
            SetPropertyValue(Of BudgetRecognitionModificationXpo)("RecognitionModificationId", fRecognitionModificationId, value)
        End Set
    End Property
    Dim fRecognitionDetailId As BudgetRecognitionModificationXpo
    <Association("BudgetRecognitionModificationDetailXpoReferencesBudgetRecognitionDetailXpo")> _
    Public Property RecognitionDetailId() As BudgetRecognitionModificationXpo
        Get
            Return fRecognitionDetailId
        End Get
        Set(ByVal value As BudgetRecognitionModificationXpo)
            SetPropertyValue(Of BudgetRecognitionModificationXpo)("RecognitionDetailId", fRecognitionDetailId, value)
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
