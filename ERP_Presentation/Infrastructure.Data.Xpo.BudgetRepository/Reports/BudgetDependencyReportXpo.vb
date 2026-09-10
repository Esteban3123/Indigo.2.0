Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.Dependency")> _
Public Class BudgetDependencyReportXpo
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
    <Association("Budget_DependencyReferencesBudget_BudgetaryValidity")> _
    Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_Code_Dependency", Unique:=True)> _
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
    Dim fResponsibleId As CommonThirdPartyReportXpo
    <Association("Budget_DependencyReferencesCommon_ThirdParty")> _
    Public Property ResponsibleId() As CommonThirdPartyReportXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ResponsibleId", fResponsibleId, value)
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
    <Association("Budget_RecognitionReferencesBudget_Dependency", GetType(BudgetRecognitionReportXpo))> _
    Public ReadOnly Property Budget_Recognition() As XPCollection(Of BudgetRecognitionReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionReportXpo)("Budget_Recognition")
        End Get
    End Property
    <Association("Budget_AvailabilityReferencesBudget_Dependency", GetType(BudgetAvailabilityReportXpo))> _
    Public ReadOnly Property Budget_Availability() As XPCollection(Of BudgetAvailabilityReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityReportXpo)("Budget_Availability")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
