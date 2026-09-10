Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.SettingPortfolio")> _
Public Class PortfolioSettingPortfolioXpo
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
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fJournalVoucherTypeCreditNotesId As Integer
    Public Property JournalVoucherTypeCreditNotesId() As Integer
        Get
            Return fJournalVoucherTypeCreditNotesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherTypeCreditNotesId", fJournalVoucherTypeCreditNotesId, value)
        End Set
    End Property
    Dim fJournalVoucherTypeDebitNotesId As Integer
    Public Property JournalVoucherTypeDebitNotesId() As Integer
        Get
            Return fJournalVoucherTypeDebitNotesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherTypeDebitNotesId", fJournalVoucherTypeDebitNotesId, value)
        End Set
    End Property
    Dim fJournalVoucherTypeTranslationId As Integer
    Public Property JournalVoucherTypeTranslationId() As Integer
        Get
            Return fJournalVoucherTypeTranslationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherTypeTranslationId", fJournalVoucherTypeTranslationId, value)
        End Set
    End Property
    Dim fJournalVoucherTypeProvisionId As Integer
    Public Property JournalVoucherTypeProvisionId() As Integer
        Get
            Return fJournalVoucherTypeProvisionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherTypeProvisionId", fJournalVoucherTypeProvisionId, value)
        End Set
    End Property
    Dim fJournalVoucherTypeFilingAccountId As Integer
    Public Property JournalVoucherTypeFilingAccountId() As Integer
        Get
            Return fJournalVoucherTypeFilingAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherTypeFilingAccountId", fJournalVoucherTypeFilingAccountId, value)
        End Set
    End Property
    Dim fNameMinimumAgeRange As String
    Public Property NameMinimumAgeRange() As String
        Get
            Return fNameMinimumAgeRange
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameMinimumAgeRange", fNameMinimumAgeRange, value)
        End Set
    End Property
    Dim fNameMaximumAgeRange As String
    Public Property NameMaximumAgeRange() As String
        Get
            Return fNameMaximumAgeRange
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameMaximumAgeRange", fNameMaximumAgeRange, value)
        End Set
    End Property
    Dim fMaximunAgeRange As Integer
    Public Property MaximunAgeRange() As Integer
        Get
            Return fMaximunAgeRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaximunAgeRange", fMaximunAgeRange, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
    <Association("PortfolioAgesPortfolioXpoReferencesPortfolioSettingPortfolioXpo", GetType(PortfolioAgesPortfolioXpo))> _
    Public ReadOnly Property PortfolioAgesPortfolioXpo() As XPCollection(Of PortfolioAgesPortfolioXpo)
        Get
            Return GetCollection(Of PortfolioAgesPortfolioXpo)("PortfolioAgesPortfolioXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
