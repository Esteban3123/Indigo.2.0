Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.BudgetaryEntity")> _
Public Class BudgetBudgetaryEntityReportXpo
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
    '<Indexed(Name:="IX_Code_BudgetaryEntity", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fSection As String
    <Size(4)> _
    Public Property Section() As String
        Get
            Return fSection
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Section", fSection, value)
        End Set
    End Property
    Dim fUnit As String
    <Size(2)> _
    Public Property Unit() As String
        Get
            Return fUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Unit", fUnit, value)
        End Set
    End Property
    Dim fRegion As String
    <Size(4)> _
    Public Property Region() As String
        Get
            Return fRegion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Region", fRegion, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fBudgetaryValidityId As BudgetBudgetaryValidityReportXpo
    <Association("Budget_BudgetaryEntityReferencesBudget_BudgetaryValidity")> _
    Public Property BudgetaryValidityId() As BudgetBudgetaryValidityReportXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetBudgetaryValidityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryValidityReportXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty3")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fRecognition As Boolean
    Public Property Recognition() As Boolean
        Get
            Return fRecognition
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Recognition", fRecognition, value)
        End Set
    End Property
    Dim fESE As Boolean
    Public Property ESE() As Boolean
        Get
            Return fESE
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ESE", fESE, value)
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
    <Association("Budget_BudgetaryValidityReferencesBudget_BudgetaryEntity", GetType(BudgetBudgetaryValidityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryValiditys() As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryValidityReportXpo)("Budget_BudgetaryValiditys")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
