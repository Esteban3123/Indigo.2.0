'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Cuentas de terceros usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.ThridPartyBankAccounts")> _
Public Class ThridPartyBankAccountXpo
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
    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(255)> _
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fType As Integer
    <Persistent("Type")> _
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(30)> _
    <Persistent("Number")> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fIdThirParty As CommonThirdPartyXpo
    <Association("TreasuryThirdReferencesThirdPayroll")> _
    Public Property IdThirParty() As CommonThirdPartyXpo
        Get
            Return fIdThirParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirParty", fIdThirParty, value)
        End Set
    End Property
    Dim fIdBank As Integer
    <Persistent("IdBank")> _
    Public Property IdBank() As Integer
        Get
            Return fIdBank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdBank", fIdBank, value)
        End Set
    End Property
    Dim fIdRadicationCity As Integer
    <Persistent("IdRadicationCity")> _
    Public Property IdRadicationCity() As Integer
        Get
            Return fIdRadicationCity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRadicationCity", fIdRadicationCity, value)
        End Set
    End Property
    Dim fIdBankCity As Integer
    <Persistent("IdBankCity")> _
    Public Property IdBankCity() As Integer
        Get
            Return fIdBankCity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdBankCity", fIdBankCity, value)
        End Set
    End Property
    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
