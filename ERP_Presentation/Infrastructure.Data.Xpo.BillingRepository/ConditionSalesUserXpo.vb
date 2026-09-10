Imports DevExpress.Xpo

<Persistent("Billing.ConditionSalesUser")>
Public Class ConditionSalesUserXpo
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

    Dim fConditionSalesId As ConditionSalesXpo
    <Association("ConditionSalesUserReferencesConditionSales")>
    Public Property ConditionSalesId() As ConditionSalesXpo
        Get
            Return fConditionSalesId
        End Get
        Set(ByVal value As ConditionSalesXpo)
            SetPropertyValue(Of ConditionSalesXpo)("ConditionSalesId", fConditionSalesId, value)
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