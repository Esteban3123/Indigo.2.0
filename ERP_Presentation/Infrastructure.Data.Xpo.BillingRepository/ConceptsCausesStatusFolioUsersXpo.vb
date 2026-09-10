#Region "Imports"
Imports DevExpress.Xpo
#End Region

<Persistent("Billing.ConceptsCausesStatusFolioUsers")>
Public Class ConceptsCausesStatusFolioUsersXpo
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

    Dim fConceptsCausesStatusFolioId As ConceptsCausesStatusFolioXpo
    <Association("ConceptsCausesStatusFolioReferences_ConceptsCausesStatusFolioUsers")>
    Public Property ConceptsCausesStatusFolioId() As ConceptsCausesStatusFolioXpo
        Get
            Return fConceptsCausesStatusFolioId
        End Get
        Set(ByVal value As ConceptsCausesStatusFolioXpo)
            SetPropertyValue(Of ConceptsCausesStatusFolioXpo)("ConceptsCausesStatusFolioId", fConceptsCausesStatusFolioId, value)
        End Set
    End Property

    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fFullNameUser As String
    Public Property FullNameUser() As String
        Get
            Return fFullNameUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullNameUser", fFullNameUser, value)
        End Set
    End Property


#End Region

#Region "Custom Members"

    <PersistentAlias("CONCAT(UserCode,'-',FullNameUser)")>
    Public ReadOnly Property CodeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Association"

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
