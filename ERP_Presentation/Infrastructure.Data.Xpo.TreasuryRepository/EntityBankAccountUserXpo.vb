'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-07-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.EntityBankAccountUser")> _
Public Class EntityBankAccountUserXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdEntityBankAccount As Integer
    <Persistent("IdEntityBankAccount")> _
    Public Property IdEntityBankAccount() As Integer
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property
    Dim fCodUser As String
    <Persistent("CodUser")> _
    Public Property CodUser() As String
        Get
            Return fCodUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodUser", fCodUser, value)
        End Set
    End Property

#End Region

#Region "Builders"

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
