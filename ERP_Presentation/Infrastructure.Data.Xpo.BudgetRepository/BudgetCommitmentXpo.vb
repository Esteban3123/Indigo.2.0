Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.Commitment")> _
Public Class BudgetCommitmentXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fBudgetaryValidityId As BudgetValidityXpo
    <Association("BudgetCommitmentXpoReferencesBudgetValidityXpo")>
    Public Property BudgetaryValidityId() As BudgetValidityXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetValidityXpo)
            SetPropertyValue(Of BudgetValidityXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("Budget_Commitment_References_Common_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
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

    Dim fCommitmentType As Byte
    Public Property CommitmentType() As Byte
        Get
            Return fCommitmentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CommitmentType", fCommitmentType, value)
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

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(DocumentSource = 1, 'Otro', Iif(DocumentSource = 2, 'Orden de Trabajo',Iif(DocumentSource = 3, 'Contrato', '')))")>
    Public ReadOnly Property DocumentSourceName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentSourceName"))
        End Get
    End Property

    <PersistentAlias("Iif(CommitmentType = 1, 'Compromiso', Iif(CommitmentType = 2, 'Reserva',Iif(CommitmentType = 3, 'Vigencia Futura', '')))")>
    Public ReadOnly Property CommitmentTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CommitmentTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("BudgetCommitmentDetailXpo.Sum(InitialValue)")>
    Public ReadOnly Property InitialValue As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("InitialValue"))
        End Get
    End Property

    <PersistentAlias("BudgetCommitmentDetailXpo.Sum(TotalCommitment)")>
    Public ReadOnly Property TotalCommitment As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("TotalCommitment"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("BudgetCommitmentDetailXpoReferencesBudgetCommitmentXpo", GetType(BudgetCommitmentDetailXpo))> _
    Public ReadOnly Property BudgetCommitmentDetailXpo() As XPCollection(Of BudgetCommitmentDetailXpo)
        Get
            Return GetCollection(Of BudgetCommitmentDetailXpo)("BudgetCommitmentDetailXpo")
        End Get
    End Property

    <Association("BudgetCommitmentModificationXpoReferencesBudgetCommitmentXpo", GetType(BudgetCommitmentModificationXpo))>
    Public ReadOnly Property BudgetCommitmentModificationXpo() As XPCollection(Of BudgetCommitmentModificationXpo)
        Get
            Return GetCollection(Of BudgetCommitmentModificationXpo)("BudgetCommitmentModificationXpo")
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
