Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Address")> _
Public Class CommonAddressXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdPerson As CommonPersonXpoP
    <Association("CommonAddressXpoReferencesCommonPersonXpoP")> _
    Public Property IdPerson() As CommonPersonXpoP
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As CommonPersonXpoP)
            SetPropertyValue(Of CommonPersonXpoP)("IdPerson", fIdPerson, value)
        End Set
    End Property
    Dim fAddresss As String
    Public Property Addresss() As String
        Get
            Return fAddresss
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addresss", fAddresss, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    Dim fSynchronized As Char
    Public Property Synchronized() As Char
        Get
            Return fSynchronized
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Synchronized", fSynchronized, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
