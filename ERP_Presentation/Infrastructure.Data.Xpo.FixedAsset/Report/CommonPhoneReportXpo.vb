Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Phone")> _
 Public Class CommonPhoneReportXpo
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
    Dim fIdPerson As CommonPersonReportXpo
    <Association("CommonPersonReportXpoReferencesCommonPhoneReportXpo")> _
    Public Property IdPerson() As CommonPersonReportXpo
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As CommonPersonReportXpo)
            SetPropertyValue(Of CommonPersonReportXpo)("IdPerson", fIdPerson, value)
        End Set
    End Property
    Dim fPhone As String
    <Size(15)> _
    Public Property Phone() As String
        Get
            Return fPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Phone", fPhone, value)
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
    Dim fIdPhoneType As Short
    Public Property IdPhoneType() As Short
        Get
            Return fIdPhoneType
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("IdPhoneType", fIdPhoneType, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
