'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-05-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CancellationChecks")> _
Public Class CancellationCheckXpo
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
    Dim fIdEntityAccount As EntityBankAccountXpo
    <Association("TreasuryCancellationCheckReferencesTreasuryEntityAccount")> _
    Public Property IdEntityAccount() As EntityBankAccountXpo
        Get
            Return fIdEntityAccount
        End Get
        Set(ByVal value As EntityBankAccountXpo)
            SetPropertyValue(Of EntityBankAccountXpo)("IdEntityAccount", fIdEntityAccount, value)
        End Set
    End Property
    Dim fCancellationDate As DateTime
    <Persistent("CancellationDate")> _
    Public Property CancellationDate() As DateTime
        Get
            Return fCancellationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CancellationDate", fCancellationDate, value)
        End Set
    End Property
    Dim fCheckNumber As String
    <Persistent("CheckNumber")> _
    Public Property CheckNumber() As String
        Get
            Return fCheckNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckNumber", fCheckNumber, value)
        End Set
    End Property
    Dim fDescription As String
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
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
