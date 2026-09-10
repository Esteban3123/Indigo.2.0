Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingJustificationControl")>
Public Class BillingJustificationControlXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fSkipClearance As Boolean
    Public Property SkipClearance() As Boolean
        Get
            Return fSkipClearance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SkipClearance", fSkipClearance, value)
        End Set
    End Property

#End Region

#Region "Persistent Alias"
    <PersistentAlias("Concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeName"))
        End Get
    End Property
#End Region

#Region "Association"

    <Association("BillingJustificationControlUser_References_BillingJustificationControl", GetType(BillingJustificationControlUserXpo))>
    Public ReadOnly Property BillingJustificationControlUser() As XPCollection(Of BillingJustificationControlUserXpo)
        Get
            Return GetCollection(Of BillingJustificationControlUserXpo)("Billing_BillingAuthorizationUsers")
        End Get
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class