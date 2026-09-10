'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Johan Carranza
' Created          : 28-10-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Payments.PaymentNotesDebitCredit")>
Public Class PaymentNotesDebitCreditXPO
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fNoteDate As DateTime
    <Persistent("NoteDate")>
    Public Property NoteDate() As DateTime
        Get
            Return fNoteDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("NoteDate", fNoteDate, value)
        End Set
    End Property

    Dim fNature As String
    <Persistent("Nature")>
    Public Property Nature() As String
        Get
            Return fNature
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nature", fNature, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")>
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property

    Dim fIndicatesBillAdvance As String
    <Persistent("IndicatesBillAdvance")>
    Public Property IndicatesBillAdvance() As String
        Get
            Return fIndicatesBillAdvance
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IndicatesBillAdvance", fIndicatesBillAdvance, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fIdThirdParty As Integer
    <Persistent("IdThirdParty")>
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of String)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    Dim fNit As String
    <Persistent("Nit")>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fStatusName As String
    <Persistent("StatusName")>
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")>
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("Value", fValue, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    <Persistent("CurrencyId")>
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of String)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    <Persistent("CurrencyAbbreviation")>
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
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
