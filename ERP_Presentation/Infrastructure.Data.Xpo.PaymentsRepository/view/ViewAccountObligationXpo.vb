Imports DevExpress.Xpo

<Persistent("Payments.ViewAccountObligation")>
Public Class ViewAccountObligationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsViewAccountObligation_References_PaymentsAccountPayable")>
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fObligationId As Integer
    Public Property ObligationId() As Integer
        Get
            Return fObligationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ObligationId", fObligationId, value)
        End Set
    End Property

    Dim fObligationCode As String
    Public Property ObligationCode() As String
        Get
            Return fObligationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObligationCode", fObligationCode, value)
        End Set
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
