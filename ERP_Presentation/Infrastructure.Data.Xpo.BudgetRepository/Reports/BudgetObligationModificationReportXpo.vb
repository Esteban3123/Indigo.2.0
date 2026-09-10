Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ObligationModification")> _
Public Class BudgetObligationModificationReportXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fBudgetaryValidityId As BudgetBudgetaryValidityReportXpo
    <Association("Budget_ObligationModificationReferencesBudget_BudgetaryValidity")> _
        Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fObligationId As BudgetObligationReportXpo
    <Association("Budget_ObligationModificationReferencesBudget_Obligation")> _
    Public Property ObligationId() As BudgetObligationReportXpo
        Get
            Return fObligationId
        End Get
        Set(ByVal value As BudgetObligationReportXpo)
            SetPropertyValue(Of BudgetObligationReportXpo)("ObligationId", fObligationId, value)
        End Set
    End Property
    Dim fDocument As String
    Public Property Document() As String
        Get
            Return fDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Document", fDocument, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
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
    <Association("Budget_ObligationModificationDetailReferencesBudget_ObligationModification", GetType(BudgetObligationModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_ObligationModificationDetails() As XPCollection(Of BudgetObligationModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetObligationModificationDetailReportXpo)("Budget_ObligationModificationDetails")
        End Get
    End Property
    <Association("Budget_ReimbursementResourceDetailReferencesBudget_ObligationModification", GetType(BudgetReimbursementResourceDetailReportXpo))> _
    Public ReadOnly Property Budget_ReimbursementResourceDetail() As XPCollection(Of BudgetReimbursementResourceDetailReportXpo)
        Get
            Return GetCollection(Of BudgetReimbursementResourceDetailReportXpo)("Budget_ReimbursementResourceDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
