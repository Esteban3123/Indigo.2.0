Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.Category")> _
Public Class BudgetCategoryReportXpo
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
    <Association("Budget_CategoryReportReferencesBudget_BudgetaryValidity")> _
    Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fItemType As Byte
    Public Property ItemType() As Byte
        Get
            Return fItemType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ItemType", fItemType, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_Code_Category", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fAlternativeCode As String
    <Size(20)> _
    Public Property AlternativeCode() As String
        Get
            Return fAlternativeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AlternativeCode", fAlternativeCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fCategoryOwnerId As BudgetCategoryReportXpo
    <Association("Budget_CategoryReferencesBudget_Category")> _
    Public Property CategoryOwnerId() As BudgetCategoryReportXpo
        Get
            Return fCategoryOwnerId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryOwnerId", fCategoryOwnerId, value)
        End Set
    End Property
    Dim fAuxiliary As Boolean
    Public Property Auxiliary() As Boolean
        Get
            Return fAuxiliary
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Auxiliary", fAuxiliary, value)
        End Set
    End Property
    Dim fFinancialSourceId As BudgetFinancialSourceReportXpo
    <Association("Budget_CategoryReferencesBudget_FinancialSource")> _
        Public Property FinancialSourceId() As BudgetFinancialSourceReportXpo
        Get
            Return fFinancialSourceId
        End Get
        Set(ByVal value As BudgetFinancialSourceReportXpo)
            SetPropertyValue(Of BudgetFinancialSourceReportXpo)("FinancialSourceId", fFinancialSourceId, value)
        End Set
    End Property
    Dim fPAC As Boolean
    Public Property PAC() As Boolean
        Get
            Return fPAC
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PAC", fPAC, value)
        End Set
    End Property
    Dim fStatusPAC As Byte
    Public Property StatusPAC() As Byte
        Get
            Return fStatusPAC
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusPAC", fStatusPAC, value)
        End Set
    End Property
    Dim fUsed As Boolean
    Public Property Used() As Boolean
        Get
            Return fUsed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Used", fUsed, value)
        End Set
    End Property
    Dim fFutureValidity As Boolean
    Public Property FutureValidity() As Boolean
        Get
            Return fFutureValidity
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("FutureValidity", fFutureValidity, value)
        End Set
    End Property
    Dim fInvertionProject As Boolean
    Public Property InvertionProject() As Boolean
        Get
            Return fInvertionProject
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InvertionProject", fInvertionProject, value)
        End Set
    End Property
    Dim fBalanceDeficit As Boolean
    Public Property BalanceDeficit() As Boolean
        Get
            Return fBalanceDeficit
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("BalanceDeficit", fBalanceDeficit, value)
        End Set
    End Property
    Dim fIncomeCxP As Boolean
    Public Property IncomeCxP() As Boolean
        Get
            Return fIncomeCxP
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IncomeCxP", fIncomeCxP, value)
        End Set
    End Property
    Dim fReservePacStatus As Byte
    Public Property ReservePacStatus() As Byte
        Get
            Return fReservePacStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ReservePacStatus", fReservePacStatus, value)
        End Set
    End Property
    Dim fStatusPacCxP As Byte
    Public Property StatusPacCxP() As Byte
        Get
            Return fStatusPacCxP
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusPacCxP", fStatusPacCxP, value)
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
    <Association("Budget_CategoryReferencesBudget_Category", GetType(BudgetCategoryReportXpo))> _
    Public ReadOnly Property Budget_CategoryCollection() As XPCollection(Of BudgetCategoryReportXpo)
        Get
            Return GetCollection(Of BudgetCategoryReportXpo)("Budget_CategoryCollection")
        End Get
    End Property
    <Association("Budget_BudgetReportReferencesBudget_Category", GetType(BudgetReportXpo))> _
    Public ReadOnly Property Budget_BudgetReport() As XPCollection(Of BudgetReportXpo)
        Get
            Return GetCollection(Of BudgetReportXpo)("Budget_BudgetReport")
        End Get
    End Property
    <Association("Budget_AnnualizedCashFlowReferencesBudget_Category", GetType(BudgetAnnualizedCashFlowReportXpo))> _
    Public ReadOnly Property Budget_AnnualizedCashFlow() As XPCollection(Of BudgetAnnualizedCashFlowReportXpo)
        Get
            Return GetCollection(Of BudgetAnnualizedCashFlowReportXpo)("Budget_AnnualizedCashFlow")
        End Get
    End Property
    <Association("Budget_RecognitionDetailReferencesBudget_Category", GetType(BudgetRecognitionDetailReportXpo))> _
    Public ReadOnly Property Budget_RecognitionDetail() As XPCollection(Of BudgetRecognitionDetailReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionDetailReportXpo)("Budget_RecognitionDetail")
        End Get
    End Property
    <Association("Budget_CommitmentDetailReferencesBudget_Category", GetType(BudgetCommitmentDetailReportXpo))> _
    Public ReadOnly Property Budget_CommitmentDetail() As XPCollection(Of BudgetCommitmentDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentDetailReportXpo)("Budget_CommitmentDetail")
        End Get
    End Property
    <Association("Budget_ObligationDetailReferencesBudget_Category", GetType(BudgetObligationDetailReportXpo))> _
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
