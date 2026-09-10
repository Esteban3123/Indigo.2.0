Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.AccountPayableConcepts")> _
Public Class PaymentsAccountPayableConceptsXpo
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
    '<Indexed(Name:="IX_PaymentsConcept", Unique:=True)> _
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
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsAccountPayableConceptsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fHandlesRetention As Boolean
    Public Property HandlesRetention() As Boolean
        Get
            Return fHandlesRetention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesRetention", fHandlesRetention, value)
        End Set
    End Property
    Dim fRetentionConceptId As Integer
    Public Property RetentionConceptId() As Integer
        Get
            Return fRetentionConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptId", fRetentionConceptId, value)
        End Set
    End Property
    Dim fDeferredCausation As Boolean
    Public Property DeferredCausation() As Boolean
        Get
            Return fDeferredCausation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DeferredCausation", fDeferredCausation, value)
        End Set
    End Property
    Dim fAccumulatedBudgetCalculation As Boolean
    Public Property AccumulatedBudgetCalculation() As Boolean
        Get
            Return fAccumulatedBudgetCalculation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AccumulatedBudgetCalculation", fAccumulatedBudgetCalculation, value)
        End Set
    End Property
    Dim fFreeResourcesUnexecuted As Boolean
    Public Property FreeResourcesUnexecuted() As Boolean
        Get
            Return fFreeResourcesUnexecuted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("FreeResourcesUnexecuted", fFreeResourcesUnexecuted, value)
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
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesPaymentsAccountPayableConceptsXpo", GetType(PaymentsAccountPayableDetailConceptXpo))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpo() As XPCollection(Of PaymentsAccountPayableDetailConceptXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpo)("PaymentsAccountPayableDetailConceptXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
