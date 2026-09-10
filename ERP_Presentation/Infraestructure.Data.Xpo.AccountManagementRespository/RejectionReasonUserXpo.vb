Imports DevExpress.Xpo
<Persistent("AccountManagement.RejectionReasonUser")>
Public Class RejectionReasonUserXpo
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

    Dim fRejectionReasonId As RejectionReasonXpo
    <Association("RejectionReasonUserReferencesRejectionReason")>
    Public Property RejectionReasonId() As RejectionReasonXpo
        Get
            Return fRejectionReasonId
        End Get
        Set(ByVal value As RejectionReasonXpo)
            SetPropertyValue(Of RejectionReasonXpo)("RejectionReasonId", fRejectionReasonId, value)
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
    <Size(50)>
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class
