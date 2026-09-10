Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Treasury.NoteConcepts")> _
Public Class NoteConceptsXpo
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
    'columna que devuelve el codigo y la Description concatenado
    <Size(275)> _
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_NoteConcept", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(255)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fAffectBudget As Boolean
    Public Property AffectBudget() As Boolean
        Get
            Return fAffectBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectBudget", fAffectBudget, value)
        End Set
    End Property
    Dim fAutoCollect As Boolean
    Public Property AutoCollect() As Boolean
        Get
            Return fAutoCollect
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AutoCollect", fAutoCollect, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fIdMainAccount As Integer
    Public Property IdMainAccount() As Integer
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMainAccount", fIdMainAccount, value)
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
    <Association("Treasury_TreasuryNoteDetailReferencesTreasury_NoteConcepts", GetType(TreasuryNotesDetailXpo))> _
    Public ReadOnly Property Treasury_TreasuryNoteDetails() As XPCollection(Of TreasuryNotesDetailXpo)
        Get
            Return GetCollection(Of TreasuryNotesDetailXpo)("Treasury_TreasuryNoteDetails")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesNoteConceptsXpo", GetType(TreasuryCrossingAccountDetailOtherConceptXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailOtherConceptXpo() As XPCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)("TreasuryCrossingAccountDetailOtherConceptXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
