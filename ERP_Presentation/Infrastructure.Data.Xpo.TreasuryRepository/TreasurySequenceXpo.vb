Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Treasury.TreasurySequence")>
Partial Public Class TreasurySequenceXpo
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

    Dim fIdForm As String
    Public Property IdForm() As String
        Get
            Return fIdForm
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdForm", fIdForm, value)
        End Set
    End Property

    Dim fScope As String
    Public Property Scope() As String
        Get
            Return fScope
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Scope", fScope, value)
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

