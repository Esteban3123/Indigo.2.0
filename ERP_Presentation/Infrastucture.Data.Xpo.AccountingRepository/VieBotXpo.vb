'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.VieBot")> _
Public Class VieBotXpo
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

    Dim fForm As String
    <Size(200)> _
    <Persistent("Form")> _
    Public Property Form() As String
        Get
            Return fForm
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Form", fForm, value)
        End Set
    End Property

    Dim fLegalBookId As BookXpo
    <Association("VieBotXpoReferencesBookXpo")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fAllow As Boolean
    <Persistent("Allow")> _
    Public Property Allow() As Boolean
        Get
            Return fAllow
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Allow", fAllow, value)
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
