Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.RevenueType")> _
Public Class BudgetRevenueTypeReportXpo
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
    Dim fBudgetaryValidityId As BudgetBudgetaryValidityReportXpo
    <Association("Budget_RevenueTypeReferencesBudget_BudgetaryValidity")> _
    Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_Code_RevenueType", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property
    Dim fIncomeSource As Byte
    Public Property IncomeSource() As Byte
        Get
            Return fIncomeSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("IncomeSource", fIncomeSource, value)
        End Set
    End Property
    Dim fExpenditureDefinition As Byte
    Public Property ExpenditureDefinition() As Byte
        Get
            Return fExpenditureDefinition
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ExpenditureDefinition", fExpenditureDefinition, value)
        End Set
    End Property
    Dim fExpenditureClassification As Byte
    Public Property ExpenditureClassification() As Byte
        Get
            Return fExpenditureClassification
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ExpenditureClassification", fExpenditureClassification, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    <Association("Budget_ModificationDetailReferencesBudget_BudgetRevenueType", GetType(BudgetModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_ModificationDetail() As XPCollection(Of BudgetModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetModificationDetailReportXpo)("Budget_ModificationDetail")
        End Get
    End Property
    <Association("Budget_BudgetReportReferencesBudget_BudgetRevenueType", GetType(BudgetReportXpo))> _
    Public ReadOnly Property Budget_BudgetReport() As XPCollection(Of BudgetReportXpo)
        Get
            Return GetCollection(Of BudgetReportXpo)("Budget_BudgetReport")
        End Get
    End Property
    <Association("Budget_BudgetTransferDetailReferencesBudget_BudgetRevenueType", GetType(BudgetTransferDetailReportXpo))> _
    Public ReadOnly Property Budget_TransferDetail() As XPCollection(Of BudgetTransferDetailReportXpo)
        Get
            Return GetCollection(Of BudgetTransferDetailReportXpo)("Budget_TransferDetail")
        End Get
    End Property
    <Association("Budget_RecognitionDetailReferencesBudget_BudgetRevenueType", GetType(BudgetRecognitionDetailReportXpo))> _
    Public ReadOnly Property Budget_RecognitionDetail() As XPCollection(Of BudgetRecognitionDetailReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionDetailReportXpo)("Budget_RecognitionDetail")
        End Get
    End Property
    <Association("Budget_CommitmentDetailReferencesBudget_BudgetRevenueType", GetType(BudgetCommitmentDetailReportXpo))> _
    Public ReadOnly Property Budget_CommitmentDetail() As XPCollection(Of BudgetCommitmentDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentDetailReportXpo)("Budget_CommitmentDetail")
        End Get
    End Property
    <Association("Budget_ObligationDetailReferencesBudget_BudgetRevenueType", GetType(BudgetObligationDetailReportXpo))> _
    Public ReadOnly Property Budget_ObligationDetail() As XPCollection(Of BudgetObligationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetObligationDetailReportXpo)("Budget_ObligationDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
