'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' ciudades bancarias usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CashRegisterUser")> _
Public Class CashRegisterUserXpo
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

    Dim fIdCashRegister As Integer
    <Persistent("IdCashRegister")> _
    Public Property IdCashRegister() As Integer
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

    Dim fIdUser As Integer
    <Persistent("IdUser")> _
    Public Property IdUser() As Integer
        Get
            Return fIdUser
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdUser", fIdUser, value)
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
