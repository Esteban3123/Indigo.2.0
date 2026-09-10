'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Andres Alarcon
' Created          : 2023-05-30
'
' Copyright        : (c) . All rights reserved.
'*************************************

Imports DevExpress.Xpo

<Persistent("Billing.ProductAndServiceFeeUser")>
Public Class ProductAndServiceFeeUserDetailXpo
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

    Dim fProductAndServiceFeeId As ProductAndServicesFeeXpo
    <Association("Billing_ProductAndServicesFee_Users")>
    Public Property ProductAndServiceFeeId() As ProductAndServicesFeeXpo
        Get
            Return fProductAndServiceFeeId
        End Get
        Set(ByVal value As ProductAndServicesFeeXpo)
            SetPropertyValue(Of ProductAndServicesFeeXpo)("ProductAndServiceFeeId", fProductAndServiceFeeId, value)
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


#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region
End Class
