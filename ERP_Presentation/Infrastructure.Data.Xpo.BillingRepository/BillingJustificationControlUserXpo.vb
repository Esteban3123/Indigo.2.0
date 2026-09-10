Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingJustificationControlUser")>
Public Class BillingJustificationControlUserXpo
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

    <PersistentAlias("BillingJustificationControl.Id")>
    Public ReadOnly Property BillingJustificationControlId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("BillingJustificationControlId"))
        End Get
    End Property

    Dim fBillingJustificationControl
    <Persistent("BillingJustificationControlId")>
    <Association("BillingJustificationControlUser_References_BillingJustificationControl")>
    Public Property BillingJustificationControl() As BillingJustificationControlXpo
        Get
            Return fBillingJustificationControl
        End Get
        Set(value As BillingJustificationControlXpo)
            SetPropertyValue("BillingJustificationControl", fBillingJustificationControl, value)
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


    Dim fUserName As String
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserName", fUserName, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class