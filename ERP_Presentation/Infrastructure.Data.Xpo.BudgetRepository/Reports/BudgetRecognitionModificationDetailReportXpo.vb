Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.RecognitionModificationDetail")> _
Public Class BudgetRecognitionModificationDetailReportXpo
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
    Dim fRecognitionModificationId As BudgetRecognitionModificationReportXpo
    <Association("Budget_RecognitionModificationDetailReferencesBudget_RecognitionModification")> _
    Public Property RecognitionModificationId() As BudgetRecognitionModificationReportXpo
        Get
            Return fRecognitionModificationId
        End Get
        Set(ByVal value As BudgetRecognitionModificationReportXpo)
            SetPropertyValue(Of BudgetRecognitionModificationReportXpo)("RecognitionModificationId", fRecognitionModificationId, value)
        End Set
    End Property
    Dim fRecognitionDetailId As BudgetRecognitionDetailReportXpo
    <Association("Budget_RecognitionModificationDetailReferencesBudget_RecognitionDetail")> _
    Public Property RecognitionDetailId() As BudgetRecognitionDetailReportXpo
        Get
            Return fRecognitionDetailId
        End Get
        Set(ByVal value As BudgetRecognitionDetailReportXpo)
            SetPropertyValue(Of BudgetRecognitionDetailReportXpo)("RecognitionDetailId", fRecognitionDetailId, value)
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
