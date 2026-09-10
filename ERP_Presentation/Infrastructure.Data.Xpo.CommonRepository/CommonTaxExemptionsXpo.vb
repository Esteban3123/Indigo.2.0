'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.CommonRepository
' Author           : Andres Alarcon
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo

<Persistent("Common.TaxExemptions")>
Partial Public Class CommonTaxExemptionsXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    <Size(300)>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fApplicationType As Boolean
    Public Property ApplicationType() As Boolean
        Get
            Return fApplicationType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplicationType", fApplicationType, value)
        End Set
    End Property

    Dim fInternalCode As String
    <Size(20)>
    Public Property InternalCode() As String
        Get
            Return fInternalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InternalCode", fInternalCode, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <Size(320)>
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

