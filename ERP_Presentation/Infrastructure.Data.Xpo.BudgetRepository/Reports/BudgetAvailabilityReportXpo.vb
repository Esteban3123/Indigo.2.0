Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.Availability")> _
Public Class BudgetAvailabilityReportXpo
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
    '<Indexed(Name:="IX_Code_Availability", Unique:=True)> _
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
    '<Indexed(Name:="IX_Availability")> _
    <Association("Budget_AvailabilityReferencesBudget_BudgetaryValidity")> _
        Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fDependencyId As BudgetDependencyReportXpo
    <Association("Budget_AvailabilityReferencesBudget_Dependency")> _
        Public Property DependencyId() As BudgetDependencyReportXpo
        Get
            Return fDependencyId
        End Get
        Set(ByVal value As BudgetDependencyReportXpo)
            SetPropertyValue(Of BudgetDependencyReportXpo)("DependencyId", fDependencyId, value)
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
    Dim fExpirationDays As Integer
    Public Property ExpirationDays() As Integer
        Get
            Return fExpirationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExpirationDays", fExpirationDays, value)
        End Set
    End Property
    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property
    Dim fAvailabilityType As Byte
    Public Property AvailabilityType() As Byte
        Get
            Return fAvailabilityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AvailabilityType", fAvailabilityType, value)
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
    <Association("Budget_AvailabilityDetailReferencesBudget_Availability", GetType(BudgetAvailabilityDetailReportXpo))> _
    Public ReadOnly Property Budget_AvailabilityDetails() As XPCollection(Of BudgetAvailabilityDetailReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityDetailReportXpo)("Budget_AvailabilityDetails")
        End Get
    End Property
    <Association("Budget_AvailabilityModificationReferencesBudget_Availability", GetType(BudgetAvailabilityModificationReportXpo))> _
    Public ReadOnly Property Budget_AvailabilityModifications() As XPCollection(Of BudgetAvailabilityModificationReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityModificationReportXpo)("Budget_AvailabilityModifications")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
