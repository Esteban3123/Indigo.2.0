Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.RetentionConcepts")> _
Public Class GeneralLedgerRetentionConceptsReportXpo
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
    '<Indexed(Name:="IX_RetentionConcept_Code", Unique:=True)> _
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
    Dim fRetention As Byte
    Public Property Retention() As Byte
        Get
            Return fRetention
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Retention", fRetention, value)
        End Set
    End Property
    Dim fMinBase As Decimal
    Public Property MinBase() As Decimal
        Get
            Return fMinBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MinBase", fMinBase, value)
        End Set
    End Property
    Dim fRate As Decimal
    Public Property Rate() As Decimal
        Get
            Return fRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Rate", fRate, value)
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
    Dim fTypeRounding As Byte
    Public Property TypeRounding() As Byte
        Get
            Return fTypeRounding
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeRounding", fTypeRounding, value)
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
    <Association("GeneralLedger_RetentionConceptRangesReferencesGeneralLedger_RetentionConcepts", GetType(GeneralLedgerRetentionConceptRangesReportXpo))> _
    Public ReadOnly Property GeneralLedger_RetentionConceptRangess() As XPCollection(Of GeneralLedgerRetentionConceptRangesReportXpo)
        Get
            Return GetCollection(Of GeneralLedgerRetentionConceptRangesReportXpo)("GeneralLedger_RetentionConceptRangess")
        End Get
    End Property
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_RetentionConcepts", GetType(GeneralLedgerJournalVoucherDeatilsXpo))> _
    Public ReadOnly Property GeneralLedger_MainAccounts() As XPCollection(Of GeneralLedgerJournalVoucherDeatilsXpo)
        Get
            Return GetCollection(Of GeneralLedgerJournalVoucherDeatilsXpo)("GeneralLedger_MainAccounts")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
