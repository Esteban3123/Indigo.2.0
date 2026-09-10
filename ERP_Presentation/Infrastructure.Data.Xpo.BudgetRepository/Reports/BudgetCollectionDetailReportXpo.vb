Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.CollectionDetail")> _
Public Class BudgetCollectionDetailReportXpo
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
    Dim fCollectionId As BudgetCollectionReportXpo
    <Association("Budget_CollectionDetailReferencesBudget_Collection")> _
        Public Property CollectionId() As BudgetCollectionReportXpo
        Get
            Return fCollectionId
        End Get
        Set(ByVal value As BudgetCollectionReportXpo)
            SetPropertyValue(Of BudgetCollectionReportXpo)("CollectionId", fCollectionId, value)
        End Set
    End Property
    Dim fRecognitionDetailId As BudgetRecognitionDetailReportXpo
    <Association("Budget_CollectionDetailReferencesBudget_RecognitionDetail")> _
    Public Property RecognitionDetailId() As BudgetRecognitionDetailReportXpo
        Get
            Return fRecognitionDetailId
        End Get
        Set(ByVal value As BudgetRecognitionDetailReportXpo)
            SetPropertyValue(Of BudgetRecognitionDetailReportXpo)("RecognitionDetailId", fRecognitionDetailId, value)
        End Set
    End Property
    Dim fCollectionType As Byte
    Public Property CollectionType() As Byte
        Get
            Return fCollectionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CollectionType", fCollectionType, value)
        End Set
    End Property
    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property
    Dim fDebitModificationValue As Decimal
    Public Property DebitModificationValue() As Decimal
        Get
            Return fDebitModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitModificationValue", fDebitModificationValue, value)
        End Set
    End Property
    Dim fCreditModificationValue As Decimal
    Public Property CreditModificationValue() As Decimal
        Get
            Return fCreditModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditModificationValue", fCreditModificationValue, value)
        End Set
    End Property
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    <Association("Budget_CollectionModificationDetailReferencesBudget_CollectionDetail", GetType(BudgetCollectionModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_CollectionModificationDetails() As XPCollection(Of BudgetCollectionModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCollectionModificationDetailReportXpo)("Budget_CollectionModificationDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
