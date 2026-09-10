Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AnnualizedCashFlow")> _
Public Class BudgetAnnualizedCashFlowReportXpo
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
    Dim fCategoryId As BudgetCategoryReportXpo
    '<Indexed(Name:="IX_AnnualizedCashFlow_1")> _
    <Association("Budget_AnnualizedCashFlowReferencesBudget_Category")> _
    Public Property CategoryId() As BudgetCategoryReportXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fDocumentSource As Byte
    Public Property DocumentSource() As Byte
        Get
            Return fDocumentSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentSource", fDocumentSource, value)
        End Set
    End Property
    Dim fMonth As Byte
    Public Property Month() As Byte
        Get
            Return fMonth
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Month", fMonth, value)
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
    Dim fDebitTransferValue As Decimal
    Public Property DebitTransferValue() As Decimal
        Get
            Return fDebitTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitTransferValue", fDebitTransferValue, value)
        End Set
    End Property
    Dim fCreditTransferValue As Decimal
    Public Property CreditTransferValue() As Decimal
        Get
            Return fCreditTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditTransferValue", fCreditTransferValue, value)
        End Set
    End Property
    Dim fTotalScheduled As Decimal
    Public Property TotalScheduled() As Decimal
        Get
            Return fTotalScheduled
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalScheduled", fTotalScheduled, value)
        End Set
    End Property
    Dim fExecutedValue As Decimal
    Public Property ExecutedValue() As Decimal
        Get
            Return fExecutedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecutedValue", fExecutedValue, value)
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
    Dim fReserveValue As Decimal
    Public Property ReserveValue() As Decimal
        Get
            Return fReserveValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ReserveValue", fReserveValue, value)
        End Set
    End Property
    Dim fDebitReserveModValue As Decimal
    Public Property DebitReserveModValue() As Decimal
        Get
            Return fDebitReserveModValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitReserveModValue", fDebitReserveModValue, value)
        End Set
    End Property
    Dim fCreditReserveModValie As Decimal
    Public Property CreditReserveModValie() As Decimal
        Get
            Return fCreditReserveModValie
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditReserveModValie", fCreditReserveModValie, value)
        End Set
    End Property
    Dim fDebitReserveTransValue As Decimal
    Public Property DebitReserveTransValue() As Decimal
        Get
            Return fDebitReserveTransValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitReserveTransValue", fDebitReserveTransValue, value)
        End Set
    End Property
    Dim fCreditReserveTransValue As Decimal
    Public Property CreditReserveTransValue() As Decimal
        Get
            Return fCreditReserveTransValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditReserveTransValue", fCreditReserveTransValue, value)
        End Set
    End Property
    Dim fExecutedReserveValue As Decimal
    Public Property ExecutedReserveValue() As Decimal
        Get
            Return fExecutedReserveValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecutedReserveValue", fExecutedReserveValue, value)
        End Set
    End Property
    Dim fCxPValue As Decimal
    Public Property CxPValue() As Decimal
        Get
            Return fCxPValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CxPValue", fCxPValue, value)
        End Set
    End Property
    Dim fDebitCxPModificationValue As Decimal
    Public Property DebitCxPModificationValue() As Decimal
        Get
            Return fDebitCxPModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitCxPModificationValue", fDebitCxPModificationValue, value)
        End Set
    End Property
    Dim fCreditCxPModificationValue As Decimal
    Public Property CreditCxPModificationValue() As Decimal
        Get
            Return fCreditCxPModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditCxPModificationValue", fCreditCxPModificationValue, value)
        End Set
    End Property
    Dim fDebitCxPTransferValue As Decimal
    Public Property DebitCxPTransferValue() As Decimal
        Get
            Return fDebitCxPTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitCxPTransferValue", fDebitCxPTransferValue, value)
        End Set
    End Property
    Dim fCreditCxPTransferValue As Decimal
    Public Property CreditCxPTransferValue() As Decimal
        Get
            Return fCreditCxPTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditCxPTransferValue", fCreditCxPTransferValue, value)
        End Set
    End Property
    Dim fExecutedCxPValue As Decimal
    Public Property ExecutedCxPValue() As Decimal
        Get
            Return fExecutedCxPValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecutedCxPValue", fExecutedCxPValue, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    <Association("Budget_AnnualizedCashFlowModificationDetailReferencesBudget_AnnualizedCashFlow", GetType(BudgetAnnualizedCashFlowModificationDetalReportXpo))> _
    Public ReadOnly Property Budget_AnnualizedCashFlowModificationDetails() As XPCollection(Of BudgetAnnualizedCashFlowModificationDetalReportXpo)
        Get
            Return GetCollection(Of BudgetAnnualizedCashFlowModificationDetalReportXpo)("Budget_AnnualizedCashFlowModificationDetails")
        End Get
    End Property
    <Association("Budget_AnnualizedCashFlowTrasnferDetailReferencesBudget_AnnualizedCashFlow", GetType(BudgetAnnualizedCashFlowTransferDetailReportXpo))> _
    Public ReadOnly Property Budget_AnnualizedCashFlowTrasnferDetail() As XPCollection(Of BudgetAnnualizedCashFlowTransferDetailReportXpo)
        Get
            Return GetCollection(Of BudgetAnnualizedCashFlowTransferDetailReportXpo)("Budget_AnnualizedCashFlowTrasnferDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
