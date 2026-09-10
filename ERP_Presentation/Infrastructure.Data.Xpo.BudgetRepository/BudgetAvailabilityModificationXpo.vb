Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AvailabilityModification")> _
Public Class BudgetAvailabilityModificationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fBudgetaryValidityId As BudgetValidityXpo
    <Association("BudgetAvailabilityModificationXpoReferencesBudgetValidityXpo")>
    Public Property BudgetaryValidityId() As BudgetValidityXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetValidityXpo)
            SetPropertyValue(Of BudgetValidityXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property

    Dim fAvailabilityId As BudgetAvailabilityXpo
    <Association("BudgetAvailabilityModificationXpoReferencesBudgetAvailabilityXpo")>
    Public Property AvailabilityId() As BudgetAvailabilityXpo
        Get
            Return fAvailabilityId
        End Get
        Set(ByVal value As BudgetAvailabilityXpo)
            SetPropertyValue(Of BudgetAvailabilityXpo)("AvailabilityId", fAvailabilityId, value)
        End Set
    End Property

    Dim fUpTo As Byte
    Public Property UpTo() As Byte
        Get
            Return fUpTo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UpTo", fUpTo, value)
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
    <Size(300)>
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
    <Size(20)>
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
    <Size(20)>
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
    <Size(20)>
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
    <Size(20)>
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

#End Region

#Region "Customs Members"

    <PersistentAlias("Iif(UpTo = 2, 'Compromiso / Reserva', UpTo = 3, 'Disponibilidad', UpTo = 4, 'Presupuesto', '')")>
    Public ReadOnly Property UpToName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("UpToName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Associations"

    <Association("BudgetAvailabilityModificationDetailXpoReferencesBudgetAvailabilityModificationXpo", GetType(BudgetAvailabilityModificationDetailXpo))> _
    Public ReadOnly Property BudgetAvailabilityModificationDetailXpo() As XPCollection(Of BudgetAvailabilityModificationDetailXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityModificationDetailXpo)("BudgetAvailabilityModificationDetailXpo")
        End Get
    End Property

    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetAvailabilityModificationXpo", GetType(BudgetReimbursementResourceDetailXpo))>
    Public ReadOnly Property BudgetReimbursementResourceDetailXpo() As XPCollection(Of BudgetReimbursementResourceDetailXpo)
        Get
            Return GetCollection(Of BudgetReimbursementResourceDetailXpo)("BudgetReimbursementResourceDetailXpo")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
