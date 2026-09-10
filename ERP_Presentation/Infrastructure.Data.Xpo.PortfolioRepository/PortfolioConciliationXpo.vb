#Region "Imports"
Imports DevExpress.Xpo
#End Region

<Persistent("Portfolio.PortfolioConciliation")>
Public Class PortfolioConciliationXpo
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

    Dim fConciliationConsecutive As String
    Public Property ConciliationConsecutive() As String
        Get
            Return fConciliationConsecutive
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConciliationConsecutive", fConciliationConsecutive, value)
        End Set
    End Property

    Dim fThirdPartyId As Common_ThirdParty
    <Association("Portfolio_PortfolioConciliationCommon_ThirdPartyId")>
    Public Property ThirdPartyId() As Common_ThirdParty
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Common_ThirdParty)
            SetPropertyValue(Of Common_ThirdParty)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fConciliationDate As DateTime
    Public Property ConciliationDate() As DateTime
        Get
            Return fConciliationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConciliationDate", fConciliationDate, value)
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

    Dim fComment As String
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property

    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property

    Dim fClosingDate As DateTime
    Public Property ClosingDate() As DateTime
        Get
            Return fClosingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ClosingDate", fClosingDate, value)
        End Set
    End Property

    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
        End Set
    End Property

    Dim fConfirmUser As String
    Public Property ConfirmUser() As String
        Get
            Return fConfirmUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmUser", fConfirmUser, value)
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

#Region "Custom Members"


    <PersistentAlias("Iif(State = 1, 'Sin confirmar',State = 2, 'Confirmado','Anulado')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Concat(ThirdPartyId.Nit, ' - ', ThirdPartyId.Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

#End Region

#Region "Association"
    <Association("Portfolio_PortfolioConciliationXpo_PortfolioConciliationId", GetType(PortfolioConciliationParticipantsXpo))>
    Public ReadOnly Property Portfolio_PortfolioConciliationParticipants() As XPCollection(Of PortfolioConciliationParticipantsXpo)
        Get
            Return GetCollection(Of PortfolioConciliationParticipantsXpo)("Portfolio_PortfolioConciliationParticipants")
        End Get
    End Property

    <Association("Portfolio_PortfolioConciliationXpo_ConciliationId", GetType(PortfolioConciliationDetailXpo))>
    Public ReadOnly Property Portfolio_PortfolioConciliationDetailXpo() As XPCollection(Of PortfolioConciliationDetailXpo)
        Get
            Return GetCollection(Of PortfolioConciliationDetailXpo)("Portfolio_PortfolioConciliationDetailXpo")
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
